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
using Controllers;
using Domain;

namespace OMS_AM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();

            //Assign the data source of the gridOrders data grid to the collection of all Order Headers
            gridOrders.ItemsSource = OrderController.Instance.GetOrderHeaders();
            
            //Instanstiate new order header object and assign it to the data context property
            OrderHeader o = new OrderHeader();
            DataContext = o;
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewOrder());
        }

        private void btnOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            var orderHeader = (OrderHeader)((Button)e.Source).DataContext;
            NavigationService.Navigate(new OrderDetails(orderHeader));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
} 
