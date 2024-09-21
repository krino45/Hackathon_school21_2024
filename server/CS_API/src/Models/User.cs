using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace MyApi.Models
{
    public class User
    {
        [BsonId] // Атрибут для обозначения, что это поле является ID документа
        [BsonRepresentation(BsonType.ObjectId)] // Преобразование строки в ObjectId
        public string Id { get; set; }  // _id

        public string Name { get; set; }          // name
        public string LastName { get; set; }      // lastname
        public string SecondName { get; set; }    // secondname

        [BsonElement("email")]
        public string Email { get; set; }         // email

        public List<string> Roles { get; set; } = new List<string>(); // roles
        [BsonElement("preferences")]
        public List<string> Preferences { get; set; } = new List<string>(); // preferences
        public string Profession { get; set; }      // profession
        public string RoundtableID { get; set; }    // roundtableID
        public List<string> Events { get; set; } = new List<string>(); // events

        // Конструктор по умолчанию
        public User() { }
    }
}
