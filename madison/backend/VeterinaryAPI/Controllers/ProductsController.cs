using Microsoft.AspNetCore.Mvc;
using MediatR;
using VeterinaryAPI.Application.DTOs;
using VeterinaryAPI.Application.UseCases.Queries;
using VeterinaryAPI.Application.UseCases.Commands;
using VeterinaryAPI.Application.Interfaces;


namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public ProductsController(IMediator mediator, IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get all active products with advanced filtering, searching and sorting
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Number of products per page (default: 20)</param>
        /// <param name="searchTerm">Search term for name, description, or category</param>
        /// <param name="category">Filter by category</param>
        /// <param name="sortBy">Sort by field (name, price, category, createdAt)</param>
        /// <param name="sortOrder">Sort order (asc, desc)</param>
        /// <returns>List of products with pagination information</returns>
        [HttpGet("active")]
        public async Task<ActionResult<ProductListResponse>> GetActiveProducts(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 20)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 20;

                var query = new GetActiveProductsQuery 
                { 
                    PageNumber = page, 
                    PageSize = pageSize
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error: " + ex.Message });
            }
        }

        /// <summary>
        /// Get all dangerous drugs with advanced filtering, searching and sorting
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Number of products per page (default: 20)</param>
        /// <param name="searchTerm">Search term for name, description, or category</param>
        /// <param name="category">Filter by category</param>
        /// <param name="sortBy">Sort by field (name, price, category, createdAt)</param>
        /// <param name="sortOrder">Sort order (asc, desc)</param>
        /// <returns>List of dangerous drugs with pagination information</returns>
        [HttpGet("dangerous-drugs")]
        public async Task<ActionResult<ProductListResponse>> GetDangerousDrugs(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 20)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 20;

                var query = new GetDangerousDrugsQuery 
                { 
                    PageNumber = page, 
                    PageSize = pageSize
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error: " + ex.Message });
            }
        }

        /// <summary>
        /// Update product description
        /// </summary>
        /// <param name="updateDto">Update information</param>
        /// <returns>Success or error message</returns>
        [HttpPut("update-description")]
        public async Task<ActionResult<object>> UpdateProductDescription([FromBody] UpdateProductDescriptionDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid data", errors = ModelState });
                }

                var command = new UpdateProductDescriptionCommand 
                { 
                    ProductId = updateDto.ProductId, 
                    Description = updateDto.Description 
                };
                
                var success = await _mediator.Send(command);
                
                if (success)
                {
                    return Ok(new { message = "Product description updated successfully" });
                }
                else
                {
                    return NotFound(new { message = "Product not found or cannot be updated with ID: " + updateDto.ProductId });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error: " + ex.Message });
            }
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var query = new GetProductByIdQuery { ProductId = id };
                var product = await _mediator.Send(query);
                
                if (product == null)
                {
                    return NotFound(new { message = "Product not found with ID: " + id });
                }
                
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error: " + ex.Message });
            }
        }
    }
} 
