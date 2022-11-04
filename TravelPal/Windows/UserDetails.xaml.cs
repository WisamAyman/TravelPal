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
using TravelPal.Enums;

namespace TravelPal.Windows
{
    public partial class UserDetails : Window
    {
        private UserManager _userManager;

        public UserDetails(UserManager userManager)
        {
            InitializeComponent();
                       
            _userManager = userManager;
            cbCountry.ItemsSource = Enum.GetValues(typeof(Countries)).Cast<Countries>();

            tbUserName.Text = _userManager.signedInUser.Username;
            cbCountry.Text = _userManager.signedInUser.Location.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text.Length < 3)
            {
                MessageBox.Show("Username is too short.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var foo = _userManager.users.Any(x => x.Username == tbUserName.Text);


            if (tbUserName.Text != _userManager.signedInUser.Username && foo)
            {
                MessageBox.Show("Username already exists.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _userManager.signedInUser.Username = tbUserName.Text;
                
                if (pwbNewPassword.Password != "" && pwbConfirmPassword.Password != "")
                {
                    if (pwbNewPassword.Password != pwbConfirmPassword.Password)
                    {
                        MessageBox.Show("New password does not match.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (pwbNewPassword.Password.Length < 5)
                    {
                        MessageBox.Show("New password too short.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    _userManager.signedInUser.Password = pwbNewPassword.Password;
                }
                else
                {
                    MessageBox.Show("Please provide your password.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            AccountWindow accountWindow = new AccountWindow(_userManager);
            accountWindow.Show();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow addTravelWindow = new AccountWindow(_userManager);
            addTravelWindow.Show();
            Close();
        }
    }
}
