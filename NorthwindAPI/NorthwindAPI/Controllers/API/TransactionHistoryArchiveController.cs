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
    public class TransactionHistoryArchiveController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/TransactionHistoryArchive
        public IQueryable<TransactionHistoryArchive> GetTransactionHistoryArchives()
        {
            return db.TransactionHistoryArchives;
        }

        // GET api/TransactionHistoryArchive/5
        [ResponseType(typeof(TransactionHistoryArchive))]
        public IHttpActionResult GetTransactionHistoryArchive(int id)
        {
            TransactionHistoryArchive transactionhistoryarchive = db.TransactionHistoryArchives.Find(id);
            if (transactionhistoryarchive == null)
            {
                return NotFound();
            }

            return Ok(transactionhistoryarchive);
        }

        // PUT api/TransactionHistoryArchive/5
        public IHttpActionResult PutTransactionHistoryArchive(int id, TransactionHistoryArchive transactionhistoryarchive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transactionhistoryarchive.TransactionID)
            {
                return BadRequest();
            }

            db.Entry(transactionhistoryarchive).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionHistoryArchiveExists(id))
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

        // POST api/TransactionHistoryArchive
        [ResponseType(typeof(TransactionHistoryArchive))]
        public IHttpActionResult PostTransactionHistoryArchive(TransactionHistoryArchive transactionhistoryarchive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransactionHistoryArchives.Add(transactionhistoryarchive);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TransactionHistoryArchiveExists(transactionhistoryarchive.TransactionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = transactionhistoryarchive.TransactionID }, transactionhistoryarchive);
        }

        // DELETE api/TransactionHistoryArchive/5
        [ResponseType(typeof(TransactionHistoryArchive))]
        public IHttpActionResult DeleteTransactionHistoryArchive(int id)
        {
            TransactionHistoryArchive transactionhistoryarchive = db.TransactionHistoryArchives.Find(id);
            if (transactionhistoryarchive == null)
            {
                return NotFound();
            }

            db.TransactionHistoryArchives.Remove(transactionhistoryarchive);
            db.SaveChanges();

            return Ok(transactionhistoryarchive);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionHistoryArchiveExists(int id)
        {
            return db.TransactionHistoryArchives.Count(e => e.TransactionID == id) > 0;
        }
    }
}