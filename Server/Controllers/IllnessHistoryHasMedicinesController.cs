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
        private MediCppEntities3 db = new MediCppEntities3();

        // GET: api/IllnessHistoryHasMedicines
        [HttpGet]
        public IHttpActionResult GetIllnessHistoryHasMedicines()
        {
            var lists = db.IllnessHistoryHasMedicines.ToList();

            if (lists.Count > 0)
                return Json(lists);
            else
                return Json("Empty database");
        }

        // GET: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(IllnessHistoryHasMedicines)), HttpGet]
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
        [ResponseType(typeof(void)), HttpPut]
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
        [ResponseType(typeof(IllnessHistoryHasMedicines)), HttpPost]
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
            return Json("200OK");
        }

        // DELETE: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(IllnessHistoryHasMedicines)), HttpDelete]
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

        private bool IllnessHistoryHasMedicineExists(int id)
        {
            return db.IllnessHistoryHasMedicines.Count(e => e.Id == id) > 0;
        }
    }
}