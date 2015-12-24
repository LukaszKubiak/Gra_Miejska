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
        public PointsList()
        {
            Style = (Style)Styles.styles["siteStyle"];
            StackLayout stack = new StackLayout {
                Padding = new Thickness(50, 50, 50, 10),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 15,
            };
            List<Button> buttonsList = new List<Button>();
            foreach (var item in VisitedPoints.visitedPoints)
            {
                Button btn = new Button();
                btn.Text = item.Point_nr.ToString() + ", " + item.Name;
                btn.Style = (Style)Styles.styles["buttonStyle"];
               // btn.Clicked += delegate {
                //   
                //};
                
                buttonsList.Add(btn);
            }
            foreach (var item in buttonsList)
            {
                stack.Children.Add(item);
            }
            Content = stack;
            
        }
    }
}
