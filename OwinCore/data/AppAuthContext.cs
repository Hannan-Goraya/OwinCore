using Microsoft.EntityFrameworkCore;
using OwinCore.Models;

namespace OwinCore.data
{
    public class AppAuthContext : DbContext
    {
        public AppAuthContext(DbContextOptions<AppAuthContext> options) : base(options)
        {

        }

      public  DbSet<AppUsers> AppUsers { get; set; }

    }
}
