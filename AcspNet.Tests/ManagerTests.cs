using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Threading;
using System.Web;
using System.Web.UI;

using AcspNet.Tests.Extensions.Executable;
using AcspNet.Tests.Extensions.Library;

using ApplicationHelper.Templates;

using Moq;

using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	[LoadExtensionsFromAssemblyOf(typeof(ManagerTests))]
	[LoadIndividualExtensions(typeof(ExternalExecExtension), typeof(ExternalLibExtension))]
    public class ManagerTests
	{
		public Mock<HttpContextBase> GetTestHttpContext()
		{
			var context = new HttpContext(new HttpRequest("Foo", "http://localhost", ""), new HttpResponse(new StringWriter()));

			var httpContext = new Mock<HttpContextBase>();
			var httpRequest = new Mock<HttpRequestBase>();
			var httpResponse = new Mock<HttpResponseBase>();
			var httpSession = new Mock<HttpSessionStateBase>();

			httpContext.SetupGet(r => r.Request).Returns(httpRequest.Object);
			httpRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
			httpRequest.SetupGet(r => r.QueryString).Returns(new NameValueCollection());
			httpRequest.SetupGet(r => r.Form).Returns(new NameValueCollection());
			httpRequest.SetupGet(r => r.PhysicalApplicationPath).Returns(@"C:\WebSites\FooSite\");
			httpRequest.SetupGet(r => r.Url).Returns(new Uri("http://localhost"));
			httpRequest.SetupGet(r => r.ApplicationPath).Returns("/FooSite");

			httpContext.SetupGet(r => r.Response).Returns(httpResponse.Object);
			httpResponse.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
			httpResponse.SetupGet(r => r.Cache).Returns(new HttpCachePolicyWrapper(context.Response.Cache));

			httpContext.SetupGet(r => r.Session).Returns(httpSession.Object);

			return httpContext;
		}

		public IFileSystem GetTestFileSystem()
		{
			var files = new Dictionary<string, MockFileData>();
			files.Add("ExtensionsData/Bar.en.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>");
			files.Add("ExtensionsData/Bar.ru.txt", "Hello text!");
			files.Add("ExtensionsData/BarEmpty.en.txt", "");
			files.Add("ExtensionsData/BarDefault.en.txt", "Hello default!");
			files.Add("ExtensionsData/StringTable.en.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /><item name=\"InfoTitle\" value=\"Information!\" /><item name=\"FooEnumFooItem1\" value=\"Foo item text\" /><item name=\"HtmlListDefaultItemLabel\" value=\"Default label\" /></items>");
			files.Add("ExtensionsData/StringTable.ru.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок сайта!\" /></items>");
			files.Add("Templates/Foo.tpl", "Hello world!!!");
			files.Add("Templates/Index.tpl", Template.FromManifest("TestData.Index.tpl").Get());
			files.Add("Templates/AcspNet/MessageBox/OkMessageBox.tpl", "{Title}{Message}");
			files.Add("Templates/AcspNet/MessageBox/ErrorMessageBox.tpl", "{Title}{Message}");
			files.Add("Templates/AcspNet/MessageBox/InfoMessageBox.tpl", "{Title}{Message}");
			files.Add("Templates/AcspNet/MessageBox/InlineInfoMessageBox.tpl", "{Message}");
			files.Add("Templates/AcspNet/MessageBox/InlineErrorMessageBox.tpl", "{Message}");
			files.Add("Templates/AcspNet/MessageBox/InlineOkMessageBox.tpl", "{Message}");

			return new MockFileSystem(files, "C:/WebSites/FooSite");
		}

		public IFileSystem GetTestFileSystemForAsyncTesting()
		{
			var files = new Dictionary<string, MockFileData>();

			for (var i = 0; i < 1000; i++)
				files.Add("Templates/Async" + i + ".tpl", "<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>");

			return new MockFileSystem(files, "C:/WebSites/FooSite");
		}

		public Page GetTestPage()
		{
			return new Page();
		}

		public Manager GetTestManager()
		{
			var page = GetTestPage();
			var fs = GetTestFileSystem();
			var httpContext = GetTestHttpContext();
			Template.FileSystem = fs;

			return new Manager(page, httpContext.Object, fs);
		}

		[Test]
		public void VerifyManagerInitializeExceptionsThrownCorrectly()
		{
			Assert.Throws<ArgumentNullException>(() => new Manager(null));
			Assert.Throws<ArgumentNullException>(() => new Manager(null, null, null));
			Assert.Throws<ArgumentNullException>(() => new Manager(new Page(), null, null));
			Assert.Throws<ArgumentNullException>(() => new Manager(new Page(), GetTestHttpContext().Object, null));
		}

		[Test]
		public void VerifyManagerParametersInitializedCorrectly()
		{
			var manager = GetTestManager();
			
			Assert.IsNotNull(manager.Context);
			Assert.IsNotNull(manager.Request);
			Assert.IsNotNull(manager.Page);
			Assert.IsNotNull(manager.Response);
			Assert.IsNotNull(manager.Session);
			Assert.IsNotNull(manager.QueryString);
			Assert.IsNotNull(manager.Form);
			Assert.IsNotNull(manager.StopWatch);
			Assert.IsNotNull(manager.Settings);
			Assert.IsNotNull(manager.Environment);
			Assert.IsNotNull(manager.StringTable);
			Assert.IsNotNull(manager.DataCollector);
			Assert.IsNotNull(manager.HtmlContainer);
			Assert.IsNotNull(manager.HtmlContainer.ListsGenerator);
			Assert.IsNotNull(manager.HtmlContainer.MessageBox);
			Assert.IsNotNull(manager.AuthenticationModule);
			Assert.AreEqual("C:/WebSites/FooSite/", manager.SitePhysicalPath);
			Assert.AreEqual("http://localhost/FooSite/", manager.SiteUrl);
			Assert.IsNotNull(manager.CurrentAction);
			Assert.IsNotNull(manager.CurrentMode);
		}

		[Test]
		public void VerifyEnvironmentParametersInitializedCorrectly()
		{
			var manager = GetTestManager();

			Assert.AreEqual("en", manager.Environment.Language);

			Assert.AreEqual("Main", manager.Environment.SiteStyle);
			Assert.AreEqual("Templates", manager.Environment.TemplatesPath);
			Assert.AreEqual("C:/WebSites/FooSite/Templates", manager.Environment.TemplatesPhysicalPath);
		}

		[Test]
		public void TestEnvironmentBehaviour()
		{
			var manager = GetTestManager();

			manager.Environment.SetCookieLanguage(null);
			Assert.AreEqual(0, manager.Response.Cookies.Count);
			
			manager.Environment.SetCookieLanguage("ru");
			Assert.AreEqual(1, manager.Response.Cookies.Count);

			var cookie = manager.Response.Cookies[Environment.CookieLanguageFieldName];

			Assert.IsNotNull(cookie);
			Assert.AreEqual(Environment.CookieLanguageFieldName, cookie.Name);
			Assert.AreEqual("ru", cookie.Value);
		}

		[Test]
		public void TestExtensionsDataLoaderBehaviour()
		{
			var manager = GetTestManager();

			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Foo.en.xml", manager.DataLoader.GetFilePath("Foo.xml"));
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Foo.en.xml", manager.DataLoader.GetFilePath("Foo.xml", "en"));
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Foo.en", manager.DataLoader.GetFilePath("Foo"));

			manager.Environment.SetCurrentLanguage("ru");

			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Foo.en.xml", manager.DataLoader.GetFilePath("Foo.xml"));
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Bar.en.xml", manager.DataLoader.GetFilePath("Bar.xml"));

			Assert.AreEqual("Hello text!", manager.DataLoader.LoadTextDocument("Bar.txt", "ru"));
			Assert.AreEqual("Hello text!", manager.DataLoader.LoadTextDocument("Bar.txt"));
			Assert.IsNull(manager.DataLoader.LoadTextDocument("BarNot.txt"));
			Assert.AreEqual("", manager.DataLoader.LoadTextDocument("BarEmpty.txt"));
			Assert.AreEqual("Hello default!", manager.DataLoader.LoadTextDocument("BarDefault.txt"));

			var xDoc = manager.DataLoader.LoadXDocument("BarNot.xml", "ru");
			Assert.IsNull(xDoc);

			xDoc = manager.DataLoader.LoadXDocument("Bar.xml", "en");
			Assert.IsNotNull(xDoc);

			xDoc = manager.DataLoader.LoadXDocument("Bar.xml", "ru");
			Assert.IsNotNull(xDoc);

			var root = xDoc.Root;
			Assert.IsNotNull(root);
			Assert.AreEqual("items", root.Name.ToString());

			xDoc = manager.DataLoader.LoadXDocument("Bar.xml");
			Assert.IsNotNull(xDoc);
		}

		[Test]
		public void TestStringTableBehaviour()
		{
			var manager = GetTestManager();

			Assert.AreEqual(4, manager.StringTable.Items.Count);

			Assert.AreEqual("Your site title!", manager.StringTable["SiteTitle"]);
			Assert.AreEqual("Information!", manager.StringTable["InfoTitle"]);

			manager.Environment.SetCurrentLanguage("ru");
			manager.StringTable.Reload();

			Assert.AreEqual(4, manager.StringTable.Items.Count);
			Assert.AreEqual("Заголовок сайта!", manager.StringTable["SiteTitle"]);
			Assert.AreEqual("Information!", manager.StringTable["InfoTitle"]);

			Assert.IsNull(manager.StringTable["FooNotExist"]);

			Assert.AreEqual("Foo item text", manager.StringTable.GetAssociatedValue(FooEnum.FooItem1));
			Assert.AreEqual(null, manager.StringTable.GetAssociatedValue(FooEnum.FooItem2));			
		}

		[Test]
		public void TestTemplateFactoryBehaviour()
		{
			var manager = GetTestManager();

			Assert.Throws<ArgumentNullException>(() => manager.TemplateFactory.Load(null));
			Assert.Throws<TemplateException>(() => manager.TemplateFactory.Load("Not"));

			var tpl = manager.TemplateFactory.Load("Foo.tpl");

			Assert.IsNotNull(tpl);
			Assert.AreEqual("Hello world!!!", tpl.Get());

			manager.Settings.TemplatesMemoryCache = true;

			Assert.IsNotNull(tpl);
			Assert.AreEqual("Hello world!!!", manager.TemplateFactory.Load("Foo.tpl").Get());

			Assert.IsNotNull(tpl);
			Assert.AreEqual("Hello world!!!", manager.TemplateFactory.Load("Foo.tpl").Get());

			// Test async operations

			var fs = GetTestFileSystemForAsyncTesting();
			Template.FileSystem = fs;
			var manager1 = new Manager(GetTestPage(), GetTestHttpContext().Object, fs);
			var manager2 = new Manager(GetTestPage(), GetTestHttpContext().Object, fs);

			ThreadStart first = () => manager1.TemplateFactory.Load("Async1.tpl");
			ThreadStart second = () => manager2.TemplateFactory.Load("Async1.tpl");

			first.BeginInvoke(null, null);
			second.BeginInvoke(null, null);
		}

		[Test]
		public void TestDataCollectorBehaviour()
		{
			var page = GetTestPage();
			var fs = GetTestFileSystem();
			var httpContext = GetTestHttpContext();
			Template.FileSystem = fs;

			httpContext.Setup(x => x.Response.Write(It.IsAny<string>())).Callback<string>(TestDataCollectorResponseWriteResultNormal);

			var manager = new Manager(page, httpContext.Object, fs);

			manager.DataCollector.Add(null, null);
			Assert.IsFalse(manager.DataCollector.IsDataExist("Foo"));

			manager.DataCollector.Add("Foo", null);

			Assert.IsTrue(manager.DataCollector.IsDataExist("Foo"));

			manager.DataCollector.Add("Foo", "val");
			manager.DataCollector.Add("Foo2", "val");

			Assert.IsTrue(manager.DataCollector.IsDataExist("Foo2"));
	
			manager.DataCollector.AddSt("FooEnumFooItem1");

			Assert.IsTrue(manager.DataCollector.IsDataExist("MainContent"));

			manager.DataCollector.Add("bar");

			manager.DataCollector.AddTitleSt("InfoTitle");

			Assert.IsTrue(manager.DataCollector.IsDataExist("Title"));

			manager.DataCollector.AddTitle("FooTitle");

			Assert.IsFalse(manager.DataCollector.IsDataExist("Not"));

			manager.DataCollector.DisplaySite();

			manager.DataCollector.DisableSiteDisplay();

			httpContext.Setup(x => x.Response.Write(It.IsAny<string>())).Callback<string>(TestDataCollectorResponseWriteResultPartial);

			manager.DataCollector.DisplayPartial("Test!");
		}

		public void TestDataCollectorResponseWriteResultNormal(string s)
		{
			Assert.AreEqual(Template.FromManifest("TestData.IndexResult.tpl").Get(), s);
		}

		public void TestDataCollectorResponseWriteResultPartial(string s)
		{
			Assert.AreEqual("Test!", s);
		}

		[Test]
		public void TestMainExecution()
		{
			var manager = GetTestManager();
			manager.Run();

			Assert.IsTrue(manager.IsNewSession);
		}

		[Test]
		public void TestFooBarPageExecution()
		{
			var page = GetTestPage();
			var fs = GetTestFileSystem();
			var httpContext = GetTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "foo");
			httpContext.Object.Request.QueryString.Add("mode", "bar");
			httpContext.Object.Request.QueryString.Add("id", "15");

			var manager = new Manager(page, httpContext.Object, fs);

			manager.Run();
		}

		[Test]
		public void TestFooPageExecution()
		{
			var page = GetTestPage();
			var fs = GetTestFileSystem();
			var httpContext = GetTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "foo");

			var manager = new Manager(page, httpContext.Object, fs);

			manager.Run();
		}

		[Test]
		public void TestFoo2PageExecution()
		{
			var page = GetTestPage();
			var fs = GetTestFileSystem();
			var httpContext = GetTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "foo2");
			httpContext.Object.Request.QueryString.Add("id", "2");

			var manager = new Manager(page, httpContext.Object, fs);

			manager.Run();

			Assert.Throws<ArgumentNullException>(() => manager.Redirect(""));

			manager.Redirect("http://localhost");
		}
	}

	public enum FooEnum
	{
		FooItem1,
		FooItem2
	}
}
