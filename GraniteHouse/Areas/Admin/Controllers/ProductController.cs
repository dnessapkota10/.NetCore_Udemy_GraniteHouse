using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models.ViewModel;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironmet; //Workaround for Image

        [BindProperty] //This will automatically bind the object we post from form - so we do not have to add as param/argument in action methods
        public ProductViewModel productViewModel { get; set; }

        public ProductController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment) //Coming from we defined in Startup.cs
        {
            _db = db;
            _hostingEnvironmet = hostingEnvironment;
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


        //CREATE
        //Get : Product create
        public IActionResult Create()
        {
            return View(productViewModel);
        }

        //Post: Product Create
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }
            _db.Product.Add(productViewModel.Product);
            await _db.SaveChangesAsync();

            //Image being saved
            String webRootPath = _hostingEnvironmet.WebRootPath; //wwwroot
            var files = HttpContext.Request.Form.Files; //Files that were uploaded from the view

            var productFromDb = _db.Product.Find(productViewModel.Product.Id);

            if(files.Count != 0)
            {
                //File/Image has been uploaded
                var uploaded = Path.Combine(webRootPath, StaticDetail.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploaded,productViewModel.Product.Id+extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                productFromDb.Image = @"\" + StaticDetail.ImageFolder + @"\" + productViewModel.Product.Id + extension; //save image path to DB
            }
            else
            {
                //When user does not upload image
                var uploads = Path.Combine(webRootPath, StaticDetail.ImageFolder + @"\" + StaticDetail.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + StaticDetail.ImageFolder + @"\" + productViewModel.Product.Id + ".jpg");
                productFromDb.Image = StaticDetail.ImageFolder + @"\" + productViewModel.Product.Id + ".jpg";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            
        }
    }
}