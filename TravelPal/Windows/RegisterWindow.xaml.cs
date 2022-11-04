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
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManager _userManager;

        public RegisterWindow(UserManager userManager)
        {
            InitializeComponent();

            _userManager = userManager;
            cbCountry.ItemsSource = Enum.GetValues(typeof(Countries)).Cast<Countries>();
        }

        private void btnRegisterWindowRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbRegisterUsername.Text;
            string password = pswbRegisterPassword.Password;

            Countries? country;



            if (cbCountry.SelectedItem != null)
            {
                country = (Countries)cbCountry.SelectedItem;
            }
            else
            {
                country = null;
            }

            string foo = _userManager.validateUser(username, password, country);

            if (foo == "")
            {
                _userManager.addUser(new User(username, password, country));

                MainWindow main = new MainWindow(_userManager);
                main.Show();
                Close();
            }
            else
            {
                Warning.Content = foo;
            }

            
        }
    }
}
