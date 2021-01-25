using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Contractors
{
    public class PostomateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>
        {
            {"1","Львів" },
            {"2","Київ" }
        };

        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {
                "1",
                new Dictionary<string, string>
                {
                    { "1", "Залізничний вокзал" },
                    { "2", "площа Митна" },
                    { "3", "Чорновола Ашан" },
                }
            },
            {
                "2",
                new Dictionary<string, string>
                {
                    { "4", "Київський вокзал" },
                    { "5", "площа Свободи" },
                    { "6", "ТРЦ Цум" },
                }
            }
        };

        public string UniqueCode => "Postomate";

        public string Title => "Доставка через постомати у Львові та Києві";

        public Form CreateForm(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            return new Form(UniqueCode, order.Id, 1, false, new[]
            {
                new SelectionField("Місто", "city", "1", cities)
            }
            );
        }

        public Form MoveNext(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            if (step == 1)
            {
                if (values["city"] == "1")
                {
                    return new Form(UniqueCode,orderId,2,false, new Field[] 
                    {
                        new HiddenField("Місто","city","1"),
                        new SelectionField("Постомат","postomate","1",postamates["1"])
                    });
                }
                else if (values["city"] == "2")
                {
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                       {
                        new HiddenField("Місто","city","2"),
                        new SelectionField("Постамат", "postamate", "4", postamates["2"])
                        });
                }
                else
                    throw new InvalidOperationException("Invalid postamate city.");
            }
            else if (step == 2)
            {
                return new Form(UniqueCode, orderId, 3,true , new Field[]
                       {
                        new HiddenField("Місто","city",values["city"]),
                        new HiddenField("Постамат","postamate",values["postamate"])
                        });
            }
            else
                throw new InvalidOperationException("Invalid postamate step.");
        }
    }
}
