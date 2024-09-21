using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using MyApi.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Services
{
    public class VenueService
    {
        private readonly VenueRepository _venueRepo;

        public VenueService(IMongoClient mongoClient)
        {
            _venueRepo = new VenueRepository(mongoClient);
        }

        public async Task<string> AddVenueAsyncJSON(string json)
        {
            try
            {
                var newVenue = JsonConvert.DeserializeObject<Venue>(json);
                if (newVenue == null)
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON input." });

                await _venueRepo.AddVenueAsync(newVenue);

                return JsonConvert.SerializeObject(new { Success = true, Message = "Venue added successfully." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while adding the venue.", Error = ex.Message });
            }
        }

        public async Task<string> GetVenueByIdAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var venueId = jsonObj["venueId"]?.ToString();

                if (string.IsNullOrEmpty(venueId))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Venue ID is required." });
                }

                var venue = await _venueRepo.GetVenueByIdAsync(new ObjectId(venueId));

                if (venue == null)
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Venue not found." });
                }

                return JsonConvert.SerializeObject(new { Success = true, Venue = venue });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred.", Error = ex.Message });
            }
        }

        public async Task<string> GetAllVenuesAsyncJSON()
        {
            try
            {
                var venues = await _venueRepo.GetAllVenuesAsync();
                return JsonConvert.SerializeObject(new { Success = true, Venues = venues });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while fetching the venues.", Error = ex.Message });
            }
        }

        public async Task<string> UpdateVenueAsyncJSON(string json)
        {
            try
            {
                var updatedVenue = JsonConvert.DeserializeObject<Venue>(json);
                if (updatedVenue == null)
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON input." });

                bool isUpdated = await _venueRepo.UpdateVenueAsync(updatedVenue);

                return JsonConvert.SerializeObject(new { Success = isUpdated, Message = isUpdated ? "Venue updated successfully." : "Venue not found." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while updating the venue.", Error = ex.Message });
            }
        }

        public async Task<string> DeleteVenueAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var venueId = jsonObj["venueId"]?.ToString();

                if (string.IsNullOrEmpty(venueId))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Venue ID is required." });
                }

                bool isDeleted = await _venueRepo.DeleteVenueAsync(new ObjectId(venueId));

                return JsonConvert.SerializeObject(new { Success = isDeleted, Message = isDeleted ? "Venue deleted successfully." : "Venue not found." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while deleting the venue.", Error = ex.Message });
            }
        }

        public async Task<string> GetVenuesByCityAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var city = jsonObj["city"]?.ToString();

                if (string.IsNullOrEmpty(city))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "City is required." });
                }

                var venues = await _venueRepo.GetVenuesByCityAsync(city);

                return JsonConvert.SerializeObject(new { Success = true, Venues = venues });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred.", Error = ex.Message });
            }
        }

        public async Task<string> GetVenuesByMinCapacityAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var minCapacity = jsonObj["minCapacity"]?.ToObject<int>();

                if (minCapacity == null)
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Minimum capacity is required." });
                }

                var venues = await _venueRepo.GetVenuesByMinCapacityAsync(minCapacity.Value);

                return JsonConvert.SerializeObject(new { Success = true, Venues = venues });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred.", Error = ex.Message });
            }
        }

        public async Task<string> AddEventToVenueAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var venueId = jsonObj["venueId"]?.ToString();
                var eventId = jsonObj["eventId"]?.ToString();

                if (string.IsNullOrEmpty(venueId) || string.IsNullOrEmpty(eventId))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Venue ID and Event ID are required." });
                }

                bool isAdded = await _venueRepo.AddEventToVenueAsync(new ObjectId(venueId), eventId);

                return JsonConvert.SerializeObject(new { Success = isAdded, Message = isAdded ? "Event added to venue." : "Failed to add event to venue." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred.", Error = ex.Message });
            }
        }

        public async Task<string> RemoveEventFromVenueAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var venueId = jsonObj["venueId"]?.ToString();
                var eventId = jsonObj["eventId"]?.ToString();

                if (string.IsNullOrEmpty(venueId) || string.IsNullOrEmpty(eventId))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Venue ID and Event ID are required." });
                }

                bool isRemoved = await _venueRepo.RemoveEventFromVenueAsync(new ObjectId(venueId), eventId);

                return JsonConvert.SerializeObject(new { Success = isRemoved, Message = isRemoved ? "Event removed from venue." : "Failed to remove event from venue." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred.", Error = ex.Message });
            }
        }
    }
}
