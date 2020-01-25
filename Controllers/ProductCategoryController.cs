using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using net_core_bootcamp_b1.DTOs;
using net_core_bootcamp_b1.Helpers;
using net_core_bootcamp_b1.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace net_core_bootcamp_b1.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]ProductCategoryAddDto model)
        {
            var result = await _productCategoryService.Add(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]ProductCategoryUpdateDto model)
        {
            var result = await _productCategoryService.Update(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([BindRequired]Guid id)
        {
            var result = await _productCategoryService.Delete(id);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(IList<ProductCategoryGetDto>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await _productCategoryService.Get();

            return Ok(result);
        }
    }
}