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
    public class ProductDocumentController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductDocument
        public IQueryable<ProductDocument> GetProductDocuments()
        {
            return db.ProductDocuments;
        }

        // GET api/ProductDocument/5
        [ResponseType(typeof(ProductDocument))]
        public IHttpActionResult GetProductDocument(int id)
        {
            ProductDocument productdocument = db.ProductDocuments.Find(id);
            if (productdocument == null)
            {
                return NotFound();
            }

            return Ok(productdocument);
        }

        // PUT api/ProductDocument/5
        public IHttpActionResult PutProductDocument(int id, ProductDocument productdocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productdocument.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productdocument).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDocumentExists(id))
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

        // POST api/ProductDocument
        [ResponseType(typeof(ProductDocument))]
        public IHttpActionResult PostProductDocument(ProductDocument productdocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductDocuments.Add(productdocument);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductDocumentExists(productdocument.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productdocument.ProductID }, productdocument);
        }

        // DELETE api/ProductDocument/5
        [ResponseType(typeof(ProductDocument))]
        public IHttpActionResult DeleteProductDocument(int id)
        {
            ProductDocument productdocument = db.ProductDocuments.Find(id);
            if (productdocument == null)
            {
                return NotFound();
            }

            db.ProductDocuments.Remove(productdocument);
            db.SaveChanges();

            return Ok(productdocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductDocumentExists(int id)
        {
            return db.ProductDocuments.Count(e => e.ProductID == id) > 0;
        }
    }
}