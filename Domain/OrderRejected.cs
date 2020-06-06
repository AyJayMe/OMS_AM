using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class OrderRejected : OrderState
    {
        public override OrderStates State => OrderStates.Rejected;

        public OrderRejected(OrderHeader orderHeader)
            : base(orderHeader)
        {
        }
    }
}
