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
    public class ProductInventoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductInventory
        public IQueryable<ProductInventory> GetProductInventories()
        {
            return db.ProductInventories;
        }

        // GET api/ProductInventory/5
        [ResponseType(typeof(ProductInventory))]
        public IHttpActionResult GetProductInventory(int id)
        {
            ProductInventory productinventory = db.ProductInventories.Find(id);
            if (productinventory == null)
            {
                return NotFound();
            }

            return Ok(productinventory);
        }

        // PUT api/ProductInventory/5
        public IHttpActionResult PutProductInventory(int id, ProductInventory productinventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productinventory.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productinventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInventoryExists(id))
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

        // POST api/ProductInventory
        [ResponseType(typeof(ProductInventory))]
        public IHttpActionResult PostProductInventory(ProductInventory productinventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductInventories.Add(productinventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductInventoryExists(productinventory.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productinventory.ProductID }, productinventory);
        }

        // DELETE api/ProductInventory/5
        [ResponseType(typeof(ProductInventory))]
        public IHttpActionResult DeleteProductInventory(int id)
        {
            ProductInventory productinventory = db.ProductInventories.Find(id);
            if (productinventory == null)
            {
                return NotFound();
            }

            db.ProductInventories.Remove(productinventory);
            db.SaveChanges();

            return Ok(productinventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductInventoryExists(int id)
        {
            return db.ProductInventories.Count(e => e.ProductID == id) > 0;
        }
    }
}