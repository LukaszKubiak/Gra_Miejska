using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Gra_Miejska
{
    class ApiWebServices
    {
        public ApiWebServices()
        {

        }
        HttpClient client;
        public static async Task<List<User>> GetUserAsync(string Login)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://restgra.herokuapp.com/user/" + Login);
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
            var response = await client.GetAsync("https://restgra.herokuapp.com/point/" + Point_nr);
            var point = new List<Point>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                point = JsonConvert.DeserializeObject<List<Point>>(content);
            }
            return point;

        }
        public static async Task<List<VisitedPoint>> GetPointsAsync(int Uid)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://restgra.herokuapp.com/visited/" + Uid.ToString());
            var point = new List<VisitedPoint>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                point = JsonConvert.DeserializeObject<List<VisitedPoint>>(content);
            }
            return point;

        }
        public static async Task PutVisitedAsync(int Uid, int Pid)
        {
            var client = new HttpClient();            
            string content = "";
            var cont = new StringContent(content);
            HttpResponseMessage response = null;
            response = await client.PutAsync("https://restgra.herokuapp.com/visited/" + Uid.ToString() + ";" + Pid.ToString(), cont);
        }
        
    }
}
