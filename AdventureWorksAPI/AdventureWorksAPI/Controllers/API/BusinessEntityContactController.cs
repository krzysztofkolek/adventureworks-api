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
    public class BusinessEntityContactController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/BusinessEntityContact
        public IQueryable<BusinessEntityContact> GetBusinessEntityContacts()
        {
            return db.BusinessEntityContacts;
        }

        // GET api/BusinessEntityContact/5
        [ResponseType(typeof(BusinessEntityContact))]
        public IHttpActionResult GetBusinessEntityContact(int id)
        {
            BusinessEntityContact businessentitycontact = db.BusinessEntityContacts.Find(id);
            if (businessentitycontact == null)
            {
                return NotFound();
            }

            return Ok(businessentitycontact);
        }

        // PUT api/BusinessEntityContact/5
        public IHttpActionResult PutBusinessEntityContact(int id, BusinessEntityContact businessentitycontact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != businessentitycontact.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(businessentitycontact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityContactExists(id))
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

        // POST api/BusinessEntityContact
        [ResponseType(typeof(BusinessEntityContact))]
        public IHttpActionResult PostBusinessEntityContact(BusinessEntityContact businessentitycontact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BusinessEntityContacts.Add(businessentitycontact);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BusinessEntityContactExists(businessentitycontact.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = businessentitycontact.BusinessEntityID }, businessentitycontact);
        }

        // DELETE api/BusinessEntityContact/5
        [ResponseType(typeof(BusinessEntityContact))]
        public IHttpActionResult DeleteBusinessEntityContact(int id)
        {
            BusinessEntityContact businessentitycontact = db.BusinessEntityContacts.Find(id);
            if (businessentitycontact == null)
            {
                return NotFound();
            }

            db.BusinessEntityContacts.Remove(businessentitycontact);
            db.SaveChanges();

            return Ok(businessentitycontact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessEntityContactExists(int id)
        {
            return db.BusinessEntityContacts.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}