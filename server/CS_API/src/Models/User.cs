using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MyApi.Models
{
    public class User
    {
        private const int SaltSize = 16;       // 128-bit salt
        private const int KeySize = 32;        // 256-bit hash
        private const int Iterations = 10000;  // PBKDF2 iterations

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("last_name")]
        public string? LastName { get; set; }

        [BsonElement("first_name")]
        public string? FirstName { get; set; }

        [BsonElement("middle_name")]
        public string? MiddleName { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("password_hash")]
        public string? PasswordHash { get; set; }

        [BsonElement("password_salt")]
        public string? PasswordSalt { get; set; }

        [BsonElement("role")]
        public string? Role { get; set; }

        [BsonElement("preferences")]
        public List<string>? Preferences { get; set; }

        [BsonElement("profession")]
        public string? Profession { get; set; }

        [BsonElement("roundtable_id")]
        public int? RoundtableId { get; set; }

        [BsonElement("events")]
        public List<ObjectId>? Events { get; set; }
        public User() { }

        public User(string lastName, string firstName, string middleName, string email, string password,
                    string role, List<string>? preferences, string profession,
                    int? roundtableId, List<ObjectId>? events)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Email = email;
            Role = role;
            Preferences = preferences;
            Profession = profession;
            RoundtableId = roundtableId;
            Events = events;

            SetPassword(password);
        }

        public void SetPassword(string password)
        {
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
                PasswordSalt = Convert.ToBase64String(saltBytes);
            }

            using (var deriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(PasswordSalt), Iterations))
            {
                byte[] hashBytes = deriveBytes.GetBytes(KeySize);
                PasswordHash = Convert.ToBase64String(hashBytes);
            }
        }

        public static bool ValidatePassword(string enteredPassword, string storedPasswordHash, string storedSalt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(enteredPassword, Convert.FromBase64String(storedSalt), Iterations))
            {
                byte[] enteredHashBytes = deriveBytes.GetBytes(KeySize);
                string enteredPasswordHash = Convert.ToBase64String(enteredHashBytes);

                return storedPasswordHash == enteredPasswordHash;
            }
        }
    }
}