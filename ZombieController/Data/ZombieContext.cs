using Microsoft.EntityFrameworkCore;
using ZombieControl.Model;

namespace ZombieControl.Data
{
    public partial class ZombieContext : DbContext
    {

        public ZombieContext(DbContextOptions<ZombieContext> options) : base(options)
        {

        }

        public virtual DbSet<Zombie> Zombies { get; set; }

        public virtual DbSet<Command> Commands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ZombieController;Trusted_Connection=true");
        }

    }
}
