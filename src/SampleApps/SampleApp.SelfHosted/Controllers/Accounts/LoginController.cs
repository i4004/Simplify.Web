using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SampleApp.SelfHosted.Models.Accounts;
using SampleApp.SelfHosted.Views;
using SampleApp.SelfHosted.Views.Accounts;
using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace SampleApp.SelfHosted.Controllers.Accounts
{
	[Post("login")]
	public class LoginController : Controller<LoginViewModel>
	{
		public override ControllerResponse Invoke()
		{
			if (Model.Password == "1" && Model.UserName == "Foo")
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, Model.UserName)
				};

				var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				Context.Context.SignInAsync(new ClaimsPrincipal(id));

				return new Redirect(RedirectionType.LoginReturnUrl);
			}

			return new Tpl(GetView<LoginView>().Get(Model, GetView<MessageBoxView>().Get(StringTable.WrongUserNameOrPassword)), StringTable.PageTitleLogin);
		}
	}
}