using System;
using System.ComponentModel.DataAnnotations;

namespace ZombieControl.Model
{
    public class Command
    {
        private DateTime _runBefore;
        private DateTime _runAfter;

        public Command()
        {
            this._runBefore = DateTime.Now.AddDays(7);
            this._runAfter = DateTime.Now;
        }

        [Key]
        public Guid CommandId { get; set; }

        public string CommandText { get; set; }

        public Guid? ZombieId { get; set; }

        public DateTime RunAfter
        {
            get { return _runAfter; }
            set { _runAfter = value; }
        }

        public DateTime RunBefore
        {
            get { return _runBefore; }
            set { _runBefore = value; }
        }

        public virtual Zombie Zombie { get; set; }
    }
}
