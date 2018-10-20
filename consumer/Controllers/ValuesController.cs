using System.Collections.Generic;
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
        public async Task<IEnumerable<Item>> Get()
        {
            return await _client.GetItems();
        }

        [HttpGet("{id}")]
        public async Task<Item> Get(int id)
        {
            return await _client.GetItem(id);
        }

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
