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
    public class PersonCreditCardController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/PersonCreditCard
        public IQueryable<PersonCreditCard> GetPersonCreditCards()
        {
            return db.PersonCreditCards;
        }

        // GET api/PersonCreditCard/5
        [ResponseType(typeof(PersonCreditCard))]
        public IHttpActionResult GetPersonCreditCard(int id)
        {
            PersonCreditCard personcreditcard = db.PersonCreditCards.Find(id);
            if (personcreditcard == null)
            {
                return NotFound();
            }

            return Ok(personcreditcard);
        }

        // PUT api/PersonCreditCard/5
        public IHttpActionResult PutPersonCreditCard(int id, PersonCreditCard personcreditcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personcreditcard.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(personcreditcard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonCreditCardExists(id))
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

        // POST api/PersonCreditCard
        [ResponseType(typeof(PersonCreditCard))]
        public IHttpActionResult PostPersonCreditCard(PersonCreditCard personcreditcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonCreditCards.Add(personcreditcard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonCreditCardExists(personcreditcard.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = personcreditcard.BusinessEntityID }, personcreditcard);
        }

        // DELETE api/PersonCreditCard/5
        [ResponseType(typeof(PersonCreditCard))]
        public IHttpActionResult DeletePersonCreditCard(int id)
        {
            PersonCreditCard personcreditcard = db.PersonCreditCards.Find(id);
            if (personcreditcard == null)
            {
                return NotFound();
            }

            db.PersonCreditCards.Remove(personcreditcard);
            db.SaveChanges();

            return Ok(personcreditcard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonCreditCardExists(int id)
        {
            return db.PersonCreditCards.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}