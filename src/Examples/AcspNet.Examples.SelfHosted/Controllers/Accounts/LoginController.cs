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

				var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

				var authenticationManager = Context.Context.Authentication;
				authenticationManager.SignIn(id);

				return new Redirect(RedirectionType.PreviousPage);
			}

			return new Tpl(GetView<LoginView>().Get(Model), StringTable.PageTitleLogin);
		}
	}
}