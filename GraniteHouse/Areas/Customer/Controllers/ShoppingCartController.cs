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


        //Post: Shopping Cart - Schedule appointment
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public IActionResult ScheduleAppointment()
        {
            List<int> listItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            shoppingCartViewModel.Appointment.AppointmentDate = shoppingCartViewModel.Appointment.AppointmentDate //Workaround to push appointment date time in a single column appointment date
                                                                .AddHours(shoppingCartViewModel.Appointment.AppointmentTime.Hour)
                                                                .AddMinutes(shoppingCartViewModel.Appointment.AppointmentTime.Minute);
            Appointment appointment = shoppingCartViewModel.Appointment;
            _db.Appointment.Add(appointment);
            _db.SaveChanges();

            int appointmentId = appointment.Id;

            //Use this Id to insert into ProductSelectedAppointment
            foreach(int productId in listItems)
            {
                ProductSelectedForAppointment productSelectedForAppointment = new ProductSelectedForAppointment()
                {
                    AppointmentId = appointmentId,
                    ProductId = productId
                };
                _db.ProductSelectedForAppointment.Add(productSelectedForAppointment);                
            }
             _db.SaveChanges();

            //Clear out the session and reset 
            listItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", listItems);

            return RedirectToAction(nameof(Index));
        }
        
    }
}