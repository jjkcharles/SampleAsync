using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<TestTable> OnlyTable { get; set; }
    }
}
