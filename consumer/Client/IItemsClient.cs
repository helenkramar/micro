using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;

using consumer.Models;
using Microsoft.AspNetCore.Mvc;

namespace consumer.Client
{
    public interface IItemsClient
    {
        [Get("/api/items")]
        Task<IEnumerable<Item>> GetItems();

        [Get("/api/items/{id}")]
        Task<Item> GetItem(int id);
    }
}