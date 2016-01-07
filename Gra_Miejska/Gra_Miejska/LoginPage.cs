using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PCLCrypto;

 



namespace Gra_Miejska
{
    class LoginPage : ContentPage
    {
        Button login, exit;
        public Entry log, password;
        ActivityIndicator activity;
        StackLayout entries, buttons,cont;
        public LoginPage()
        {
            activity = new ActivityIndicator
            {
                Color = Color.Gray,                
            };
            CurrentUser.currentUser = new User();
            Style = (Style)Styles.styles["siteStyle"];
            login = new Button
            {
            Text = "Zaloguj",
            Style = (Style)Styles.styles["buttonStyle"],
           
            
            };
            login.Clicked += OnButtonClicked;
            
            exit = new Button
            {
                Text = "Wyjście",
                Style = (Style)Styles.styles["buttonStyle"],
                
                
                
            };
            log = new Entry
            {
                Placeholder = "Login",
                WidthRequest=400,
               
            };
            password = new Entry
            {
                Placeholder = "Hasło",
                IsPassword = true
            };
            exit.Clicked += OnButtonClicked;
            entries = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        Spacing = 15,
                        Padding = new Thickness(50, 10, 50, 10),
                        Children =
                        {
                            log,
                            password
                        }

                    };
            buttons = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            Spacing = 15,
                            Children =
                            {
                                login,
                                exit                              

                            }
                        };
            cont = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    entries,buttons

                }
            };
            this.Content = cont;
            
           
        }
        
           async void OnButtonClicked(object sender, EventArgs args)
            {
                activity.IsRunning = true;                
                cont.Children.Clear();
                cont.Children.Add(activity);
               
                
                Button button = (Button)sender;
                if(button == login)
                {
                    
                    string hashed_password = Encryption.Encrypt(password.Text + "+1qazxsw23edcvfr4");
                    
                    var u = await ApiWebServices.GetUserAsync(log.Text);
                    if(u.Count == 0)
                    {
                        
                        await this.DisplayAlert("Błąd!", "Błędny login lub Hasło. Spróbuj ponownie.", "OK");
                        cont.Children.Clear();
                        cont.Children.Add(entries);
                        cont.Children.Add(buttons);
                    }
                    else
                    {
                        if(u[0].Password.ToUpper() == hashed_password)
                        {
                            VisitedPoints.visitedPoints = await ApiWebServices.GetVisitedAsync(u[0].UserId);
                            CurrentUser.currentUser = u[0];
                            
                            
                            
                            await Navigation.PushAsync(new Menu());
                            cont.Children.Clear();
                            cont.Children.Add(entries);
                            cont.Children.Add(buttons);
                        }
                        else
                        {
                            
                            await this.DisplayAlert("Błąd!", "Błędny login lub Hasło. Spróbuj ponownie.", "OK");
                            cont.Children.Clear();
                            cont.Children.Add(entries);
                            cont.Children.Add(buttons);
                        }
                        
                    }

                    //await Navigation.PushAsync(new Menu());
                }
                else if(button == exit)
                {
                    
                    await Navigation.PopAsync();
                    cont.Children.Clear();
                    cont.Children.Add(entries);
                    cont.Children.Add(buttons);
                }
                
                
                
            }
            void OnPageSizeChanged(object sender, EventArgs args) 
            {
               
            }
            protected override void OnAppearing()
            {
                log.Text = "";
                password.Text = "";
                base.OnAppearing();
            }
            

            
    }
}

