using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace dotnet3._1_in_docker.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class Leads : ControllerBase
    { 
        [HttpGet] //api/leads
        public IActionResult Get()
        {
            int exit_code;
            List<Entry> leadList = new List<Entry>();
            leadList = DBControls.loadLeads(out exit_code);
            if(exit_code == 0)
            {
                return Ok(leadList); //200
            }
            else
            {
                APIResponse_with_reason response = new APIResponse_with_reason();
                response.status = "failure";
                response.reason = "invalid input";
                return BadRequest(response); //400
            }
        }

        // GET api/<Leads>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id != null && id > 0)
            {
                int exit_code;
                Entry lead = DBControls.fetchLeadByID(id, out exit_code);
                if (exit_code == 0)
                {
                    return Ok(lead); //200
                }
                else
                {
                    return NotFound("{}"); //404
                }
            }
            else
            {
                APIResponse_with_reason response = new APIResponse_with_reason();
                response.status = "failure";
                response.reason = "invalid parameter";
                return BadRequest(response);//400
            }
        } 

        // POST api/<Leads>
        [HttpPost]
        public IActionResult Post([FromBody] Entry lead)
        {
            int exit_code;            
            DBControls.saveLeads(lead, out exit_code);
            Console.WriteLine("* Exitcode: " + exit_code);
            if (exit_code == 0)
            {               
                return Created("", lead); //201
            } else
            {
                APIResponse_with_reason response = new APIResponse_with_reason();
                response.status = "failure";
                response.reason = "invalid input";
                return BadRequest(response); //400
            }            
        }

        // PUT api/<Leads>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Entry lead)
        {
            int exit_code;
            DBControls.editLeadByID(id, lead, out exit_code);
            Console.WriteLine("* Exitcode: " + exit_code);
            if (exit_code == 0)
            {
                APIResponse response = new APIResponse();
                response.status = "success";
                return StatusCode(202, response); // CUSTOM 202
            } else
            {
                APIResponse_with_reason response = new APIResponse_with_reason();
                response.status = "failure";
                response.reason = "invalid input";
                return BadRequest(response); //400
            }
        }      
        // DELETE api/<Leads>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int exit_code;
            DBControls.removeLeadByID(id, out exit_code);
            Console.WriteLine("* Exitcode: " + exit_code);
            if(exit_code == 0)
            {
                APIResponse response = new APIResponse();
                response.status = "success";
                return Ok(response); //200
            } else
            {
                APIResponse_with_reason response = new APIResponse_with_reason();
                response.status = "failure";
                response.reason = "invalid input";
                return BadRequest(response); //400
            }        
        }


    }
}
