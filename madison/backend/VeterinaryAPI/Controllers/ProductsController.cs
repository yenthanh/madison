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
        /// Lấy tất cả sản phẩm active và không bị xóa, sắp xếp theo thời gian tạo mới nhất
        /// </summary>
        /// <returns>Danh sách sản phẩm</returns>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Product>>> GetActiveProducts()
        {
            try
            {
                var products = await _productService.GetAllActiveProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy tất cả thuốc nguy hiểm, loại trừ những sản phẩm đã xóa hoặc không active
        /// </summary>
        /// <returns>Danh sách thuốc nguy hiểm</returns>
        [HttpGet("dangerous-drugs")]
        public async Task<ActionResult<IEnumerable<Product>>> GetDangerousDrugs()
        {
            try
            {
                var products = await _productService.GetDangerousDrugsAsync();
                return Ok(products);
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
        public async Task<ActionResult<Product>> GetProduct(int id)
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
