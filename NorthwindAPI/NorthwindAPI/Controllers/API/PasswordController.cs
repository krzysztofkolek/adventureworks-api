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
    public class PasswordController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/Password
        public IQueryable<Password> GetPasswords()
        {
            return db.Passwords;
        }

        // GET api/Password/5
        [ResponseType(typeof(Password))]
        public IHttpActionResult GetPassword(int id)
        {
            Password password = db.Passwords.Find(id);
            if (password == null)
            {
                return NotFound();
            }

            return Ok(password);
        }

        // PUT api/Password/5
        public IHttpActionResult PutPassword(int id, Password password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != password.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(password).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordExists(id))
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

        // POST api/Password
        [ResponseType(typeof(Password))]
        public IHttpActionResult PostPassword(Password password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Passwords.Add(password);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PasswordExists(password.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = password.BusinessEntityID }, password);
        }

        // DELETE api/Password/5
        [ResponseType(typeof(Password))]
        public IHttpActionResult DeletePassword(int id)
        {
            Password password = db.Passwords.Find(id);
            if (password == null)
            {
                return NotFound();
            }

            db.Passwords.Remove(password);
            db.SaveChanges();

            return Ok(password);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PasswordExists(int id)
        {
            return db.Passwords.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}