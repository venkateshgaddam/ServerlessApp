using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ServerlessApp.DAL;
using Newtonsoft.Json;

namespace ServerlessApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EC2Controller : ControllerBase
    {
        // GET: api/EC2
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EC2/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EC2
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpGet]
        [Route("Create")]
        public string CreateEC2() {

            clsCreateEC2 clsCreateEC2 = new clsCreateEC2();
            return clsCreateEC2.CreateInstance();
        }

        // PUT: api/EC2/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
