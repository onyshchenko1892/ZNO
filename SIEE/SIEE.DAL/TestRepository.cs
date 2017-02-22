using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SIEE.DAL.DTOs;
using SIEE.DAL.DTOs.Questions;

namespace SIEE.DAL
{
    public class TestRepository: ITestRepository
    {
        private readonly string _url;
        public TestRepository(string url)
        {
            _url = url;
        }

        public async Task<DTOs.Test> GetTestByIdAsync(int testId)
        {
            MongoClient client = new MongoClient(_url);
            IMongoDatabase dataBase = client.GetDatabase("dbsiee");
            var collection = dataBase.GetCollection<BsonDocument>("Test");
            var filter = new BsonDocument(new Dictionary<string,object>()) {{"TestId", testId}};
            List<Test> tests = new List<Test>();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var document in cursor.Current)
                    {
                        tests.Add(new Test()
                        {
                            TestId = document["TestId"].AsInt32,
                            SubjectId = document["SubjectId"].AsInt32,
                            Name = document["Name"].AsString,
                            NumberOfQuestions = document["NumberOfQuestions"].AsInt32,
                            Year = document["Year"].AsInt32,
                        });
                    }
                }
            }
            return tests.First(t => t.TestId == testId);
        }

        public async Task<List<DTOs.Test>> GetAllTestsBySubjectIdAsync(int subjectId)
        {
            MongoClient client = new MongoClient(_url);
            IMongoDatabase dataBase = client.GetDatabase("dbsiee");
            var collection = dataBase.GetCollection<BsonDocument>("Test");
            var filter = new BsonDocument(new Dictionary<string, object>()) { { "SubjectId", subjectId } };
            List<Test> tests = new List<Test>();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var document in cursor.Current)
                    {
                        tests.Add(new Test()
                        {
                            TestId = document["TestId"].AsInt32,
                            SubjectId = document["SubjectId"].AsInt32,
                            Name = document["Name"].AsString,
                            NumberOfQuestions = document["NumberOfQuestions"].AsInt32,
                            Year = document["Year"].AsInt32,
                        });
                    }
                }
            }
            return tests;
        }

        public async Task<List<Question>> GetAllTestQuestionsByTestIdAsync(int testId)
        {
            List<Question> questions = new List<Question>();
            questions.AddRange(await GetAllRadioQuestionsByTestIdAsync(testId));
            questions.AddRange(await GetAllSelfAnswerQuestionsByTestIdAsync(testId));
            return questions;
        }

        public async Task<List<Question>> GetAllRadioQuestionsByTestIdAsync(int testId)
        {
            MongoClient client = new MongoClient(_url);
            IMongoDatabase dataBase = client.GetDatabase("dbsiee");
            var collection = dataBase.GetCollection<BsonDocument>("RadioQuestion");
            var filter = new BsonDocument(new Dictionary<string, object>() { { "TestId", testId } });
            List<Question> questions = new List<Question>();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var document in cursor.Current)
                    {
                        questions.Add(new RadioQuestion()
                        {
                            TestId = document["TestId"].AsInt32,
                            QuestionNumber = document["QuestionNumber"].AsInt32,
                            QuestionSentence = document["QuestionSentence"].AsString,
                            ImagePath = document["ImagePath"].GetType() == typeof(MongoDB.Bson.BsonNull) ? null : document["ImagePath"].AsString,
                            A = document["A"].AsString,
                            B = document["B"].AsString,
                            C = document["C"].AsString,
                            D = document["D"].AsString,
                            E = document["E"].AsString,
                            QuestionPrice = document["QuestionPrice"].AsInt32,
                            TrueAnswer = document["TrueAnswer"].AsString
                        });
                    }
                }
                return questions;
            }
        }

        public async Task<List<Question>> GetAllSelfAnswerQuestionsByTestIdAsync(int testId)
        {
            MongoClient client = new MongoClient(_url);
            IMongoDatabase dataBase = client.GetDatabase("dbsiee");
            var collection = dataBase.GetCollection<BsonDocument>("SelfAnswer");
            var filter = new BsonDocument(new Dictionary<string, object>() { { "TestId", testId } });
            List<Question> questions = new List<Question>();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var document in cursor.Current)
                    {
                        questions.Add(new SelfAnswer()
                        {
                            TestId = document["TestId"].AsInt32,
                            QuestionNumber = document["QuestionNumber"].AsInt32,
                            QuestionSentence = document["QuestionSentence"].AsString,
                            ImagePath = document["ImagePath"].GetType() == typeof(MongoDB.Bson.BsonNull) ? null : document["ImagePath"].AsString,
                            QuestionPrice = document["QuestionPrice"].AsInt32,
                            TrueAnswer = document["TrueAnswer"].AsString
                        });
                    }
                }
                return questions;
            }
        }
    }
}
