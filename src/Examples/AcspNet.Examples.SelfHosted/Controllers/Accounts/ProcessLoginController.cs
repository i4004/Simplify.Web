using System.Collections.Generic;
using System.Security.Claims;
using AcspNet.Attributes;
using Microsoft.AspNet.Identity;

namespace AcspNet.Examples.SelfHosted.Controllers.Accounts
{
	[Get("processLogin")]
	public class ProcessLoginController : Controller
	{
		public override ControllerResponse Invoke()
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, "Foo"),
				new Claim(ClaimTypes.Email, "Foobar@gmail.com")
			};

			var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

			var authenticationManager = Context.Context.Authentication;
			authenticationManager.SignIn(id);

			//return new Navigate(NavigationType.PreviousPage);
			return null;
		}
	}
}