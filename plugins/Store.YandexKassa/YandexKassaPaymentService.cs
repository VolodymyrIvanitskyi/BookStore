using BookStore;
using BookStore.Contractors;
using BookStore.Web.Contractors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.YandexKassa
{
    public class YandexKassaPaymentService : IPaymentService, IWebContractorService

    {
        public string UniqueCode => "YandexKassa";

        public string Title => "Оплата банківською картою";

        public string GetUri => "/YandexKassa/";

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(UniqueCode, "Оплата картою", new Dictionary<string, string>());
        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> value)
        {
            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}
