using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SIEE.DAL;
using SIEE.WEB.UI.Code;

namespace SIEE.WEB.UI.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;
        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        // GET: Test
        public async Task<ActionResult> Index(int testId)
        {
            string testTitle = (await _testRepository.GetTestByIdAsync(testId)).Name;
            ViewBag.testTitle = testTitle;
            return View();
        }
        
        [HttpGet]
        [AllowCrossSiteJson]
        public async Task<JsonResult> GetTestQuestionsById(int testId)
        {
            List<DAL.DTOs.Questions.Question> questions = await _testRepository.GetAllTestQuestionsByTestIdAsync(testId);
            return Json(questions, JsonRequestBehavior.AllowGet);
        }
        
    }
}