using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        public UserRepository(IMongoClient client)
        {
            var database = client.GetDatabase("eventsgroup");
            _usersCollection = database.GetCollection<User>("Users");
        }

        public async Task<bool> ValidateLoginAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            if (user == null)
            { 
                return false;
            }

            return User.ValidatePassword(password, user.PasswordHash!, user.PasswordSalt!);
        }

        public async Task AddUserAsync(User newUser)
        {
            await _usersCollection.InsertOneAsync(newUser);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }
        public async Task<User?> GetUserByIdAsync(ObjectId id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateUserAsync(User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, updatedUser.Id);
            var result = await _usersCollection.ReplaceOneAsync(filter, updatedUser);
            return result.ModifiedCount > 0;
        }
        public async Task<bool> DeleteUserAsync(ObjectId id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var result = await _usersCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
        public async Task<List<User>> GetUsersByRoleAsync(string role)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Role, role);
            return await _usersCollection.Find(filter).ToListAsync();
        }
    }
}
