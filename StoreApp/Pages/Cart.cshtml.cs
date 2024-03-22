using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

// Yorum satırına aldığım yerleri (-A yazdığım yerler) SessionCart.cs Modelinde ekleme çıkarma işlemi yaparken session ile uyumlu hale getirdim. bknz:StoreApp/Models/SessionCart.cs
// Bu sebeple burada tekrardan yazmama gerek kalmıyor.Fakat o sınıfı yazmasaydık nasıl olurdu ? Sorusunun yanıtı için satırları silmedim. 
// Sonuç olarak SessionCart.cs sınıfının yazımı zorunlu değil fakat kod tekrarlılığını azaltı okunabilirliği artırmak amacıyla yazılması faydalı olacaktır.

namespace StoreApp.Pages
{

    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;
        public Cart Cart { get; set; } //IoC yapıyoruz

        public CartModel(IServiceManager manager,Cart cartservice)
        {
            _manager = manager;
            Cart=cartservice;
        }


        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
          //  Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();-A
            //varsa olan cart nesnesini yoksa yeni bir Cart nesnesi oluşturfuk
        }

        public IActionResult OnPost(int Id, string returnUrl)
        {
            Product? product = _manager.ProductService.GetOneProduct(Id, false);

            if (product is not null)
            {

              //  Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();-A
                Cart.AddItem(product, 1);
              //  HttpContext.Session.SetJson<Cart>("cart", Cart);-A
            }
            return RedirectToPage(new{
              returnUrl=returnUrl
            }); 

        }

        public IActionResult OnPostRemove(int Id, string returnUrl)
        {


        // Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(); -A
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.Id.Equals(Id)).Product);  
         // HttpContext.Session.SetJson<Cart>("cart", Cart); -A
            return Page();
        }
    }
}

