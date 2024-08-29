using Application.Feature.Products.Commands.CreateProduct;
using Application.Feature.Products.Commands.DeleteProduct;
using Application.Feature.Products.Commands.ReduceProductStock;
using Application.Feature.Products.Commands.UpdateProduct;
using Application.Feature.Products.Queries.GetAllProducts;
using Application.Feature.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommandRequest { ProductId = id };
            var result = await _mediator.Send(command);
            if (result.IsDeleted)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var query = new GetProductByIdQueryRequest { ProductId = id };
            var result = await _mediator.Send(query);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPut("{id}/reduce-stock")]
        public async Task<IActionResult> ReduceProductStock(int id, [FromBody] int quantityToReduce)
        {
            var command = new ReduceProductStockCommandRequest
            {
                ProductId = id,
                QuantityToReduce = quantityToReduce
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
