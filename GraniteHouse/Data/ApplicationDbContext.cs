using System;
using System.Collections.Generic;
using System.Text;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Adding model to the database        
        public DbSet<ProductTypes> ProductTypes { get; set; }

        //Adding Product
        public DbSet<Product> Product { get; set; }
        
        //Adding Appointment
        public DbSet<Appointment> Appointment { get; set; }

        //Adding ProductSelectedForAppointment
        public DbSet<ProductSelectedForAppointment> ProductSelectedForAppointment { get; set; }

        //After this step you will need to add migration and update database
        // PM> add-migration addProductTypesToDatabase
        // PM> update-database
    }
}
