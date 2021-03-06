﻿using Microsoft.EntityFrameworkCore;
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
    public interface IProductService
    {
        Task<ApiResult> Add(ProductAddDto model);
        Task<ApiResult> Update(ProductUpdateDto model);
        Task<ApiResult> Delete(Guid id);
        Task<IList<ProductGetDto>> Get();
        Task<IList<ProductGetCountInCategoryDto>> GetCountInCategory();
        Task<IList<ProductGetCountInCategoryDto>> GetCountInCategoryMTwo();
        Task<IList<ProductGetTotalPriceInCategoryDto>> GetTotalPriceInCategory();
    }

    public class ProductService : IProductService
    {
        private readonly BootcampDbContext _context;

        public ProductService(BootcampDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult> Add(ProductAddDto model)
        {
            Product entity = new Product
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            entity.Name = model.Name;
            entity.Desc = model.Desc;
            entity.Price = model.Price ?? 0;
            entity.ProductCategoryId = model.ProductCategoryId;

            await _context.Product.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Update(ProductUpdateDto model)
        {
            var entity = await _context.Product.Where(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = model.Id, Message = ApiResultMessages.PRE01 };

            entity.Name = model.Name;
            entity.Desc = model.Desc;
            entity.ProductCategoryId = model.ProductCategoryId;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Delete(Guid id)
        {
            var entity = await _context.Product.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = id, Message = ApiResultMessages.PRE01 };

            entity.IsDeleted = true;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.Id, Message = ApiResultMessages.Ok };
        }

        public async Task<IList<ProductGetDto>> Get()
        {
            var result = new List<ProductGetDto>();

            result = await _context.Product
                .Where(x => !x.IsDeleted)
                .Select(s => new ProductGetDto
                {
                    Id = s.Id,
                    CreatedAt = s.CreatedAt,
                    Name = s.Name,
                    Desc = s.Desc,
                    Price = s.Price,
                    ProductCategoryId = s.ProductCategoryId,
                    ProductCategoryName = s.ProductCategory.Name
                })
                .ToListAsync();

            return result;
        }

        public async Task<IList<ProductGetCountInCategoryDto>> GetCountInCategory()
        {
            var result = await
                (from product in _context.Product
                 join category in _context.ProductCategory
                    on new { product.ProductCategoryId, IsDeleted = false }
                    equals new { ProductCategoryId = category.Id, category.IsDeleted }
                 where !product.IsDeleted
                 group category by new { category.Id, category.Name } into gp
                 select new ProductGetCountInCategoryDto
                 {
                     CategoryId = gp.Key.Id,
                     CategoryName = gp.Key.Name,
                     ProductCount = gp.Count()
                 })
                 .OrderByDescending(o => o.ProductCount)
                 .ToListAsync();

            return result;
        }

        public async Task<IList<ProductGetCountInCategoryDto>> GetCountInCategoryMTwo()
        {
            var result = await _context.Product
                .Where(x => !x.IsDeleted)
                .GroupBy(gp => new { id = gp.ProductCategoryId, name = gp.ProductCategory.Name })
                .Select(s => new ProductGetCountInCategoryDto
                {
                    CategoryId = s.Key.id,
                    CategoryName = s.Key.name,
                    ProductCount = s.Count()
                })
                .OrderByDescending(o => o.ProductCount)
                .ToListAsync();

            return result;
        }

        public async Task<IList<ProductGetTotalPriceInCategoryDto>> GetTotalPriceInCategory()
        {
            var result = await _context.Product
                .Where(x => !x.IsDeleted)
                .GroupBy(gp => new { id = gp.ProductCategoryId, name = gp.ProductCategory.Name })
                .Select(s => new ProductGetTotalPriceInCategoryDto
                {
                    CategoryId = s.Key.id,
                    CategoryName = s.Key.name,
                    TotalPrice = s.Sum(sm => sm.Price)
                })
                .OrderByDescending(o => o.TotalPrice)
                .ToListAsync();

            return result;
        }
    }
}