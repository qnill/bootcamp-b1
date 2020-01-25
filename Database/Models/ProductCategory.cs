using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace net_core_bootcamp_b1.Models
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }

        // FK
        // Product
        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }
    }
}