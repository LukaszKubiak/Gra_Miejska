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
        
        public LoginPage()
        {
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

            this.Content = new StackLayout
            {

                Children =
                { 
                    new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        Spacing = 15,
                        Padding = new Thickness(50, 10, 50, 10),
                        Children=
                        {
                            log,
                            password
                        }
                        
                    },
                    new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,                            
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            Spacing = 15,
                            Children=
                            {
                                login,
                                exit                              

                            }
                        }
                }
            };
            
           
        }
        
           async void OnButtonClicked(object sender, EventArgs args)
            {
                
                Button button = (Button)sender;
                if(button == login)
                {
                    
                    string hashed_password = Encryption.Encrypt(password.Text + "+1qazxsw23edcvfr4");
                    
                    var u = await ApiWebServices.GetUserAsync(log.Text);
                    if(u.Count == 0)
                    {
                        var result = await DisplayActionSheet("Błędny login lub hasło!", "Spróbuj ponownie", "OK");
                        if (result == "OK")
                        {}
                        else if (result == "Spróbuj ponownie")
                        {
                            OnButtonClicked(sender, args);
                        }
                    }
                    else
                    {
                        if(u[0].Password.ToUpper() == hashed_password)
                        {
                            CurrentUser.currentUser = u[0];

                            await Navigation.PushAsync(new Menu());
                        }
                        else
                        {
                            var result = await DisplayActionSheet("Błędny login lub hasło!", "Spróbuj ponownie", "OK");
                            if (result == "OK")
                            { }
                            else if (result == "Spróbuj ponownie")
                            {
                                OnButtonClicked(sender, args);
                            }
                        }
                        
                    }

                    //await Navigation.PushAsync(new Menu());
                }
                else if(button == exit)
                {
                    await Navigation.PopAsync();
                }
                
                
                
            }
            void OnPageSizeChanged(object sender, EventArgs args) 
            {
               
            }
            protected override void OnAppearing()
            {
                log.Text = "";
                password.Text = "";
                log.Text = CurrentUser.currentUser.Login;
                base.OnAppearing();
            }

            
    }
}

