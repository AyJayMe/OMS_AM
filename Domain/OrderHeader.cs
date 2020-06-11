using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Domain
{
    public class OrderHeader
    {
        private OrderState _state;

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStates State { get => _state.State; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int OrderItemCount { get => OrderItems.Sum(oi => oi.Quantity); }
        public decimal Total { get => OrderItems.Sum(oi => oi.Total); }
        

        public OrderHeader(int id, DateTime date, int stateId)
        {
            Id = id;
            Date = date;
            setState(stateId);
        }
        public OrderHeader()
        {

        }
        public void setState(int stateId)
        {
            //Calls the constructor for current state of order
            switch (stateId)
            {
                case 1:
                    _state = new OrderNew(this);
                    break;
                case 2:
                    _state = new OrderPending(this);
                    break;
                case 3:
                    _state = new OrderCompleted(this);
                    break;
                case 4:
                    _state = new OrderRejected(this);
                    break;
                default:
                    throw new InvalidOperationException($"The state id was invalid: {stateId}");
            }
        }
        public void AddOrderItem(OrderItem orderItem)
        {
            //Declare local variables
            bool updatedOrderItem = false;

            //Check to see if orderItem is already in order
            foreach(OrderItem item in OrderItems)
            {
                //Update item values if the item is already in the order
                if (orderItem.StockItemId == item.StockItemId)
                {
                    item.Quantity = orderItem.Quantity;
                    updatedOrderItem = true;
                }
            }

            //If no order item has been updated (i.e. item not already in order), then add orderItem to OrderItems
            if (!updatedOrderItem)
            {
                OrderItems.Add(orderItem);
            }
        }

        public void RemoveOrderItem(int orderItemId)
        {
            List<int> indexes = new List<int>();
            //Cycles through list and removes orderItems
            foreach (OrderItem item in OrderItems)
            {
                if (orderItemId == item.StockItemId)
                {
                    indexes.Add(OrderItems.IndexOf(item));
                }
            }

            try
            {
                for (int i = 0; i <= indexes.Count - 1; i++)
                {
                    OrderItems.RemoveAt(indexes[i]);
                }
            }
            catch (Exception ex)
            {
                throw new OperationCanceledException($"The item was not found in the order: {ex}");
            }
        }
    }
}
