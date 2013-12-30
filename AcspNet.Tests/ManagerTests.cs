using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
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
			httpRequest.SetupGet(r => r.PhysicalApplicationPath).Returns(@"C:\WebSites\FooSite\");

			httpContext.SetupGet(r => r.Response).Returns(httpResponse.Object);
			httpResponse.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());

			return httpContext.Object;
		}

		public IFileSystem GetTestFileSystem()
		{
			var files = new Dictionary<string, MockFileData>();
			files.Add("bar.ru.xml", null);

			return new MockFileSystem(files, "C:/WebSites/FooSite/ExtensionsData");
		}

		public Page GetTestPage()
		{
			return new Page();
		}

		[Test]
		public void TestManagerExecution()
		{
			Assert.Throws<ArgumentNullException>(() => new Manager(null, null, null));
			Assert.Throws<ArgumentNullException>(() => new Manager(new Page(), null, null));
			Assert.Throws<ArgumentNullException>(() => new Manager(new Page(), GetTestHttpContext(), null));

			var page = GetTestPage();
			var fs = GetTestFileSystem();
			var httpContext = GetTestHttpContext();
			var manager = new Manager(page, httpContext, fs);

			Assert.IsNotNull(manager.Context);
			Assert.IsNotNull(manager.Request);
			Assert.IsNotNull(manager.Page);
			Assert.IsNotNull(manager.Response);
			Assert.IsNotNull(manager.StopWatch);
			Assert.IsNotNull(manager.Settings);
			Assert.IsNotNull(manager.Environment);
			Assert.AreEqual("C:/WebSites/FooSite/", manager.SitePhysicalPath);
			Assert.AreEqual("en", manager.Environment.Language);
			Assert.IsNull(manager.Environment.SiteStyle);
			Assert.AreEqual("Templates", manager.Environment.TemplatesPath);
			Assert.AreEqual("C:/WebSites/FooSite/Templates", manager.Environment.TemplatesPhysicalPath);

			TestEnvironment(manager);
			TestExtensionsDataLoader(manager);

			// Testing seconds page request
			
			var sessings = manager.Settings;
			manager = new Manager(page, httpContext, fs);

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
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/foo.en.xml", manager.DataLoader.GetFilePath("foo.xml"));
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/foo.en.xml", manager.DataLoader.GetFilePath("foo.xml", "en"));
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/foo.en", manager.DataLoader.GetFilePath("foo"));

			manager.Environment.SetCurrentLanguage("ru");

			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/foo.en.xml", manager.DataLoader.GetFilePath("foo.xml"));
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/bar.ru.xml", manager.DataLoader.GetFilePath("bar.xml"));
		}
    }
}
