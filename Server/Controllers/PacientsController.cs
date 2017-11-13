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
        private MediCppEntities2 db = new MediCppEntities2();

        // GET: api/Pacients
        public IHttpActionResult GetPacients()
        {
            return Json(db.Pacient.ToList());
        }

        // GET: api/Pacients/5
        [ResponseType(typeof(Pacient))]
        public IHttpActionResult GetPacient(int id)
        {
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return NotFound();
            }

            return Json(pacient);
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

            db.Pacient.Add(pacient);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return Ok();
        }

        // DELETE: api/Pacients/5
        [ResponseType(typeof(Pacient))]
        public IHttpActionResult DeletePacient(int id)
        {
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return NotFound();
            }

            db.Pacient.Remove(pacient);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return Ok();
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
            return db.Pacient.Count(e => e.Id == id) > 0;
        }
    }
}