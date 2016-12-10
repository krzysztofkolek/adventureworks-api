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
    public class IllustrationController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/Illustration
        public IQueryable<Illustration> GetIllustrations()
        {
            return db.Illustrations;
        }

        // GET api/Illustration/5
        [ResponseType(typeof(Illustration))]
        public IHttpActionResult GetIllustration(int id)
        {
            Illustration illustration = db.Illustrations.Find(id);
            if (illustration == null)
            {
                return NotFound();
            }

            return Ok(illustration);
        }

        // PUT api/Illustration/5
        public IHttpActionResult PutIllustration(int id, Illustration illustration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != illustration.IllustrationID)
            {
                return BadRequest();
            }

            db.Entry(illustration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IllustrationExists(id))
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

        // POST api/Illustration
        [ResponseType(typeof(Illustration))]
        public IHttpActionResult PostIllustration(Illustration illustration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Illustrations.Add(illustration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = illustration.IllustrationID }, illustration);
        }

        // DELETE api/Illustration/5
        [ResponseType(typeof(Illustration))]
        public IHttpActionResult DeleteIllustration(int id)
        {
            Illustration illustration = db.Illustrations.Find(id);
            if (illustration == null)
            {
                return NotFound();
            }

            db.Illustrations.Remove(illustration);
            db.SaveChanges();

            return Ok(illustration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IllustrationExists(int id)
        {
            return db.Illustrations.Count(e => e.IllustrationID == id) > 0;
        }
    }
}