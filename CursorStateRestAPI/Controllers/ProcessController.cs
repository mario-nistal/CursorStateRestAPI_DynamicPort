using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace YourNamespace.Controllers
{
    public class ProcessController : ApiController
    {
        [HttpGet]
        [Route("api/process")]
        public IHttpActionResult GetCpuUsage(string process, int pollTime = 1000)
        {
            try
            {
                var processInstance = Process.GetProcessesByName(process).FirstOrDefault();
                if (processInstance == null)
                {
                    return NotFound();
                }

                var cpuCounter = new PerformanceCounter("Process", "% Processor Time", processInstance.ProcessName, true);
                cpuCounter.NextValue(); // Call once to initialize
                System.Threading.Thread.Sleep(pollTime); // Wait for the specified time to get a valid reading
                float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;

                return Ok(new { ProcessName = process, CpuUsage = cpuUsage });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}