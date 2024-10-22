﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApi.Models
{
    public class PreferenceTag
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("tag")]
        public string? Tag { get; private set; }

        public PreferenceTag()
        { }
        public PreferenceTag(string tag)
        {
            Tag = tag;
        }
    }
}
