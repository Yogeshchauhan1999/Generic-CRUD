using GenericOps.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace GenericOps.Context
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }

        
        public DbSet<Product> tblProduct {  get; set; }
        public DbSet<Category> tblCategory {  get; set; }
    }
}
