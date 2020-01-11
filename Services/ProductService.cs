using net_core_bootcamp_b1.DTOs;
using net_core_bootcamp_b1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace net_core_bootcamp_b1.Services
{
    public interface IProductService
    {
        string Add(ProductAddDto model);
        string Update(ProductUpdateDto model);
        string Delete(Guid id);
        IList<ProductGetDto> Get();
    }

    public class ProductService : IProductService
    {
        private static readonly IList<Product> data = new List<Product>();

        public string Add(ProductAddDto model)
        {
            Product entity = new Product
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            entity.Name = model.Name;
            entity.Desc = model.Desc;
            entity.Price = model.Price ?? 0;

            data.Add(entity);

            return ($"{model.Name} eklendi");
        }

        public string Update(ProductUpdateDto model)
        {
            var entity = data.Where(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefault();
            if (entity == null)
                return ($"{model.Id} 'e ait kayıt bulunamadı");

            entity.Name = model.Name;
            entity.Desc = model.Desc;

            return ($"{entity.Id} 'e ait kayıt güncellendi");
        }

        public string Delete(Guid id)
        {
            var entity = data.Where(x => x.Id == id).FirstOrDefault();
            if (entity == null)
                return ($"{id} 'e ait kayıt bulunamadı");

            entity.IsDeleted = true;

            return ($"{entity.Name} silindi");
        }

        public IList<ProductGetDto> Get()
        {
            var result = new List<ProductGetDto>();

            result = data
                .Where(x => !x.IsDeleted)
                .Select(s => new ProductGetDto
                {
                    Id = s.Id,
                    CreatedAt = s.CreatedAt,
                    Name = s.Name,
                    Desc = s.Desc,
                    Price = s.Price
                })
                .ToList();

            return result;
        }
    }
}