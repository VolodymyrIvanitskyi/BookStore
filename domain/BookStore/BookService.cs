using System;
using System.Collections.Generic;
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
        public Book[] GetByQuery(string query)
        {
            // можливо тут зробити валідацію запиту, ToUpper і т.д
            if (Book.IsIsbn(query))
                return bookRepository.GetByIsbn(query);
            
            return bookRepository.GetByTitleOrAuthor(query);
        }
    }
}
