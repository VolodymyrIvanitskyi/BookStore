using BookStore.Contractors;
using BookStore.Messages;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BookStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IEnumerable<IDeliveryService> deliveryServices;
        private readonly INotificationService notificationService;

        public OrderController(IBookRepository bookRepository,
                                IOrderRepository orderRepository,
                                IEnumerable<IDeliveryService> deliveryServices,
                                INotificationService notificationService)
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
            this.deliveryServices = deliveryServices;
            this.notificationService = notificationService;
        }

        [HttpGet] 
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                //Якщо в сесії є запис про корзину
                var order = orderRepository.GetById(cart.OrderId);
                OrderModel model = Map(order);
                return View(model);
            }
            return View("Empty");
        }

        private OrderModel Map(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);
            var books = bookRepository.GetAllByIds(bookIds);
            var itemModels = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderItemModel
                             {
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
                                 Price = item.Price,
                                 Count = item.Count
                             };
            return new OrderModel
            {
                Id = order.Id,
                Items = itemModels.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice
            };
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);
        }
        [HttpPost]
        public IActionResult AddItem(int bookId, int count = 1)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            var book = bookRepository.GetById(bookId);

            order.AddorUpdateItem(book, count);

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Book", new { id = bookId });

        }
        [HttpPost]
        public IActionResult UpdateItem(int bookId, int count)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.GetItem(bookId).Count = count;
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Order");
        }

        private (Order order, Cart cart) GetOrCreateOrderAndCart()
        {
            Order order;

            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                //Якщо в сесії є запис про корзину
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                //якщо в сесії немає запису про корзину то створюємо її
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }
            return (order, cart);
        }

        [HttpPost]
        public IActionResult RemoveItem(int bookId)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.RemoveItem(bookId);
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult SendConfirmationCode(int id, string cellPhone)
        {
            var order = orderRepository.GetById(id);
            var model = Map(order);

            if (!IsValidateCellPhone(cellPhone))
            {
                model.Errors["cellPhone"] = "Невірний номер телефону";
                return View("Index", model);
            }

            //int code = 1111; //Random.Next(1000,10000)
            Random random = new Random();
            int code = random.Next(1000, 10000);
            HttpContext.Session.SetInt32(cellPhone, code);
            notificationService.SendConfirmationCode(cellPhone, code);

            return View("Confirmation",
                new ConfirmationModel
                {
                    OrderId = id,
                    CellPhone = cellPhone
                }) ;
        }
        private bool IsValidateCellPhone(string cellPhone)
        {
            if (cellPhone == null)
                return false;
            cellPhone = cellPhone.Replace(" ", "").Replace("-", "");
            return Regex.IsMatch(cellPhone, @"^\+?\d{12}$");
        }

        [HttpPost]
        public IActionResult Confirmate(int id, string cellPhone, int code)
        {
            int? storedCode = HttpContext.Session.GetInt32(cellPhone);
            if (storedCode == null)
            {
                return View("Confirmation",
                new ConfirmationModel
                {
                    OrderId = id,
                    CellPhone = cellPhone,
                    Errors = new Dictionary<string, string>
                    {
                        { "code", "Пустий код, повторіть відправлення" }
                    }
                }) ;
            }

            if (storedCode != code)
            {
                return View("Confirmation",
                new ConfirmationModel
                {
                    OrderId = id,
                    CellPhone = cellPhone,
                    Errors = new Dictionary<string, string>
                    {
                        { "code", "Невірний код" }
                    }
                });
            }

            //todo: Зберегти cellPhone

            HttpContext.Session.Remove(cellPhone);

            var model = new DeliveryModel
            {
                OrderId = id,
                Methods = deliveryServices.ToDictionary(service=>service.UniqueCode,
                                                        service=>service.Title)
            };

            return View("DeliveryMethod", model);
        }

        [HttpPost]
        public IActionResult StartDelivery(int id, string uniqueCode)
        {
            var deliveryService = deliveryServices.Single(service => service.UniqueCode == uniqueCode);
            var order = orderRepository.GetById(id);

            var form = deliveryService.CreateForm(order);

            return View("DeliveryStep", form);
        }

        [HttpPost]
        public IActionResult NextDelivery(int id, string uniqueCode, int step, Dictionary<string,string> values)
        {
            var deliveryService = deliveryServices.Single(service => service.UniqueCode == uniqueCode);

            var form = deliveryService.MoveNext(id, step, values);

            if (form.IsFinal)
            {
                return null; //Доробити
            }

            return View("DeliveryStep", form);
        }
    }
}