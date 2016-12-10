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
    public class TransactionHistoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/TransactionHistory
        public IQueryable<TransactionHistory> GetTransactionHistories()
        {
            return db.TransactionHistories;
        }

        // GET api/TransactionHistory/5
        [ResponseType(typeof(TransactionHistory))]
        public IHttpActionResult GetTransactionHistory(int id)
        {
            TransactionHistory transactionhistory = db.TransactionHistories.Find(id);
            if (transactionhistory == null)
            {
                return NotFound();
            }

            return Ok(transactionhistory);
        }

        // PUT api/TransactionHistory/5
        public IHttpActionResult PutTransactionHistory(int id, TransactionHistory transactionhistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transactionhistory.TransactionID)
            {
                return BadRequest();
            }

            db.Entry(transactionhistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionHistoryExists(id))
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

        // POST api/TransactionHistory
        [ResponseType(typeof(TransactionHistory))]
        public IHttpActionResult PostTransactionHistory(TransactionHistory transactionhistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransactionHistories.Add(transactionhistory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transactionhistory.TransactionID }, transactionhistory);
        }

        // DELETE api/TransactionHistory/5
        [ResponseType(typeof(TransactionHistory))]
        public IHttpActionResult DeleteTransactionHistory(int id)
        {
            TransactionHistory transactionhistory = db.TransactionHistories.Find(id);
            if (transactionhistory == null)
            {
                return NotFound();
            }

            db.TransactionHistories.Remove(transactionhistory);
            db.SaveChanges();

            return Ok(transactionhistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionHistoryExists(int id)
        {
            return db.TransactionHistories.Count(e => e.TransactionID == id) > 0;
        }
    }
}