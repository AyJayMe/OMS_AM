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
            return repository.GetOrderHeaders().First(o => o.Id == orderId);
        }
        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            return repository.GetOrderHeaders();
        }
        public OrderHeader CreateNewOrderHeader()
        {
            return repository.InsertOrderHeader();
        }

        public void UpsertOrderItem(OrderHeader orderHeader, OrderItem orderItem)
        {
            repository.UpsertOrderItem(orderHeader, orderItem);
        }
        public void SubmitOrder(OrderHeader orderHeader)
        {
            int stateId = 2; //2 refers to pending order status
            repository.UpdateOrderState(orderHeader, stateId);
        }
        public void ProcessOrder(OrderHeader orderHeader)
        {
            int stateId = 3; //3 refers to the processed order status
            repository.UpdateOrderState(orderHeader, stateId);
        }
        public void RejectOrder(OrderHeader orderHeader)
        {
            int stateId = 4; //3 refers to the rejected order status
            repository.UpdateOrderState(orderHeader, stateId);
        }
        public void DeleteOrderHeaderAndOrderItems(OrderHeader orderHeader)
        {
            repository.DeleteOrderHeaderAndOrderItems(orderHeader);
        }
        public void DeleteOrderItem(OrderHeader orderHeader, int stockItemId)
        {
            repository.DeleteOrderItem(orderHeader, stockItemId);
        }

    }
}
