using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
                APIResponse_with_comms response = new APIResponse_with_comms();
                response.status = "Contacted";
                response.communication = leadComm.communication;
                return StatusCode(202, response); //CUSTOM 202
            }
            else
            {
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
