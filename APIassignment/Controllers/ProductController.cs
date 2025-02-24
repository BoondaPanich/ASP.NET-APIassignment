﻿
using APIassignment.DataAccess;
using APIassignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_workshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public ProductController(ApplicationContext context)
        {
            _context = context;
        }

        // indicate that this will be get api 
        // GET
        [HttpGet("Products")]

        // can return various status result
        public IActionResult Get()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // GET ONE
        [HttpGet("Product/{id}")]

        // can return various status result
        public IActionResult GetProduct([FromRoute] int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST
        [Authorize]
        [HttpPost("Products")]

        // can return various status result
        public IActionResult Save([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok();
        }

        // PUT
        [Authorize]
        [HttpPut("Products")]
        public IActionResult Update([FromBody] Product product)
        {
            // if no AsNoTracking we cannot saveChanges because the program is still tracking the old array 
            var result = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == product.Id);

            if (result == null)
            {
                return NotFound();
            }

            _context.Products.Update(product);
            _context.SaveChanges();

            return Ok();
        }

        // PUT
        [Authorize]
        [HttpDelete("Products")]
        public IActionResult Delete([FromQuery] int id)
        {
            //var deleteProduct = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            var deleteProduct = _context.Products.FirstOrDefault(x => x.Id == id);


            if (deleteProduct == null)
            {
                return NotFound();
            }

            //_context.Products.Remove(deleteProduct);
            _context.Products.Entry(deleteProduct).State = EntityState.Deleted;

            _context.SaveChanges();

            return Ok();
        }
    }
}
