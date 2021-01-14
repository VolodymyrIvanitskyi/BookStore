using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public interface IBookRepository
    {
        Book[] GetByIsbn(string isbn);
        Book[] GetByTitleOrAuthor(string titleOrAuthorPart);
        Book GetById(int id);
        Book[] GetAllByIds(IEnumerable<int> bookIds);
    }
}
