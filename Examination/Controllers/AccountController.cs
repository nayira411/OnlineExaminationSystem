using Examination.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Examination.Repo;
using Examination.Models;

namespace Examination.Controllers
{
    public class AccountController : Controller
    {

        IAccountRepo loginRepo;
        public AccountController(IAccountRepo _loginRepo)
        {
            loginRepo = _loginRepo;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var (user, userType) = loginRepo.GetUser(model);
			if (user == null&& userType==UserType.Unknown)
			{
				ModelState.AddModelError("", "Invalid username or password");
				return View(model);
			}

			Claim claim1;
			Claim claim2;
			Claim claim3;
            Claim claim4;

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

			switch (userType)
			{
				case UserType.Student:
					var student = (Student)user;
					claim1 = new Claim(ClaimTypes.Email, student.Semail);
					claim2 = new Claim(ClaimTypes.Name, student.Sname);
					claim3 = new Claim(ClaimTypes.Sid, student.SId.ToString());
                    claim4 = new Claim("ImageUrl", "noUser.png");

					claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Student"));
					break;
				case UserType.Instructor:
					var instructor = (Instructor)user;
					claim1 = new Claim(ClaimTypes.Email, instructor.Insemail);
					claim2 = new Claim(ClaimTypes.Name, instructor.Insname);
					claim3 = new Claim(ClaimTypes.Sid, instructor.InsId.ToString());

               
                    string imageUrl = string.IsNullOrEmpty(instructor?.InsImg) ? "noUser.png" : instructor.InsImg;
                    claim4 = new Claim("ImageUrl", imageUrl);


                    //  claim4 = new Claim("ImageUrl", instructor?.InsImg);
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Instructor"));
					break;
				case UserType.Admin:
					var admin = (Admin)user;
					claim1 = new Claim(ClaimTypes.Email, admin.Aemail);
					claim2 = new Claim(ClaimTypes.Name, admin.Aname);
					claim3 = new Claim(ClaimTypes.Sid, admin.AId.ToString());
                    claim4 = new Claim("ImageUrl", "noUser.png");
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

					break;
				default:
					return RedirectToAction("Login", "Account");
			}

			claimsIdentity.AddClaim(claim1);
			claimsIdentity.AddClaim(claim2);
			claimsIdentity.AddClaim(claim3);
            claimsIdentity.AddClaim(claim4);

			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

			return RedirectToAction("Index", "Home");
		}


		public async Task <IActionResult> LogOut()

        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        

		public IActionResult EditProfile()
		{
            var user = HttpContext.User;
            var userInfo = new
            {

                Name = "",
                Email = "",
                ImageUrl = ""
            };

            userInfo = new
            {
                Name = user.FindFirst(ClaimTypes.Name)?.Value ?? "",
                Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "",
                ImageUrl = user.FindFirst("ImageUrl")?.Value ?? ""
            };
            Console.WriteLine(userInfo.ImageUrl);
            ViewBag.UserInfo = userInfo;
            return View();
		}
		[HttpPost]
        public IActionResult EditProfile(EditProfileViewModel data)
        {

            var user = HttpContext.User;
            var userInfo = new
            {

                Name = "",
                Email = "",
                ImageUrl = ""
            };

            userInfo = new
            {
                Name = user.FindFirst(ClaimTypes.Name)?.Value ?? "",
                Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "",
                ImageUrl = user.FindFirst("ImageUrl")?.Value ?? ""
            };

            ViewBag.UserInfo = userInfo;
            AccountRepo ARepo = new AccountRepo();
            string role = user.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;

            if (ModelState.IsValid)
            {
                if (data.newPass != data.conFirmPass)
                {
                    ModelState.AddModelError("conFirmPass", "The new password and confirm password do not match.");
                    return View(data);
                }

                string res = ARepo.UpdatePass(role, data.email, data.oldPass, data.newPass, data.conFirmPass);

                if (res == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(data);
                }

                ViewBag.UpdateResult = res; // Add the result state to ViewBag
                return RedirectToAction("Login", "Account");
            }

            return View(data);
        }


    }
}
