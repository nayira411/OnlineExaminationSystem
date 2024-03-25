using Examination.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Examination.Repo
{
    public interface IquestionRepo
    {
        public int AddQuestion(Question q);
        public int AddAnswers(Answer A);


    }
    public class QuestionRepo:IquestionRepo
    {
        ExamContext db = new ExamContext();

        public int AddQuestion(Question q)
        {
            try
            {
                // Create SqlParameter instances with explicit SqlDbType
                var qbodyParam = new SqlParameter("@Qbody", SqlDbType.VarChar, 100)
                {
                    Value = q.Qbody
                };

                var qTypeParam = new SqlParameter("@Qtype", SqlDbType.VarChar, 20)
                {
                    Value = q.Qtype
                };

                var qmarkParam = new SqlParameter("@Qmark", SqlDbType.Int)
                {
                    Value = q.Qmark
                };

                var cridParam = new SqlParameter("@CrId", SqlDbType.Int)
                {
                    Value = q.CrId
                };

                var insertedIdParameter = new SqlParameter("@InsertedId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Execute the stored procedure
                db.Database.ExecuteSqlRaw("EXEC AddQuestion @Qbody, @Qtype, @Qmark, @CrId, @InsertedId OUTPUT",
                    qbodyParam,
                    qTypeParam,
                    qmarkParam,
                    cridParam,
                    insertedIdParameter);

                // Retrieve the inserted ID from the output parameter
                int insertedId = (int)insertedIdParameter.Value;
                return insertedId;
            }
            catch
            {
                return 0;
            }
        }


        public int AddAnswers(Answer A)
        {
            try
            {
                var answerBodyParam = new SqlParameter("@Answerbody", SqlDbType.VarChar, 100)
                {
                    Value = A.Answerbody
                };

                var qidParam = new SqlParameter("@QId", SqlDbType.Int)
                {
                    Value = A.QId
                };

                var isCorrectParam = new SqlParameter("@IsCorrect", SqlDbType.Bit)
                {
                    Value = A.IsCorrect
                };

                db.Database.ExecuteSqlRaw("EXEC AddAnswer @Answerbody, @QId, @IsCorrect",
                    answerBodyParam,
                    qidParam,
                    isCorrectParam);

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
