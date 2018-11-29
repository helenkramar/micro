using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

using provider.API;
using provider.Models;
using provider.Service;
using provider.Extensions;

namespace provider.Controllers
{
    using Microsoft.AspNetCore.Routing;

    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        string route = typeof(ItemsController).CustomAttributes
            .Single(a => a.AttributeType == typeof(RouteAttribute))
            .ConstructorArguments.FirstOrDefault().Value.ToString();

        public List<ItemDTO> Items { get; }

        public ItemsController()
        {
            Items = DbService.ReadItems().ToList();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var items = Items.Select(item => item.AsAPIEntity<ItemAPI>());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                return Ok(Items.Single(item => item.Id == id));
            }
            catch (Exception e)
            {
                return NotFound(new Message
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Text = e.Message
                });
            }
        }

        [HttpPost]
        public ActionResult<ItemDTO> Post([FromBody] ItemDTO newItem)
        {
            int newId = Items.Last().Id;
            newId++;
            newItem.Id = newId;
            newItem.LastModified = DateTime.Now;
            Items.Add(newItem);

            var result = Created($"{route}/{newId}", newItem);
            result.ContentTypes = new MediaTypeCollection{ "application/json" };

            SaveChanges();
            return result;
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ItemDTO updatedItem)
        {
            try
            {
                var item = Items.Single(i => i.Id == id);
                item.Name = updatedItem.Name;
                item.LastModified = DateTime.Now;

                var result = Ok(item);
                result.ContentTypes = new MediaTypeCollection { "application/json" };

                SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return NotFound(new Message
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Text = e.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Message> Delete(int id)
        {
            try
            {
                var item = Items.Single(i => i.Id.Equals(id));
                Items.Remove(item);

                SaveChanges();
                return Ok(new Message
                {
                    StatusCode = HttpStatusCode.OK,
                    Text = $"Item with id '{id}' was successfully deleted."
                });
            }
            catch (Exception e)
            {
                return NotFound(new Message
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Text = e.Message
                });
            }
        }

        private void SaveChanges()
        {
            DbService.WriteItems(Items.OrderBy(i => i.Id));
        }
    }
}
