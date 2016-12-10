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
    public class CountryRegionCurrencyController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/CountryRegionCurrency
        public IQueryable<CountryRegionCurrency> GetCountryRegionCurrencies()
        {
            return db.CountryRegionCurrencies;
        }

        // GET api/CountryRegionCurrency/5
        [ResponseType(typeof(CountryRegionCurrency))]
        public IHttpActionResult GetCountryRegionCurrency(string id)
        {
            CountryRegionCurrency countryregioncurrency = db.CountryRegionCurrencies.Find(id);
            if (countryregioncurrency == null)
            {
                return NotFound();
            }

            return Ok(countryregioncurrency);
        }

        // PUT api/CountryRegionCurrency/5
        public IHttpActionResult PutCountryRegionCurrency(string id, CountryRegionCurrency countryregioncurrency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryregioncurrency.CountryRegionCode)
            {
                return BadRequest();
            }

            db.Entry(countryregioncurrency).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryRegionCurrencyExists(id))
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

        // POST api/CountryRegionCurrency
        [ResponseType(typeof(CountryRegionCurrency))]
        public IHttpActionResult PostCountryRegionCurrency(CountryRegionCurrency countryregioncurrency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CountryRegionCurrencies.Add(countryregioncurrency);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CountryRegionCurrencyExists(countryregioncurrency.CountryRegionCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = countryregioncurrency.CountryRegionCode }, countryregioncurrency);
        }

        // DELETE api/CountryRegionCurrency/5
        [ResponseType(typeof(CountryRegionCurrency))]
        public IHttpActionResult DeleteCountryRegionCurrency(string id)
        {
            CountryRegionCurrency countryregioncurrency = db.CountryRegionCurrencies.Find(id);
            if (countryregioncurrency == null)
            {
                return NotFound();
            }

            db.CountryRegionCurrencies.Remove(countryregioncurrency);
            db.SaveChanges();

            return Ok(countryregioncurrency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryRegionCurrencyExists(string id)
        {
            return db.CountryRegionCurrencies.Count(e => e.CountryRegionCode == id) > 0;
        }
    }
}