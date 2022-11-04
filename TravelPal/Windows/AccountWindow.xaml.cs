using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelPal.Models;

namespace TravelPal.Windows
{

    public partial class AccountWindow : Window
    {
        private UserManager _userManager;

        public AccountWindow(UserManager userManager)
        {           
            InitializeComponent();
            _userManager = userManager;


            if (_userManager.isAdmin)
            {
                btnAddTravel.IsEnabled = false;
                btnInfo.IsEnabled = false;
                btnUser.IsEnabled = false;

                Welcome.Content = "admin";

                List<Travel> travels = new List<Travel>();

                List<User> usersWithTravels = new();

                foreach (IUser user in userManager.users)
                {
                    if(user is User)
                    {
                        usersWithTravels.Add((User)user);
                    }
                }

                foreach (User user in usersWithTravels)
                {
                    foreach (Travel travel in user.Travels)
                    {
                        travels.Add(travel);
                    }
                }


                foreach (Travel travel in travels)
                {
                    ListViewItem item = new ListViewItem();

                    item.Tag = travel;
                    item.Content = travel.Destination;
                    lvListTravels.Items.Add(item);
                }
                return;
            }
            
            foreach (Travel travel in _userManager.signedInUser.Travels)
            {
                ListViewItem item = new ListViewItem();

                item.Tag = travel;
                item.Content = travel.Destination;
                lvListTravels.Items.Add(item);
            }
            Welcome.Content = _userManager.signedInUser.Username;
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            _userManager.isAdmin = false;
            MainWindow mainWindow = new MainWindow(_userManager);
            Close();
            mainWindow.Show();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();

            infoWindow.Show();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            UserDetails userdetails = new UserDetails(_userManager);            
            userdetails.Show();
            Close();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new AddTravelWindow(_userManager);

            addTravelWindow.Show();
            this.Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            int foo = lvListTravels.SelectedIndex;
            
            if (foo == -1)
            {
                MessageBox.Show("Select/Add travel.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_userManager.isAdmin)
            {
                ListViewItem listViewItem = lvListTravels.SelectedItem as ListViewItem;

                Travel selectedTravel = listViewItem.Tag as Travel;
                _userManager.AdminRemoveTravel(selectedTravel);
            }
            else 
                _userManager.signedInUser.Travels.RemoveAt(foo);

            lvListTravels.Items.RemoveAt(foo);


        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvListTravels.SelectedIndex != -1) 
            {
                ListViewItem foo = lvListTravels.SelectedItem as ListViewItem;

                var selectedTravel = foo.Tag;

                if (selectedTravel is Vacation)
                {
                    var vacation = selectedTravel as Vacation;

                    TravelDetailsWindow detailsWindow = new TravelDetailsWindow(vacation);
                    detailsWindow.Show();
                }
                else if (selectedTravel is Trip)
                {
                    var trip = selectedTravel as Trip;

                    TravelDetailsWindow detailsWindow = new TravelDetailsWindow(trip);
                    detailsWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Select travel country.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
