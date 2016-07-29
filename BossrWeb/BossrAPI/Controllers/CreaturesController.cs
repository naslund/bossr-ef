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
    [Authorize]
    public class CreaturesController : ApiController
    {
        private readonly BossrDbContext db = new BossrDbContext();

        // GET: api/Creatures
        public IQueryable<Creature> GetCreatures()
        {
            return db.Creatures;
        }

        // GET: api/Creatures/5
        [ResponseType(typeof(Creature))]
        public async Task<IHttpActionResult> GetCreature(int id)
        {
            var creature = await db.Creatures.FindAsync(id);
            if (creature == null)
            {
                return NotFound();
            }

            return Ok(creature);
        }

        // PUT: api/Creatures/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCreature(int id, Creature creature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != creature.Id)
            {
                return BadRequest();
            }

            db.Entry(creature).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreatureExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Creatures
        [ResponseType(typeof(Creature))]
        public async Task<IHttpActionResult> PostCreature(Creature creature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Creatures.Add(creature);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = creature.Id}, creature);
        }

        // DELETE: api/Creatures/5
        [ResponseType(typeof(Creature))]
        public async Task<IHttpActionResult> DeleteCreature(int id)
        {
            var creature = await db.Creatures.FindAsync(id);
            if (creature == null)
            {
                return NotFound();
            }

            db.Creatures.Remove(creature);
            await db.SaveChangesAsync();

            return Ok(creature);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CreatureExists(int id)
        {
            return db.Creatures.Count(e => e.Id == id) > 0;
        }
    }
}