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
        private MediCppEntities3 db = new MediCppEntities3();

        // GET: api/IllnessHistories
        [HttpGet]
        public IHttpActionResult GetIllnessHistories()
        {
            var lists = db.IllnessHistory.ToList();
            if (lists.Count > 0)
                return Json(lists);
            else
                return Json("Empty database");
        }

        // GET: api/IllnessHistories/5
        [ResponseType(typeof(IllnessHistory)), HttpGet]
        public IHttpActionResult GetIllnessHistory(int id)
        {
            IllnessHistory illnessHistory = db.IllnessHistory.Find(id);
            if (illnessHistory == null)
            {
                return NotFound();
            }

            return Json(illnessHistory);
        }

        // PUT: api/IllnessHistories/5
        [ResponseType(typeof(void)), HttpPut]
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
        [ResponseType(typeof(IllnessHistory)), HttpPost]
        public IHttpActionResult PostIllnessHistory(IllnessHistory illnessHistory)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IllnessHistory.Add(illnessHistory);
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

        // DELETE: api/IllnessHistories/5
        [ResponseType(typeof(IllnessHistory)), HttpDelete]
        public IHttpActionResult DeleteIllnessHistory(int id)
        {
            IllnessHistory illnessHistory = db.IllnessHistory.Find(id);
            if (illnessHistory == null)
            {
                return NotFound();
            }

            db.IllnessHistory.Remove(illnessHistory);
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

        private bool IllnessHistoryExists(int id)
        {
            return db.IllnessHistory.Count(e => e.Id == id) > 0;
        }
    }
}