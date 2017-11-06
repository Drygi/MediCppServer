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
    public class PacientsController : ApiController
    {
        private MediCppEntities1 db = new MediCppEntities1();

        // GET: api/Pacients
        public IQueryable<Pacient> GetPacients()
        {
            return db.Pacients;
        }

        // GET: api/Pacients/5
        [ResponseType(typeof(Pacient))]
        public IHttpActionResult GetPacient(int id)
        {
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return NotFound();
            }

            return Ok(pacient);
        }

        // PUT: api/Pacients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPacient(int id, Pacient pacient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pacient.Id)
            {
                return BadRequest();
            }

            db.Entry(pacient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacientExists(id))
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

        // POST: api/Pacients
        [ResponseType(typeof(Pacient))]
        public IHttpActionResult PostPacient(Pacient pacient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pacients.Add(pacient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pacient.Id }, pacient);
        }

        // DELETE: api/Pacients/5
        [ResponseType(typeof(Pacient))]
        public IHttpActionResult DeletePacient(int id)
        {
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return NotFound();
            }

            db.Pacients.Remove(pacient);
            db.SaveChanges();

            return Ok(pacient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PacientExists(int id)
        {
            return db.Pacients.Count(e => e.Id == id) > 0;
        }
    }
}