using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra_Miejska
{
    class VisitedPoint
    {

        public int UserID { get; set; }
        public int Point_nr { get; set; }
        public string Name { get; set; }
        public double Latitude {get; set;}
        public double Longnitude {get; set;}
        public VisitedPoint(int UserID, int Point_nr, string Name,double Latitude,double Longnitude)
        {
        
            this.UserID = UserID;
            this.Point_nr = Point_nr;
            this.Name = Name;
            this.Latitude = Latitude;
            this.Longnitude = Longnitude;
        }
    }
}
