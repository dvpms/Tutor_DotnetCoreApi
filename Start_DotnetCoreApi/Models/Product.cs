using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApi.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }

    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category {get;set;}
        public decimal Price { get; set; }
    }

}