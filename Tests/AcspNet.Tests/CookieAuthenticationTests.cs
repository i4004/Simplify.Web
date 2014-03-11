using System;
using System.Collections.Specialized;
using System.Web;
using AcspNet.Authentication;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class CookieAuthenticationTests
	{
		[Test]
		public void LogInCookieUser_NullParameters_ExceptionsThrown()
		{
			var ca = new CookieAuthentication(null, null, null);

			Assert.Throws<ArgumentNullException>(() => ca.LogInCookieUser(null, null));
			Assert.Throws<ArgumentNullException>(() => ca.LogInCookieUser("Test", null));
		}

		[Test]
		public void LogInCookieUser_RegularUser_DataAddedToCookies()
		{
			var cookies = new HttpCookieCollection();
			var state = new Mock<IAuthenticationState>();

			var ca = new CookieAuthentication(null, cookies, state.Object);

			Assert.AreEqual(0, cookies.Count);

			ca.LogInCookieUser("FooName", "FooPassword");

			Assert.AreEqual(2, cookies.Count);

			var nameCookie = cookies[CookieAuthentication.CookieUserNameFieldName];
			Assert.IsNotNull(nameCookie);
			Assert.IsTrue(nameCookie.Value == "FooName");
			Assert.AreEqual(DateTime.MinValue, nameCookie.Expires);

			var passwordCookie = cookies[CookieAuthentication.CookieUserPasswordFieldName];
			Assert.IsNotNull(passwordCookie);
			Assert.IsTrue(passwordCookie.Value == "FooPassword");
			Assert.AreEqual(DateTime.MinValue, passwordCookie.Expires);
		}

		[Test]
		public void LogInCookieUser_RegularUserAutologin_DataAddedToCookies()
		{
			var cookies = new HttpCookieCollection();
			var state = new Mock<IAuthenticationState>();

			var tp = new Mock<TimeProvider>();
			tp.SetupGet(x => x.Now).Returns(new DateTime(2014, 03, 11));
			TimeProvider.Current = tp.Object;

			var ca = new CookieAuthentication(null, cookies, state.Object);

			Assert.AreEqual(0, cookies.Count);

			ca.LogInCookieUser("FooName", "FooPassword", true);

			Assert.AreEqual(2, cookies.Count);

			var nameCookie = cookies[CookieAuthentication.CookieUserNameFieldName];
			Assert.IsNotNull(nameCookie);
			Assert.IsTrue(nameCookie.Value == "FooName");
			Assert.AreEqual(tp.Object.Now.AddDays(256), nameCookie.Expires);

			var passwordCookie = cookies[CookieAuthentication.CookieUserPasswordFieldName];
			Assert.IsNotNull(passwordCookie);
			Assert.IsTrue(passwordCookie.Value == "FooPassword");
			Assert.AreEqual(tp.Object.Now.AddDays(256), passwordCookie.Expires);
		}

		//[Test]
		//public void LogOutSessionUser_RegularUser_DataRemovedFromSession()
		//{
		//	var session = new Mock<HttpSessionStateBase>();
		//	var state = new Mock<IAuthenticationState>();

		//	var sa = new SessionAuthentication(session.Object, state.Object);

		//	sa.LogOutSessionUser();

		//	session.Verify(
		//		x => x.Remove(It.Is<string>(c => c == SessionAuthentication.SessionUserAuthenticationStatusFieldName)),
		//		Times.Once());

		//	session.Verify(
		//		x => x.Remove(It.Is<string>(c => c == SessionAuthentication.SessionUserIdFieldName)),
		//		Times.Once());

		//	state.Verify(x => x.Reset(), Times.Once);
		//}

		//[Test]
		//public void AuthenticateSessionUser_LoggedUser_DataRemovedFromSession()
		//{
		//	var session = new Mock<HttpSessionStateBase>();
		//	var state = new Mock<IAuthenticationState>();

		//	session.Setup(x => x[It.Is<string>(c => c == SessionAuthentication.SessionUserAuthenticationStatusFieldName)])
		//		.Returns("authenticated");
		//	session.Setup(x => x[It.Is<string>(c => c == SessionAuthentication.SessionUserIdFieldName)])
		//		.Returns(5);

		//	//session.Object.Add(SessionAuthentication.SessionUserAuthenticationStatusFieldName, "authenticated");
		//	//session.Object.Add(SessionAuthentication.SessionUserIdFieldName, 5);

		//	var sa = new SessionAuthentication(session.Object, state.Object);

		//	sa.AuthenticateSessionUser();

		//	state.Verify(x => x.SetAuthenticated(It.Is<int>(c => c == 5), It.IsAny<string>()), Times.Once);
		//}

		//[Test]
		//public void AuthenticateSessionUser_NotLoggedUser_DataRemovedFromSession()
		//{
		//	var session = new Mock<HttpSessionStateBase>();
		//	var state = new Mock<IAuthenticationState>();

		//	var sa = new SessionAuthentication(session.Object, state.Object);

		//	sa.AuthenticateSessionUser();

		//	state.Verify(x => x.SetAuthenticated(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
		//}
	}
}