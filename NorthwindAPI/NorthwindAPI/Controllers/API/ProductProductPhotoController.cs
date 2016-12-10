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
    public class ProductProductPhotoController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductProductPhoto
        public IQueryable<ProductProductPhoto> GetProductProductPhotoes()
        {
            return db.ProductProductPhotoes;
        }

        // GET api/ProductProductPhoto/5
        [ResponseType(typeof(ProductProductPhoto))]
        public IHttpActionResult GetProductProductPhoto(int id)
        {
            ProductProductPhoto productproductphoto = db.ProductProductPhotoes.Find(id);
            if (productproductphoto == null)
            {
                return NotFound();
            }

            return Ok(productproductphoto);
        }

        // PUT api/ProductProductPhoto/5
        public IHttpActionResult PutProductProductPhoto(int id, ProductProductPhoto productproductphoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productproductphoto.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productproductphoto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductProductPhotoExists(id))
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

        // POST api/ProductProductPhoto
        [ResponseType(typeof(ProductProductPhoto))]
        public IHttpActionResult PostProductProductPhoto(ProductProductPhoto productproductphoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductProductPhotoes.Add(productproductphoto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductProductPhotoExists(productproductphoto.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productproductphoto.ProductID }, productproductphoto);
        }

        // DELETE api/ProductProductPhoto/5
        [ResponseType(typeof(ProductProductPhoto))]
        public IHttpActionResult DeleteProductProductPhoto(int id)
        {
            ProductProductPhoto productproductphoto = db.ProductProductPhotoes.Find(id);
            if (productproductphoto == null)
            {
                return NotFound();
            }

            db.ProductProductPhotoes.Remove(productproductphoto);
            db.SaveChanges();

            return Ok(productproductphoto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductProductPhotoExists(int id)
        {
            return db.ProductProductPhotoes.Count(e => e.ProductID == id) > 0;
        }
    }
}