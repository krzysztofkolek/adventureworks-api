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
    public class ProductSubcategoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductSubcategory
        public IQueryable<ProductSubcategory> GetProductSubcategories()
        {
            return db.ProductSubcategories;
        }

        // GET api/ProductSubcategory/5
        [ResponseType(typeof(ProductSubcategory))]
        public IHttpActionResult GetProductSubcategory(int id)
        {
            ProductSubcategory productsubcategory = db.ProductSubcategories.Find(id);
            if (productsubcategory == null)
            {
                return NotFound();
            }

            return Ok(productsubcategory);
        }

        // PUT api/ProductSubcategory/5
        public IHttpActionResult PutProductSubcategory(int id, ProductSubcategory productsubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productsubcategory.ProductSubcategoryID)
            {
                return BadRequest();
            }

            db.Entry(productsubcategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSubcategoryExists(id))
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

        // POST api/ProductSubcategory
        [ResponseType(typeof(ProductSubcategory))]
        public IHttpActionResult PostProductSubcategory(ProductSubcategory productsubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductSubcategories.Add(productsubcategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productsubcategory.ProductSubcategoryID }, productsubcategory);
        }

        // DELETE api/ProductSubcategory/5
        [ResponseType(typeof(ProductSubcategory))]
        public IHttpActionResult DeleteProductSubcategory(int id)
        {
            ProductSubcategory productsubcategory = db.ProductSubcategories.Find(id);
            if (productsubcategory == null)
            {
                return NotFound();
            }

            db.ProductSubcategories.Remove(productsubcategory);
            db.SaveChanges();

            return Ok(productsubcategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductSubcategoryExists(int id)
        {
            return db.ProductSubcategories.Count(e => e.ProductSubcategoryID == id) > 0;
        }
    }
}