using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraniteHouse.Models;
using GraniteHouse.Data;
using Microsoft.EntityFrameworkCore;
using GraniteHouse.Extensions;

namespace GraniteHouse.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var productList = await _db.Product.Include(m => m.ProductTypes).ToListAsync(); //included ProductTypes if needed - incase
            return View(productList);
        }

        //Get : Product Detail
        public async Task<IActionResult> Details(int ? id)
        {
            var product = await _db.Product.Include(m => m.ProductTypes).Where(m=>m.Id==id).FirstOrDefaultAsync(); //included ProductTypes if needed - incase
            return View(product);
        }
        
        //Post : Product Detail
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPost(int id)
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart"); //Get from Session using Extension method of Extension class SesssionExtensions
            if (listShoppingCart == null)
            {
                listShoppingCart = new List<int>();
            }
            listShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", listShoppingCart); //Setting our session variable ssShoppingCart
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
