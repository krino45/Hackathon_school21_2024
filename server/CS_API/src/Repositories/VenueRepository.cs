using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Repositories
{
    public class VenueRepository
    {
        private readonly IMongoCollection<Venue> _venuesCollection;

        public VenueRepository(IMongoClient client)
        {
            var database = client.GetDatabase("eventsgroup");
            _venuesCollection = database.GetCollection<Venue>("venues");
        }
        public async Task AddVenueAsync(Venue newVenue)
        {
            await _venuesCollection.InsertOneAsync(newVenue);
        }
        
        public async Task<Venue?> GetVenueByIdAsync(ObjectId venueId)
        {
            var filter = Builders<Venue>.Filter.Eq(v => v.Id, venueId);
            return await _venuesCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Venue>> GetAllVenuesAsync()
        {
            return await _venuesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateVenueAsync(Venue updatedVenue)
        {
            var filter = Builders<Venue>.Filter.Eq(v => v.Id, updatedVenue.Id);
            var result = await _venuesCollection.ReplaceOneAsync(filter, updatedVenue);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteVenueAsync(ObjectId venueId)
        {
            var filter = Builders<Venue>.Filter.Eq(v => v.Id, venueId);
            var result = await _venuesCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<List<Venue>> GetVenuesByCityAsync(string city)
        {
            var filter = Builders<Venue>.Filter.Eq(v => v.City, city);
            return await _venuesCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Venue>> GetVenuesByMinCapacityAsync(int minCapacity)
        {
            var filter = Builders<Venue>.Filter.Gte(v => v.Capacity, minCapacity);
            return await _venuesCollection.Find(filter).ToListAsync();
        }

        public async Task<bool> AddEventToVenueAsync(ObjectId venueId, string eventId)
        {
            var filter = Builders<Venue>.Filter.Eq(v => v.Id, venueId);
            var update = Builders<Venue>.Update.AddToSet(v => v.EventIDs, eventId);
            var result = await _venuesCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> RemoveEventFromVenueAsync(ObjectId venueId, string eventId)
        {
            var filter = Builders<Venue>.Filter.Eq(v => v.Id, venueId);
            var update = Builders<Venue>.Update.Pull(v => v.EventIDs, eventId);
            var result = await _venuesCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}