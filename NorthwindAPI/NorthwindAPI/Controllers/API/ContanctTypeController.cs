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
    public class ContanctTypeController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ContanctType
        public IQueryable<ContactType> GetContactTypes()
        {
            return db.ContactTypes;
        }

        // GET api/ContanctType/5
        [ResponseType(typeof(ContactType))]
        public IHttpActionResult GetContactType(int id)
        {
            ContactType contacttype = db.ContactTypes.Find(id);
            if (contacttype == null)
            {
                return NotFound();
            }

            return Ok(contacttype);
        }

        // PUT api/ContanctType/5
        public IHttpActionResult PutContactType(int id, ContactType contacttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacttype.ContactTypeID)
            {
                return BadRequest();
            }

            db.Entry(contacttype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(id))
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

        // POST api/ContanctType
        [ResponseType(typeof(ContactType))]
        public IHttpActionResult PostContactType(ContactType contacttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactTypes.Add(contacttype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contacttype.ContactTypeID }, contacttype);
        }

        // DELETE api/ContanctType/5
        [ResponseType(typeof(ContactType))]
        public IHttpActionResult DeleteContactType(int id)
        {
            ContactType contacttype = db.ContactTypes.Find(id);
            if (contacttype == null)
            {
                return NotFound();
            }

            db.ContactTypes.Remove(contacttype);
            db.SaveChanges();

            return Ok(contacttype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactTypeExists(int id)
        {
            return db.ContactTypes.Count(e => e.ContactTypeID == id) > 0;
        }
    }
}