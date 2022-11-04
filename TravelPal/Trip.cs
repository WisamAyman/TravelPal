using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal
{
    public class Trip : Travel
    {
        public TripTypes Type { get; set; }
                

        public Trip(string destination, Countries country, int travellers, TripTypes type) : base(destination, country, travellers)
        {
            Type = type;
        }

        public override string GetInfo()  //Metod för om det är trip/vacation
        {
            return "Trip"; 
        }
        
    }
}
