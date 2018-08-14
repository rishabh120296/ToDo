using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<CheckList> Checklists { get; set; }
        public List<Labels> Labels { get; set; }
        public bool IsPinned { get; set; }
    }
}
