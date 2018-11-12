using consumer.Client;
using consumer.Models;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;

namespace consumer.Controllers
{
    [Route("api/v1/it")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsClient _client;

        public ItemsController(IItemsClient client)
        {
            _client = client;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_client.Get().Result.Content);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var response = _client.Get(id).Result;
            if (!response.IsSuccessStatusCode)
                return Ok(JObject.Parse(response.Error.Content));

            return Ok(response.Content);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Item value)
        {
            var response = _client.Post(value).Result;
            if (!response.IsSuccessStatusCode)
                return Ok(JObject.Parse(response.Error.Content));

            return Created(response.Headers.Location, response.Content);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Item value)
        {
            var response = _client.Put(id, value).Result;
            if (!response.IsSuccessStatusCode)
                return Ok(JObject.Parse(response.Error.Content));

            return Ok(response.Content);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var response = _client.Delete(id).Result;
            if (!response.IsSuccessStatusCode)
                return Ok(JObject.Parse(response.Error.Content));

            return Ok(response.Content);
        }
    }
}
