using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Models;

namespace WebApi.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions options)
             : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
