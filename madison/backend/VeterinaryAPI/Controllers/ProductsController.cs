using Microsoft.AspNetCore.Mvc;
using VeterinaryAPI.Models;
using VeterinaryAPI.Services;

namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Lấy tất cả sản phẩm active và không bị xóa, có phân trang
        /// </summary>
        /// <param name="page">Số trang (mặc định: 1)</param>
        /// <param name="pageSize">Số sản phẩm mỗi trang (mặc định: 20)</param>
        /// <returns>Danh sách sản phẩm với thông tin phân trang</returns>
        [HttpGet("active")]
        public async Task<ActionResult<ProductListResponse>> GetActiveProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 20;

                var result = await _productService.GetAllActiveProductsAsync(page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy tất cả thuốc nguy hiểm, có phân trang
        /// </summary>
        /// <param name="page">Số trang (mặc định: 1)</param>
        /// <param name="pageSize">Số sản phẩm mỗi trang (mặc định: 20)</param>
        /// <returns>Danh sách thuốc nguy hiểm với thông tin phân trang</returns>
        [HttpGet("dangerous-drugs")]
        public async Task<ActionResult<ProductListResponse>> GetDangerousDrugs([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 20;

                var result = await _productService.GetDangerousDrugsAsync(page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật mô tả sản phẩm
        /// </summary>
        /// <param name="updateDto">Thông tin cập nhật</param>
        /// <returns>Thông báo thành công hoặc lỗi</returns>
        [HttpPut("update-description")]
        public async Task<ActionResult<object>> UpdateProductDescription([FromBody] UpdateProductDescriptionDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
                }

                var success = await _productService.UpdateProductDescriptionAsync(updateDto.ProductId, updateDto.Description);
                
                if (success)
                {
                    return Ok(new { message = "Cập nhật mô tả sản phẩm thành công" });
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy sản phẩm với ID: " + updateDto.ProductId });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy sản phẩm theo ID
        /// </summary>
        /// <param name="id">ID sản phẩm</param>
        /// <returns>Sản phẩm</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                
                if (product == null)
                {
                    return NotFound(new { message = "Không tìm thấy sản phẩm với ID: " + id });
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }
    }
} 
