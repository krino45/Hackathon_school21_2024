using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        [HttpGet]
        [Route("getTags")]
        public async Task<IActionResult> GetTagCollectionJSON()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            if (connectionString == null)
            {
                return BadRequest(new { message = "MONGODB_URI environment variable is not set." });
            }

            var client = new MongoClient(connectionString);
            List<PreferenceTag> collection = await client
                .GetDatabase("eventsgroup")
                .GetCollection<PreferenceTag>("tags")
                .Find(FilterDefinition<PreferenceTag>.Empty)
                .ToListAsync();

            string collectionJson = JsonConvert.SerializeObject(collection);

            return Content(collectionJson, "application/json");
        }
    }
}
