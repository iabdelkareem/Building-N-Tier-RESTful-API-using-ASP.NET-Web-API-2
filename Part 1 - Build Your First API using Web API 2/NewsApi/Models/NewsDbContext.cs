using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewsApi.Models
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext()
        {
            Database.SetInitializer(new NewsDbInitializer());
        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Article> Articles { get; set;}
        
    }
}