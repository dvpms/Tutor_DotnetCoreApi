using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreApi.Data;
using DotnetCoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Start_DotnetCoreApi.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _Config;
        private readonly ApplicationDbContext _dbContext;

        public ProductsController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            this._Config = configuration;
            this._dbContext = dbContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAllAsync([FromRoute] int id)
        {
            var productResult = await (from product in this._dbContext.Products.Include(p => p.Category)
                where product.ProductID == id
                select new
                {
                    id = product.ProductID,
                    name = product.ProductName,
                    category = product.Category.CategoryName
                })
                .FirstOrDefaultAsync();
            
            if (productResult is null)
                return NotFound();

            return Ok(productResult);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("getbycategory")]
        public async Task<IActionResult> GetbyCategory([FromQuery] string categoryName)
        {
            var productsResult = await (from product in this._dbContext.Products.Include(p=>p.Category)
                where product.Category.CategoryName.Contains(categoryName)
                select new
                {
                    id = product.ProductID,
                    name = product.ProductName,
                    category = product.Category.CategoryName
                })
                .ToListAsync();

            if (productsResult.Count <= 0)
                return NotFound();

            return Ok(productsResult);
        }


    }
}
