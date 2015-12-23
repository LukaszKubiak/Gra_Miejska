using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra_Miejska
{
    
    public class Point
    {
        public int Point_nr { get; set; }
        public string Name { get; set; }
        public string Line { get; set; }
        public string Link { get; set; }
        public string Desc { get; set; }
        public string Question { get; set; }
        public string Next_point { get; set; }
        public double Latitude { get; set; }
        public double Longnitude { get; set; }
    }
}
