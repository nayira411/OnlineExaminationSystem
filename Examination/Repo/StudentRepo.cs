using Examination.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examination.Repo
{
    public interface IStudentRepo
    {
        public List<Question> GetExamQuestions(int ExamId);
        public Exam GetExam(int ExamId);
        public Course GetCourseById(int CourseId);
        public void StudentAnswers(Student_Answer sa);
        public int CalculateExamScore(int ExamId, int StudentId);
        public int CalculateTotalExamScore(int ExamId);
    }
    public class StudentRepo: IStudentRepo
    {
        ExamContext db=new ExamContext();

        public List<Question> GetExamQuestions(int ExamId)
        {
            Exam examQuestion = db.Exams.FirstOrDefault(e => e.EId == ExamId);
            List<Question> questions = examQuestion.QIds.ToList();
            return questions;
        }
        public Exam GetExam(int ExamId)
        {
            Exam ExamInfo = db.Exams.FirstOrDefault(e => e.EId == ExamId);
            return ExamInfo;
        }
        public Course GetCourseById(int CourseId)
        {
            Course course = db.Courses.FirstOrDefault(c => c.CrId == CourseId);
            return course;
        }
        public void StudentAnswers(Student_Answer sa)
        {
            db.Student_Answers.Add(sa);
            db.SaveChanges();
        }
        public int CalculateExamScore(int ExamId, int StudentId)
        {
            SqlParameter studentIdParam = new SqlParameter("@StudentId", StudentId);
            SqlParameter examIdParam = new SqlParameter("@ExamId", ExamId);

            SqlParameter outputParam = new SqlParameter
            {
                ParameterName = "@Score",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            db.Database.ExecuteSqlRaw("EXEC GradeExamAnswer @ExamId, @StudentId, @Score OUTPUT", examIdParam, studentIdParam, outputParam);

            int score = (int)outputParam.Value;

            return score;
        }
        public int CalculateTotalExamScore(int ExamId)
        {
            Exam examQuestion = db.Exams.FirstOrDefault(e => e.EId == ExamId);
            int questions = examQuestion.QIds.Sum(q => q.Qmark);
            return questions;
        }

    }
}
