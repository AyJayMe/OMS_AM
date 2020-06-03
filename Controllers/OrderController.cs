using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;

namespace Controllers
{
    public class OrderController
    {
        private readonly Orders repository = new Orders();

        private static OrderController instance;

        public static OrderController Instance
        {
            get
            {
                //If no instance of OrderController exists, instantiate Orders.
                if (instance == null)
                {
                    instance = new OrderController();
                };
                return instance;
            }
        }

        public OrderHeader GetOrderHeader(int orderId)
        {
            return repository.GetOrderHeaders().First(o => o.OrderHeaderId == orderId);
        }
        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            return repository.GetOrderHeaders();
        }
        public OrderHeader CreateNewOrderHeader()
        {
            return repository.InsertOrderHeader();
        }

        public void UpsertOrderItem(int orderHeaderId, StockItem stockItem, int quantity)
        {
            repository.UpsertOrderItem(orderHeaderId, stockItem, quantity);
        }
        public OrderHeader SubmitOrder(int orderHeaderId)
        {
            int stateId = 2; //2 refers to pending order status
            return repository.UpdateOrderState(orderHeaderId, stateId);
        }
        public OrderHeader ProcessOrder(int orderHeaderId)
        {
            int stateId = 3; //3 refers to the processed order status
            return repository.UpdateOrderState(orderHeaderId, stateId);
        }
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            repository.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }
        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            repository.DeleteOrderItem(orderHeaderId, stockItemId);
        }

    }
}
