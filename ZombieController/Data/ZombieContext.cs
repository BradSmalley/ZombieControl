using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZombieController.Model;

namespace ZombieController.Data
{
    public partial class ZombieContext : DbContext
    {

        public ZombieContext(DbContextOptions<ZombieContext> options) : base(options)
        {

        }

        public virtual DbSet<Zombie> Zombies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ZombieController;Trusted_Connection=true");
        }

    }
}
