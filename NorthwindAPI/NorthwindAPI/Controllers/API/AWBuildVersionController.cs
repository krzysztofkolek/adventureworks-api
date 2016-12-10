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
    public class AWBuildVersionController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/AWBuildVersion
        public IQueryable<AWBuildVersion> GetAWBuildVersions()
        {
            return db.AWBuildVersions;
        }

        // GET api/AWBuildVersion/5
        [ResponseType(typeof(AWBuildVersion))]
        public IHttpActionResult GetAWBuildVersion(byte id)
        {
            AWBuildVersion awbuildversion = db.AWBuildVersions.Find(id);
            if (awbuildversion == null)
            {
                return NotFound();
            }

            return Ok(awbuildversion);
        }

        // PUT api/AWBuildVersion/5
        public IHttpActionResult PutAWBuildVersion(byte id, AWBuildVersion awbuildversion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != awbuildversion.SystemInformationID)
            {
                return BadRequest();
            }

            db.Entry(awbuildversion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AWBuildVersionExists(id))
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

        // POST api/AWBuildVersion
        [ResponseType(typeof(AWBuildVersion))]
        public IHttpActionResult PostAWBuildVersion(AWBuildVersion awbuildversion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AWBuildVersions.Add(awbuildversion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = awbuildversion.SystemInformationID }, awbuildversion);
        }

        // DELETE api/AWBuildVersion/5
        [ResponseType(typeof(AWBuildVersion))]
        public IHttpActionResult DeleteAWBuildVersion(byte id)
        {
            AWBuildVersion awbuildversion = db.AWBuildVersions.Find(id);
            if (awbuildversion == null)
            {
                return NotFound();
            }

            db.AWBuildVersions.Remove(awbuildversion);
            db.SaveChanges();

            return Ok(awbuildversion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AWBuildVersionExists(byte id)
        {
            return db.AWBuildVersions.Count(e => e.SystemInformationID == id) > 0;
        }
    }
}