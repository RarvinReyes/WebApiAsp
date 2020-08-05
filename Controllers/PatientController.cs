using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ArvinReyes.Models;

namespace ArvinReyes.Controllers
{
    public class PatientController : ApiController
    {
        private HospitalDBEntities db = new HospitalDBEntities();

        // GET: api/Patient
        public Tuple<DbSet, string> GetPatients()
        {
            DbSet<Patient> patients = db.Patients;
            string mapPath = System.Web.HttpContext.Current.Server.MapPath("/Images/");
            Tuple<DbSet, string> response = new Tuple<DbSet, string> (patients, mapPath);
            return response;
        }

        // GET: api/Patient/5
        [ResponseType(typeof(Patient))]
        public IHttpActionResult GetPatient(Guid id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patient/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatient(Guid id, Patient patient)
        {

            if (id != patient.Id)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PatientExists(id))
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
                else
                {
                    throw ex;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Patient
        [ResponseType(typeof(Patient))]
        public IHttpActionResult PostPatient(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            patient.RecordCreationDate = DateTime.Now;

            db.Patients.Add(patient);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
                if (PatientExists(patient.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw ex;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patient/5
        [ResponseType(typeof(Patient))]
        public IHttpActionResult DeletePatient(Guid id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
            db.SaveChanges();

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(Guid id)
        {
            return db.Patients.Count(e => e.Id == id) > 0;
        }

        [HttpPost]
        [Route("api/UploadFile")]
        public IHttpActionResult UploadImage() {
            var httpRequest = System.Web.HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            if (postedFile != null) {
                string fileName = postedFile.FileName;
                var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Images/" + fileName);
                postedFile.SaveAs(filePath);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}