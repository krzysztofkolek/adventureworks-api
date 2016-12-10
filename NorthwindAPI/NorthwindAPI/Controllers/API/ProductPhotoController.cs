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
    public class ProductPhotoController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductPhoto
        public IQueryable<ProductPhoto> GetProductPhotoes()
        {
            return db.ProductPhotoes;
        }

        // GET api/ProductPhoto/5
        [ResponseType(typeof(ProductPhoto))]
        public IHttpActionResult GetProductPhoto(int id)
        {
            ProductPhoto productphoto = db.ProductPhotoes.Find(id);
            if (productphoto == null)
            {
                return NotFound();
            }

            return Ok(productphoto);
        }

        // PUT api/ProductPhoto/5
        public IHttpActionResult PutProductPhoto(int id, ProductPhoto productphoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productphoto.ProductPhotoID)
            {
                return BadRequest();
            }

            db.Entry(productphoto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPhotoExists(id))
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

        // POST api/ProductPhoto
        [ResponseType(typeof(ProductPhoto))]
        public IHttpActionResult PostProductPhoto(ProductPhoto productphoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductPhotoes.Add(productphoto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productphoto.ProductPhotoID }, productphoto);
        }

        // DELETE api/ProductPhoto/5
        [ResponseType(typeof(ProductPhoto))]
        public IHttpActionResult DeleteProductPhoto(int id)
        {
            ProductPhoto productphoto = db.ProductPhotoes.Find(id);
            if (productphoto == null)
            {
                return NotFound();
            }

            db.ProductPhotoes.Remove(productphoto);
            db.SaveChanges();

            return Ok(productphoto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductPhotoExists(int id)
        {
            return db.ProductPhotoes.Count(e => e.ProductPhotoID == id) > 0;
        }
    }
}