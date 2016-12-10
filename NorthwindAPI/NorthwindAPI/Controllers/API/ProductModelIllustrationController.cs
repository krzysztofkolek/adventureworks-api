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
    public class ProductModelIllustrationController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductModelIllustration
        public IQueryable<ProductModelIllustration> GetProductModelIllustrations()
        {
            return db.ProductModelIllustrations;
        }

        // GET api/ProductModelIllustration/5
        [ResponseType(typeof(ProductModelIllustration))]
        public IHttpActionResult GetProductModelIllustration(int id)
        {
            ProductModelIllustration productmodelillustration = db.ProductModelIllustrations.Find(id);
            if (productmodelillustration == null)
            {
                return NotFound();
            }

            return Ok(productmodelillustration);
        }

        // PUT api/ProductModelIllustration/5
        public IHttpActionResult PutProductModelIllustration(int id, ProductModelIllustration productmodelillustration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productmodelillustration.ProductModelID)
            {
                return BadRequest();
            }

            db.Entry(productmodelillustration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelIllustrationExists(id))
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

        // POST api/ProductModelIllustration
        [ResponseType(typeof(ProductModelIllustration))]
        public IHttpActionResult PostProductModelIllustration(ProductModelIllustration productmodelillustration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductModelIllustrations.Add(productmodelillustration);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductModelIllustrationExists(productmodelillustration.ProductModelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productmodelillustration.ProductModelID }, productmodelillustration);
        }

        // DELETE api/ProductModelIllustration/5
        [ResponseType(typeof(ProductModelIllustration))]
        public IHttpActionResult DeleteProductModelIllustration(int id)
        {
            ProductModelIllustration productmodelillustration = db.ProductModelIllustrations.Find(id);
            if (productmodelillustration == null)
            {
                return NotFound();
            }

            db.ProductModelIllustrations.Remove(productmodelillustration);
            db.SaveChanges();

            return Ok(productmodelillustration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductModelIllustrationExists(int id)
        {
            return db.ProductModelIllustrations.Count(e => e.ProductModelID == id) > 0;
        }
    }
}