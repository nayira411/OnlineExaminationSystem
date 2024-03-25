namespace Examination.ViewModel
{
    public class QuestionAnswerViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public List<string> Answers { get; set; }
        public List<int> AnswerIds { get; set; } 
        public string StudentAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public int? CorrectAnswerId { get; set; }
    }
}
