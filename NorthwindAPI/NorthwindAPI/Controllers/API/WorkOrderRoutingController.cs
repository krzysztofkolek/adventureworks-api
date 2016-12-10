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
    public class WorkOrderRoutingController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/WorkOrderRouting
        public IQueryable<WorkOrderRouting> GetWorkOrderRoutings()
        {
            return db.WorkOrderRoutings;
        }

        // GET api/WorkOrderRouting/5
        [ResponseType(typeof(WorkOrderRouting))]
        public IHttpActionResult GetWorkOrderRouting(int id)
        {
            WorkOrderRouting workorderrouting = db.WorkOrderRoutings.Find(id);
            if (workorderrouting == null)
            {
                return NotFound();
            }

            return Ok(workorderrouting);
        }

        // PUT api/WorkOrderRouting/5
        public IHttpActionResult PutWorkOrderRouting(int id, WorkOrderRouting workorderrouting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workorderrouting.WorkOrderID)
            {
                return BadRequest();
            }

            db.Entry(workorderrouting).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderRoutingExists(id))
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

        // POST api/WorkOrderRouting
        [ResponseType(typeof(WorkOrderRouting))]
        public IHttpActionResult PostWorkOrderRouting(WorkOrderRouting workorderrouting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkOrderRoutings.Add(workorderrouting);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WorkOrderRoutingExists(workorderrouting.WorkOrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = workorderrouting.WorkOrderID }, workorderrouting);
        }

        // DELETE api/WorkOrderRouting/5
        [ResponseType(typeof(WorkOrderRouting))]
        public IHttpActionResult DeleteWorkOrderRouting(int id)
        {
            WorkOrderRouting workorderrouting = db.WorkOrderRoutings.Find(id);
            if (workorderrouting == null)
            {
                return NotFound();
            }

            db.WorkOrderRoutings.Remove(workorderrouting);
            db.SaveChanges();

            return Ok(workorderrouting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrderRoutingExists(int id)
        {
            return db.WorkOrderRoutings.Count(e => e.WorkOrderID == id) > 0;
        }
    }
}