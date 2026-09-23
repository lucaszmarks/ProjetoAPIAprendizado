using Microsoft.EntityFrameworkCore;
using ProjetoAPIAprendizado.Models;

namespace ProjetoAPIAprendizado.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Image> Images { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
    }
}
