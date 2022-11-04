using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;
using TravelPal.Models;

namespace TravelPal
{
    public class User : IUser
    {
        public List<Travel> Travels { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public Countries? Location { get; set; }

        public User(string username, string password, Countries? location)
        {
            Username = username;
            Password = password;
            Location = location;
            Travels = new List<Travel>();
        }
    }
}
