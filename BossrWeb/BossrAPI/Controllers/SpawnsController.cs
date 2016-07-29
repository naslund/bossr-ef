using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BossrAPI.Models;
using BossrLib;

namespace BossrAPI.Controllers
{
    public class SpawnsController : ApiController
    {
        private readonly BossrDbContext db = new BossrDbContext();

        // GET: api/Spawns
        public IQueryable<Spawn> GetSpawn()
        {
            return db.Spawn.Include(x => x.World).Include(x => x.Creature);
        }

        // GET: api/Spawns/5
        [ResponseType(typeof(Spawn))]
        public async Task<IHttpActionResult> GetSpawn(int id)
        {
            var spawn = await db.Spawn.FindAsync(id);
            if (spawn == null)
            {
                return NotFound();
            }

            return Ok(spawn);
        }

        // PUT: api/Spawns/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSpawn(int id, Spawn spawn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spawn.Id)
            {
                return BadRequest();
            }

            db.Entry(spawn).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpawnExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Spawns
        [ResponseType(typeof(Spawn))]
        public async Task<IHttpActionResult> PostSpawn(Spawn spawn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Spawn.Add(spawn);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = spawn.Id}, spawn);
        }

        // DELETE: api/Spawns/5
        [ResponseType(typeof(Spawn))]
        public async Task<IHttpActionResult> DeleteSpawn(int id)
        {
            var spawn = await db.Spawn.FindAsync(id);
            if (spawn == null)
            {
                return NotFound();
            }

            db.Spawn.Remove(spawn);
            await db.SaveChangesAsync();

            return Ok(spawn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpawnExists(int id)
        {
            return db.Spawn.Count(e => e.Id == id) > 0;
        }
    }
}