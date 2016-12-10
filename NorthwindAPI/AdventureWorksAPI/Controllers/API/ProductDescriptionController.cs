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
    public class ProductDescriptionController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductDescription
        public IQueryable<ProductDescription> GetProductDescriptions()
        {
            return db.ProductDescriptions;
        }

        // GET api/ProductDescription/5
        [ResponseType(typeof(ProductDescription))]
        public IHttpActionResult GetProductDescription(int id)
        {
            ProductDescription productdescription = db.ProductDescriptions.Find(id);
            if (productdescription == null)
            {
                return NotFound();
            }

            return Ok(productdescription);
        }

        // PUT api/ProductDescription/5
        public IHttpActionResult PutProductDescription(int id, ProductDescription productdescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productdescription.ProductDescriptionID)
            {
                return BadRequest();
            }

            db.Entry(productdescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDescriptionExists(id))
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

        // POST api/ProductDescription
        [ResponseType(typeof(ProductDescription))]
        public IHttpActionResult PostProductDescription(ProductDescription productdescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductDescriptions.Add(productdescription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productdescription.ProductDescriptionID }, productdescription);
        }

        // DELETE api/ProductDescription/5
        [ResponseType(typeof(ProductDescription))]
        public IHttpActionResult DeleteProductDescription(int id)
        {
            ProductDescription productdescription = db.ProductDescriptions.Find(id);
            if (productdescription == null)
            {
                return NotFound();
            }

            db.ProductDescriptions.Remove(productdescription);
            db.SaveChanges();

            return Ok(productdescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductDescriptionExists(int id)
        {
            return db.ProductDescriptions.Count(e => e.ProductDescriptionID == id) > 0;
        }
    }
}