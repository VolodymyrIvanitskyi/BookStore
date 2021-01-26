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

        public string CellPhone { get; set; }

        public OrderDelivery Delivery { get; set; }
        public OrderPayment Payment { get; set; }
        public IReadOnlyCollection<OrderItem> Items
        {
            get { return items; }
        }

        public int TotalCount => items.Sum(item => item.Count);
        
        public decimal TotalPrice => items.Sum(item => item.Price * item.Count + (Delivery?.Amount ?? 0m));
        
        public Order(int id, IEnumerable<OrderItem> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            Id = id;
            this.items = new List<OrderItem>(items);
        }

        public OrderItem GetItem(int bookId)
        {
            int index = items.FindIndex(item => item.BookId == bookId);

            if (index == -1)
                ThrowBookException("Book not found",bookId);

            return items[index];
        }
       

        public void AddorUpdateItem(Book book, int count)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            int index = items.FindIndex(item => item.BookId == book.Id);
        
            if (index == -1)
            {
                items.Add(new OrderItem(book.Id, count, book.Price));
            }
            else //якщо книга є в замовленні, то видаляємо попереднє замовлення і створюємо нове 
                 // кількість книг = кількість книг до нового замовлення + к-ть книг нового замовлення
            {
                items[index].Count += count;
            }
        }
        
        public void RemoveItem(int bookId)
        {
            int index = items.FindIndex(item => item.BookId == bookId);

            if (index == -1)
                ThrowBookException("Cart does not contain an item", bookId);

            items.RemoveAt(index);
        }

        private void ThrowBookException(string message, int bookId)
        {
            var exception = new InvalidOperationException(message);
            exception.Data["BookId"] = bookId;
            

            throw exception;
        }
    }
}
