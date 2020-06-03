using Controllers;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace OMS_AM
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Page
    {
        private OrderHeader orderHeader;
        public NewOrder()
        {
            InitializeComponent();
            orderHeader = OrderController.Instance.CreateNewOrderHeader();
            txtOrderNumber.Text = "Order number: " + orderHeader.OrderHeaderId;
            txtOrderDateTime.Text = orderHeader.Date.ToString();
            DataContext = orderHeader;
            
        }
        public NewOrder(OrderHeader o)
        {
            txtOrderNumber.Text = "Order number: " + o.OrderHeaderId;
            txtOrderDateTime.Text = o.Date.ToString();
            grdOrderItems.ItemsSource = o.OrderItems;
        }

        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var orderHeader = (OrderHeader)((Button)e.Source).DataContext;
            NavigationService.Navigate(new AddItem(orderHeader));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            OrderController.Instance.DeleteOrderHeaderAndOrderItems(orderHeader.OrderHeaderId);
            NavigationService.Navigate(new MainWindow());
        }
    }
}
