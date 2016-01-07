using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Gra_Miejska
{
    public class MapPage : ContentPage
    {
        public static List<GPSPoint> points;
        public static GPSPoint loc;
        public MapPage()
        {
             
            var position = new Position(loc.Latitude,loc.Longnitude);
            
            var map = new Map(
            MapSpan.FromCenterAndRadius(
                    new Position(loc.Latitude,loc.Longnitude), Distance.FromMiles(0.3)))
                    {
                        IsShowingUser = true,
                        HeightRequest = 100,
                        WidthRequest = 960,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };
            List<Pin> PinsList = new List<Pin>();
            foreach (var item in points)
            {
                Pin pin = new Pin();
                pin.Label = (points.IndexOf(item)+1).ToString();
                pin.Position = new Position(item.Latitude, item.Longnitude);
                PinsList.Add(pin);
            }
            foreach (var item in VisitedPoints.visitedPoints)
            {
                Pin pin = new Pin();
                pin.Label = item.Name;
                pin.Position = new Position(item.Latitude, item.Longnitude);
                PinsList.Add(pin);
            }
         
            foreach (var item in PinsList)
            {
                map.Pins.Add(item);
            }
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

        }

       

    }
    
}
