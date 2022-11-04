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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPal.Windows;

namespace TravelPal
{

    public partial class MainWindow : Window
    {
        public UserManager userManager;

        public MainWindow() 
        {
            InitializeComponent();

            userManager = new UserManager();
            Admin admin = new Admin("admin", "password", Enums.Countries.Afghanistan);
            userManager.addUser(admin);

            User userGandalf = new User("gandalf", "password", Enums.Countries.Yemen);
            userGandalf.Travels.AddRange
                (
                    new List<Travel>()
                    {
                        new Trip("Yemen", Enums.Countries.Turkey, 3, Enums.TripTypes.Work),
                        new Vacation("Afghanistan", Enums.Countries.Romania, 5, false)
                    }
                );
            userManager.addUser(userGandalf);
        }

        public MainWindow(UserManager userManager)
        {
            InitializeComponent();

            if (userManager == null)
            {
                userManager = new UserManager();
            }
            else
            {
                this.userManager = userManager;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(userManager);
                       
            registerWindow.Show();
            this.Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (userManager.signInUser(tbUsername.Text, pswbPassword.Password))
            {
                AccountWindow accountWindow = new AccountWindow(userManager);

                accountWindow.Show();
                Close();
            }
            else
            {
                Warning.Content = "Wrong username or password";
            }
        }
    }
}
