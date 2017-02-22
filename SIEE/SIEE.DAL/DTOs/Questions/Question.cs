
namespace SIEE.DAL.DTOs.Questions
{
    public class Question
    {
        public int TestId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionSentence { get; set; }
        public string TrueAnswer { get; set; }
        public string Type { get; set; }
        public int QuestionPrice { get; set; }
    }
}
