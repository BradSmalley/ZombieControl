using System;
using System.ComponentModel.DataAnnotations;

namespace ZombieController.Model
{
    public partial class Zombie
    {

        [Key]
        public Guid ZombieId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastReportedIPAddress { get; set; }

    }
}
