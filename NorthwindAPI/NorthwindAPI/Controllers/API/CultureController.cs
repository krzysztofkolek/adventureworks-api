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
    public class CultureController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/Culture
        public IQueryable<Culture> GetCultures()
        {
            return db.Cultures;
        }

        // GET api/Culture/5
        [ResponseType(typeof(Culture))]
        public IHttpActionResult GetCulture(string id)
        {
            Culture culture = db.Cultures.Find(id);
            if (culture == null)
            {
                return NotFound();
            }

            return Ok(culture);
        }

        // PUT api/Culture/5
        public IHttpActionResult PutCulture(string id, Culture culture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != culture.CultureID)
            {
                return BadRequest();
            }

            db.Entry(culture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CultureExists(id))
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

        // POST api/Culture
        [ResponseType(typeof(Culture))]
        public IHttpActionResult PostCulture(Culture culture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cultures.Add(culture);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CultureExists(culture.CultureID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = culture.CultureID }, culture);
        }

        // DELETE api/Culture/5
        [ResponseType(typeof(Culture))]
        public IHttpActionResult DeleteCulture(string id)
        {
            Culture culture = db.Cultures.Find(id);
            if (culture == null)
            {
                return NotFound();
            }

            db.Cultures.Remove(culture);
            db.SaveChanges();

            return Ok(culture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CultureExists(string id)
        {
            return db.Cultures.Count(e => e.CultureID == id) > 0;
        }
    }
}