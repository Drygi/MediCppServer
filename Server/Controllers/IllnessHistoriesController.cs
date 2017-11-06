using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Server;

namespace Server.Controllers
{
    public class IllnessHistoriesController : ApiController
    {
        private MediCppEntities1 db = new MediCppEntities1();

        // GET: api/IllnessHistories
        public IQueryable<IllnessHistory> GetIllnessHistories()
        {
            return db.IllnessHistories;
        }

        // GET: api/IllnessHistories/5
        [ResponseType(typeof(IllnessHistory))]
        public IHttpActionResult GetIllnessHistory(int id)
        {
            IllnessHistory illnessHistory = db.IllnessHistories.Find(id);
            if (illnessHistory == null)
            {
                return NotFound();
            }

            return Ok(illnessHistory);
        }

        // PUT: api/IllnessHistories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIllnessHistory(int id, IllnessHistory illnessHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != illnessHistory.Id)
            {
                return BadRequest();
            }

            db.Entry(illnessHistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IllnessHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IllnessHistories
        [ResponseType(typeof(IllnessHistory))]
        public IHttpActionResult PostIllnessHistory(IllnessHistory illnessHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IllnessHistories.Add(illnessHistory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = illnessHistory.Id }, illnessHistory);
        }

        // DELETE: api/IllnessHistories/5
        [ResponseType(typeof(IllnessHistory))]
        public IHttpActionResult DeleteIllnessHistory(int id)
        {
            IllnessHistory illnessHistory = db.IllnessHistories.Find(id);
            if (illnessHistory == null)
            {
                return NotFound();
            }

            db.IllnessHistories.Remove(illnessHistory);
            db.SaveChanges();

            return Ok(illnessHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IllnessHistoryExists(int id)
        {
            return db.IllnessHistories.Count(e => e.Id == id) > 0;
        }
    }
}