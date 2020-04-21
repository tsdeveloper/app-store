using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AcessoIngressoContext : DbContext
    {
        public AcessoIngressoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}