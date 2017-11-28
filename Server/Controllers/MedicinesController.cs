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
    public class MedicinesController : ApiController
    {
        private MediCppEntities3 db = new MediCppEntities3();

        // GET: api/Medicines
        [HttpGet]
        public IHttpActionResult GetMedicines()
        {
            var lists = db.Medicine.ToList();

            if (lists.Count > 0)
                return Json(lists);
            else
                return Json("Empty database");


        }

        // GET: api/Medicines/5
        [ResponseType(typeof(Medicine)), HttpGet]
        public IHttpActionResult GetMedicine(int id)
        {
            Medicine medicine = db.Medicine.Find(id);
            if (medicine == null)
            {
                return Json("Empty database");
            }

            return Json(medicine);
        }

        // PUT: api/Medicines/5
        [ResponseType(typeof(void)), HttpPut]
        public IHttpActionResult PutMedicine(int id, Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicine.Id)
            {
                return BadRequest();
            }

            db.Entry(medicine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
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

        // POST: api/Medicines
        [ResponseType(typeof(Medicine)), HttpPost]
        public IHttpActionResult PostMedicine(Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Medicine.Add(medicine);
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

        // DELETE: api/Medicines/5
        [ResponseType(typeof(Medicine)), HttpDelete]
        public IHttpActionResult DeleteMedicine(int id)
        {
            Medicine medicine = db.Medicine.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }

            db.Medicine.Remove(medicine);
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

        private bool MedicineExists(int id)
        {
            return db.Medicine.Count(e => e.Id == id) > 0;
        }
    }
}