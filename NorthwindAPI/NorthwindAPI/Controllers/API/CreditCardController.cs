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
    public class CreditCardController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/CreditCard
        public IQueryable<CreditCard> GetCreditCards()
        {
            return db.CreditCards;
        }

        // GET api/CreditCard/5
        [ResponseType(typeof(CreditCard))]
        public IHttpActionResult GetCreditCard(int id)
        {
            CreditCard creditcard = db.CreditCards.Find(id);
            if (creditcard == null)
            {
                return NotFound();
            }

            return Ok(creditcard);
        }

        // PUT api/CreditCard/5
        public IHttpActionResult PutCreditCard(int id, CreditCard creditcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != creditcard.CreditCardID)
            {
                return BadRequest();
            }

            db.Entry(creditcard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardExists(id))
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

        // POST api/CreditCard
        [ResponseType(typeof(CreditCard))]
        public IHttpActionResult PostCreditCard(CreditCard creditcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CreditCards.Add(creditcard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = creditcard.CreditCardID }, creditcard);
        }

        // DELETE api/CreditCard/5
        [ResponseType(typeof(CreditCard))]
        public IHttpActionResult DeleteCreditCard(int id)
        {
            CreditCard creditcard = db.CreditCards.Find(id);
            if (creditcard == null)
            {
                return NotFound();
            }

            db.CreditCards.Remove(creditcard);
            db.SaveChanges();

            return Ok(creditcard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CreditCardExists(int id)
        {
            return db.CreditCards.Count(e => e.CreditCardID == id) > 0;
        }
    }
}