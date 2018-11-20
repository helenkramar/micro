using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Refit;

using consumer.Models;

namespace consumer.Client
{
    public interface IItemsClient
    {
        [Get("/api/items")]
        Task<ApiResponse<IEnumerable<Item>>> Get();

        [Get("/api/items/{id}")]
        Task<ApiResponse<Item>> Get(int id);

        [Post("/api/items")]
        Task<ApiResponse<Item>> Post([FromBody] Item newItem);

        [Put("/api/items/{id}")]
        Task<ApiResponse<Item>> Put(int id, [FromBody] Item updatedItem);

        [Delete("/api/items/{id}")]
        Task<ApiResponse<Message>> Delete(int id);
    }
}