using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using MyApi.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MyApi
{
    public class UserService
    {
        private readonly UserRepository _user_repo;

        public UserService(IMongoClient mongoClient)
        {
            _user_repo = new UserRepository(mongoClient);
        }
        public async Task<string> RegisterUserAsyncJSON(string json)
        {
            try
            {
                var newUser = JsonConvert.DeserializeObject<User>(json);
                if (newUser == null)
                    throw new ArgumentException("Invalid JSON input.");
                if (newUser.Email == null)
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Enter email." });

                var existingUser = await _user_repo.GetUserByEmailAsync(newUser.Email);
                if (existingUser != null)
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Email is already in use." });
                if (newUser.PasswordHash == null)
                    return JsonConvert.SerializeObject(new { success = false, Message = "Enter password!" });
                newUser.SetPassword(newUser.PasswordHash);

                await _user_repo.AddUserAsync(newUser);
                return JsonConvert.SerializeObject(new { Success = true, Message = "User registered successfully." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while registering the user.", Error = ex.Message });
            }
        }

        public async Task<string> GetUserByIdAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var id = jsonObj["userId"]?.ToString();

                if (string.IsNullOrEmpty(id))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "User ID is required." });
                }

                var user = await _user_repo.GetUserByIdAsync(new ObjectId(id));

                if (user == null)
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "User not found." });
                }

                return JsonConvert.SerializeObject(new { Success = true, User = user });
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

        public async Task<string?> GetUserByEmailAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var email = jsonObj["email"]?.ToString();

                if (string.IsNullOrEmpty(email))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Email is required." });
                }

                var user = await _user_repo.GetUserByEmailAsync(email);

                if (user == null)
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "User not found." });
                }

                return JsonConvert.SerializeObject(new { Success = true, User = user });
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

        public async Task<string> DeleteUserByIdAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var userId = jsonObj["userId"]?.ToString();

                if (string.IsNullOrEmpty(userId))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "User ID is required." });
                }

                if (!ObjectId.TryParse(userId, out ObjectId objectId))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid User ID format." });
                }

                bool isDeleted = await _user_repo.DeleteUserAsync(objectId);

                return JsonConvert.SerializeObject(new { Success = isDeleted, Message = isDeleted ? "User deleted successfully." : "User not found." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while deleting the user.", Error = ex.Message });
            }
        }

        //{
        //  "userId": "60f7f09f5cafe305ac34b589",
        //  "newEmail": "john.newemail@example.com"
        //}

    public async Task<string> UpdateUserEmailAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JsonConvert.DeserializeObject<dynamic>(json);
                if (jsonObj == null)
                    throw new ArgumentException("Invalid JSON input.");

                string userId = jsonObj.userId;
                string newEmail = jsonObj.newEmail;

                var existingUser = await _user_repo.GetUserByEmailAsync(newEmail);
                if (existingUser != null)
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Email is already in use." });

                var objectId = new ObjectId(userId);
                var user = await _user_repo.GetUserByIdAsync(objectId);
                user.Email = newEmail;
                bool isUpdated = await _user_repo.UpdateUserAsync(user);

                return JsonConvert.SerializeObject(new { Success = isUpdated, Message = isUpdated ? "Email updated successfully." : "User not found." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while updating the email.", Error = ex.Message });
            }
        }

        //{
        //  "userId": "60f7f09f5cafe305ac34b589",
        //  "currentPassword": "Secret123",
        //  "newPassword": "NewSecret456"
        //}

    public async Task<string> ChangePasswordAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JsonConvert.DeserializeObject<dynamic>(json);
                if (jsonObj == null)
                    throw new ArgumentException("Invalid JSON input.");

                string userId = jsonObj.userId;
                string currentPassword = jsonObj.currentPassword;
                string newPassword = jsonObj.newPassword;

                var objectId = new ObjectId(userId);
                var user = await _user_repo.GetUserByIdAsync(objectId);

                if (user == null)
                    return JsonConvert.SerializeObject(new { Success = false, Message = "User not found." });

                if (!User.ValidatePassword(currentPassword, user.PasswordHash!, user.PasswordSalt!))
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Current password is incorrect." });

                user.SetPassword(newPassword);

                bool isUpdated = await _user_repo.UpdateUserAsync(user);

                return JsonConvert.SerializeObject(new { Success = isUpdated, Message = isUpdated ? "Password changed successfully." : "Password change failed." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while changing the password.", Error = ex.Message });
            }
        }

        public async Task<string> ChangePreferencesAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                string userId = jsonObj["userId"]?.ToString();
                var preferencesArray = jsonObj["preferences"] as JArray;

                if (string.IsNullOrEmpty(userId) || preferencesArray == null)
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid input. Missing userId or preferences." });
                }

                var objectId = new ObjectId(userId);
                var user = await _user_repo.GetUserByIdAsync(objectId);

                if (user == null)
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "User not found." });
                }

                var newPreferences = preferencesArray.ToObject<List<PreferenceTag>>();

                user.Preferences = newPreferences;

                bool isUpdated = await _user_repo.UpdateUserAsync(user);

                return JsonConvert.SerializeObject(new { Success = isUpdated, Message = isUpdated ? "Preferences updated successfully." : "Failed to update preferences." });
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
