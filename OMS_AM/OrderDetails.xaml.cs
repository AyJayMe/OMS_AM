using Domain;
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

namespace OMS_AM
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Page
    {
        private OrderHeader orderHeader;
        public OrderDetails(OrderHeader o)
        {
            orderHeader = o;
            InitializeComponent();
            txtOrderNumber.Text = "Order number: " + orderHeader.OrderHeaderId;
            txtOrderDateTime.Text = "" + orderHeader.Date;
            txtOrderValue.Text = "Order Total: $" + orderHeader.Total;
            DataContext = orderHeader;
            grdOrderDetails.ItemsSource = o.OrderItems;
            
        }
        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }
    }
}
