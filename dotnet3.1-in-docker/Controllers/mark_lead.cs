using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet3._1_in_docker.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class mark_lead : ControllerBase
    {
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EntryBase leadComm)
        {
            int exit_code;
            DBControls.markLeadByID(id, leadComm.communication, out exit_code);
            Console.WriteLine("* Exitcode: " + exit_code);
            if (exit_code == 0)
            {
                /* MOHLO BYT UDELANO TAKTO A APIResponse TRIDY ODSTRANENY
                 var response = new {status="Contacted", communication=leadComm.communication}
                 */
                APIResponse_with_comms response = new APIResponse_with_comms();
                response.status = "Contacted";
                response.communication = leadComm.communication;
                return StatusCode(202, response); //CUSTOM 202
            }
            else
            {
                /* MOHLO BYT UDELANO TAKTO A APIResponse TRIDY ODSTRANENY
                 var response = new {status="failure", reason="invalid input"}
                 */
                APIResponse_with_reason response = new APIResponse_with_reason();
                response.status = "failure";
                response.reason = "invalid input";
                return BadRequest(response); //400
            }
        }


        [HttpGet]
        public string Get()
        {
            return "use api/mark_lead/'id'";
        }


    }
}
