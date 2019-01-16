using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public string Image { get; set; }
        public string ShadeColor { get; set; }

        [Display(Name="Product Type")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")] //Foreign key from Product Type --Integrity of FK relation
        public virtual ProductTypes ProductTypes{ get; set; } //Virtaul -- will not be added to the database
    }
}
