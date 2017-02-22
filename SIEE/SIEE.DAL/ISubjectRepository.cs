using System.Collections.Generic;
using System.Threading.Tasks;
using SIEE.DAL.DTOs;

namespace SIEE.DAL
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllSubjectsAsync();
    }
}
