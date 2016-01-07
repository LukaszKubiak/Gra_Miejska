using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Gra_Miejska
{
    public class PointsList : ContentPage
    {
        ActivityIndicator activity;
        StackLayout act;
        ScrollView scroll;
        List<VisitedPoint> list = new List<VisitedPoint>();
        public PointsList()
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
            StackLayout stack = new StackLayout {
                Padding = new Thickness(50, 50, 50, 10),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 15,
            };
            scroll = new ScrollView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = stack
            };
            List<Button> buttonsList = new List<Button>();
            foreach (var item in VisitedPoints.visitedPoints)
            {
                Button btn = new Button();
                btn.Text = item.Point_nr.ToString() + ", " + item.Name;
                btn.Style = (Style)Styles.styles["buttonStyle"];
                
                btn.Clicked += delegate {
                    this.Content = act;
                    NavigateTo(item.Point_nr);
                    this.Content = scroll;

                };
                
                buttonsList.Add(btn);
            }
            foreach (var item in buttonsList)
            {
                stack.Children.Add(item);
            }
            Content = scroll;
            
        }
        async public void NavigateTo(int Point_nr)
        {
            var point = await ApiWebServices.GetPointAsync(Point_nr.ToString());
            if (point.Count == 0)
            {
                await this.DisplayAlert("Błąd!", "Brak takiego punktu w bazie danych.", "OK");
                this.Content = scroll;
            }
            else
            {
                await Navigation.PushAsync(new PointDisplay(point[0]));
                this.Content = scroll;
            }
        }
       
    }
}
