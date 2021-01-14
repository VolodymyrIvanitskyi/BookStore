using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore
{
    public class Order
    {
        public int Id { get; }

        private List<OrderItem> items;

        public IReadOnlyCollection<OrderItem> Items
        {
            get { return items; }
        }

        public int TotalCount
        {
            get { return items.Sum(item => item.Count); }
        }
        public decimal TotalPrice
        {
            get { return items.Sum(item => item.Price * item.Count); }
        }
        public Order(int id, IEnumerable<OrderItem> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            Id = id;
            this.items = new List<OrderItem>(items);
        }

        public void AddItem(Book book, int count)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var item = items.SingleOrDefault(i => i.BookId == book.Id);

            if(item == null)
            {
                items.Add(new OrderItem(book.Id, count, book.Price));
            }
            else //якщо книга є в замовленні, то видаляємо попереднє замовлення і створюємо нове 
                 // кількість книг = кількість книг до нового замовлення + к-ть книг нового замовлення
            {
                items.Remove(item);
                items.Add(new OrderItem(book.Id, item.Count + count, book.Price)); 

            }
        }
    }
}
