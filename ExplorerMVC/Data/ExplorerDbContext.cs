using ExplorerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ExplorerMVC.Data
{
    public class ExplorerDbContext : DbContext
    {
        public ExplorerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Folder> Folders { get; set; }
    }
}
