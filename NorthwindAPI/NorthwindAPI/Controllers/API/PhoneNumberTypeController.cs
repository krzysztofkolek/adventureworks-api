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
    public class PhoneNumberTypeController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/PhoneNumberType
        public IQueryable<PhoneNumberType> GetPhoneNumberTypes()
        {
            return db.PhoneNumberTypes;
        }

        // GET api/PhoneNumberType/5
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult GetPhoneNumberType(int id)
        {
            PhoneNumberType phonenumbertype = db.PhoneNumberTypes.Find(id);
            if (phonenumbertype == null)
            {
                return NotFound();
            }

            return Ok(phonenumbertype);
        }

        // PUT api/PhoneNumberType/5
        public IHttpActionResult PutPhoneNumberType(int id, PhoneNumberType phonenumbertype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phonenumbertype.PhoneNumberTypeID)
            {
                return BadRequest();
            }

            db.Entry(phonenumbertype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberTypeExists(id))
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

        // POST api/PhoneNumberType
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult PostPhoneNumberType(PhoneNumberType phonenumbertype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhoneNumberTypes.Add(phonenumbertype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = phonenumbertype.PhoneNumberTypeID }, phonenumbertype);
        }

        // DELETE api/PhoneNumberType/5
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult DeletePhoneNumberType(int id)
        {
            PhoneNumberType phonenumbertype = db.PhoneNumberTypes.Find(id);
            if (phonenumbertype == null)
            {
                return NotFound();
            }

            db.PhoneNumberTypes.Remove(phonenumbertype);
            db.SaveChanges();

            return Ok(phonenumbertype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneNumberTypeExists(int id)
        {
            return db.PhoneNumberTypes.Count(e => e.PhoneNumberTypeID == id) > 0;
        }
    }
}