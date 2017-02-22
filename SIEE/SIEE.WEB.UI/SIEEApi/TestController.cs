using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SIEE.DAL;
using SIEE.DAL.DTOs;
using SIEE.DAL.DTOs.Questions;

namespace SIEE.WEB.UI.SIEEApi
{
    public class TestController : ApiController
    {
        private readonly ITestRepository _testRepository;
        
        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        // GET api/test/testId
        [Route(("api/test/{testId}"))]
        public async Task<Test> GetTestAsync(int testId)
        {
            return await _testRepository.GetTestByIdAsync(testId);
        }

        // GET api/tests/subjectId
        [Route(("api/tests/{subjectId}"))]
        public async Task<IEnumerable<Test>> GetTestsBySubjectIdAsync(int subjectId)
        {
            return await _testRepository.GetAllTestsBySubjectIdAsync(subjectId);
        }

        // GET api/test/questions/testId
        [Route(("api/test/{testId}/questions"))]
        public async Task<IEnumerable<Question>> GetAllTestQuestionsByTestIdAsync(int testId)
        {
            return await _testRepository.GetAllTestQuestionsByTestIdAsync(testId);
        }
    }
}