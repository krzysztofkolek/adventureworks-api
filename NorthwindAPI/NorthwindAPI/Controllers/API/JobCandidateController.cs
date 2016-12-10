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
    public class JobCandidateController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/JobCandidate
        public IQueryable<JobCandidate> GetJobCandidates()
        {
            return db.JobCandidates;
        }

        // GET api/JobCandidate/5
        [ResponseType(typeof(JobCandidate))]
        public IHttpActionResult GetJobCandidate(int id)
        {
            JobCandidate jobcandidate = db.JobCandidates.Find(id);
            if (jobcandidate == null)
            {
                return NotFound();
            }

            return Ok(jobcandidate);
        }

        // PUT api/JobCandidate/5
        public IHttpActionResult PutJobCandidate(int id, JobCandidate jobcandidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobcandidate.JobCandidateID)
            {
                return BadRequest();
            }

            db.Entry(jobcandidate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCandidateExists(id))
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

        // POST api/JobCandidate
        [ResponseType(typeof(JobCandidate))]
        public IHttpActionResult PostJobCandidate(JobCandidate jobcandidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobCandidates.Add(jobcandidate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobcandidate.JobCandidateID }, jobcandidate);
        }

        // DELETE api/JobCandidate/5
        [ResponseType(typeof(JobCandidate))]
        public IHttpActionResult DeleteJobCandidate(int id)
        {
            JobCandidate jobcandidate = db.JobCandidates.Find(id);
            if (jobcandidate == null)
            {
                return NotFound();
            }

            db.JobCandidates.Remove(jobcandidate);
            db.SaveChanges();

            return Ok(jobcandidate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobCandidateExists(int id)
        {
            return db.JobCandidates.Count(e => e.JobCandidateID == id) > 0;
        }
    }
}