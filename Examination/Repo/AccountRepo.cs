using Examination.Models;
using Examination.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
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
        public string UpdatePass(string role, string email, string oldPass, string newPass, string conFirmPass);


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
			var student = db.Students.FirstOrDefault(a => a.Semail == model.Email && a.password == model.Password);
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
        public string UpdatePass(string role, string email, string oldPass, string newPass, string conFirmPass)
        {
            if (role == "Student")
            {
                var student = db.Students.FirstOrDefault(a => a.Semail == email && a.password == oldPass);
                if (student != null)
                {
                    student.password = newPass;
                    db.Students.Update(student);
                    db.SaveChanges();
                    return "Password updated successfully.";
                }
                else
                {
                    return null;
                }
            }
            else if (role == "Instructor")
            {
                var instructor = db.Instructors.FirstOrDefault(a => a.Insemail == email && a.Inspassword == oldPass);
                if (instructor != null)
                {
                    instructor.Inspassword = newPass;
                    db.Instructors.Update(instructor);
                    db.SaveChanges();
                    return "Password updated successfully.";
                }
                else
                {
                    return null;
                }
            }
            else if (role == "Admin")
            {
                var admin = db.Admins.FirstOrDefault(a => a.Aemail == email && a.Apassword == oldPass);
                if (admin != null)
                {
                    admin.Apassword = newPass;
                    db.Admins.Update(admin);
                    db.SaveChanges();
                    return "Password updated successfully.";
                }
                else
                {
                    return null;
                }
            }

            return "User not found.";
        }





    }
}
