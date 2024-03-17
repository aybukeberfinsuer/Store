using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages
{

    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;
        public Cart Cart { get; set; } //IoC yapıyoruz

        public CartModel(IServiceManager manager)
        {
            _manager = manager;
        }


        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            //varsa olan cart nesnesini yoksa yeni bir Cart nesnesi oluşturfuk
        }

        public IActionResult OnPost(int Id, string returnUrl)
        {
            Product? product = _manager.ProductService.GetOneProduct(Id, false);

            if (product is not null)
            {

                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson<Cart>("cart", Cart);
            }
            return Page(); //returnUrl logici yönlendireceğiz.

        }

        public IActionResult OnPostRemove(int Id, string returnUrl)
        {


            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.Id.Equals(Id)).Product);
            HttpContext.Session.SetJson<Cart>("cart", Cart);
            return Page();
        }
    }
}