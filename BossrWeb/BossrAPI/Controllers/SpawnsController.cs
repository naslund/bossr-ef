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
        public async Task<IHttpActionResult> GetSpawns()
        {
            return Ok(await db.Spawns/*.Include(x => x.World).Include(x => x.Creature.Category)*/.ToListAsync());
        }

        // GET: api/worlds/5/spawns
        [HttpGet]
        [Route("api/worlds/{worldid}/spawns")]
        public async Task<IHttpActionResult> GetSpawnsByWorld(int worldid)
        {
            if (db.Worlds.Any(x => x.Id == worldid) == false)
                return NotFound();

            return Ok(await db.Spawns.Where(x => x.WorldId == worldid).ToListAsync());
        }

        // GET: api/creatures/5/spawns
        [HttpGet]
        [Route("api/creatures/{creatureid}/spawns")]
        public async Task<IHttpActionResult> GetSpawnsByCreature(int creatureid)
        {
            if (db.Creatures.Any(x => x.Id == creatureid) == false)
                return NotFound();

            return Ok(await db.Spawns.Where(x => x.CreatureId == creatureid).ToListAsync());
        }

        // GET: api/Spawns/5
        [ResponseType(typeof(Spawn))]
        public async Task<IHttpActionResult> GetSpawn(int id)
        {
            var spawn = await db.Spawns.FindAsync(id);
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

            db.Spawns.Add(spawn);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = spawn.Id}, spawn);
        }

        // DELETE: api/Spawns/5
        [ResponseType(typeof(Spawn))]
        public async Task<IHttpActionResult> DeleteSpawn(int id)
        {
            var spawn = await db.Spawns.FindAsync(id);
            if (spawn == null)
            {
                return NotFound();
            }

            db.Spawns.Remove(spawn);
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
            return db.Spawns.Count(e => e.Id == id) > 0;
        }
    }
}