using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SIEE.DAL;

namespace SIEE.WEB.UI.SIEEApi
{
    public class SubjectController : ApiController
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        // GET api/subjects
        [Route("api/subjects")]
        public async Task<IEnumerable<DAL.DTOs.Subject>> GetSubjects()
        {
            return await _subjectRepository.GetAllSubjectsAsync();
        }
    }
}