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
        
        ActivityIndicator activity;
        Button Scan, Points, Instruction, Statistics, Map, Exit;
        GPSPoint current = new GPSPoint();
        ScrollView scroll;
        StackLayout act;
        public Menu()
        {
            activity = new ActivityIndicator
            {
                Color = Color.Gray,
                IsRunning = true
            };
            act = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    activity
                }
            };
            
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
                this.Content = act;
                if (!scanResult.Success)   
                {
                    this.Content = scroll;
                    await this.DisplayAlert("Alert ! ", "Sorry ! \n Failed to read the Barcode !", "OK");  
                }   
                else   
                {  
                    var point = await ApiWebServices.GetPointAsync(scanResult.Code);
                    if (point.Count == 0)
                    {
                        
                        await this.DisplayAlert("Błąd!", "Brak takiego punktu w bazie danych.", "OK");
                        this.Content = scroll;
                    }
                    else
                    {

                        try
                        {
                            current = await GetLocationAsync();
                            double lat1 = current.Latitude - point[0].Latitude;
                            double long1 = current.Longnitude - point[0].Longnitude;
                            var distance = GPSPoint.countDistance(current.Latitude, point[0].Latitude, current.Longnitude, point[0].Longnitude);
                            if (distance < 0.05)
                            {
                                await ApiWebServices.PutVisitedAsync(CurrentUser.currentUser.UserId, point[0].Point_nr, point[0].Name);
                                await ApiWebServices.PutLocalizationAsync(current.Latitude, current.Longnitude, CurrentUser.currentUser.UserId);
                                VisitedPoints.visitedPoints.Add(new VisitedPoint(CurrentUser.currentUser.UserId, point[0].Point_nr, point[0].Name,point[0].Latitude,point[0].Longnitude));
                                await Navigation.PushAsync(new PointDisplay(point[0]));
                                this.Content = scroll;
                            }
                            else
                            {
                                
                                await this.DisplayAlert("Błąd!", "Błędna lokalizacja! Jesteś "+ distance *1000 + " metrów od punktu.", "OK");
                                this.Content = scroll;
                            }
                        }
                        catch(Exception ex)
                        {
                            
                            this.DisplayAlert("Błąd!", "Lokalizacja zablokowana w systemie!", "OK");
                            this.Content = scroll;
                        }
                        
                        

                    }
                }  
            };
            Exit.Clicked += Exit_Clicked;
            Map.Clicked += Map_Clicked;
            Points.Clicked += Points_Clicked;
            scroll = new ScrollView
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
            Content = scroll;
        }

        void Points_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PointsList());
        }
        public static async Task<GPSPoint>  GetLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(10000);

            GPSPoint point = new GPSPoint();
            point.Latitude = position.Latitude;
            point.Longnitude = position.Longitude;
            return point;
        }

       

        async void Map_Clicked(object sender, EventArgs e)
        {
            this.Content = act;
            MapPage.points = await ApiWebServices.GetGPSAsync(CurrentUser.currentUser.UserId);
            MapPage.loc = await GetLocationAsync();
            await Navigation.PushAsync(new MapPage());
            this.Content = scroll;
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
        protected override void OnAppearing()
        {
 	        base.OnAppearing();
        }   
        
    }
}
