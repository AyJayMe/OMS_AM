using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum OrderStates {
        New = 1,
        Pending = 2,
        Confirmed = 3,
        Rejected = 4
    }
    public abstract class OrderState
    {
        protected OrderHeader _orderHeader;
        public OrderState(OrderHeader orderHeader)
        {
            _orderHeader = orderHeader;
        }
        public abstract OrderStates State { get; }
    }
}
