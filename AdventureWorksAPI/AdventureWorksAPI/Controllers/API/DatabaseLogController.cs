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
    public class DatabaseLogController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/DatabaseLog
        public IQueryable<DatabaseLog> GetDatabaseLogs()
        {
            return db.DatabaseLogs;
        }

        // GET api/DatabaseLog/5
        [ResponseType(typeof(DatabaseLog))]
        public IHttpActionResult GetDatabaseLog(int id)
        {
            DatabaseLog databaselog = db.DatabaseLogs.Find(id);
            if (databaselog == null)
            {
                return NotFound();
            }

            return Ok(databaselog);
        }

        // PUT api/DatabaseLog/5
        public IHttpActionResult PutDatabaseLog(int id, DatabaseLog databaselog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != databaselog.DatabaseLogID)
            {
                return BadRequest();
            }

            db.Entry(databaselog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatabaseLogExists(id))
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

        // POST api/DatabaseLog
        [ResponseType(typeof(DatabaseLog))]
        public IHttpActionResult PostDatabaseLog(DatabaseLog databaselog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DatabaseLogs.Add(databaselog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = databaselog.DatabaseLogID }, databaselog);
        }

        // DELETE api/DatabaseLog/5
        [ResponseType(typeof(DatabaseLog))]
        public IHttpActionResult DeleteDatabaseLog(int id)
        {
            DatabaseLog databaselog = db.DatabaseLogs.Find(id);
            if (databaselog == null)
            {
                return NotFound();
            }

            db.DatabaseLogs.Remove(databaselog);
            db.SaveChanges();

            return Ok(databaselog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DatabaseLogExists(int id)
        {
            return db.DatabaseLogs.Count(e => e.DatabaseLogID == id) > 0;
        }
    }
}