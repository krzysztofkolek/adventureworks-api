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
    public class ProductModelProductDescriptionCultureController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductModelProductDescriptionCulture
        public IQueryable<ProductModelProductDescriptionCulture> GetProductModelProductDescriptionCultures()
        {
            return db.ProductModelProductDescriptionCultures;
        }

        // GET api/ProductModelProductDescriptionCulture/5
        [ResponseType(typeof(ProductModelProductDescriptionCulture))]
        public IHttpActionResult GetProductModelProductDescriptionCulture(int id)
        {
            ProductModelProductDescriptionCulture productmodelproductdescriptionculture = db.ProductModelProductDescriptionCultures.Find(id);
            if (productmodelproductdescriptionculture == null)
            {
                return NotFound();
            }

            return Ok(productmodelproductdescriptionculture);
        }

        // PUT api/ProductModelProductDescriptionCulture/5
        public IHttpActionResult PutProductModelProductDescriptionCulture(int id, ProductModelProductDescriptionCulture productmodelproductdescriptionculture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productmodelproductdescriptionculture.ProductModelID)
            {
                return BadRequest();
            }

            db.Entry(productmodelproductdescriptionculture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelProductDescriptionCultureExists(id))
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

        // POST api/ProductModelProductDescriptionCulture
        [ResponseType(typeof(ProductModelProductDescriptionCulture))]
        public IHttpActionResult PostProductModelProductDescriptionCulture(ProductModelProductDescriptionCulture productmodelproductdescriptionculture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductModelProductDescriptionCultures.Add(productmodelproductdescriptionculture);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductModelProductDescriptionCultureExists(productmodelproductdescriptionculture.ProductModelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productmodelproductdescriptionculture.ProductModelID }, productmodelproductdescriptionculture);
        }

        // DELETE api/ProductModelProductDescriptionCulture/5
        [ResponseType(typeof(ProductModelProductDescriptionCulture))]
        public IHttpActionResult DeleteProductModelProductDescriptionCulture(int id)
        {
            ProductModelProductDescriptionCulture productmodelproductdescriptionculture = db.ProductModelProductDescriptionCultures.Find(id);
            if (productmodelproductdescriptionculture == null)
            {
                return NotFound();
            }

            db.ProductModelProductDescriptionCultures.Remove(productmodelproductdescriptionculture);
            db.SaveChanges();

            return Ok(productmodelproductdescriptionculture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductModelProductDescriptionCultureExists(int id)
        {
            return db.ProductModelProductDescriptionCultures.Count(e => e.ProductModelID == id) > 0;
        }
    }
}