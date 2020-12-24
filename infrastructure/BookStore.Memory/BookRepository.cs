using System;
using System.Linq;

namespace BookStore.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 12312-31231", "D. Knuth", "Art Of Programming"),
            new Book(2, "ISBN 12312-31232", "M. Fowler", "Refactoring"),
            new Book(3, "ISBN 12312-31233", "B. Kernighan, D. Ritchie", "C Programming Language")
        };
         
        public Book[] GetByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn).ToArray();
        }

        /*public Book[] GetByTitle(string titlePart)
        {
            return books.Where(book => book.Title.Contains(titlePart)).ToArray();
        }*/

        public Book[] GetByTitleOrAuthor(string titleOrAuthorPart)
        {
            return books.Where(book => book.Title.Contains(titleOrAuthorPart)
                            || book.Author.Contains(titleOrAuthorPart))
                            .ToArray();
        }
    }
}
