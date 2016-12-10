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
    public class StateProvinceController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/StateProvince
        public IQueryable<StateProvince> GetStateProvinces()
        {
            return db.StateProvinces;
        }

        // GET api/StateProvince/5
        [ResponseType(typeof(StateProvince))]
        public IHttpActionResult GetStateProvince(int id)
        {
            StateProvince stateprovince = db.StateProvinces.Find(id);
            if (stateprovince == null)
            {
                return NotFound();
            }

            return Ok(stateprovince);
        }

        // PUT api/StateProvince/5
        public IHttpActionResult PutStateProvince(int id, StateProvince stateprovince)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stateprovince.StateProvinceID)
            {
                return BadRequest();
            }

            db.Entry(stateprovince).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateProvinceExists(id))
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

        // POST api/StateProvince
        [ResponseType(typeof(StateProvince))]
        public IHttpActionResult PostStateProvince(StateProvince stateprovince)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StateProvinces.Add(stateprovince);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stateprovince.StateProvinceID }, stateprovince);
        }

        // DELETE api/StateProvince/5
        [ResponseType(typeof(StateProvince))]
        public IHttpActionResult DeleteStateProvince(int id)
        {
            StateProvince stateprovince = db.StateProvinces.Find(id);
            if (stateprovince == null)
            {
                return NotFound();
            }

            db.StateProvinces.Remove(stateprovince);
            db.SaveChanges();

            return Ok(stateprovince);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateProvinceExists(int id)
        {
            return db.StateProvinces.Count(e => e.StateProvinceID == id) > 0;
        }
    }
}