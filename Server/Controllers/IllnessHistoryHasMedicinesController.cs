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
    public class IllnessHistoryHasMedicinesController : ApiController
    {
        private MediCppEntities2 db = new MediCppEntities2();

        // GET: api/IllnessHistoryHasMedicines
        public IHttpActionResult GetIllnessHistoryHasMedicines()
        {
            return Json(db.IllnessHistoryHasMedicines.ToList());
        }

        // GET: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(IllnessHistoryHasMedicines))]
        public IHttpActionResult GetIllnessHistoryHasMedicine(int id)
        {
            IllnessHistoryHasMedicines illnessHistoryHasMedicine = db.IllnessHistoryHasMedicines.Find(id);
            if (illnessHistoryHasMedicine == null)
            {
                return NotFound();
            }

            return Json(illnessHistoryHasMedicine);
        }

        // PUT: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIllnessHistoryHasMedicine(int id, IllnessHistoryHasMedicines illnessHistoryHasMedicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != illnessHistoryHasMedicine.Id)
            {
                return BadRequest();
            }

            db.Entry(illnessHistoryHasMedicine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IllnessHistoryHasMedicineExists(id))
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

        // POST: api/IllnessHistoryHasMedicines
        [ResponseType(typeof(IllnessHistoryHasMedicines))]
        public IHttpActionResult PostIllnessHistoryHasMedicine(IllnessHistoryHasMedicines illnessHistoryHasMedicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IllnessHistoryHasMedicines.Add(illnessHistoryHasMedicine);

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

        // DELETE: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(IllnessHistoryHasMedicines))]
        public IHttpActionResult DeleteIllnessHistoryHasMedicine(int id)
        {
            IllnessHistoryHasMedicines illnessHistoryHasMedicine = db.IllnessHistoryHasMedicines.Find(id);
            if (illnessHistoryHasMedicine == null)
            {
                return NotFound();
            }

            db.IllnessHistoryHasMedicines.Remove(illnessHistoryHasMedicine);
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

        private bool IllnessHistoryHasMedicineExists(int id)
        {
            return db.IllnessHistoryHasMedicines.Count(e => e.Id == id) > 0;
        }
    }
}