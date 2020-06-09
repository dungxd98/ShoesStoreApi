using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStoreApi.Data;
using ShoesStoreApi.DTOs;
using ShoesStoreApi.Mapping;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Controllers {
    [Route ("api/{controller}")]
    [ApiController]
    public class ProductsController : ControllerBase {
        private readonly ShoesStoreApiContext _context;
        public ProductsController (ShoesStoreApiContext context) {
            _context = context;
        }

        //GET /api/products 
        //[HttpGet ("GetProduct")]
        [HttpGet ()]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts () {
            return await _context.Products
                .Select (p => p.ToDTO ())
                .ToListAsync ();
        }
        [HttpGet()]
        [Route("GetProductsByCategory")]
        //GET : /api/products/GetProductsByCategory?category=Nike
        public IActionResult GetProductsByCategory(string category)
        {
            try
            {
                var product = _context.Products.Where(c => c.Category == category).ToList();

                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet()]
        [Route("GetProductsByPrice")]
        //GET : /api/products/GetProductsByPrice?price=
        public IActionResult GetProductsByPrice(decimal price)
        {
            try
            {
                var product = _context.Products.Where(c => Convert.ToDecimal(c.Price) <= price).ToList();

                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct (int id) {
            var product = await _context.Products.FindAsync (id);
            return product.ToDTO ();
        }

        [HttpPost]
        //201
        public ActionResult<ProductDTO> CreateProduct (ProductDTO productDTO) {
            _context.Products.Add (productDTO.ToProduct ());
            _context.SaveChanges ();
            return CreatedAtAction (nameof (GetProduct), new { id = productDTO.Id }, productDTO);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateProduct (int id, Product product) {
            if (id != product.Id) {
                return BadRequest ();
            }
            _context.Products.Update (product);
            try {
                await _context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                if (!ProductExists (id)) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            return NoContent ();
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteProduct (int id) {
            var product = await _context.Products.FindAsync (id);
            _context.Products.Remove (product);
            await _context.SaveChangesAsync ();

            return NoContent ();
        }

        private bool ProductExists (int id) {
            return _context.Products.Any (product => product.Id == id);
        }

    }
}