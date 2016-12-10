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
    public class BillOfMaterialController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/BillOfMaterial
        public IQueryable<BillOfMaterial> GetBillOfMaterials()
        {
            return db.BillOfMaterials;
        }

        // GET api/BillOfMaterial/5
        [ResponseType(typeof(BillOfMaterial))]
        public IHttpActionResult GetBillOfMaterial(int id)
        {
            BillOfMaterial billofmaterial = db.BillOfMaterials.Find(id);
            if (billofmaterial == null)
            {
                return NotFound();
            }

            return Ok(billofmaterial);
        }

        // PUT api/BillOfMaterial/5
        public IHttpActionResult PutBillOfMaterial(int id, BillOfMaterial billofmaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != billofmaterial.BillOfMaterialsID)
            {
                return BadRequest();
            }

            db.Entry(billofmaterial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillOfMaterialExists(id))
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

        // POST api/BillOfMaterial
        [ResponseType(typeof(BillOfMaterial))]
        public IHttpActionResult PostBillOfMaterial(BillOfMaterial billofmaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BillOfMaterials.Add(billofmaterial);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = billofmaterial.BillOfMaterialsID }, billofmaterial);
        }

        // DELETE api/BillOfMaterial/5
        [ResponseType(typeof(BillOfMaterial))]
        public IHttpActionResult DeleteBillOfMaterial(int id)
        {
            BillOfMaterial billofmaterial = db.BillOfMaterials.Find(id);
            if (billofmaterial == null)
            {
                return NotFound();
            }

            db.BillOfMaterials.Remove(billofmaterial);
            db.SaveChanges();

            return Ok(billofmaterial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BillOfMaterialExists(int id)
        {
            return db.BillOfMaterials.Count(e => e.BillOfMaterialsID == id) > 0;
        }
    }
}