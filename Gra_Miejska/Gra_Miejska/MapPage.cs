using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Gra_Miejska
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            
            var position = new Position(52.402766, 16.953460);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Politechnika Poznańska",
                Address = "PUT"
            };
            var map = new Map(
            MapSpan.FromCenterAndRadius(
                    new Position(52.402766, 16.953460), Distance.FromMiles(0.3)))
                    {
                        IsShowingUser = true,
                        HeightRequest = 100,
                        WidthRequest = 960,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };
            map.Pins.Add(pin);
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
                
           
        }
    }
}
