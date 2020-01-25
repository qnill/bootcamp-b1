using System;
using System.ComponentModel.DataAnnotations;

namespace net_core_bootcamp_b1.DTOs
{
    public class ProductAddDto
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Required, Range(0, 1000)]
        public double? Price { get; set; }
        [Required]
        public Guid ProductCategoryId { get; set; }
    }

    public class ProductUpdateDto
    {
        [Required]
        public Guid? Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Required]
        public Guid ProductCategoryId { get; set; }
    }

    public class ProductGetDto
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }

        #region ProductCategory

        public Guid ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

        #endregion
    }
}