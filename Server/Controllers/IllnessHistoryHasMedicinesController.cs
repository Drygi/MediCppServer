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
        private MediCppEntities1 db = new MediCppEntities1();

        // GET: api/IllnessHistoryHasMedicines
        public IQueryable<IllnessHistoryHasMedicine> GetIllnessHistoryHasMedicines()
        {
            return db.IllnessHistoryHasMedicines;
        }

        // GET: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(IllnessHistoryHasMedicine))]
        public IHttpActionResult GetIllnessHistoryHasMedicine(int id)
        {
            IllnessHistoryHasMedicine illnessHistoryHasMedicine = db.IllnessHistoryHasMedicines.Find(id);
            if (illnessHistoryHasMedicine == null)
            {
                return NotFound();
            }

            return Ok(illnessHistoryHasMedicine);
        }

        // PUT: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIllnessHistoryHasMedicine(int id, IllnessHistoryHasMedicine illnessHistoryHasMedicine)
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
        [ResponseType(typeof(IllnessHistoryHasMedicine))]
        public IHttpActionResult PostIllnessHistoryHasMedicine(IllnessHistoryHasMedicine illnessHistoryHasMedicine)
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
            catch (DbUpdateException)
            {
                if (IllnessHistoryHasMedicineExists(illnessHistoryHasMedicine.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = illnessHistoryHasMedicine.Id }, illnessHistoryHasMedicine);
        }

        // DELETE: api/IllnessHistoryHasMedicines/5
        [ResponseType(typeof(IllnessHistoryHasMedicine))]
        public IHttpActionResult DeleteIllnessHistoryHasMedicine(int id)
        {
            IllnessHistoryHasMedicine illnessHistoryHasMedicine = db.IllnessHistoryHasMedicines.Find(id);
            if (illnessHistoryHasMedicine == null)
            {
                return NotFound();
            }

            db.IllnessHistoryHasMedicines.Remove(illnessHistoryHasMedicine);
            db.SaveChanges();

            return Ok(illnessHistoryHasMedicine);
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