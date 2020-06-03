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
            lblOrderNum.Content = "Order #" + orderHeader.OrderHeaderId;
            DataContext = new StockItem();
            gridStock.ItemsSource = StockController.Instance.GetStockItems();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;

            int.TryParse(txtQuantity.Text, out int quantity);

            if (quantity > 0)
            {
                success = true;
            }

            if (success)
            {
                var stockItem = (StockItem)((Button)e.Source).DataContext;
                OrderController.Instance.UpsertOrderItem(orderHeader.OrderHeaderId, stockItem, quantity);
                MessageBox.Show("Item Added Successfully");
                //NavigationService.Navigate(new NewOrder(orderHeader));
            }
            else
            {
                MessageBox.Show("Invalid order quantity, please enter a whole number greater than 0.");
            }
            
        }

        private void btnExit_CLick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderDetails(orderHeader));
        }
    }
}
