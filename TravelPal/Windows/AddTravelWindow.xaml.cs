using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        public UserManager _userManager;

        public AddTravelWindow(UserManager userManager)
        {
            InitializeComponent();

            _userManager = userManager;
            cbCountries.ItemsSource = Enum.GetValues(typeof(Countries)).Cast<Countries>();
            cbTripOrVacation.Items.Add("Trip");
            cbTripOrVacation.Items.Add("Vacation");
            cbTripType.ItemsSource = Enum.GetValues(typeof(TripTypes)).Cast<TripTypes>();
        }

        public void IsButtonEnabled() //Metod för Save button i AddTravel - enable/disable
        {
            if (cbCountries.SelectedValue != null && cbTripOrVacation.SelectedValue != null)
            {
                if (cbTripOrVacation.SelectedValue == "Trip" && cbTripType.SelectedValue != null)
                {
                    btnSave.IsEnabled = true;
                    return;
                }
                if (cbTripOrVacation.SelectedValue == "Vacation")
                {
                    btnSave.IsEnabled = true;
                    return;
                }
                btnSave.IsEnabled=false;
                return;
            }
            btnSave.IsEnabled = false;
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCountries.SelectedItem != null)
            {
                cbTripOrVacation.Visibility = Visibility.Visible;
                lblTripOrVacation.Visibility= Visibility.Visible;
            }
            IsButtonEnabled();
        }

        private void cbTripOrVacation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTripOrVacation.SelectedItem == "Trip")
            {
                cbTripType.Visibility = Visibility.Visible;
                lblLeisureOrWork.Visibility = Visibility.Visible;
                chkbxAllInclusive.Visibility = Visibility.Hidden;
                lblAllInclusive.Visibility = Visibility.Hidden;
            }
            else if (cbTripOrVacation.SelectedItem == "Vacation")
            {
                chkbxAllInclusive.Visibility = Visibility.Visible;
                lblAllInclusive.Visibility = Visibility.Visible;
                cbTripType.Visibility = Visibility.Hidden;
                lblLeisureOrWork.Visibility = Visibility.Hidden;
            }
            IsButtonEnabled();
        }

        private void cbTripType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsButtonEnabled();
        }
        private void chkbxAllInclusive_Checked(object sender, RoutedEventArgs e)
        {
            
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int foo = Convert.ToInt32(tbTravelers.Text);

            if (foo > 0 && foo < 51)
            {
                if (cbTripOrVacation.SelectedValue == "Trip")
                {
                    TripTypes tripType = (TripTypes)Enum.Parse(typeof(TripTypes), cbTripType.SelectedValue.ToString());

                    Trip trip = new Trip(cbCountries.SelectedValue.ToString(), _userManager.signedInUser.Location.Value, int.Parse(tbTravelers.Text), tripType);
                    _userManager.signedInUser.Travels.Add(trip);
                }
                if (cbTripOrVacation.SelectedValue == "Vacation")
                {
                    Vacation vacation = new Vacation(cbCountries.SelectedValue.ToString(), _userManager.signedInUser.Location.Value, int.Parse(tbTravelers.Text), chkbxAllInclusive.IsChecked.Value);
                    _userManager.signedInUser.Travels.Add(vacation);
                }

                AccountWindow accountWindow = new AccountWindow(_userManager);
                accountWindow.Show();
                Close();
            }
            else
            {
                lblTravelersWarning.Content = "50 is max travelers.";
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); 
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow(_userManager);
            accountWindow.Show();
            Close();
        }
    }
}
