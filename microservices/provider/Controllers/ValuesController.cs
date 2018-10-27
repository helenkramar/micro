using System.Linq;
using Microsoft.AspNetCore.Mvc;
using provider.Model;
using System.Collections.Generic;

namespace provider.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        IEnumerable<Item> items = new[] { new Item { Id = 1, Name = "Toy" }, new Item { Id = 2, Name = "Book" } };

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(items);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            return Ok(items.First(item => item.Id == id));
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
