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
    public class BusinessEntityController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/BusinessEntity
        public IQueryable<BusinessEntity> GetBusinessEntities()
        {
            return db.BusinessEntities;
        }

        // GET api/BusinessEntity/5
        [ResponseType(typeof(BusinessEntity))]
        public IHttpActionResult GetBusinessEntity(int id)
        {
            BusinessEntity businessentity = db.BusinessEntities.Find(id);
            if (businessentity == null)
            {
                return NotFound();
            }

            return Ok(businessentity);
        }

        // PUT api/BusinessEntity/5
        public IHttpActionResult PutBusinessEntity(int id, BusinessEntity businessentity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != businessentity.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(businessentity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityExists(id))
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

        // POST api/BusinessEntity
        [ResponseType(typeof(BusinessEntity))]
        public IHttpActionResult PostBusinessEntity(BusinessEntity businessentity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BusinessEntities.Add(businessentity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = businessentity.BusinessEntityID }, businessentity);
        }

        // DELETE api/BusinessEntity/5
        [ResponseType(typeof(BusinessEntity))]
        public IHttpActionResult DeleteBusinessEntity(int id)
        {
            BusinessEntity businessentity = db.BusinessEntities.Find(id);
            if (businessentity == null)
            {
                return NotFound();
            }

            db.BusinessEntities.Remove(businessentity);
            db.SaveChanges();

            return Ok(businessentity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessEntityExists(int id)
        {
            return db.BusinessEntities.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}