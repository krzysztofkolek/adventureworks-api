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
    public class CountryRegionController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/CountryRegion
        public IQueryable<CountryRegion> GetCountryRegions()
        {
            return db.CountryRegions;
        }

        // GET api/CountryRegion/5
        [ResponseType(typeof(CountryRegion))]
        public IHttpActionResult GetCountryRegion(string id)
        {
            CountryRegion countryregion = db.CountryRegions.Find(id);
            if (countryregion == null)
            {
                return NotFound();
            }

            return Ok(countryregion);
        }

        // PUT api/CountryRegion/5
        public IHttpActionResult PutCountryRegion(string id, CountryRegion countryregion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryregion.CountryRegionCode)
            {
                return BadRequest();
            }

            db.Entry(countryregion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryRegionExists(id))
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

        // POST api/CountryRegion
        [ResponseType(typeof(CountryRegion))]
        public IHttpActionResult PostCountryRegion(CountryRegion countryregion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CountryRegions.Add(countryregion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CountryRegionExists(countryregion.CountryRegionCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = countryregion.CountryRegionCode }, countryregion);
        }

        // DELETE api/CountryRegion/5
        [ResponseType(typeof(CountryRegion))]
        public IHttpActionResult DeleteCountryRegion(string id)
        {
            CountryRegion countryregion = db.CountryRegions.Find(id);
            if (countryregion == null)
            {
                return NotFound();
            }

            db.CountryRegions.Remove(countryregion);
            db.SaveChanges();

            return Ok(countryregion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryRegionExists(string id)
        {
            return db.CountryRegions.Count(e => e.CountryRegionCode == id) > 0;
        }
    }
}