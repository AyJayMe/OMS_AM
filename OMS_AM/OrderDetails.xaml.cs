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
        /// <summary>
        /// Creates the Order Details page
        /// </summary>
        /// <param name="o">OrderHeader</param>
        public OrderDetails(OrderHeader o)
        {
            //Assign OrderHeader o to the local variable orderHeader
            orderHeader = o;

            InitializeComponent();
            txtOrderNumber.Text = "Order number: " + orderHeader.Id;
            txtOrderDateTime.Text = "" + orderHeader.Date;
            txtOrderValue.Text = "Order Total: $" + orderHeader.Total;
            DataContext = orderHeader;
            grdOrderDetails.ItemsSource = orderHeader.OrderItems;

            //Remove the process order button if order has already been processed
            if(orderHeader.State.ToString() == "Confirmed" || orderHeader.State.ToString() == "Rejected")
            {
                btnConfirmOrder.Visibility = Visibility.Collapsed;
            }
            
        }
        /// <summary>
        /// Processes the order as Confirmed or Rejected by calculating whether there is enough stock available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            //Variable controlling whether the order be fulfilled
            bool orderFulfilled = true;

            //Iterate through each order item and marks whether there is enough stock to process the order.
            foreach (OrderItem item in orderHeader.OrderItems)
            {
                StockItem stockItem = StockController.Instance.GetStockItem(item.StockItemId);
                if(stockItem.InStock < item.Quantity)
                {
                    orderFulfilled = false;
                    MessageBox.Show($"There is not enough stock for #{stockItem.StockId} ({stockItem.Name}) to fulfil your order.");
                }
            }

            //If the order can be fulfilled
            if (orderFulfilled)
            {
                OrderController.Instance.ProcessOrder(orderHeader);
                if (StockController.Instance.UpdateStockItemAmount(orderHeader))
                {
                    MessageBox.Show("Order Processed");
                }
                else
                {
                    MessageBox.Show("There was an error processing the order.");
                }
                NavigationService.Navigate(new MainWindow());
            }
            else
            {
                OrderController.Instance.RejectOrder(orderHeader);
                MessageBox.Show("The order was rejected.");
                NavigationService.Navigate(new MainWindow());
            }
        }
        /// <summary>
        /// Exits to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }
        /// <summary>
        /// Deletes the order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderController.Instance.DeleteOrderHeaderAndOrderItems(orderHeader);
            NavigationService.Navigate(new MainWindow());
        }
    }
}
