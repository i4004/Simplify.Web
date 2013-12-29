using System;
using System.Web;
using System.Web.UI;

using Moq;

using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
    public class ManagerTests
	{
		public HttpContextBase GetTestHttpContext()
		{
			var httpContext = new Mock<HttpContextBase>();
			var httpRequest = new Mock<HttpRequestBase>();
			var httpResponse = new Mock<HttpResponseBase>();


			httpContext.SetupGet(r => r.Request).Returns(httpRequest.Object);
			httpRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
			httpRequest.SetupGet(r => r.PhysicalApplicationPath).Returns(@"C:\WebSites\TestSite\");

			httpContext.SetupGet(r => r.Response).Returns(httpResponse.Object);
			httpResponse.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());

			return httpContext.Object;
		}

		[Test]
		public void TestManagerExecution()
		{
			Assert.Throws<ArgumentNullException>(() => new Manager(null, null));
			Assert.Throws<ArgumentNullException>(() => new Manager(new Page(), null));

			var manager = new Manager(new Page(), GetTestHttpContext());

			Assert.IsNotNull(manager.Context);
			Assert.IsNotNull(manager.Request);
			Assert.IsNotNull(manager.Page);
			Assert.IsNotNull(manager.Response);
			Assert.IsNotNull(manager.StopWatch);
			Assert.IsNotNull(manager.Settings);
			Assert.IsNotNull(manager.Environment);
			Assert.AreEqual("C:/WebSites/TestSite/", manager.SitePhysicalPath);
			Assert.AreEqual("en", manager.Environment.Language);
			Assert.IsNull(manager.Environment.SiteStyle);
			Assert.AreEqual("Templates", manager.Environment.TemplatesPath);
			Assert.AreEqual("C:/WebSites/TestSite/Templates", manager.Environment.TemplatesPhysicalPath);

			TestEnvironment(manager);
			TestExtensionsDataLoader(manager);
			
			var sessings = manager.Settings;

			manager = new Manager(new Page(), GetTestHttpContext());

			Assert.AreEqual(sessings, manager.Settings);
		}

		public void TestEnvironment(Manager manager)
		{
			manager.Environment.SetCookieLanguage(null);
			Assert.AreEqual(0, manager.Response.Cookies.Count);
			
			manager.Environment.SetCookieLanguage("ru");
			Assert.AreEqual(1, manager.Response.Cookies.Count);

			var cookie = manager.Response.Cookies[Environment.CookieLanguageFieldName];

			Assert.IsNotNull(cookie);
			Assert.AreEqual(Environment.CookieLanguageFieldName, cookie.Name);
			Assert.AreEqual("ru", cookie.Value);
		}

		public void TestExtensionsDataLoader(Manager manager)
		{
			Assert.AreEqual("C:/WebSites/TestSite/ExtensionsData/test.en.xml", manager.DataLoader.GetFilePath("test.xml"));
			Assert.AreEqual("C:/WebSites/TestSite/ExtensionsData/test.en.xml", manager.DataLoader.GetFilePath("test.xml", "en"));
			Assert.AreEqual("C:/WebSites/TestSite/ExtensionsData/test.en", manager.DataLoader.GetFilePath("test"));
		}
    }
}
