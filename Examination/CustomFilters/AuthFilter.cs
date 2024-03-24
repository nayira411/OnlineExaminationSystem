using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD.CustomFilters
{
	public class AuthFilter:ActionFilterAttribute
	{
		//befor action execute. 
		public override void OnActionExecuting(ActionExecutingContext context)
		{

			if (context.HttpContext.User.Identity.IsAuthenticated==false)
			{
				context.Result=new RedirectToActionResult("login","account",null);
			} 
			base.OnActionExecuting(context);
		}
	}
}
  