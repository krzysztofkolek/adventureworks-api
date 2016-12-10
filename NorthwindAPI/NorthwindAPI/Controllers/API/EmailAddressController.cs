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
    public class EmailAddressController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/EmailAddress
        public IQueryable<EmailAddress> GetEmailAddresses()
        {
            return db.EmailAddresses;
        }

        // GET api/EmailAddress/5
        [ResponseType(typeof(EmailAddress))]
        public IHttpActionResult GetEmailAddress(int id)
        {
            EmailAddress emailaddress = db.EmailAddresses.Find(id);
            if (emailaddress == null)
            {
                return NotFound();
            }

            return Ok(emailaddress);
        }

        // PUT api/EmailAddress/5
        public IHttpActionResult PutEmailAddress(int id, EmailAddress emailaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emailaddress.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(emailaddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailAddressExists(id))
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

        // POST api/EmailAddress
        [ResponseType(typeof(EmailAddress))]
        public IHttpActionResult PostEmailAddress(EmailAddress emailaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmailAddresses.Add(emailaddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmailAddressExists(emailaddress.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = emailaddress.BusinessEntityID }, emailaddress);
        }

        // DELETE api/EmailAddress/5
        [ResponseType(typeof(EmailAddress))]
        public IHttpActionResult DeleteEmailAddress(int id)
        {
            EmailAddress emailaddress = db.EmailAddresses.Find(id);
            if (emailaddress == null)
            {
                return NotFound();
            }

            db.EmailAddresses.Remove(emailaddress);
            db.SaveChanges();

            return Ok(emailaddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmailAddressExists(int id)
        {
            return db.EmailAddresses.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}