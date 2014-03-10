using System;
using System.Web;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class EnvironmentTests
	{
		[Test]
		public void Constructor_DefaultParamenters_PropertiesSetCorrecly()
		{
			var settings = new AcspSettings();
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var env = new Environment("C:/Test", settings, requestCookies, responseCookies);

			Assert.AreEqual("en", env.Language);
			Assert.AreEqual("Main", env.SiteStyle);
			Assert.AreEqual("Templates", env.TemplatesPath);
			Assert.AreEqual("ExtensionsData", env.ExtensionsDataPath);
			Assert.AreEqual("MainContent", env.MainContentVariableName);
			Assert.AreEqual("Index.tpl", env.MasterTemplateFileName);
			Assert.IsFalse(env.TemplatesMemoryCache);
			Assert.AreEqual("Templates", env.TemplatesPath);
			Assert.AreEqual("C:/Test/Templates", env.TemplatesPhysicalPath);
			Assert.AreEqual("Title", env.TitleVariableName);
		}

		[Test]
		public void SetCookieLanguage_NullValue_ExceptionThrown()
		{
			var settings = new AcspSettings();
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var env = new Environment("C:/Test/", settings, requestCookies, responseCookies);

			Assert.Throws<ArgumentNullException>(() => env.SetCookieLanguage(null));
		}

		[Test]
		public void SetCookieLanguage_NormalValue_CookieCreatedCorrectly()
		{
			var settings = new AcspSettings();
			var requestCookies = new HttpCookieCollection();
			var responseCookies = new HttpCookieCollection();
			var env = new Environment("C:/Test/", settings, requestCookies, responseCookies);

			Assert.AreEqual(0, requestCookies.Count);
			Assert.AreEqual(0, responseCookies.Count);

			env.SetCookieLanguage("ru");

			Assert.AreEqual(0, requestCookies.Count);
			Assert.AreEqual(1, responseCookies.Count);

			var cookie = responseCookies[Environment.CookieLanguageFieldName];

			Assert.IsNotNull(cookie);
			Assert.AreEqual(Environment.CookieLanguageFieldName, cookie.Name);
			Assert.AreEqual("ru", cookie.Value);
		}
	}
}
