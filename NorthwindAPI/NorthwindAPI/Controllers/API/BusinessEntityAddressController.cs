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
    public class BusinessEntityAddressController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/BusinessEntityAddress
        public IQueryable<BusinessEntityAddress> GetBusinessEntityAddresses()
        {
            return db.BusinessEntityAddresses;
        }

        // GET api/BusinessEntityAddress/5
        [ResponseType(typeof(BusinessEntityAddress))]
        public IHttpActionResult GetBusinessEntityAddress(int id)
        {
            BusinessEntityAddress businessentityaddress = db.BusinessEntityAddresses.Find(id);
            if (businessentityaddress == null)
            {
                return NotFound();
            }

            return Ok(businessentityaddress);
        }

        // PUT api/BusinessEntityAddress/5
        public IHttpActionResult PutBusinessEntityAddress(int id, BusinessEntityAddress businessentityaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != businessentityaddress.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(businessentityaddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityAddressExists(id))
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

        // POST api/BusinessEntityAddress
        [ResponseType(typeof(BusinessEntityAddress))]
        public IHttpActionResult PostBusinessEntityAddress(BusinessEntityAddress businessentityaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BusinessEntityAddresses.Add(businessentityaddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BusinessEntityAddressExists(businessentityaddress.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = businessentityaddress.BusinessEntityID }, businessentityaddress);
        }

        // DELETE api/BusinessEntityAddress/5
        [ResponseType(typeof(BusinessEntityAddress))]
        public IHttpActionResult DeleteBusinessEntityAddress(int id)
        {
            BusinessEntityAddress businessentityaddress = db.BusinessEntityAddresses.Find(id);
            if (businessentityaddress == null)
            {
                return NotFound();
            }

            db.BusinessEntityAddresses.Remove(businessentityaddress);
            db.SaveChanges();

            return Ok(businessentityaddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessEntityAddressExists(int id)
        {
            return db.BusinessEntityAddresses.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}