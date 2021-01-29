
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService bookService;
        public BookController(BookService bookService)
        {
            this.bookService = bookService;
        }
        public IActionResult Index(int id)
        {
            var book = bookService.GetById(id);
            return View(book);
        }
    }
}