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
    public class CurrencyRateController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/CurrencyRate
        public IQueryable<CurrencyRate> GetCurrencyRates()
        {
            return db.CurrencyRates;
        }

        // GET api/CurrencyRate/5
        [ResponseType(typeof(CurrencyRate))]
        public IHttpActionResult GetCurrencyRate(int id)
        {
            CurrencyRate currencyrate = db.CurrencyRates.Find(id);
            if (currencyrate == null)
            {
                return NotFound();
            }

            return Ok(currencyrate);
        }

        // PUT api/CurrencyRate/5
        public IHttpActionResult PutCurrencyRate(int id, CurrencyRate currencyrate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currencyrate.CurrencyRateID)
            {
                return BadRequest();
            }

            db.Entry(currencyrate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyRateExists(id))
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

        // POST api/CurrencyRate
        [ResponseType(typeof(CurrencyRate))]
        public IHttpActionResult PostCurrencyRate(CurrencyRate currencyrate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CurrencyRates.Add(currencyrate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = currencyrate.CurrencyRateID }, currencyrate);
        }

        // DELETE api/CurrencyRate/5
        [ResponseType(typeof(CurrencyRate))]
        public IHttpActionResult DeleteCurrencyRate(int id)
        {
            CurrencyRate currencyrate = db.CurrencyRates.Find(id);
            if (currencyrate == null)
            {
                return NotFound();
            }

            db.CurrencyRates.Remove(currencyrate);
            db.SaveChanges();

            return Ok(currencyrate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurrencyRateExists(int id)
        {
            return db.CurrencyRates.Count(e => e.CurrencyRateID == id) > 0;
        }
    }
}