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
    public class SalesOrderDetailController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SalesOrderDetail
        public IQueryable<SalesOrderDetail> GetSalesOrderDetails()
        {
            return db.SalesOrderDetails;
        }

        // GET api/SalesOrderDetail/5
        [ResponseType(typeof(SalesOrderDetail))]
        public IHttpActionResult GetSalesOrderDetail(int id)
        {
            SalesOrderDetail salesorderdetail = db.SalesOrderDetails.Find(id);
            if (salesorderdetail == null)
            {
                return NotFound();
            }

            return Ok(salesorderdetail);
        }

        // PUT api/SalesOrderDetail/5
        public IHttpActionResult PutSalesOrderDetail(int id, SalesOrderDetail salesorderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesorderdetail.SalesOrderID)
            {
                return BadRequest();
            }

            db.Entry(salesorderdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderDetailExists(id))
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

        // POST api/SalesOrderDetail
        [ResponseType(typeof(SalesOrderDetail))]
        public IHttpActionResult PostSalesOrderDetail(SalesOrderDetail salesorderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesOrderDetails.Add(salesorderdetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalesOrderDetailExists(salesorderdetail.SalesOrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salesorderdetail.SalesOrderID }, salesorderdetail);
        }

        // DELETE api/SalesOrderDetail/5
        [ResponseType(typeof(SalesOrderDetail))]
        public IHttpActionResult DeleteSalesOrderDetail(int id)
        {
            SalesOrderDetail salesorderdetail = db.SalesOrderDetails.Find(id);
            if (salesorderdetail == null)
            {
                return NotFound();
            }

            db.SalesOrderDetails.Remove(salesorderdetail);
            db.SaveChanges();

            return Ok(salesorderdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesOrderDetailExists(int id)
        {
            return db.SalesOrderDetails.Count(e => e.SalesOrderID == id) > 0;
        }
    }
}