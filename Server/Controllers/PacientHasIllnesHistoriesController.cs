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
        private MediCppEntities3 db = new MediCppEntities3();

        // GET: api/PacientHasIllnesHistories
        [HttpGet]
        public IHttpActionResult GetPacientHasIllnesHistories()
        {
            var lists = db.PacientHasIllnesHistory.ToList();

            if (lists.Count > 0)
                return Json(lists);
            else
                return Json("Empty database");
        }

        // GET: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(PacientHasIllnesHistory)), HttpGet]
        public IHttpActionResult GetPacientHasIllnesHistory(int id)
        {
            var illnesHistories = (from hist
                                   in db.PacientHasIllnesHistory
                                   where hist.idPacient == id
                                   select hist).ToList();



        
            if (illnesHistories == null)
            {
                return Json("Empty database");
            }

            return Json(illnesHistories);
        }

        // PUT: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(void)), HttpPut]
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
        [ResponseType(typeof(PacientHasIllnesHistory)), HttpPost]
        public IHttpActionResult PostPacientHasIllnesHistory(PacientHasIllnesHistory pacientHasIllnesHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            pacientHasIllnesHistory.Pacient = db.Pacient.Find(pacientHasIllnesHistory.idPacient);
            pacientHasIllnesHistory.IllnessHistory = db.IllnessHistory.Find(pacientHasIllnesHistory.idIllenssHistory);
            pacientHasIllnesHistory.VisitDate = DateTime.Now;

            db.PacientHasIllnesHistory.Add(pacientHasIllnesHistory);

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

        // DELETE: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(PacientHasIllnesHistory)), HttpDelete]
        public IHttpActionResult DeletePacientHasIllnesHistory(int id)
        {
            PacientHasIllnesHistory pacientHasIllnesHistory = db.PacientHasIllnesHistory.Find(id);
            if (pacientHasIllnesHistory == null)
            {
                return NotFound();
            }

            db.PacientHasIllnesHistory.Remove(pacientHasIllnesHistory);
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

        private bool PacientHasIllnesHistoryExists(int id)
        {
            return db.PacientHasIllnesHistory.Count(e => e.Id == id) > 0;
        }
    }
}