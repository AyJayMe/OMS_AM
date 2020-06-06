using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class OrderNew : OrderState
    {
        public override OrderStates State => OrderStates.New;

        public OrderNew(OrderHeader orderHeader)
            : base(orderHeader)
        {
        }
    }
}
