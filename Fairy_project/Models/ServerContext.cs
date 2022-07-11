using Microsoft.EntityFrameworkCore;

namespace Fairy_project.Models
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:wo7473.database.windows.net,1433;Initial Catalog=wooHouse;Persist Security Info=False;User ID=ispanwo@086@wo7473;Password=p@ssw0rd-iii;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Exhibition> exhibitions { get; set; }
        public DbSet<Manufactures> manufactures { get; set; }
        public DbSet<Member> members { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Booth> booths { get; set; }

    }
}
