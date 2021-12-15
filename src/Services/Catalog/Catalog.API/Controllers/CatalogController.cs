using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CatalogController : ControllerBase
	{
		private readonly IProductRepository _productRepository;
		private readonly ILogger<CatalogController> _logger;

		public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var products = await _productRepository.Get();

			return Ok(products);
		}

		[HttpGet("{id:length(24)}", Name = "GetProduct")]
		[ProducesResponseType(typeof(Product),StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Product>> GetProductById(string id)
		{
			var product = await _productRepository.GetById(id);

			if (product == null)
			{
				_logger.LogError($"Product with id: {id}, not found");
				return NotFound();
			}

			return Ok(product);
		}

		[HttpGet]
		[Route("[action]/{category}", Name = "GetProductByCategory")]
		[ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
		{
			var products = await _productRepository.GetProductByCategory(category);

			return Ok(products);
		}

		[HttpPost]
		[ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
		public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
		{
			await _productRepository.Create(product);

			return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
		}
	}
}