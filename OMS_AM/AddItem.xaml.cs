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
using System.Windows.Controls.Primitives;

namespace OMS_AM
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Page
    {
        private OrderHeader orderHeader;
        public AddItem(OrderHeader o)
        {
            InitializeComponent();
            orderHeader = o;
            lblOrderNum.Content = "Order #" + orderHeader.Id;
            DataContext = new StockItem();
            gridStock.ItemsSource = StockController.Instance.GetStockItems();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;

            //Get the values form the DataContext
            var stockItem = (StockItem)((Button)e.Source).DataContext;

            int.TryParse(txtQuantity.Text, out int quantity);

            if (quantity > 0)
            {
                success = true;
                if (quantity > stockItem.InStock)
                {
                    MessageBox.Show("There is currently not enough stock to fulfil your order and it may be rejected when being processed.");
                }
            }

            if (success)
            {
                //Create an Orderitem
                var orderItem = new OrderItem(orderHeader.Id, stockItem.StockId, stockItem.Name, quantity, stockItem.Price);
                OrderController.Instance.UpsertOrderItem(orderHeader, orderItem);
                MessageBox.Show("Item Added Successfully");
                NavigationService.Navigate(new NewOrder(orderHeader));
            }
            else
            {
                MessageBox.Show("Invalid order quantity, please enter a whole number greater than 0.");
            }
            
        }

        private void btnExit_CLick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewOrder(orderHeader));
        }
    }
}
