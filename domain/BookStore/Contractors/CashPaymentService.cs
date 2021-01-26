using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Contractors
{
    public class CashPaymentService : IPaymentService
    {
        public string UniqueCode => "Cash";

        public string Title => "Оплата готівкою";

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            if (form.UniqueCode != UniqueCode || !form.IsFinal)
                throw new InvalidOperationException("Invalid payment form.");

            return new OrderPayment(UniqueCode, "Оплата готівкою", new Dictionary<string,string>());
        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> value)
        {
            if (step != 1)
                throw new InvalidOperationException("Invalid cash step");

            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}
