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
    public class ProductCostHistoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductCostHistory
        public IQueryable<ProductCostHistory> GetProductCostHistories()
        {
            return db.ProductCostHistories;
        }

        // GET api/ProductCostHistory/5
        [ResponseType(typeof(ProductCostHistory))]
        public IHttpActionResult GetProductCostHistory(int id)
        {
            ProductCostHistory productcosthistory = db.ProductCostHistories.Find(id);
            if (productcosthistory == null)
            {
                return NotFound();
            }

            return Ok(productcosthistory);
        }

        // PUT api/ProductCostHistory/5
        public IHttpActionResult PutProductCostHistory(int id, ProductCostHistory productcosthistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productcosthistory.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productcosthistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCostHistoryExists(id))
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

        // POST api/ProductCostHistory
        [ResponseType(typeof(ProductCostHistory))]
        public IHttpActionResult PostProductCostHistory(ProductCostHistory productcosthistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductCostHistories.Add(productcosthistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductCostHistoryExists(productcosthistory.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productcosthistory.ProductID }, productcosthistory);
        }

        // DELETE api/ProductCostHistory/5
        [ResponseType(typeof(ProductCostHistory))]
        public IHttpActionResult DeleteProductCostHistory(int id)
        {
            ProductCostHistory productcosthistory = db.ProductCostHistories.Find(id);
            if (productcosthistory == null)
            {
                return NotFound();
            }

            db.ProductCostHistories.Remove(productcosthistory);
            db.SaveChanges();

            return Ok(productcosthistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductCostHistoryExists(int id)
        {
            return db.ProductCostHistories.Count(e => e.ProductID == id) > 0;
        }
    }
}