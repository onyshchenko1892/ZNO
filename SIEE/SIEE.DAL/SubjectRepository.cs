using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SIEE.DAL.DTOs;

namespace SIEE.DAL
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly string _url;

        public SubjectRepository() { }

        public SubjectRepository(string url)
        {
            _url = url;
        }
        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            MongoClient client = new MongoClient(_url);
            IMongoDatabase dataBase = client.GetDatabase("dbsiee");
            var collection = dataBase.GetCollection<BsonDocument>("Subject");
            var filter = new BsonDocument();
            List<Subject> subjects = new List<Subject>();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var document in cursor.Current)
                    {
                        subjects.Add(new Subject()
                        {
                            SubjectId = document["SubjectId"].AsInt32,
                            Name = document["Name"].AsString
                        });
                    }
                }
                return subjects;
            }
        }
    }
}
