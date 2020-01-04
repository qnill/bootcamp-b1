using System;

namespace net_core_bootcamp_b1
{
    public class Product
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
    }
}