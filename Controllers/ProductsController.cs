using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        [Route("GetProductsProcessed")]
        //GET : /api/products/GetProductsProcessed
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsProcessed()
        {
            return await _context.Products
                .Where(p => p.Status == "processed")
                .Select(p => p.ToDTO())
                .ToListAsync();
        }

        [HttpGet()]
        [Route("GetProductsPending")]
        //GET : /api/products/GetProductsPending
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsPending()
        {
            return await _context.Products
                .Where(p => p.Status == "pending")
                .Select(p => p.ToDTO())
                .ToListAsync();
        }
        [HttpGet()]
        [Route("GetProductsNew")]
        //GET : /api/products/GetProductsNew
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsNew()
        {
            return await _context.Products
                .OrderByDescending(p => p.Id)
                .Where(p=> p.Status == "processed")
                .Take(12)
                .Select( p => p.ToDTO())
                .ToListAsync();
        }
        [HttpGet()]
        [Route("GetProductsByCategory")]
        //GET : /api/products/GetProductsByCategory?category=Nike
        public IActionResult GetProductsByCategory(string category)
        {
            try
            {
                var product = _context.Products.Where(c => c.Category == category && c.Status == "processed").ToList();

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
                var product = _context.Products.Where(c => Convert.ToDecimal(c.Price) <= price && c.Status == "processed" ).ToList();

                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet()]
        [Route("GetProductsSearch")]
        //GET : /api/products/GetProductsByPrice?price=
        public IActionResult GetProductsSearch(decimal price, string category)
        {
            try
            {
                var product = _context.Products.Where(c => Convert.ToDecimal(c.Price) <= price && c.Category == category && c.Status == "processed").ToList();

                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet()]
        [Route("GetProductsByName")]
        //GET : /api/products/GetProductsByName?name=Nike
        public IActionResult GetProductsByName(string name)
        {
            try
            {
                var product = _context.Products.Where(c => c.Name.Contains(name) && c.Status == "processed").ToList();

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
        [HttpPost]
        [Route("UpdateAmount")]
        //POST : /api/products/UpdateAmount
        public async Task<IActionResult> UpdateAmount(int id, int amount)
        {
            var product = await _context.Products.FindAsync(id);
            product.Amount = amount;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("UpdateStatusProcessed")]
        //POST : /api/products/UpdateStatusProcessed
        public async Task<IActionResult> UpdateStatusProcessed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product.Status == "pending")
            {
                product.Status = "processed";
            }
            else
            {
                return BadRequest(new { message = "Phản hồi đã được xử lý." });
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return NoContent();
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
        [HttpPost, DisableRequestSizeLimit]
        [Route("Upload")]
        //POST : /api/products/Upload
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}