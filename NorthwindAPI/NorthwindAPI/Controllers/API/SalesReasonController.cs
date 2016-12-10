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
    public class SalesReasonController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SalesReason
        public IQueryable<SalesReason> GetSalesReasons()
        {
            return db.SalesReasons;
        }

        // GET api/SalesReason/5
        [ResponseType(typeof(SalesReason))]
        public IHttpActionResult GetSalesReason(int id)
        {
            SalesReason salesreason = db.SalesReasons.Find(id);
            if (salesreason == null)
            {
                return NotFound();
            }

            return Ok(salesreason);
        }

        // PUT api/SalesReason/5
        public IHttpActionResult PutSalesReason(int id, SalesReason salesreason)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesreason.SalesReasonID)
            {
                return BadRequest();
            }

            db.Entry(salesreason).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesReasonExists(id))
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

        // POST api/SalesReason
        [ResponseType(typeof(SalesReason))]
        public IHttpActionResult PostSalesReason(SalesReason salesreason)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesReasons.Add(salesreason);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = salesreason.SalesReasonID }, salesreason);
        }

        // DELETE api/SalesReason/5
        [ResponseType(typeof(SalesReason))]
        public IHttpActionResult DeleteSalesReason(int id)
        {
            SalesReason salesreason = db.SalesReasons.Find(id);
            if (salesreason == null)
            {
                return NotFound();
            }

            db.SalesReasons.Remove(salesreason);
            db.SaveChanges();

            return Ok(salesreason);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesReasonExists(int id)
        {
            return db.SalesReasons.Count(e => e.SalesReasonID == id) > 0;
        }
    }
}