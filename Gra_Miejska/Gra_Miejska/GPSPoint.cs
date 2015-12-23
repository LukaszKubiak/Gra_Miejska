using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gra_Miejska
{
    public class GPSPoint
    {
        public double Latitude { get; set; }
        public double Longnitude { get; set; }
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
        public static double countDistance(double lat1, double lat2, double lon1,double lon2)
        {
            var R = 637100;

            var dLat = ConvertToRadians(lat2-lat1);
            var dLon = ConvertToRadians(lon2 - lon1);
            var lati1 = ConvertToRadians(lat1);
            var lati2 = ConvertToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
        Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lati1) * Math.Cos(lati2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d/100;
        }
    }
}
