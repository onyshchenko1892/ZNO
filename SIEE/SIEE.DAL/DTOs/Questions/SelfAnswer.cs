namespace SIEE.DAL.DTOs.Questions
{
    public class SelfAnswer: Question
    {
        public SelfAnswer()
        {
            Type = this.GetType().Name;
        }
        public string ImagePath { get; set; }
    }
}
