using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consumer.Client;
using consumer.Models;
using Microsoft.AspNetCore.Mvc;

namespace consumer.Controllers
{
    [Route("api/v1/it")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IItemsClient _client;

        public ValuesController(IItemsClient client)
        {
            _client = client;
        }

        [HttpGet]
        public Task<ActionResult> Get()
        {
            return _client.GetItems();
        }


        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new ActionResult<IEnumerable<string>>(new[] {"vs", "nm"});
        //}

        //[HttpGet("/")]
        //public async Task<ActionResult<Reply>> Get()
        //{
        //    return await _client.GetMessageAsync();
        //}

        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
