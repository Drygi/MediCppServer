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
        private MediCppEntities3 db = new MediCppEntities3();

        // GET: api/Pacients
        [HttpGet]
        public IHttpActionResult GetPacients()
        {
            var lists = db.Pacient.ToList();

            if (lists.Count > 0)
                return Json(lists);
            else
                return Json("Empty database");
        }


        // GET: api/Pacients/5
        [ResponseType(typeof(Pacient)), HttpGet]
        public IHttpActionResult GetPacient(int id)
        {
            Doctor doctor = db.Doctor.Find(id);
            var pacients = doctor.Pacient.ToList();
            if (pacients == null)
                return Json("Empty database");

            return Json(pacients);
        }

        // PUT: api/Pacients/5
        [ResponseType(typeof(void)), HttpPut]
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
        [ResponseType(typeof(Pacient)), HttpPost]
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
            return Json("200OK");
        }

        // DELETE: api/Pacients/5
        [ResponseType(typeof(Pacient)), HttpDelete]
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
            return Json("200OK");
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