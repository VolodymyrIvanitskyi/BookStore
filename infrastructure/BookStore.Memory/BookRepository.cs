using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 0201038013", "D. Knuth", "Art Of Programming", "Description 1", 7.19m),
            new Book(2, "ISBN 0201485672", "M. Fowler", "Refactoring", "Description 2", 12.45m),
            new Book(3, "ISBN 0131101633", "B. Kernighan, D. Ritchie", "C Programming Language", "Description 3", 14.98m)
        };

        public Book[] GetAllByIds(IEnumerable<int> bookIds)
        {
            var foundBooks = from book in books
                             join bookId in bookIds on book.Id equals bookId
                             select book;
            return foundBooks.ToArray();
        }

        public Book GetById(int id)
        {
            return books.Single(book => book.Id == id);
        }

        public Book[] GetByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn).ToArray();
        }


        public Book[] GetByTitleOrAuthor(string titleOrAuthorPart)
        {
            return books.Where(book => book.Title.Contains(titleOrAuthorPart)
                            || book.Author.Contains(titleOrAuthorPart))
                            .ToArray();
        }
    }
}
