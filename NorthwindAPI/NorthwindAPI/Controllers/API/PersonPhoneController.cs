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
    public class PersonPhoneController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/PersonPhone
        public IQueryable<PersonPhone> GetPersonPhones()
        {
            return db.PersonPhones;
        }

        // GET api/PersonPhone/5
        [ResponseType(typeof(PersonPhone))]
        public IHttpActionResult GetPersonPhone(int id)
        {
            PersonPhone personphone = db.PersonPhones.Find(id);
            if (personphone == null)
            {
                return NotFound();
            }

            return Ok(personphone);
        }

        // PUT api/PersonPhone/5
        public IHttpActionResult PutPersonPhone(int id, PersonPhone personphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personphone.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(personphone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonPhoneExists(id))
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

        // POST api/PersonPhone
        [ResponseType(typeof(PersonPhone))]
        public IHttpActionResult PostPersonPhone(PersonPhone personphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonPhones.Add(personphone);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonPhoneExists(personphone.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = personphone.BusinessEntityID }, personphone);
        }

        // DELETE api/PersonPhone/5
        [ResponseType(typeof(PersonPhone))]
        public IHttpActionResult DeletePersonPhone(int id)
        {
            PersonPhone personphone = db.PersonPhones.Find(id);
            if (personphone == null)
            {
                return NotFound();
            }

            db.PersonPhones.Remove(personphone);
            db.SaveChanges();

            return Ok(personphone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonPhoneExists(int id)
        {
            return db.PersonPhones.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}