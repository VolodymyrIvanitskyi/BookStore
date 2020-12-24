using System;
using System.Text.RegularExpressions;

namespace BookStore
{
    public class Book
    {
        public Book(int id, string isbn, string author, string title)
        {
            Id = id;
            Isbn = isbn;
            Author = author;
            Title = title;
        }
        public string Isbn { get; }
        public string Author { get; }
        public int Id { get; }
        public string Title { get; }
    
        internal static bool IsIsbn(string query)
        {
            if (query == null)
                return false;

            query = query.Replace("-", "")
                .Replace(" ", "")
                .ToUpper();
            return Regex.IsMatch(query,"^ISBN\\d{10}(\\d{3})?$");
        }
    }
}
