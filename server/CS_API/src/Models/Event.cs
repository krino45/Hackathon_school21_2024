﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApi.Models
{

    public class PrivateEvent : BaseEvent
    {
    }
    public class RoundtableEvent : BaseEvent
    {
        public int? roundtableID { get; set; }
    }
    public class IndustryEvent : BaseEvent
    {
        public List<PreferenceTag>? tags { get; set; }
    }
    public class PublicEvent : BaseEvent
    {
        public List<PreferenceTag>? tags { get; set; }
    }
}