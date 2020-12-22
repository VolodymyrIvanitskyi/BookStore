using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public interface IBookRepository
    {
        Book[] GetByTitle(string titlePart);
    }
}
