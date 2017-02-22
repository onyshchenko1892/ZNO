using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SIEE.DAL;
using SIEE.WEB.UI.Models;

namespace SIEE.WEB.UI.Controllers
{
    public class TestsController : Controller
    {
        private readonly ITestRepository _testRepository;

        public TestsController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        // GET: Test
        public async Task<ActionResult> Index(int subjectId)
        {
            List<DAL.DTOs.Test> dtoTests = await _testRepository.GetAllTestsBySubjectIdAsync(subjectId);
            List<Models.Test> modelTests = (from t in dtoTests
                select new Test()
                {
                    TestId = t.TestId,
                    Name = t.Name,
                    Year = t.Year,
                    NumberQuestions = t.NumberOfQuestions,
                    NumberMemberAttempt = 1,
                    Time = 30
                }).ToList();
            IEnumerable <IGrouping<int, Models.Test>> tests = modelTests.GroupBy(i => i.Year);
            ViewBag.subjectName = "Математика";
            ViewBag.tests = tests;
            return View();
        }
    }
}