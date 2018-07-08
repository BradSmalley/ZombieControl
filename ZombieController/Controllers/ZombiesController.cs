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
    public class ZombiesController : Controller
    {
        private readonly ZombieContext db;

        public ZombiesController(ZombieContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var result = new List<string>();
            foreach(var z in db.Zombies.ToList())
            {
                result.Add(JsonConvert.SerializeObject(z));
            }
            return result;
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(Guid id)
        {
            return JsonConvert.SerializeObject(db.Zombies.Find(id));
        }
        
        [HttpPost]
        public IActionResult Post()
        {
            var r = Request;
            using (var reader = new StreamReader(r.Body))
            {
                var sZombie = reader.ReadToEnd();
                var zombie = JsonConvert.DeserializeObject<Zombie>(sZombie);
                zombie.ZombieId = Guid.NewGuid();
                db.Zombies.Add(zombie);
                db.SaveChanges();
                return CreatedAtRoute("Get", new { id = zombie.ZombieId }, zombie);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id)
        {
            var r = Request;
            using (var reader = new StreamReader(r.Body))
            {
                var sZombie = reader.ReadToEnd();
                var zombie = JsonConvert.DeserializeObject<Zombie>(sZombie);
                db.Zombies.Attach(zombie);
                db.Entry<Zombie>(zombie).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(zombie);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            db.Zombies.Remove(db.Zombies.Find(id));
            db.SaveChanges();
            return NoContent();
        }
    }
}
