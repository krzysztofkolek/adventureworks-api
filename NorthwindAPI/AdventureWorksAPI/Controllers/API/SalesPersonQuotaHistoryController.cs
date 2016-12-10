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
    public class SalesPersonQuotaHistoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SalesPersonQuotaHistory
        public IQueryable<SalesPersonQuotaHistory> GetSalesPersonQuotaHistories()
        {
            return db.SalesPersonQuotaHistories;
        }

        // GET api/SalesPersonQuotaHistory/5
        [ResponseType(typeof(SalesPersonQuotaHistory))]
        public IHttpActionResult GetSalesPersonQuotaHistory(int id)
        {
            SalesPersonQuotaHistory salespersonquotahistory = db.SalesPersonQuotaHistories.Find(id);
            if (salespersonquotahistory == null)
            {
                return NotFound();
            }

            return Ok(salespersonquotahistory);
        }

        // PUT api/SalesPersonQuotaHistory/5
        public IHttpActionResult PutSalesPersonQuotaHistory(int id, SalesPersonQuotaHistory salespersonquotahistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salespersonquotahistory.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(salespersonquotahistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPersonQuotaHistoryExists(id))
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

        // POST api/SalesPersonQuotaHistory
        [ResponseType(typeof(SalesPersonQuotaHistory))]
        public IHttpActionResult PostSalesPersonQuotaHistory(SalesPersonQuotaHistory salespersonquotahistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesPersonQuotaHistories.Add(salespersonquotahistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalesPersonQuotaHistoryExists(salespersonquotahistory.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salespersonquotahistory.BusinessEntityID }, salespersonquotahistory);
        }

        // DELETE api/SalesPersonQuotaHistory/5
        [ResponseType(typeof(SalesPersonQuotaHistory))]
        public IHttpActionResult DeleteSalesPersonQuotaHistory(int id)
        {
            SalesPersonQuotaHistory salespersonquotahistory = db.SalesPersonQuotaHistories.Find(id);
            if (salespersonquotahistory == null)
            {
                return NotFound();
            }

            db.SalesPersonQuotaHistories.Remove(salespersonquotahistory);
            db.SaveChanges();

            return Ok(salespersonquotahistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesPersonQuotaHistoryExists(int id)
        {
            return db.SalesPersonQuotaHistories.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}