using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.App
{
    public class Cart
    {
        /*public IDictionary<int, int> Items { get; set; } = new Dictionary<int, int>();
        public decimal Amount { get; set; }*/
        public int OrderId { get; }
        public int TotalCount { get;}
        public decimal TotalPrice { get;}

        public Cart(int orderId, int totalCount, decimal totalPrice)
        {
            OrderId = orderId;
            TotalCount = totalCount;
            TotalPrice = totalPrice;
        }
    }
}
