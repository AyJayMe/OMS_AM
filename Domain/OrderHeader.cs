using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace Domain
{
    public class OrderHeader
    {
        
        public int OrderHeaderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime Date { get; set; }
        public int OrderState { get; set; }
        public decimal Total { get; set; }

        public OrderHeader(int orderHeaderId, int orderState, DateTime date, List<OrderItem> orderItems)
        {
            OrderHeaderId = orderHeaderId;
            Date = date;
            OrderState = orderState;
            OrderItems = orderItems;
            
            foreach(OrderItem item in OrderItems)
            {
                Total += item.Total;
            }
        }
        public OrderHeader(int orderHeaderId, DateTime date)
        {
            OrderHeaderId = orderHeaderId;
            Date = date;
        }
        public OrderHeader()
        {

        }
        public void AddOrderitem(OrderItem orderItem)
        {
            //Declare local variables
            int stockItemId = orderItem.StockItemId;
            bool updateOrderItem = true;

            //Check to see if orderItem is already in order
            foreach(OrderItem item in OrderItems)
            {
                //Update item values if the item is already in the order
                if (stockItemId == item.StockItemId)
                {
                    item.StockItemId += orderItem.Quantity;
                    item.Total = item.Quantity * item.Price;
                    updateOrderItem = false;
                }
            }

            //If no order item has been updated (i.e. item not already in order), then add orderItem to OrderItems
            if (updateOrderItem)
            {
                OrderItems.Add(orderItem);
            }
        }
        public void Complete()
        {
            OrderState = 4;
        }
        public void Reject()
        {
            OrderState = 3;
        }
        public void Submit()
        {

        }
        public void SetState()
        {
            OrderState = 2;
        }
    }
}
