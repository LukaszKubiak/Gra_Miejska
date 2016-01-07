using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace Gra_Miejska
{
    class ApiWebServices
    {

        private static int count = 0;
        public ApiWebServices()
        {
            
            
        }
        
        public static async Task<List<User>> GetUserAsync(string Login)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restgra.herokuapp.com/");
            var response = await client.GetAsync("user/" + Login);
            var user = new List<User>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<List<User>>(content);
            }
            
            return user;

        }
        public static async Task<List<Point>> GetPointAsync(string Point_nr)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restgra.herokuapp.com/");
            var point = new List<Point>();
            try
            {
               
                var response = await client.GetAsync("point/" + Point_nr);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    point = JsonConvert.DeserializeObject<List<Point>>(content);
                }
            }
            catch(Exception ex)
            {
                int a = 8;
            }
            return point;

        }
        
        
        public static async Task<List<VisitedPoint>> GetVisitedAsync(int Uid)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restgra.herokuapp.com/");
            var point = new List<VisitedPoint>();
            try
            {
               
                var response = await client.GetAsync("visited/"+Uid.ToString()+";"+count.ToString());
                count++;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    point = JsonConvert.DeserializeObject<List<VisitedPoint>>(content);

                }
            }
            catch (Exception ex)
            {
                int a = 8;
            }
              
            return point;
                        
        }
        
        
        public static async Task<List<GPSPoint>> GetGPSAsync(int Uid)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restgra.herokuapp.com/");
            var response = await client.GetAsync("gps/" + Uid.ToString()+";"+count.ToString());
            count++;
            var point = new List<GPSPoint>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                point = JsonConvert.DeserializeObject<List<GPSPoint>>(content);
            }
            return point;

        }
        public static async Task PutVisitedAsync(int Uid, int Pid, string Name)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restgra.herokuapp.com/");         
            string content = "";
            var cont = new StringContent(content);
            HttpResponseMessage response = null;
            response = await client.PutAsync("visited/" + Uid.ToString() + ";" + Pid.ToString()+";"+Name, cont);
        }
        public static async Task PutLocalizationAsync(double Latitude,double Longnitude, int UserID)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restgra.herokuapp.com/");
            string content = "";
            var cont = new StringContent(content);
            HttpResponseMessage response = null;
            string LatitudeString = Latitude.ToString();
            LatitudeString = LatitudeString.Replace(',', '.');
            string LongnitudeString = Longnitude.ToString();
            LongnitudeString = LongnitudeString.Replace(',', '.');
            response = await client.PutAsync("gps/" + LatitudeString + ";" + LongnitudeString + ";" + UserID.ToString(), cont);
        }
        
    }
}
