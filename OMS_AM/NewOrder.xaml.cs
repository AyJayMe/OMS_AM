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
            txtOrderNumber.Text = "Order number: " + orderHeader.Id;
            txtOrderDateTime.Text = orderHeader.Date.ToString();
            DataContext = orderHeader;
            
        }
        public NewOrder(OrderHeader o)
        {
            InitializeComponent();
            orderHeader = o;
            DataContext = orderHeader;
            txtOrderNumber.Text = "Order number: " + orderHeader.Id;
            txtOrderValue.Text = "Order Total: $" + orderHeader.Total;
            txtOrderDateTime.Text = orderHeader.Date.ToString();
            grdOrderItems.ItemsSource = orderHeader.OrderItems;
        }

        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderHeader = (OrderHeader)((Button)e.Source).DataContext;
            OrderController.Instance.SubmitOrder(orderHeader);
            NavigationService.Navigate(new MainWindow());
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var orderHeader = (OrderHeader)((Button)e.Source).DataContext;
            NavigationService.Navigate(new AddItem(orderHeader));
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var orderItem = (OrderItem)((Button)e.Source).DataContext;
            OrderController.Instance.DeleteOrderItem(orderHeader, orderItem.StockItemId);
            NavigationService.Navigate(new NewOrder(orderHeader));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            OrderController.Instance.DeleteOrderHeaderAndOrderItems(orderHeader);
            NavigationService.Navigate(new MainWindow());
        }
    }
}
