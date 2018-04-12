using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FHIR.CutomActions
{
    public class FhirJsonResult : IActionResult
    {
        private readonly string _json;

        public FhirJsonResult(Resource resource)
        {
            FhirJsonSerializer serializer = new FhirJsonSerializer();
            _json = serializer.SerializeToString(resource);
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(ActionContext context)
        {
            var bytes = Encoding.UTF8.GetBytes(_json);

            context.HttpContext.Response.ContentType = "application/json";

            await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            await context.HttpContext.Response.Body.FlushAsync();
        }
    }
}
