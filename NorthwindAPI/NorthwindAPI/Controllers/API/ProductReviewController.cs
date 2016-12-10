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
    public class ProductReviewController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ProductReview
        public IQueryable<ProductReview> GetProductReviews()
        {
            return db.ProductReviews;
        }

        // GET api/ProductReview/5
        [ResponseType(typeof(ProductReview))]
        public IHttpActionResult GetProductReview(int id)
        {
            ProductReview productreview = db.ProductReviews.Find(id);
            if (productreview == null)
            {
                return NotFound();
            }

            return Ok(productreview);
        }

        // PUT api/ProductReview/5
        public IHttpActionResult PutProductReview(int id, ProductReview productreview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productreview.ProductReviewID)
            {
                return BadRequest();
            }

            db.Entry(productreview).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductReviewExists(id))
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

        // POST api/ProductReview
        [ResponseType(typeof(ProductReview))]
        public IHttpActionResult PostProductReview(ProductReview productreview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductReviews.Add(productreview);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productreview.ProductReviewID }, productreview);
        }

        // DELETE api/ProductReview/5
        [ResponseType(typeof(ProductReview))]
        public IHttpActionResult DeleteProductReview(int id)
        {
            ProductReview productreview = db.ProductReviews.Find(id);
            if (productreview == null)
            {
                return NotFound();
            }

            db.ProductReviews.Remove(productreview);
            db.SaveChanges();

            return Ok(productreview);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductReviewExists(int id)
        {
            return db.ProductReviews.Count(e => e.ProductReviewID == id) > 0;
        }
    }
}