using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi
{
    public class UserService
    {
        private readonly IMongoClient _mongoClient;

        public UserService(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<IResult> SaveChanges(UserSettings userSettings)
        {
            var database = _mongoClient.GetDatabase("eventsgroup");
            var usersCollection = database.GetCollection<User>("Users");

            var update = Builders<User>.Update
                .Set(u => u.Email, userSettings.Email)
                .Set(u => u.Preferences, userSettings.Preferences);

            ObjectId objectId = ObjectId.Parse(userSettings.Id);

            var result = await usersCollection.UpdateOneAsync(u => u.Id.Equals(userSettings.Id), update);

            if (result.ModifiedCount > 0)
                return Results.Ok("User data has been updated successfully.");
            return Results.Problem("User not found or data hasn't changed.");
        }
    }
}
