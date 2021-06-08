using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MyFants.Entities
{
    public class Comments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("productid")]
        public string productid { get; set; }
        public string time { get; set; }
        public string text { get; set; }
    }

    public class CommentsstoreDatabaseSettings : ICommentsstoreDatabaseSettings
    {
        public string CommentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICommentsstoreDatabaseSettings
    {
        string CommentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}