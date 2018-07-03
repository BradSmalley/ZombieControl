using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZombieControl.ModelBinders;
using ZombieController.Data;
using ZombieController.Model;

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

 
        // GET: api/Zombies
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

        // GET: api/Zombies/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(Guid id)
        {
            return JsonConvert.SerializeObject(db.Zombies.Find(id));
        }
        
        // POST: api/Zombies
        [HttpPost]
        public IActionResult Post()
        {
            var r = Request;
            using (var reader = new StreamReader(r.Body))
            {
                var sZombie = reader.ReadToEnd();
                var s = new JsonSerializerSettings();
                
                var zombie = JsonConvert.DeserializeObject<Zombie>(sZombie);
                zombie.ZombieId = Guid.NewGuid();

                db.Zombies.Add(zombie);
                db.SaveChanges();

                return CreatedAtRoute("Get", new { id = zombie.ZombieId }, zombie);
            }

        }
        
        // PUT: api/Zombies/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id)
        {
            var r = Request;
            using (var reader = new StreamReader(r.Body))
            {
                var sZombie = reader.ReadToEnd();
                var s = new JsonSerializerSettings();

                var zombie = JsonConvert.DeserializeObject<Zombie>(sZombie);

                db.Zombies.Attach(zombie);
                db.Entry<Zombie>(zombie).State = EntityState.Modified;
                db.SaveChanges();

                return CreatedAtRoute("Get", new { id = zombie.ZombieId }, zombie);
            }

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {

            db.Zombies.Remove(db.Zombies.Find(id));
            db.SaveChanges();

        }
    }


}
