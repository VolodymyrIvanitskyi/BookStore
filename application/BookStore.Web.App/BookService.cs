using BookStore.Web.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore
{
    public class BookService
    {
        private readonly IBookRepository bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public IReadOnlyCollection<BookModel> GetByQuery(string query)
        {
            var books = Book.IsIsbn(query) 
                ? bookRepository.GetByIsbn(query) 
                : bookRepository.GetByTitleOrAuthor(query);

            // можливо тут зробити валідацію запиту, ToUpper і т.д
            return books.Select(Map).ToArray();
        }

        private BookModel Map(Book book)
        {
            return new BookModel
            {
                Author = book.Author,
                Isbn = book.Isbn,
                Id = book.Id,
                Price = book.Price,
                Title = book.Title,
                Description = book.Description
            };
        }

        public BookModel GetById(int id)
        {
            return Map(bookRepository.GetById(id));
        }
    }
}
