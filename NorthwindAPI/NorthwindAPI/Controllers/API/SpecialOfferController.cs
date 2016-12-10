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
    public class SpecialOfferController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SpecialOffer
        public IQueryable<SpecialOffer> GetSpecialOffers()
        {
            return db.SpecialOffers;
        }

        // GET api/SpecialOffer/5
        [ResponseType(typeof(SpecialOffer))]
        public IHttpActionResult GetSpecialOffer(int id)
        {
            SpecialOffer specialoffer = db.SpecialOffers.Find(id);
            if (specialoffer == null)
            {
                return NotFound();
            }

            return Ok(specialoffer);
        }

        // PUT api/SpecialOffer/5
        public IHttpActionResult PutSpecialOffer(int id, SpecialOffer specialoffer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specialoffer.SpecialOfferID)
            {
                return BadRequest();
            }

            db.Entry(specialoffer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialOfferExists(id))
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

        // POST api/SpecialOffer
        [ResponseType(typeof(SpecialOffer))]
        public IHttpActionResult PostSpecialOffer(SpecialOffer specialoffer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SpecialOffers.Add(specialoffer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = specialoffer.SpecialOfferID }, specialoffer);
        }

        // DELETE api/SpecialOffer/5
        [ResponseType(typeof(SpecialOffer))]
        public IHttpActionResult DeleteSpecialOffer(int id)
        {
            SpecialOffer specialoffer = db.SpecialOffers.Find(id);
            if (specialoffer == null)
            {
                return NotFound();
            }

            db.SpecialOffers.Remove(specialoffer);
            db.SaveChanges();

            return Ok(specialoffer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpecialOfferExists(int id)
        {
            return db.SpecialOffers.Count(e => e.SpecialOfferID == id) > 0;
        }
    }
}