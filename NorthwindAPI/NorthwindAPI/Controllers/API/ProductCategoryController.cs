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
    public class ProductCategoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductCategory
        public IQueryable<ProductCategory> GetProductCategories()
        {
            return db.ProductCategories;
        }

        // GET api/ProductCategory/5
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult GetProductCategory(int id)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return NotFound();
            }

            return Ok(productcategory);
        }

        // PUT api/ProductCategory/5
        public IHttpActionResult PutProductCategory(int id, ProductCategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productcategory.ProductCategoryID)
            {
                return BadRequest();
            }

            db.Entry(productcategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST api/ProductCategory
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult PostProductCategory(ProductCategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductCategories.Add(productcategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productcategory.ProductCategoryID }, productcategory);
        }

        // DELETE api/ProductCategory/5
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult DeleteProductCategory(int id)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return NotFound();
            }

            db.ProductCategories.Remove(productcategory);
            db.SaveChanges();

            return Ok(productcategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductCategoryExists(int id)
        {
            return db.ProductCategories.Count(e => e.ProductCategoryID == id) > 0;
        }
    }
}