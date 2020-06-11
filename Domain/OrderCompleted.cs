using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class OrderCompleted : OrderState
    {
        public override OrderStates State => OrderStates.Confirmed;

        public OrderCompleted(OrderHeader orderHeader)
            : base(orderHeader)
        {
        }
    }
}
