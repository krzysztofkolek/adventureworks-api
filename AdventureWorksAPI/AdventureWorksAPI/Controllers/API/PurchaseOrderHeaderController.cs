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
    public class PurchaseOrderHeaderController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/PurchaseOrderHeader
        public IQueryable<PurchaseOrderHeader> GetPurchaseOrderHeaders()
        {
            return db.PurchaseOrderHeaders;
        }

        // GET api/PurchaseOrderHeader/5
        [ResponseType(typeof(PurchaseOrderHeader))]
        public IHttpActionResult GetPurchaseOrderHeader(int id)
        {
            PurchaseOrderHeader purchaseorderheader = db.PurchaseOrderHeaders.Find(id);
            if (purchaseorderheader == null)
            {
                return NotFound();
            }

            return Ok(purchaseorderheader);
        }

        // PUT api/PurchaseOrderHeader/5
        public IHttpActionResult PutPurchaseOrderHeader(int id, PurchaseOrderHeader purchaseorderheader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseorderheader.PurchaseOrderID)
            {
                return BadRequest();
            }

            db.Entry(purchaseorderheader).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderHeaderExists(id))
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

        // POST api/PurchaseOrderHeader
        [ResponseType(typeof(PurchaseOrderHeader))]
        public IHttpActionResult PostPurchaseOrderHeader(PurchaseOrderHeader purchaseorderheader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrderHeaders.Add(purchaseorderheader);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseorderheader.PurchaseOrderID }, purchaseorderheader);
        }

        // DELETE api/PurchaseOrderHeader/5
        [ResponseType(typeof(PurchaseOrderHeader))]
        public IHttpActionResult DeletePurchaseOrderHeader(int id)
        {
            PurchaseOrderHeader purchaseorderheader = db.PurchaseOrderHeaders.Find(id);
            if (purchaseorderheader == null)
            {
                return NotFound();
            }

            db.PurchaseOrderHeaders.Remove(purchaseorderheader);
            db.SaveChanges();

            return Ok(purchaseorderheader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderHeaderExists(int id)
        {
            return db.PurchaseOrderHeaders.Count(e => e.PurchaseOrderID == id) > 0;
        }
    }
}