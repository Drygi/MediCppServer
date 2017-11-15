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
        private MediCppEntities2 db = new MediCppEntities2();

        // GET: api/PacientHasIllnesHistories
        [HttpGet]
        public IHttpActionResult GetPacientHasIllnesHistories()
        {
            return Json(db.PacientHasIllnesHistory.ToList());
        }

        // GET: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(PacientHasIllnesHistory)),HttpGet]
        public IHttpActionResult GetPacientHasIllnesHistory(int id)
        {
            PacientHasIllnesHistory pacientHasIllnesHistory = db.PacientHasIllnesHistory.Find(id);
            if (pacientHasIllnesHistory == null)
            {
                return NotFound();
            }

            return Json(pacientHasIllnesHistory);
        }

        // PUT: api/PacientHasIllnesHistories/5
        [ResponseType(typeof(void)),HttpPut]
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
        [ResponseType(typeof(PacientHasIllnesHistory)),HttpPost]
        public IHttpActionResult PostPacientHasIllnesHistory(PacientHasIllnesHistory pacientHasIllnesHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            pacientHasIllnesHistory = new PacientHasIllnesHistory();

            pacientHasIllnesHistory.idIllnessHistory = 1;
            pacientHasIllnesHistory.idPacient = 1;

            pacientHasIllnesHistory.Pacient = db.Pacient.Find(pacientHasIllnesHistory.idPacient);
            pacientHasIllnesHistory.IllnessHistory = db.IllnessHistory.Find(pacientHasIllnesHistory.idIllnessHistory);

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
            return Ok();
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

        private bool PacientHasIllnesHistoryExists(int id)
        {
            return db.PacientHasIllnesHistory.Count(e => e.Id == id) > 0;
        }
    }
}