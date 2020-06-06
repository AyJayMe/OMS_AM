using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace Domain
{
    public class OrderItem
    {
        public string Description { get; set; }
        public int OrderHeaderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StockItemId { get; set; }
        public decimal Total { get; set; }

        public OrderItem(int orderHeaderId, int stockItemId, string description, int quantity, decimal price)
        {
            OrderHeaderId = orderHeaderId;
            Description = description;
            Price = price;
            Quantity = quantity;
            StockItemId = stockItemId;
            Total = Quantity * Price;
        }
    }
}
