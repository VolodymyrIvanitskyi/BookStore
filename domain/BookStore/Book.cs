using System;

namespace BookStore
{
    public class Book
    {
        public Book(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public int Id { get; }
        public string Title { get; }
    }
}
