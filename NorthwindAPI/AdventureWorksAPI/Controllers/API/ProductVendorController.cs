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
    public class ProductVendorController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductVendor
        public IQueryable<ProductVendor> GetProductVendors()
        {
            return db.ProductVendors;
        }

        // GET api/ProductVendor/5
        [ResponseType(typeof(ProductVendor))]
        public IHttpActionResult GetProductVendor(int id)
        {
            ProductVendor productvendor = db.ProductVendors.Find(id);
            if (productvendor == null)
            {
                return NotFound();
            }

            return Ok(productvendor);
        }

        // PUT api/ProductVendor/5
        public IHttpActionResult PutProductVendor(int id, ProductVendor productvendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productvendor.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productvendor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductVendorExists(id))
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

        // POST api/ProductVendor
        [ResponseType(typeof(ProductVendor))]
        public IHttpActionResult PostProductVendor(ProductVendor productvendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductVendors.Add(productvendor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductVendorExists(productvendor.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productvendor.ProductID }, productvendor);
        }

        // DELETE api/ProductVendor/5
        [ResponseType(typeof(ProductVendor))]
        public IHttpActionResult DeleteProductVendor(int id)
        {
            ProductVendor productvendor = db.ProductVendors.Find(id);
            if (productvendor == null)
            {
                return NotFound();
            }

            db.ProductVendors.Remove(productvendor);
            db.SaveChanges();

            return Ok(productvendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductVendorExists(int id)
        {
            return db.ProductVendors.Count(e => e.ProductID == id) > 0;
        }
    }
}