using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MyApi.Models
{
    public class User
    {
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

        [BsonElement("password")]
        public string? Password { get; set; }

        [BsonElement("role")]
        public string? Role { get; set; }

        [BsonElement("preferences")]
        public List<PreferenceTag>? Preferences { get; set; }

        [BsonElement("profession")]
        public string? Profession { get; set; }

        [BsonElement("roundtable_id")]
        public int? RoundtableId { get; set; }

        [BsonElement("events")]
        public List<BaseEvent>? Events { get; set; }
        public User()
        { }
        public User(string lastName, string firstName, string middleName, string email, string password,
                    string role, List<PreferenceTag>? preferences, string profession,
                    int roundtableId, List<BaseEvent>? events)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Email = email;
            Password = password;
            Role = role;
            Preferences = preferences;
            Profession = profession;
            RoundtableId = roundtableId;
            Events = events;
        }
    }
}