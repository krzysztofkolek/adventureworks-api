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
    public class EmployeeDepartmentHistoryController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/EmployeeDepartmentHistory
        public IQueryable<EmployeeDepartmentHistory> GetEmployeeDepartmentHistories()
        {
            return db.EmployeeDepartmentHistories;
        }

        // GET api/EmployeeDepartmentHistory/5
        [ResponseType(typeof(EmployeeDepartmentHistory))]
        public IHttpActionResult GetEmployeeDepartmentHistory(int id)
        {
            EmployeeDepartmentHistory employeedepartmenthistory = db.EmployeeDepartmentHistories.Find(id);
            if (employeedepartmenthistory == null)
            {
                return NotFound();
            }

            return Ok(employeedepartmenthistory);
        }

        // PUT api/EmployeeDepartmentHistory/5
        public IHttpActionResult PutEmployeeDepartmentHistory(int id, EmployeeDepartmentHistory employeedepartmenthistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeedepartmenthistory.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(employeedepartmenthistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDepartmentHistoryExists(id))
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

        // POST api/EmployeeDepartmentHistory
        [ResponseType(typeof(EmployeeDepartmentHistory))]
        public IHttpActionResult PostEmployeeDepartmentHistory(EmployeeDepartmentHistory employeedepartmenthistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeDepartmentHistories.Add(employeedepartmenthistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeDepartmentHistoryExists(employeedepartmenthistory.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employeedepartmenthistory.BusinessEntityID }, employeedepartmenthistory);
        }

        // DELETE api/EmployeeDepartmentHistory/5
        [ResponseType(typeof(EmployeeDepartmentHistory))]
        public IHttpActionResult DeleteEmployeeDepartmentHistory(int id)
        {
            EmployeeDepartmentHistory employeedepartmenthistory = db.EmployeeDepartmentHistories.Find(id);
            if (employeedepartmenthistory == null)
            {
                return NotFound();
            }

            db.EmployeeDepartmentHistories.Remove(employeedepartmenthistory);
            db.SaveChanges();

            return Ok(employeedepartmenthistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeDepartmentHistoryExists(int id)
        {
            return db.EmployeeDepartmentHistories.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}