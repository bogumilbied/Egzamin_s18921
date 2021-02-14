using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw11.Models;
using cw11.Request;
using Microsoft.AspNetCore.Mvc;

namespace Egzamin_s18921
{
    public class HospitalController : ControllerBase
    {
        private readonly HospitalDbContext hospitalDbContext;

        public HospitalController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }

        

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            if (hospitalDbContext.Doctor.Where(d => d.IdDoctor == id).Any())
            {
                var doctor = hospitalDbContext.Doctor.SingleOrDefault(d => d.IdDoctor == id);
                hospitalDbContext.Doctor.Remove(doctor);
                hospitalDbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            if (hospitalDbContext.Patient.Where(d => d.IdPatient == id).Any())
            {

                var prescription = hospitalDbContext.Prescription.SingleOrDefault(d => d.IdPatient == id);

                var prescriptionmedicament = hospitalDbContext.Prescription_Medicament.Where(d => d.IdPrescription == prescription.IdPrescription).ToList();

                var patient = hospitalDbContext.Patient.SingleOrDefault(d => d.IdPatient == id);
                foreach (var item in prescriptionmedicament)
                {
                    hospitalDbContext.Prescription_Medicament.Remove(item);
                }
                hospitalDbContext.Prescription.Remove(presc);
                hospitalDbContext.Patient.Remove(patient);
                hospitalDbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            if (hospitalDbContext.Doctor.Where(d => d.IdDoctor == id).Any())
            {
                var doctor = hospitalDbContext.Doctor.SingleOrDefault(d => d.IdDoctor == id);
                return Ok(doctor);
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult AddDoctor(AddDoctor addDoctor)
        {
            if (!(hospitalDbContext.Doctor.Where(d => d.IdDoctor == addDoctor.IdDoctor).Any()))
            {
                var doctor = new Doctor
                {
                    IdDoctor = addDoctor.IdDoctor,
                    FirstName = addDoctor.FirstName,
                    LastName = addDoctor.LastName,
                    Email = addDoctor.Email
                };
                hospitalDbContext.Doctor.Add(doctor);
                hospitalDbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}