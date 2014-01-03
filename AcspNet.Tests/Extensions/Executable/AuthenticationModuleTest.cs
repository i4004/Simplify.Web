using System;

using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("foo2")]
	public class AuthenticationModuleTest : ExecExtension
	{
		public override void Invoke()
		{
			AuthenticationModule.LogInSessionUser(12);
			AuthenticationModule.LogOutSessionUser();

			Assert.Throws<ArgumentNullException>(() => AuthenticationModule.LogInCookieUser(null, "FooPassword"));
			Assert.Throws<ArgumentNullException>(() => AuthenticationModule.LogInCookieUser("FooUser", null));
			
			AuthenticationModule.LogInCookieUser("FooUser", "FooPassword", true);
			AuthenticationModule.LogOutCookieUser();

			//Assert.IsTrue((int)Manager.Session[Authentication.AuthenticationModule.SessionUserIdFieldName] == 12);
			//Assert.IsTrue((string)Manager.Session[Authentication.AuthenticationModule.SessionUserAuthenticationStatusFieldName] == "authenticated");
		}
	}
}