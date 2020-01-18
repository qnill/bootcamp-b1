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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        ///  Git Example Changes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]ProductAddDto model)
        {
            var result = await _productService.Add(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        ///  Git example master
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]ProductUpdateDto model)
        {
            var result = await _productService.Update(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([BindRequired]Guid id)
        {
            var result = await _productService.Delete(id);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Get All Products
        /// </summary>        
        /// <returns></returns>
        [HttpGet("Get")]
        [ProducesResponseType(typeof(IList<ProductGetDto>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.Get();

            return Ok(result);
        }
    }
}