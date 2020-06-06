using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class OrderPending : OrderState
    {
        public override OrderStates State => OrderStates.Pending;

        public OrderPending(OrderHeader orderHeader)
            : base(orderHeader)
        {
        }
    }
}
