using System;

namespace net_core_bootcamp_b1.DTOs
{
    public class ProductAddDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
    }

    public class ProductUpdateDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class ProductGetDto
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
    }
}