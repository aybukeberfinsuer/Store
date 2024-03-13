using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace StoreApp.Pages{

    public class CartModel:PageModel
    {
        private readonly IServiceManager _manager;
        public Cart Cart { get; set; } //IoC yapıyoruz

        public CartModel(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            Cart = cart;
        }


        public string  ReturnUrl { get; set; } ="/";

        public void OnGet(string returnUrl){
            ReturnUrl= returnUrl ?? "/";
        }

        public IActionResult OnPost(int Id,string returnUrl)
        {
            Product? product= _manager.ProductService.GetOneProduct(Id,false);

            if(product is not null){
                Cart.AddItem(product,1);
            }
            return Page(); //returnUrl logici yönlendireceğiz.
            
        }

        public IActionResult OnPostRemove(int Id,string returnUrl){
            Cart.RemoveLine(Cart.Lines.First(cl=>cl.Product.Id.Equals(Id)).Product);
            return Page();
        }
    }
}