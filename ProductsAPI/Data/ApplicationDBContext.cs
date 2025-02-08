using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models.Domain;

namespace ProductsAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public virtual DbSet<Products> Products { get; set; }
    }
}
