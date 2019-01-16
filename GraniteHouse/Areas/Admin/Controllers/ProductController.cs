using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty] //This will automatically bind the object we post from form - so we do not have to add as param/argument in action methods
        public ProductViewModel productViewModel { get; set; }

        public ProductController(ApplicationDbContext db) //Coming from we defined in Startup.cs
        {
            _db = db;
            productViewModel = new ProductViewModel()
            {
                ProductTypes = _db.ProductTypes.ToList(),
                Product = new Models.Product()
                
            };

        }

        public async Task<IActionResult> Index()
        {
            var products = _db.Product.Include(m => m.ProductTypes); //Includes ProductTypes using FK from Product
            return View(await products.ToListAsync());
        }
    }
}