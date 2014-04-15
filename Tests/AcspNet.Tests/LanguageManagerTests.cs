using System;
using System.Web;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class LanguageManagerTests
	{
		[Test]
		public void SetCookieLanguage_NullValue_ExceptionThrown()
		{
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var env = new LanguageManager("en", requestCookies, responseCookies);

			Assert.Throws<ArgumentNullException>(() => env.SetCookieLanguage(null));
		}

		[Test]
		public void SetCookieLanguage_NormalValue_CookieCreatedCorrectly()
		{
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var env = new LanguageManager("en", requestCookies, responseCookies);

			Assert.AreEqual(0, requestCookies.Count);
			Assert.AreEqual(0, responseCookies.Count);

			env.SetCookieLanguage("ru");

			Assert.AreEqual(0, requestCookies.Count);
			Assert.AreEqual(1, responseCookies.Count);

			var cookie = responseCookies[LanguageManager.CookieLanguageFieldName];

			Assert.IsNotNull(cookie);
			Assert.AreEqual(LanguageManager.CookieLanguageFieldName, cookie.Name);
			Assert.AreEqual("ru", cookie.Value);
		}
	}
}