using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;
using TravelPal.Models;

namespace TravelPal
{
    public class UserManager
    {
        public List<IUser> users = new List<IUser>();
        public User? signedInUser { get; set; }
        public Admin admin { get; set; }
        public bool isAdmin { get; set; }
        

        public bool addUser(IUser user) //Metod för add user
        {
            users.Add(user);
            return true;
        }

        public bool updateUsername(IUser user, string findUsername) //Metod för update username
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == findUsername)
                {
                    users[i] = user;
                    return true;
                }
            }
            return false;
        }

        public string validateUser(string username, string password, Countries? country) //Metod för att bekräfta användare
        {
            foreach (IUser user in users)
            {
                if (user.Username == username)
                {
                    return "Username already exists.";
                }
            }
            if (username.Length < 3)
            {
                return "Username is too short";
            }
            if (password.Length < 3)
            {
                return "Password is too short";
            }
            if (country == null)
            {
                return "Please select a country";
            }
            return "";           
        }

        public bool signInUser(string username, string password) //Metod för att att logga in, med vanligt konto/admin konto
        {
            foreach (IUser user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    if(user is Admin)
                    {
                        admin = (Admin)user;
                        isAdmin = true;
                        return true;
                    }
                    signedInUser = (User)user;
                    return true;
                }                
            }
            return false;
            
        }

        public bool AdminRemoveTravel(Travel travelToRemove) //Metod för att kunna radera en resa som admin
        {
            List<User> onlyUsers = new();

            foreach (IUser user in users)
            {
                if (user is User)
                {
                    onlyUsers.Add((User)user);
                }
            }

            foreach (User user in onlyUsers)
            {
                foreach (Travel travel in user.Travels)
                {
                    if (travel == travelToRemove)
                    {
                        user.Travels.Remove(travel);
                        break;
                    }
                }
            }
            return true;
        }

    }

}
