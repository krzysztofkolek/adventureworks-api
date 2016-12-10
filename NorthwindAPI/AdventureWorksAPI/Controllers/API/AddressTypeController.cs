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
    public class AddressTypeController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/AddressType
        public IQueryable<AddressType> GetAddressTypes()
        {
            return db.AddressTypes;
        }

        // GET api/AddressType/5
        [ResponseType(typeof(AddressType))]
        public IHttpActionResult GetAddressType(int id)
        {
            AddressType addresstype = db.AddressTypes.Find(id);
            if (addresstype == null)
            {
                return NotFound();
            }

            return Ok(addresstype);
        }

        // PUT api/AddressType/5
        public IHttpActionResult PutAddressType(int id, AddressType addresstype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addresstype.AddressTypeID)
            {
                return BadRequest();
            }

            db.Entry(addresstype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        // POST api/AddressType
        [ResponseType(typeof(AddressType))]
        public IHttpActionResult PostAddressType(AddressType addresstype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AddressTypes.Add(addresstype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = addresstype.AddressTypeID }, addresstype);
        }

        // DELETE api/AddressType/5
        [ResponseType(typeof(AddressType))]
        public IHttpActionResult DeleteAddressType(int id)
        {
            AddressType addresstype = db.AddressTypes.Find(id);
            if (addresstype == null)
            {
                return NotFound();
            }

            db.AddressTypes.Remove(addresstype);
            db.SaveChanges();

            return Ok(addresstype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressTypeExists(int id)
        {
            return db.AddressTypes.Count(e => e.AddressTypeID == id) > 0;
        }
    }
}