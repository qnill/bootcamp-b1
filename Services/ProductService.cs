using net_core_bootcamp_b1.DTOs;
using net_core_bootcamp_b1.Helpers;
using net_core_bootcamp_b1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace net_core_bootcamp_b1.Services
{
    public interface IProductService
    {
        ApiResult Add(ProductAddDto model);
        ApiResult Update(ProductUpdateDto model);
        ApiResult Delete(Guid id);
        IList<ProductGetDto> Get();
    }

    public class ProductService : IProductService
    {
        private static readonly IList<Product> data = new List<Product>();

        public ApiResult Add(ProductAddDto model)
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

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public ApiResult Update(ProductUpdateDto model)
        {
            var entity = data.Where(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefault();
            if (entity == null)
                return new ApiResult { Data = model.Id, Message = ApiResultMessages.PRE01 };

            entity.Name = model.Name;
            entity.Desc = model.Desc;

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public ApiResult Delete(Guid id)
        {
            var entity = data.Where(x => x.Id == id).FirstOrDefault();
            if (entity == null)
                return new ApiResult { Data = id, Message = ApiResultMessages.PRE01 };

            entity.IsDeleted = true;

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
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