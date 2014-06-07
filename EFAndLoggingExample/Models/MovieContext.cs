using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;

namespace EFAndLoggingExample.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("MoviesConnectionString")
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
    }
}