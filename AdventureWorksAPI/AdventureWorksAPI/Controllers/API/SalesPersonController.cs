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
    public class SalesPersonController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/SalesPerson
        public IQueryable<SalesPerson> GetSalesPersons()
        {
            return db.SalesPersons;
        }

        // GET api/SalesPerson/5
        [ResponseType(typeof(SalesPerson))]
        public IHttpActionResult GetSalesPerson(int id)
        {
            SalesPerson salesperson = db.SalesPersons.Find(id);
            if (salesperson == null)
            {
                return NotFound();
            }

            return Ok(salesperson);
        }

        // PUT api/SalesPerson/5
        public IHttpActionResult PutSalesPerson(int id, SalesPerson salesperson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesperson.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(salesperson).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPersonExists(id))
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

        // POST api/SalesPerson
        [ResponseType(typeof(SalesPerson))]
        public IHttpActionResult PostSalesPerson(SalesPerson salesperson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesPersons.Add(salesperson);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalesPersonExists(salesperson.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salesperson.BusinessEntityID }, salesperson);
        }

        // DELETE api/SalesPerson/5
        [ResponseType(typeof(SalesPerson))]
        public IHttpActionResult DeleteSalesPerson(int id)
        {
            SalesPerson salesperson = db.SalesPersons.Find(id);
            if (salesperson == null)
            {
                return NotFound();
            }

            db.SalesPersons.Remove(salesperson);
            db.SaveChanges();

            return Ok(salesperson);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesPersonExists(int id)
        {
            return db.SalesPersons.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}