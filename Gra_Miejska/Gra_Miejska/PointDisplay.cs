using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Gra_Miejska
{
    public class PointDisplay : ContentPage
    {
      
        public PointDisplay(Point point)
        {
            Point current_point= point;
            Label Point_nr, Name, Line, Desc,Question,Next_point;
            ImageCell imgt;
            var img = new Image { Aspect = Aspect.AspectFill };
            if (point.Next_point.Contains("http://") || point.Next_point.Contains("https://"))
            {

                img.Source = ImageSource.FromUri(new Uri(point.Next_point));
                imgt = new ImageCell
                {
                    ImageSource = ImageSource.FromUri(new Uri(point.Next_point))
                };
            }
            Style = (Style)Styles.styles["siteStyle"];
            Point_nr = new Label
            {
                Text = "Punkt: "+current_point.Point_nr.ToString(),
                Style = (Style)Styles.styles["LabelStyle"]

            };
            Name = new Label
            {
                Text = current_point.Name,
                
                Style = (Style)Styles.styles["LabelStyle"]
            };
            Line = new Label
            {
                Text = current_point.Line,
                Style = (Style)Styles.styles["LabelStyle"]

            };
            Desc = new Label
            {
                Text = '\n'+current_point.Desc+'\n',
                Style = (Style)Styles.styles["LabelStyle"]
            };
            Question = new Label
            {
                Text = '\n'+current_point.Question+'\n',
                Style = (Style)Styles.styles["LabelStyle"]
            };
            Next_point = new Label
            {
                Text = '\n'+current_point.Next_point+'\n',
                Style = (Style)Styles.styles["LabelStyle"]
            };
            Grid grid = new Grid
                {

                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ColumnSpacing =5,
                    RowSpacing = 5,
                    RowDefinitions = 
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                    
                               
                };
            grid.Children.Add(Point_nr, 0, 0);
            grid.Children.Add(Name, 0,1);
            grid.Children.Add(Line, 0, 2);
            grid.Children.Add(Desc, 0, 3);
            grid.Children.Add(Question, 0, 4);
            if (current_point.Next_point.Contains("http://") || current_point.Next_point.Contains("https://"))
            {}
            else
            {
                grid.Children.Add(Next_point, 0, 5);
            }
            StackLayout stack = new StackLayout
            {
            };
            stack.Children.Add(grid);
            if (current_point.Next_point.Contains("http://") || current_point.Next_point.Contains("https://"))
            {
                stack.Children.Add(img);
            }
            Content = new ScrollView
            {
                
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                
                Content = stack
             };
            
                       

        }
        
    }
}
