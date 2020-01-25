using Microsoft.EntityFrameworkCore;
using net_core_bootcamp_b1.Database;
using net_core_bootcamp_b1.DTOs;
using net_core_bootcamp_b1.Helpers;
using net_core_bootcamp_b1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_bootcamp_b1.Services
{
    public interface IProductCategoryService
    {
        Task<ApiResult> Add(ProductCategoryAddDto model);
        Task<ApiResult> Update(ProductCategoryUpdateDto model);
        Task<ApiResult> Delete(Guid id);
        Task<IList<ProductCategoryGetDto>> Get();
    }

    public class ProductCategoryService: IProductCategoryService
    {
        private readonly BootcampDbContext _context;

        public ProductCategoryService(BootcampDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult> Add(ProductCategoryAddDto model)
        {
            var entity = new ProductCategory
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            entity.Name = model.Name;

            await _context.ProductCategory.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Update(ProductCategoryUpdateDto model)
        {
            var entity = await _context.ProductCategory
                .Where(x => !x.IsDeleted && x.Id == model.Id)
                .FirstOrDefaultAsync();

            if (entity == null)
                return new ApiResult { Data = model.Id, Message = ApiResultMessages.PCE01 };

            entity.Name = model.Name;
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Delete(Guid id)
        {
            var entity = await _context.ProductCategory
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
                return new ApiResult { Data = id, Message = ApiResultMessages.PCE01 };

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public async Task<IList<ProductCategoryGetDto>> Get()
        {
            var result = await _context.ProductCategory
                .Where(x => !x.IsDeleted)
                .Select(s => new ProductCategoryGetDto
                {
                    Id = s.Id,
                    CreatedAt = s.CreatedAt,
                    Name = s.Name
                })
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return result;
        }
    }
}