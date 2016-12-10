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
    public class ProductListsPriceHistoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductListsPriceHistory
        public IQueryable<ProductListPriceHistory> GetProductListPriceHistories()
        {
            return db.ProductListPriceHistories;
        }

        // GET api/ProductListsPriceHistory/5
        [ResponseType(typeof(ProductListPriceHistory))]
        public IHttpActionResult GetProductListPriceHistory(int id)
        {
            ProductListPriceHistory productlistpricehistory = db.ProductListPriceHistories.Find(id);
            if (productlistpricehistory == null)
            {
                return NotFound();
            }

            return Ok(productlistpricehistory);
        }

        // PUT api/ProductListsPriceHistory/5
        public IHttpActionResult PutProductListPriceHistory(int id, ProductListPriceHistory productlistpricehistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productlistpricehistory.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productlistpricehistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductListPriceHistoryExists(id))
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

        // POST api/ProductListsPriceHistory
        [ResponseType(typeof(ProductListPriceHistory))]
        public IHttpActionResult PostProductListPriceHistory(ProductListPriceHistory productlistpricehistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductListPriceHistories.Add(productlistpricehistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductListPriceHistoryExists(productlistpricehistory.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productlistpricehistory.ProductID }, productlistpricehistory);
        }

        // DELETE api/ProductListsPriceHistory/5
        [ResponseType(typeof(ProductListPriceHistory))]
        public IHttpActionResult DeleteProductListPriceHistory(int id)
        {
            ProductListPriceHistory productlistpricehistory = db.ProductListPriceHistories.Find(id);
            if (productlistpricehistory == null)
            {
                return NotFound();
            }

            db.ProductListPriceHistories.Remove(productlistpricehistory);
            db.SaveChanges();

            return Ok(productlistpricehistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductListPriceHistoryExists(int id)
        {
            return db.ProductListPriceHistories.Count(e => e.ProductID == id) > 0;
        }
    }
}