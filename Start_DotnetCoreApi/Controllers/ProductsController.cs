using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreApi.Data;
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
            var productResult = (from product in this._dbContext.Products.Include(p => p.Category)
                where product.ProductID == id
                select new
                {
                    Id = product.ProductID,
                    Name = product.ProductName,
                    Category = product.Category.CategoryName
                })
                .FirstOrDefaultAsync();
            
            if (productResult is null)
                return NotFound();
                
            return Ok(productResult);
        }

    }
}