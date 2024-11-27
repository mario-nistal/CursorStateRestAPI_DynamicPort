using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace YourNamespace.Controllers
{
    public class MemoryController : ApiController
    {
        [HttpGet]
        [Route("api/memory")]
        public IHttpActionResult GetMemoryUsage(string process)
        {
            try
            {
                var processInstance = Process.GetProcessesByName(process).FirstOrDefault();
                if (processInstance == null)
                {
                    return NotFound();
                }

                // Use PerformanceCounter to get "Working Set - Private" memory usage
                var counter = new PerformanceCounter("Process", "Working Set - Private", processInstance.ProcessName, true);
                float privateMemoryBytes = counter.NextValue();

                // Convert bytes to megabytes and round to 2 decimal places
                double privateMemoryMB = Math.Round(privateMemoryBytes / (1024.0 * 1024.0), 2);

                return Ok(new { ProcessName = process, PrivateMemoryMB = privateMemoryMB });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}