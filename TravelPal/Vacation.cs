using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }


        public Vacation(string destination, Countries country, int travellers, bool allInclusive) : base(destination, country, travellers)
        {
            AllInclusive = allInclusive;    
        }

        public override string GetInfo() //Metod för om det är trip/vacation som overridas i traveldetailwindow
        {
            return "Vacation";
        }
    }
}
