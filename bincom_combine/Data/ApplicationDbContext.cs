using Microsoft.EntityFrameworkCore;
using bincom_conbine.Models;
using bincom_combine.Models;

namespace bincom_conbine.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Picture> Pictures { get; set; }

       
        public DbSet<ContactUs> ContactUsEntries { get; set; }
    }
}
