using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        //DBContext - DI
        private readonly Data.ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
                
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList()); //EF to access db, extract list of product types and send to view
        }

        //CREATE
        //GET - Create new product type
        public IActionResult Create()
        {
            return View();
        }

        //POST - Create new product type
        [HttpPost]
        [ValidateAntiForgeryToken] // Security mechanism that .net implmented for us. Gets added to request and server checks if the request is not altered on the way
        public async Task<IActionResult> Create(Models.ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Add(productTypes);
                await _db.SaveChangesAsync();
                //return RedirectToAction("Index"); //This can have typo
                return RedirectToAction(nameof(Index));                
            }
            return View(productTypes);
        }

        //EDIT 
        //GET - Edit product type
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return NotFound(); 
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound(); 
            }
            return View(productType);
        }

        //POST - Edit product type
        [HttpPost]
        [ValidateAntiForgeryToken] // Security mechanism that .net implmented for us. Gets added to request and server checks if the request is not altered on the way
        public async Task<IActionResult> Edit(int id, Models.ProductTypes productTypes)
        {
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                //return RedirectToAction("Index"); //This can have typo
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }


        //DETAILS 
        //GET - Details product type
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

    }
}