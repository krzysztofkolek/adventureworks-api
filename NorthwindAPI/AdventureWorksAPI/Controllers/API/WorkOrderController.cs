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
    public class WorkOrderController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/WorkOrder
        public IQueryable<WorkOrder> GetWorkOrders()
        {
            return db.WorkOrders;
        }

        // GET api/WorkOrder/5
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult GetWorkOrder(int id)
        {
            WorkOrder workorder = db.WorkOrders.Find(id);
            if (workorder == null)
            {
                return NotFound();
            }

            return Ok(workorder);
        }

        // PUT api/WorkOrder/5
        public IHttpActionResult PutWorkOrder(int id, WorkOrder workorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workorder.WorkOrderID)
            {
                return BadRequest();
            }

            db.Entry(workorder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderExists(id))
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

        // POST api/WorkOrder
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult PostWorkOrder(WorkOrder workorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkOrders.Add(workorder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workorder.WorkOrderID }, workorder);
        }

        // DELETE api/WorkOrder/5
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult DeleteWorkOrder(int id)
        {
            WorkOrder workorder = db.WorkOrders.Find(id);
            if (workorder == null)
            {
                return NotFound();
            }

            db.WorkOrders.Remove(workorder);
            db.SaveChanges();

            return Ok(workorder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrderExists(int id)
        {
            return db.WorkOrders.Count(e => e.WorkOrderID == id) > 0;
        }
    }
}