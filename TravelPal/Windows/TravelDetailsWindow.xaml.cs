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

namespace TravelPal.Windows
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        public TravelDetailsWindow(Trip travel)
        {
            InitializeComponent();

            lblCountries.Content = travel.Destination;
            lblTripType.Content = travel.Type;
            lblTravelers.Content = travel.Travelers;
            lblAllInclusive.Visibility = Visibility.Hidden;
            lblCurrentTripOrVacation.Content = travel.GetInfo();

        }
        public TravelDetailsWindow(Vacation travel)
        {
            InitializeComponent();

            lblCountries.Content = travel.Destination;
            lblCurrentTripOrVacation.Content = travel.GetInfo();
            lblTravelers.Content = travel.Travelers;


            if (travel.AllInclusive)
            {
                lblIsAllInclusive.Content = "All Inclusive";
            }
            else
            {
                lblAllInclusive.Content = "";
            }
            lblLeisureOrWork.Visibility = Visibility.Hidden;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
