using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;

using consumer.Models;
using Microsoft.AspNetCore.Mvc;

namespace consumer.Client
{
    public interface IItemsClient
    {
        [Get("/items")]
        Task<ActionResult> GetItems();

        [Get("/items/{id}")]
        Task<Item> GetItem(int id);
    }

    public interface IHelloClient
    {
        [Get("/helloworld")]
        Task<Reply> GetMessageAsync();
    }

    public class Reply
    {
        public string Message { get; set; } = "kuku";
    }
}