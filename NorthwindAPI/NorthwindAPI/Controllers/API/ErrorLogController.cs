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
using AdventureWorksAPI.DBModels;

namespace AdventureWorksAPI.Controllers.API
{
    public class ErrorLogController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ErrorLog
        public IQueryable<ErrorLog> GetErrorLogs()
        {
            return db.ErrorLogs;
        }

        // GET api/ErrorLog/5
        [ResponseType(typeof(ErrorLog))]
        public IHttpActionResult GetErrorLog(int id)
        {
            ErrorLog errorlog = db.ErrorLogs.Find(id);
            if (errorlog == null)
            {
                return NotFound();
            }

            return Ok(errorlog);
        }

        // PUT api/ErrorLog/5
        public IHttpActionResult PutErrorLog(int id, ErrorLog errorlog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != errorlog.ErrorLogID)
            {
                return BadRequest();
            }

            db.Entry(errorlog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorLogExists(id))
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

        // POST api/ErrorLog
        [ResponseType(typeof(ErrorLog))]
        public IHttpActionResult PostErrorLog(ErrorLog errorlog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ErrorLogs.Add(errorlog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = errorlog.ErrorLogID }, errorlog);
        }

        // DELETE api/ErrorLog/5
        [ResponseType(typeof(ErrorLog))]
        public IHttpActionResult DeleteErrorLog(int id)
        {
            ErrorLog errorlog = db.ErrorLogs.Find(id);
            if (errorlog == null)
            {
                return NotFound();
            }

            db.ErrorLogs.Remove(errorlog);
            db.SaveChanges();

            return Ok(errorlog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ErrorLogExists(int id)
        {
            return db.ErrorLogs.Count(e => e.ErrorLogID == id) > 0;
        }
    }
}