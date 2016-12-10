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
    public class PurchaseOrderDetailController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/PurchaseOrderDetail
        public IQueryable<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return db.PurchaseOrderDetails;
        }

        // GET api/PurchaseOrderDetail/5
        [ResponseType(typeof(PurchaseOrderDetail))]
        public IHttpActionResult GetPurchaseOrderDetail(int id)
        {
            PurchaseOrderDetail purchaseorderdetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseorderdetail == null)
            {
                return NotFound();
            }

            return Ok(purchaseorderdetail);
        }

        // PUT api/PurchaseOrderDetail/5
        public IHttpActionResult PutPurchaseOrderDetail(int id, PurchaseOrderDetail purchaseorderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseorderdetail.PurchaseOrderID)
            {
                return BadRequest();
            }

            db.Entry(purchaseorderdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderDetailExists(id))
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

        // POST api/PurchaseOrderDetail
        [ResponseType(typeof(PurchaseOrderDetail))]
        public IHttpActionResult PostPurchaseOrderDetail(PurchaseOrderDetail purchaseorderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrderDetails.Add(purchaseorderdetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PurchaseOrderDetailExists(purchaseorderdetail.PurchaseOrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = purchaseorderdetail.PurchaseOrderID }, purchaseorderdetail);
        }

        // DELETE api/PurchaseOrderDetail/5
        [ResponseType(typeof(PurchaseOrderDetail))]
        public IHttpActionResult DeletePurchaseOrderDetail(int id)
        {
            PurchaseOrderDetail purchaseorderdetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseorderdetail == null)
            {
                return NotFound();
            }

            db.PurchaseOrderDetails.Remove(purchaseorderdetail);
            db.SaveChanges();

            return Ok(purchaseorderdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderDetailExists(int id)
        {
            return db.PurchaseOrderDetails.Count(e => e.PurchaseOrderID == id) > 0;
        }
    }
}