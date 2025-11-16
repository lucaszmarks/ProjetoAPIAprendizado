using Microsoft.EntityFrameworkCore;
using ProjetoAPIAprendizado.Models;

namespace ProjetoAPIAprendizado
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }


        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
    }
}
