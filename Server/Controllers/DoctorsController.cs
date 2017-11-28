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
    public class DoctorsController : ApiController
    {
        private MediCppEntities3 db = new MediCppEntities3();

        // GET: api/Doctors
        [HttpGet]
        public IHttpActionResult GetDoctors()
        {
            if (db.Doctor.Count() < 1)
                return Json("Empty database");
            else
                return Json(db.Doctor.ToList());
        }

        // GET: api/Doctors/5
        [ResponseType(typeof(Doctor)), HttpGet]
        public IHttpActionResult GetDoctor(int id)
        {
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return Json(doctor);
        }

        // PUT: api/Doctors/5
        [ResponseType(typeof(void)), HttpPut]
        public IHttpActionResult PutDoctor(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctor.Id)
            {
                return BadRequest();
            }

            db.Entry(doctor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        [ResponseType(typeof(Doctor)), HttpPost]
        public IHttpActionResult PostDoctor(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Doctor.Add(doctor);
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

        // DELETE: api/Doctors/5
        [ResponseType(typeof(Doctor)), HttpDelete]
        public IHttpActionResult DeleteDoctor(int id)
        {
            //usunac pacjentow od doktora   
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ///usuwanie doktorow nie dziala, ponieważ ma on przypisanych pacjentow.
            db.Doctor.Remove(doctor);
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

        private bool DoctorExists(int id)
        {
            return db.Doctor.Count(e => e.Id == id) > 0;
        }
    }
}