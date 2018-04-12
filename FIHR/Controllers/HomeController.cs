using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using FHIR.CutomActions;

namespace FHIR.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public string Index()
        {
            return "FIHR API";
        }
        
        [Route("Patient/{id}")]
        public IActionResult ServeClient(string id)
        {
            // Query database etc etc etc. See Patient rvrbk as the result of this
            Patient patient = new Patient();

            patient.Id = "rvrbk";
            patient.Gender = AdministrativeGender.Male;
            patient.Name = new List<HumanName>
            {
                new HumanName
                {
                    Family = "Verbeek",
                    Given = new List<string>
                    {
                        "Rik"
                    },
                    Suffix = new List<string>
                    {
                        "The Small"
                    }
                }
            };
            patient.BirthDate = "2004-03-10";
            patient.Active = true;

            return new FhirJsonResult(patient); 
        }

        [Route("get/{id}")]
        public Patient GetClient(string id)
        {
            FhirClient client = new FhirClient("http://localhost:54240/");

            Patient patient = client.Read<Patient>($"Patient/{id}");

            return patient;
        }
    }
}