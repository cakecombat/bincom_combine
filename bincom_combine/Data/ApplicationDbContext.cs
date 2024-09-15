using Microsoft.EntityFrameworkCore;
using bincom_conbine.Models;

namespace bincom_conbine.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Picture> Pictures { get; set; }
    }
}
