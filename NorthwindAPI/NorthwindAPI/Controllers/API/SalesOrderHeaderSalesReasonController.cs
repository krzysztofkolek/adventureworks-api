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
using NorthwindAPI.DBModels;

namespace NorthwindAPI.Controllers.API
{
    public class SalesOrderHeaderSalesReasonController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SalesOrderHeaderSalesReason
        public IQueryable<SalesOrderHeaderSalesReason> GetSalesOrderHeaderSalesReasons()
        {
            return db.SalesOrderHeaderSalesReasons;
        }

        // GET api/SalesOrderHeaderSalesReason/5
        [ResponseType(typeof(SalesOrderHeaderSalesReason))]
        public IHttpActionResult GetSalesOrderHeaderSalesReason(int id)
        {
            SalesOrderHeaderSalesReason salesorderheadersalesreason = db.SalesOrderHeaderSalesReasons.Find(id);
            if (salesorderheadersalesreason == null)
            {
                return NotFound();
            }

            return Ok(salesorderheadersalesreason);
        }

        // PUT api/SalesOrderHeaderSalesReason/5
        public IHttpActionResult PutSalesOrderHeaderSalesReason(int id, SalesOrderHeaderSalesReason salesorderheadersalesreason)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesorderheadersalesreason.SalesOrderID)
            {
                return BadRequest();
            }

            db.Entry(salesorderheadersalesreason).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderHeaderSalesReasonExists(id))
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

        // POST api/SalesOrderHeaderSalesReason
        [ResponseType(typeof(SalesOrderHeaderSalesReason))]
        public IHttpActionResult PostSalesOrderHeaderSalesReason(SalesOrderHeaderSalesReason salesorderheadersalesreason)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesOrderHeaderSalesReasons.Add(salesorderheadersalesreason);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalesOrderHeaderSalesReasonExists(salesorderheadersalesreason.SalesOrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salesorderheadersalesreason.SalesOrderID }, salesorderheadersalesreason);
        }

        // DELETE api/SalesOrderHeaderSalesReason/5
        [ResponseType(typeof(SalesOrderHeaderSalesReason))]
        public IHttpActionResult DeleteSalesOrderHeaderSalesReason(int id)
        {
            SalesOrderHeaderSalesReason salesorderheadersalesreason = db.SalesOrderHeaderSalesReasons.Find(id);
            if (salesorderheadersalesreason == null)
            {
                return NotFound();
            }

            db.SalesOrderHeaderSalesReasons.Remove(salesorderheadersalesreason);
            db.SaveChanges();

            return Ok(salesorderheadersalesreason);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesOrderHeaderSalesReasonExists(int id)
        {
            return db.SalesOrderHeaderSalesReasons.Count(e => e.SalesOrderID == id) > 0;
        }
    }
}