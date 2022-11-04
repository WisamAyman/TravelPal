using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal
{
    public class Travel
    {
        public string Destination { get; set; }
        public Countries Country { get; set; }
        public int Travelers { get; set; }


        public Travel(string destination, Countries country, int travelers)
        {
            Destination = destination;
            Country = country;
            Travelers = travelers;
        }

        public virtual string GetInfo() //Metod för om det är trip/vacation som overridas i traveldetailwindow
        {
            return "Info not available"; 
        }
    }
}
