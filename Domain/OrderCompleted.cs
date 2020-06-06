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

        public override void Complete(ref OrderState _state)
        {
            throw new InvalidOperationException("An order cannot be 'completed' when it has already been completed.");

        }

        public override void Reject(ref OrderState _state)
        {
            throw new InvalidOperationException("An order cannot be 'rejected' when it has already been completed.");
        }

        public override void Submit(ref OrderState _state)
        {
            throw new InvalidOperationException("An order cannot be 'submitted' when it has already been completed.");
        }
    }
}
