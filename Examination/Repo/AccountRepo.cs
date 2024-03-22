using Examination.Data;
using Examination.Models;
using Examination.ViewModel;
namespace Examination.Repo
{
	public enum UserType
	{
		Student,
		Instructor,
		Admin,
		Unknown // Default value if user type cannot be determined
	}
	public interface IAccountRepo
    {
		public (object user, UserType userType) GetUser(LoginViewModel model);
	}
    public class AccountRepo : IAccountRepo
    {
         ExamContext db= new ExamContext();
        //public AccountRepo(ExamContext _db)
        //{
        //    db = _db;
        //}
        public (object user, UserType userType) GetUser( LoginViewModel model)
        {
			var student = db.Students.FirstOrDefault(a => a.Semail == model.Email && a.Password == model.Password);
			if (student != null)
			{
				return (student, UserType.Student);
			}

			var instructor = db.Instructors.FirstOrDefault(a => a.Insemail == model.Email && a.Inspassword == model.Password);
			if (instructor != null)
			{
				return (instructor, UserType.Instructor);
			}

			var admin = db.Admins.FirstOrDefault(a => a.Aemail == model.Email && a.Apassword == model.Password);
			if (admin != null)
			{
				return (admin, UserType.Admin);
			}

			// If no user is found, return null for the user and Unknown for the user type
			return (null, UserType.Unknown);
		
	}

    }
}
