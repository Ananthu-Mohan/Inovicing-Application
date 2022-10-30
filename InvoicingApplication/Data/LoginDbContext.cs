using InvoicingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicingApplication.Data
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {
        }
        public DbSet<LoginModelClass> Users { get; set; }
    }
}
