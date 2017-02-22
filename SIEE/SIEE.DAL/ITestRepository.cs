using System.Collections.Generic;
using System.Threading.Tasks;
using SIEE.DAL.DTOs;
using SIEE.DAL.DTOs.Questions;

namespace SIEE.DAL
{
    public interface ITestRepository
    {
        Task<Test> GetTestByIdAsync(int testId);
        Task<List<Test>> GetAllTestsBySubjectIdAsync(int subjectId);
        Task<List<Question>> GetAllTestQuestionsByTestIdAsync(int testId);
    }
}
