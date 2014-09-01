using System.Collections.Generic;
using System.Security.Claims;
using AcspNet.Attributes;
using AcspNet.Examples.SelfHosted.Models.Accounts;
using AcspNet.Examples.SelfHosted.Views.Accounts;
using AcspNet.Modules;
using AcspNet.Responses;
using Microsoft.AspNet.Identity;

namespace AcspNet.Examples.SelfHosted.Controllers.Accounts
{
	[Post("login")]
	[ViewModel(typeof(LoginViewModel))]
	public class LoginController : Controller
	{
		public override ControllerResponse Invoke()
		{
			var loginModel = (LoginViewModel)Model;

			if (loginModel.Password == "1" && loginModel.UserName == "Foo")
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, loginModel.UserName)
				};

				var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

				var authenticationManager = Context.Context.Authentication;
				authenticationManager.SignIn(id);

				return new Redirect(RedirectionType.PreviousPage);
			}

			return new Tpl(GetView<LoginView>().Get(loginModel));
		}
	}
}