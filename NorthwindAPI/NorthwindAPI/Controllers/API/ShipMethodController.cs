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
    public class ShipMethodController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ShipMethod
        public IQueryable<ShipMethod> GetShipMethods()
        {
            return db.ShipMethods;
        }

        // GET api/ShipMethod/5
        [ResponseType(typeof(ShipMethod))]
        public IHttpActionResult GetShipMethod(int id)
        {
            ShipMethod shipmethod = db.ShipMethods.Find(id);
            if (shipmethod == null)
            {
                return NotFound();
            }

            return Ok(shipmethod);
        }

        // PUT api/ShipMethod/5
        public IHttpActionResult PutShipMethod(int id, ShipMethod shipmethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipmethod.ShipMethodID)
            {
                return BadRequest();
            }

            db.Entry(shipmethod).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipMethodExists(id))
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

        // POST api/ShipMethod
        [ResponseType(typeof(ShipMethod))]
        public IHttpActionResult PostShipMethod(ShipMethod shipmethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShipMethods.Add(shipmethod);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shipmethod.ShipMethodID }, shipmethod);
        }

        // DELETE api/ShipMethod/5
        [ResponseType(typeof(ShipMethod))]
        public IHttpActionResult DeleteShipMethod(int id)
        {
            ShipMethod shipmethod = db.ShipMethods.Find(id);
            if (shipmethod == null)
            {
                return NotFound();
            }

            db.ShipMethods.Remove(shipmethod);
            db.SaveChanges();

            return Ok(shipmethod);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShipMethodExists(int id)
        {
            return db.ShipMethods.Count(e => e.ShipMethodID == id) > 0;
        }
    }
}