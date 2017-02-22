namespace SIEE.DAL.DTOs.Questions
{
    public class RadioQuestion: Question
    {
        public RadioQuestion()
        {
            Type = this.GetType().Name;
        }

        public string ImagePath { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
    }
}
