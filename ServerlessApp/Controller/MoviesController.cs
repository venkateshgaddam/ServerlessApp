using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerlessApp.DAL;
using ServerlessApp.Models;

namespace ServerlessApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private clsDynamoDB clsDBOperations = new clsDynamoDB();
        // GET: api/Movies
        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
            var strngARRAY = new List<DataRow>();
            strngARRAY = await clsDBOperations.GetMoviesAsync();

            return new JsonResult(strngARRAY);
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "GetDetails")]
        [Route("GetDetails")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Movies
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        // PUT: api/Movies/5
        [HttpPost("{movieName}")]
        public async Task Edit(string movieName, [FromBody] Movies test)
        {
            var Result = await clsDBOperations.GetMoviesAsync(movieName);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{movieName}")]
        public async Task<string> Delete(string movieName)
        {
            string successMessage = "";
            await clsDBOperations.DeleteItem(movieName, successMessage);
            return successMessage;
        }
    }
}
