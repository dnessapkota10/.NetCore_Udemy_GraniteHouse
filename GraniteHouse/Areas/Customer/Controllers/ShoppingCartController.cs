using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel shoppingCartViewModel { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            shoppingCartViewModel = new ShoppingCartViewModel()
            {
                Products = new List<Models.Product>()                
            };
        }

        //Get: Index Shopping Cart
        public async Task<IActionResult> Index()
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if(listShoppingCart.Count > 0)
            {
                foreach(int cartItem in listShoppingCart)
                {
                    Product product = _db.Product.Include(p=>p.ProductTypes).Where(p => p.Id == cartItem).FirstOrDefault();
                    shoppingCartViewModel.Products.Add(product);
                }
            }
            return View(shoppingCartViewModel);
        }
    }
}