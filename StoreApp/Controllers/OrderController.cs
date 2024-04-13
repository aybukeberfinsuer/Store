using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers
{

    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly Cart cart;



        public OrderController(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            this.cart = cart;
        }

        [Authorize]
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout([FromForm] Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                _manager.OrderService.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Complete", new { OrderId = order.OrderId });
            }
            else
            {
                return View();

            }
        }
    }
}