using Examination.Models;
using System.ComponentModel.DataAnnotations;

namespace Examination.ViewModel
{
    public class QuestionAnswersModel
    {
        public int QId { get; set; }

        [Required(ErrorMessage = "Question body is required")]
        public string Qbody { get; set; }

        [Required(ErrorMessage = "Question type is required")]
        public string Qtype { get; set; }

        [Required(ErrorMessage = "Question mark is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Question mark must be greater than 0")]
        public int Qmark { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int CrId { get; set; }
        public List<Answer> Answers { get; set; }

    }
}
