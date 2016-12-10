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
    public class EmployeePayHistoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/EmployeePayHistory
        public IQueryable<EmployeePayHistory> GetEmployeePayHistories()
        {
            return db.EmployeePayHistories;
        }

        // GET api/EmployeePayHistory/5
        [ResponseType(typeof(EmployeePayHistory))]
        public IHttpActionResult GetEmployeePayHistory(int id)
        {
            EmployeePayHistory employeepayhistory = db.EmployeePayHistories.Find(id);
            if (employeepayhistory == null)
            {
                return NotFound();
            }

            return Ok(employeepayhistory);
        }

        // PUT api/EmployeePayHistory/5
        public IHttpActionResult PutEmployeePayHistory(int id, EmployeePayHistory employeepayhistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeepayhistory.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(employeepayhistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeePayHistoryExists(id))
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

        // POST api/EmployeePayHistory
        [ResponseType(typeof(EmployeePayHistory))]
        public IHttpActionResult PostEmployeePayHistory(EmployeePayHistory employeepayhistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeePayHistories.Add(employeepayhistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeePayHistoryExists(employeepayhistory.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employeepayhistory.BusinessEntityID }, employeepayhistory);
        }

        // DELETE api/EmployeePayHistory/5
        [ResponseType(typeof(EmployeePayHistory))]
        public IHttpActionResult DeleteEmployeePayHistory(int id)
        {
            EmployeePayHistory employeepayhistory = db.EmployeePayHistories.Find(id);
            if (employeepayhistory == null)
            {
                return NotFound();
            }

            db.EmployeePayHistories.Remove(employeepayhistory);
            db.SaveChanges();

            return Ok(employeepayhistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeePayHistoryExists(int id)
        {
            return db.EmployeePayHistories.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}