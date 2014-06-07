using System;
using System.Web;
using AcspNet.Modules.Identity;
using Moq;
using NUnit.Framework;
using Simplify.Core;

namespace AcspNet.Tests.Modules.Identity
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

		[Test]
		public void LogOutCookieUser_RegularUser_CookieExpireTimeAdded()
		{
			var cookies = new HttpCookieCollection();
			var state = new Mock<IAuthenticationState>();

			var tp = new Mock<TimeProvider>();
			tp.SetupGet(x => x.Now).Returns(new DateTime(2014, 03, 11));
			TimeProvider.Current = tp.Object;

			var ca = new CookieAuthentication(null, cookies, state.Object);

			ca.LogOutCookieUser();

			Assert.AreEqual(2, cookies.Count);

			var nameCookie = cookies[CookieAuthentication.CookieUserNameFieldName];
			Assert.IsNotNull(nameCookie);
			Assert.AreEqual(tp.Object.Now.AddDays(-1d), nameCookie.Expires);

			var passwordCookie = cookies[CookieAuthentication.CookieUserPasswordFieldName];
			Assert.IsNotNull(passwordCookie);
			Assert.AreEqual(tp.Object.Now.AddDays(-1d), passwordCookie.Expires);

			state.Verify(x => x.Reset(), Times.Once);
		}

		[Test]
		public void AuthenticateCookieUser_NullParameters_ExceptionsThrown()
		{
			var ca = new CookieAuthentication(null, null, null);

			Assert.Throws<ArgumentException>(() => ca.AuthenticateCookieUser(-1, null, null));
			Assert.Throws<ArgumentNullException>(() => ca.AuthenticateCookieUser(1, null, null));
			Assert.Throws<ArgumentNullException>(() => ca.AuthenticateCookieUser(1, "Test", null));
		}

		[Test]
		public void AuthenticateCookieUser_LoggedUser_StateSetAuthenticatedSet()
		{
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var state = new Mock<IAuthenticationState>();

			var cookie = new HttpCookie(CookieAuthentication.CookieUserNameFieldName, "FooName");
			requestCookies.Add(cookie);

			cookie = new HttpCookie(CookieAuthentication.CookieUserPasswordFieldName, "FooPassword");
			requestCookies.Add(cookie);

			var ca = new CookieAuthentication(requestCookies, responseCookies, state.Object);

			ca.AuthenticateCookieUser(1, "FooName", "FooPassword");

			state.Verify(x => x.SetAuthenticated(It.Is<int>(c => c == 1), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void AuthenticateSessionUser_NotLoggedOrInvalidUser_StateSetAuthenticatedIsNotSetAndCookieRemoved()
		{
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var state = new Mock<IAuthenticationState>();

			var cookie = new HttpCookie(CookieAuthentication.CookieUserNameFieldName, "FooName");
			requestCookies.Add(cookie);

			var ca = new CookieAuthentication(requestCookies, responseCookies, state.Object);

			ca.AuthenticateCookieUser(1, "FooName", "FooPassword");

			Assert.AreEqual(0, requestCookies.Count);
			state.Verify(x => x.Reset(), Times.Never);
		}

		[Test]
		public void UserNameAndPasswordFromCookier_DataExist_DataGet()
		{
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var state = new Mock<IAuthenticationState>();

			var cookie = new HttpCookie(CookieAuthentication.CookieUserNameFieldName, "FooName");
			requestCookies.Add(cookie);

			cookie = new HttpCookie(CookieAuthentication.CookieUserPasswordFieldName, "FooPassword");
			requestCookies.Add(cookie);

			var ca = new CookieAuthentication(requestCookies, responseCookies, state.Object);

			Assert.AreEqual("FooName", ca.UserNameFromCookie);
			Assert.AreEqual("FooPassword", ca.UserPasswordFromCookie);
		}

		[Test]
		public void UserNameAndPasswordFromCookier_DataNotExist_NullDataGet()
		{
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var state = new Mock<IAuthenticationState>();

			var ca = new CookieAuthentication(requestCookies, responseCookies, state.Object);

			Assert.IsNull(ca.UserNameFromCookie);
			Assert.IsNull(ca.UserPasswordFromCookie);
		}
	}
}