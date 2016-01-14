using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Gra_Miejska
{
    public class Styles : ContentPage
    {
        public Styles()
        {
           
        }
        public static ResourceDictionary styles = new ResourceDictionary
        {
            {
                "buttonStyle",new Style(typeof(Button))
                {
                    Setters = 
                    {
                        new Setter
                        {
                            Property = View.HorizontalOptionsProperty,
                            Value = LayoutOptions.Center
                        },
                        new Setter
                        {
                            Property = View.VerticalOptionsProperty,
                            Value = LayoutOptions.Center
                        },
                        new Setter 
                        { 
                            Property = Button.BorderWidthProperty,
                            Value = 3 
                        },
                        new Setter 
                        { 
                            Property = Button.TextColorProperty, 
                            Value = Color.White 
                        },
                        new Setter 
                        { 
                            Property = Button.BorderColorProperty, 
                            Value = Color.White 
                        },
                        new Setter 
                        { 
                            Property = Button.BackgroundColorProperty, 
                            Value = Color.FromRgb(2, 29, 82)
                        },
                        new Setter 
                        { 
                            Property = Button.WidthRequestProperty, 
                            Value = 320
                        }
                    }
                    
                }
                         
            },
            {
                "siteStyle",new Style(typeof(ContentPage))
                {
                    Setters=
                    {
                        new Setter 
                        { 
                            Property = Button.BackgroundColorProperty, 
                            Value = Color.FromRgb(2, 29, 82)
                        }
                    }

                }
            },
            {
                "LabelStyle",new Style(typeof(Label))
                {
                    Setters = 
                    {
                        new Setter
                        {
                            Property = Label.HorizontalOptionsProperty,
                            Value = LayoutOptions.FillAndExpand
                        },
                        new Setter
                        {
                            Property = Label.VerticalOptionsProperty,
                            Value = LayoutOptions.FillAndExpand
                        },
                        new Setter
                        {
                            Property = Label.BackgroundColorProperty,
                            Value = Color.White
                        },
                        new Setter
                        {
                            Property = Label.FontSizeProperty,
                            Value = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                        },
                        new Setter
                        {
                            Property = Label.TextColorProperty,
                            Value = Color.FromRgb(2, 29, 82)
                        },
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.OnPlatform(iOS:"Times New Roman", Android:"Times New Roman",  WinPhone:"Times New Roman")

                        },
                        new Setter
                        {
                            Property = Label.VerticalTextAlignmentProperty,
                            Value = TextAlignment.Center

                        },
                        new Setter
                        {
                            Property = Label.HorizontalTextAlignmentProperty,
                            Value = TextAlignment.Center

                        },
                        
                        
                    }
                }
            }
        };
    }
}
