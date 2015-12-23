using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Acr.BarCodes;
using Acr;
using Xamarin.Forms;
using System.Net;
using Plugin.Geolocator;
using System.Threading.Tasks;

namespace Gra_Miejska
{
    public class Menu : ContentPage
    {
        Button Scan, Points, Instruction, Statistics, Map, Exit;
        GPSPoint current = new GPSPoint();
        public Menu()
        {
            
            Style = (Style)Styles.styles["siteStyle"];
            Scan = new Button
            {
                Style = (Style)Styles.styles["buttonStyle"],
                Text = "Zeskanuj kod"
            };
            Points = new Button
            {
                Style = (Style)Styles.styles["buttonStyle"],
                Text = "Lista Punktów"
            };
            Instruction = new Button
            {
                Style = (Style)Styles.styles["buttonStyle"],
                Text = "Instrukcja"
            };
            Statistics = new Button
            {
                Style = (Style)Styles.styles["buttonStyle"],
                Text = "Statystyki"
            };
            Map = new Button
            {
                Style = (Style)Styles.styles["buttonStyle"],
                Text ="Mapa"
            };
            Exit = new Button
            {
                Style = (Style)Styles.styles["buttonStyle"],
                Text = "Wyloguj"
            };
           

            Scan.Clicked += async(sender, args)=>
            {  
                var scanResult = await Acr.BarCodes.BarCodes.Instance.Read();  
                if (!scanResult.Success)   
                {  
                    await this.DisplayAlert("Alert ! ", "Sorry ! \n Failed to read the Barcode !", "OK");  
                }   
                else   
                {  
                
                    //await this.DisplayAlert("Scan Successful !", String.Format("Barcode Format : {0} \n Barcode Value : {1}", scanResult.Format, scanResult.Code), "OK");
                    var point = await ApiWebServices.GetPointAsync(scanResult.Code);
                    if (point.Count == 0)
                    {
                        await this.DisplayAlert("Błąd!", "Brak takiego punktu w bazie danych.", "OK");
                    }
                    else
                    {
                        VisitedPoints.visitedPoints.Add(point[0]);

                        current = await GetLocationAsync();
                        double lat1 = current.Latitude - point[0].Latitude;
                        double long1 = current.Longnitude - point[0].Longnitude;
                        var distance = GPSPoint.countDistance(current.Latitude,point[0].Latitude,current.Longnitude,point[0].Longnitude);
                        if (distance < 0.03)
                        {
                            await ApiWebServices.PutVisitedAsync(CurrentUser.currentUser.UserId, point[0].Point_nr);
                           await Navigation.PushAsync(new PointDisplay(point[0]));
                        }
                        else
                        {
                            await this.DisplayAlert("Błąd!", "Błędna lokalizacja!", "OK");
                        }
                        

                    }
                }  
            };
            Exit.Clicked += Exit_Clicked;
            Map.Clicked += Map_Clicked;
           
            Content = new ScrollView
            {
                
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = new StackLayout
                {
                    Padding = new Thickness(50, 50, 50, 10),
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Spacing = 15,
                    Children = {
				    Scan,
                    Points,
                    Instruction,
                    Statistics,
                    Map,
                    Exit
				}
                }
            };
        }
        public async Task<GPSPoint>  GetLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(10000);

            GPSPoint point = new GPSPoint();
            point.Latitude = position.Latitude;
            point.Longnitude = position.Longitude;
            return point;
        }

       

        void Map_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MapPage());
        }

        void Exit_Clicked(object sender, EventArgs e)
        {
            CurrentUser.currentUser = new User();
            Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            CurrentUser.currentUser = new User();
            return base.OnBackButtonPressed();
        }
        
    }
}
