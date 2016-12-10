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
    public class SpecialOfferProductController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SpecialOfferProduct
        public IQueryable<SpecialOfferProduct> GetSpecialOfferProducts()
        {
            return db.SpecialOfferProducts;
        }

        // GET api/SpecialOfferProduct/5
        [ResponseType(typeof(SpecialOfferProduct))]
        public IHttpActionResult GetSpecialOfferProduct(int id)
        {
            SpecialOfferProduct specialofferproduct = db.SpecialOfferProducts.Find(id);
            if (specialofferproduct == null)
            {
                return NotFound();
            }

            return Ok(specialofferproduct);
        }

        // PUT api/SpecialOfferProduct/5
        public IHttpActionResult PutSpecialOfferProduct(int id, SpecialOfferProduct specialofferproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specialofferproduct.SpecialOfferID)
            {
                return BadRequest();
            }

            db.Entry(specialofferproduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialOfferProductExists(id))
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

        // POST api/SpecialOfferProduct
        [ResponseType(typeof(SpecialOfferProduct))]
        public IHttpActionResult PostSpecialOfferProduct(SpecialOfferProduct specialofferproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SpecialOfferProducts.Add(specialofferproduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SpecialOfferProductExists(specialofferproduct.SpecialOfferID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = specialofferproduct.SpecialOfferID }, specialofferproduct);
        }

        // DELETE api/SpecialOfferProduct/5
        [ResponseType(typeof(SpecialOfferProduct))]
        public IHttpActionResult DeleteSpecialOfferProduct(int id)
        {
            SpecialOfferProduct specialofferproduct = db.SpecialOfferProducts.Find(id);
            if (specialofferproduct == null)
            {
                return NotFound();
            }

            db.SpecialOfferProducts.Remove(specialofferproduct);
            db.SaveChanges();

            return Ok(specialofferproduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpecialOfferProductExists(int id)
        {
            return db.SpecialOfferProducts.Count(e => e.SpecialOfferID == id) > 0;
        }
    }
}