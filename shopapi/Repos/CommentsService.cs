using MongoDB.Driver;
using MyFants.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFants.Repos
{
    public class CommentsService
    {
        private readonly IMongoCollection<Comments> _comments;

        public CommentsService(ICommentsstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _comments = database.GetCollection<Comments>(settings.CommentsCollectionName);
        }

        public List<Comments> GetAllComments()
        {
            return _comments.Find(comment => true).ToList();
        }

        public Comments GetById(string id)
        {
            return _comments.Find<Comments>(comment => comment.Id == id).FirstOrDefault();
        }

        public List<Comments> GetAllCommentsByProductid(string productid)
        {
            return _comments.Find(com => com.productid == productid).ToList();
        }

        public bool Create(Comments comment)
        {
            try
            {
                _comments.InsertOne(comment);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Remove(string id)
        {
            try
            {
                _comments.DeleteOne(comment => comment.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(string id, Comments commentIn)
        {
            try
            {
                _comments.ReplaceOne(comment => comment.Id == id, commentIn);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
