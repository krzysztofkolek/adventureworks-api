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
    public class UnitMesureController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/UnitMesure
        public IQueryable<UnitMeasure> GetUnitMeasures()
        {
            return db.UnitMeasures;
        }

        // GET api/UnitMesure/5
        [ResponseType(typeof(UnitMeasure))]
        public IHttpActionResult GetUnitMeasure(string id)
        {
            UnitMeasure unitmeasure = db.UnitMeasures.Find(id);
            if (unitmeasure == null)
            {
                return NotFound();
            }

            return Ok(unitmeasure);
        }

        // PUT api/UnitMesure/5
        public IHttpActionResult PutUnitMeasure(string id, UnitMeasure unitmeasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unitmeasure.UnitMeasureCode)
            {
                return BadRequest();
            }

            db.Entry(unitmeasure).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitMeasureExists(id))
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

        // POST api/UnitMesure
        [ResponseType(typeof(UnitMeasure))]
        public IHttpActionResult PostUnitMeasure(UnitMeasure unitmeasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UnitMeasures.Add(unitmeasure);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UnitMeasureExists(unitmeasure.UnitMeasureCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = unitmeasure.UnitMeasureCode }, unitmeasure);
        }

        // DELETE api/UnitMesure/5
        [ResponseType(typeof(UnitMeasure))]
        public IHttpActionResult DeleteUnitMeasure(string id)
        {
            UnitMeasure unitmeasure = db.UnitMeasures.Find(id);
            if (unitmeasure == null)
            {
                return NotFound();
            }

            db.UnitMeasures.Remove(unitmeasure);
            db.SaveChanges();

            return Ok(unitmeasure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UnitMeasureExists(string id)
        {
            return db.UnitMeasures.Count(e => e.UnitMeasureCode == id) > 0;
        }
    }
}