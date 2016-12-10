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
    public class CurrencyController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/Currency
        public IQueryable<Currency> GetCurrencies()
        {
            return db.Currencies;
        }

        // GET api/Currency/5
        [ResponseType(typeof(Currency))]
        public IHttpActionResult GetCurrency(string id)
        {
            Currency currency = db.Currencies.Find(id);
            if (currency == null)
            {
                return NotFound();
            }

            return Ok(currency);
        }

        // PUT api/Currency/5
        public IHttpActionResult PutCurrency(string id, Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currency.CurrencyCode)
            {
                return BadRequest();
            }

            db.Entry(currency).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
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

        // POST api/Currency
        [ResponseType(typeof(Currency))]
        public IHttpActionResult PostCurrency(Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Currencies.Add(currency);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CurrencyExists(currency.CurrencyCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = currency.CurrencyCode }, currency);
        }

        // DELETE api/Currency/5
        [ResponseType(typeof(Currency))]
        public IHttpActionResult DeleteCurrency(string id)
        {
            Currency currency = db.Currencies.Find(id);
            if (currency == null)
            {
                return NotFound();
            }

            db.Currencies.Remove(currency);
            db.SaveChanges();

            return Ok(currency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurrencyExists(string id)
        {
            return db.Currencies.Count(e => e.CurrencyCode == id) > 0;
        }
    }
}