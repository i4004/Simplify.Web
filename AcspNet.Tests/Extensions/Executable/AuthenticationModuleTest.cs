using System;

using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("authenticationModuleTests")]
	public class AuthenticationModuleTest : ExecExtension
	{
		public override void Invoke()
		{
			AuthenticationModule.LogInSessionUser(12);
			AuthenticationModule.AuthenticateSessionUser();
			AuthenticationModule.LogOutSessionUser();

			Assert.Throws<ArgumentNullException>(() => AuthenticationModule.LogInCookieUser(null, "FooPassword"));
			Assert.Throws<ArgumentNullException>(() => AuthenticationModule.LogInCookieUser("FooUser", null));
			
			AuthenticationModule.LogInCookieUser("FooUser", "FooPassword", true);

			Assert.Throws<ArgumentException>(() => AuthenticationModule.AuthenticateCookieUser(0, "FooUser", "FooPassword"));
			Assert.Throws<ArgumentNullException>(() => AuthenticationModule.AuthenticateCookieUser(5, null, "FooPassword"));
			Assert.Throws<ArgumentNullException>(() => AuthenticationModule.AuthenticateCookieUser(5, "FooUser", null));
			AuthenticationModule.AuthenticateCookieUser(5, "FooUser", "FooPassword");
			AuthenticationModule.AuthenticateCookieUser(5, "FooUser", "WrongPassword");

			AuthenticationModule.LogOutCookieUser();
		}
	}
}