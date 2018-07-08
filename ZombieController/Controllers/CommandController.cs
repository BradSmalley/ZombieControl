using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZombieControl.Data;
using ZombieControl.Model;

namespace ZombieControl.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CommandController : Controller
    {
        private readonly ZombieContext db;

        public CommandController(ZombieContext db)
        {
            this.db = db;
        }

        [HttpGet("{zombieId}")]
        public string Get(Guid zombieId)
        {
            var zombieSpecific = db.Commands.Where(c => c.ZombieId.Equals(zombieId)).ToList();
            var general = db.Commands.Where(c => c.ZombieId == null).ToList();
            var combinedList = new List<Command>();
            combinedList.AddRange(zombieSpecific);
            combinedList.AddRange(general);
            return JsonConvert.SerializeObject(combinedList);
        }
        
        [HttpPost]
        public IActionResult Post()
        {
            var r = Request;
            using (var reader = new StreamReader(r.Body))
            {
                var sCommand = reader.ReadToEnd();
                var command = JsonConvert.DeserializeObject<Command>(sCommand);
                command.CommandId = Guid.NewGuid();
                db.Commands.Add(command);
                db.SaveChanges();
                return CreatedAtRoute("Get", new { id = command.CommandId }, command);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id)
        {
            var r = Request;
            using (var reader = new StreamReader(r.Body))
            {
                var sCommand = reader.ReadToEnd();
                var command = JsonConvert.DeserializeObject<Command>(sCommand);
                db.Commands.Attach(command);
                db.Entry<Command>(command).State = EntityState.Modified;
                db.SaveChanges();
                return CreatedAtRoute("Get", new { id = command.CommandId }, command);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            db.Commands.Remove(db.Commands.Find(id));
            db.SaveChanges();
            return NoContent();
        }
    }
}
