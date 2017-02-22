using SIEE.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SIEE.WEB.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITestRepository _testRepository;

        public HomeController(ISubjectRepository subjectRepository, ITestRepository testRepository)
        {
            _subjectRepository = subjectRepository;
            _testRepository = testRepository;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.subjectNumberOfTestsDictionary = await GetNumberOfTestsBySubjectsAsync();
            return View();
        }

        public async Task<Dictionary<DAL.DTOs.Subject,int>> GetNumberOfTestsBySubjectsAsync()
        {
            Dictionary<DAL.DTOs.Subject, int> subjectNumberOfTestsDictionary = new Dictionary<DAL.DTOs.Subject, int>();
            List<DAL.DTOs.Subject> subjects = await _subjectRepository.GetAllSubjectsAsync();
            for (int i = 0; i < subjects.Count; i++)
            {
                subjectNumberOfTestsDictionary.Add(subjects[i],(await _testRepository.GetAllTestsBySubjectIdAsync(subjects[i].SubjectId)).Count);
            }
            return subjectNumberOfTestsDictionary;
        }
    }
}