using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using System.ComponentModel;
using Plugin.Geolocator;
namespace Gra_Miejska
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            Device.StartTimer(TimeSpan.FromSeconds(60), () =>
            {
                if (CurrentUser.currentUser.UserId != 0)
                {
                    ReadGPS();
                }
                return true;
            });
            
            MainPage = GetMainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static Page GetMainPage()
        {
            return new NavigationPage(new WelcomePage());
        }
        public async void ReadGPS()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(10000);

            await ApiWebServices.PutLocalizationAsync(position.Latitude, position.Longitude, CurrentUser.currentUser.UserId);
        }
    }
}
