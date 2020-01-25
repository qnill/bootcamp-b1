using System;
using System.ComponentModel.DataAnnotations;

namespace net_core_bootcamp_b1.DTOs
{
    public class ProductCategoryAddDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }

    public class ProductCategoryUpdateDto : ProductCategoryAddDto
    {
        [Required]
        public Guid? Id { get; set; }
    }

    public class ProductCategoryGetDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}