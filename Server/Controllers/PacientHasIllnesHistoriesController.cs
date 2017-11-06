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
    public class PacientHasIllnesHistoriesController : ApiController
    {
        private MediCppEntities1 db = new MediCppEntities1();

        // GET: api/PacientHasIllnesHistories
        public IQueryable<PacientHasIllnesHistory> GetPacientHasIllnesHistories()
        {
            return db.PacientHasIllnesHistories;
        }

        // GET: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(PacientHasIllnesHistory))]
        public IHttpActionResult GetPacientHasIllnesHistory(int id)
        {
            PacientHasIllnesHistory pacientHasIllnesHistory = db.PacientHasIllnesHistories.Find(id);
            if (pacientHasIllnesHistory == null)
            {
                return NotFound();
            }

            return Ok(pacientHasIllnesHistory);
        }

        // PUT: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPacientHasIllnesHistory(int id, PacientHasIllnesHistory pacientHasIllnesHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pacientHasIllnesHistory.Id)
            {
                return BadRequest();
            }

            db.Entry(pacientHasIllnesHistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacientHasIllnesHistoryExists(id))
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

        // POST: api/PacientHasIllnesHistories
        [ResponseType(typeof(PacientHasIllnesHistory))]
        public IHttpActionResult PostPacientHasIllnesHistory(PacientHasIllnesHistory pacientHasIllnesHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PacientHasIllnesHistories.Add(pacientHasIllnesHistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PacientHasIllnesHistoryExists(pacientHasIllnesHistory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pacientHasIllnesHistory.Id }, pacientHasIllnesHistory);
        }

        // DELETE: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(PacientHasIllnesHistory))]
        public IHttpActionResult DeletePacientHasIllnesHistory(int id)
        {
            PacientHasIllnesHistory pacientHasIllnesHistory = db.PacientHasIllnesHistories.Find(id);
            if (pacientHasIllnesHistory == null)
            {
                return NotFound();
            }

            db.PacientHasIllnesHistories.Remove(pacientHasIllnesHistory);
            db.SaveChanges();

            return Ok(pacientHasIllnesHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PacientHasIllnesHistoryExists(int id)
        {
            return db.PacientHasIllnesHistories.Count(e => e.Id == id) > 0;
        }
    }
}