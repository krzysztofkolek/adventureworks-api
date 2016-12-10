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
    public class ProductModelController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductModel
        public IQueryable<ProductModel> GetProductModels()
        {
            return db.ProductModels;
        }

        // GET api/ProductModel/5
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult GetProductModel(int id)
        {
            ProductModel productmodel = db.ProductModels.Find(id);
            if (productmodel == null)
            {
                return NotFound();
            }

            return Ok(productmodel);
        }

        // PUT api/ProductModel/5
        public IHttpActionResult PutProductModel(int id, ProductModel productmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productmodel.ProductModelID)
            {
                return BadRequest();
            }

            db.Entry(productmodel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
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

        // POST api/ProductModel
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PostProductModel(ProductModel productmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductModels.Add(productmodel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productmodel.ProductModelID }, productmodel);
        }

        // DELETE api/ProductModel/5
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult DeleteProductModel(int id)
        {
            ProductModel productmodel = db.ProductModels.Find(id);
            if (productmodel == null)
            {
                return NotFound();
            }

            db.ProductModels.Remove(productmodel);
            db.SaveChanges();

            return Ok(productmodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductModelExists(int id)
        {
            return db.ProductModels.Count(e => e.ProductModelID == id) > 0;
        }
    }
}