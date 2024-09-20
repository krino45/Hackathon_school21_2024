using Amazon.Util.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;

namespace MyApi
{
    public static class TagService
    {
        public static async Task<string> GetTagCollectionJSON()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            if (connectionString == null)
            {
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }
            var client = new MongoClient(connectionString);
            return client.GetDatabase("eventsgroup").GetCollection<PreferenceTag>("tags").Find(FilterDefinition<PreferenceTag>.Empty).ToListAsync().ToJson();

        }
    }
}