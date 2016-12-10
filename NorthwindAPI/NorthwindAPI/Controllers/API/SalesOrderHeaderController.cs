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
    public class SalesOrderHeaderController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SalesOrderHeader
        public IQueryable<SalesOrderHeader> GetSalesOrderHeaders()
        {
            return db.SalesOrderHeaders;
        }

        // GET api/SalesOrderHeader/5
        [ResponseType(typeof(SalesOrderHeader))]
        public IHttpActionResult GetSalesOrderHeader(int id)
        {
            SalesOrderHeader salesorderheader = db.SalesOrderHeaders.Find(id);
            if (salesorderheader == null)
            {
                return NotFound();
            }

            return Ok(salesorderheader);
        }

        // PUT api/SalesOrderHeader/5
        public IHttpActionResult PutSalesOrderHeader(int id, SalesOrderHeader salesorderheader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesorderheader.SalesOrderID)
            {
                return BadRequest();
            }

            db.Entry(salesorderheader).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderHeaderExists(id))
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

        // POST api/SalesOrderHeader
        [ResponseType(typeof(SalesOrderHeader))]
        public IHttpActionResult PostSalesOrderHeader(SalesOrderHeader salesorderheader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesOrderHeaders.Add(salesorderheader);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = salesorderheader.SalesOrderID }, salesorderheader);
        }

        // DELETE api/SalesOrderHeader/5
        [ResponseType(typeof(SalesOrderHeader))]
        public IHttpActionResult DeleteSalesOrderHeader(int id)
        {
            SalesOrderHeader salesorderheader = db.SalesOrderHeaders.Find(id);
            if (salesorderheader == null)
            {
                return NotFound();
            }

            db.SalesOrderHeaders.Remove(salesorderheader);
            db.SaveChanges();

            return Ok(salesorderheader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesOrderHeaderExists(int id)
        {
            return db.SalesOrderHeaders.Count(e => e.SalesOrderID == id) > 0;
        }
    }
}