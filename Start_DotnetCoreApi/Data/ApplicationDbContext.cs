using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base (options)
        {
            
        }

        public DbSet<User> Users {get;set;}
        public DbSet<Role> Roles {get;set;}
        public DbSet<Product> Products {get;set;}
        public DbSet<Category> Categories {get;set;}
    }

}