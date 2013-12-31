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
			files.Add("Bar.en.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>");
			files.Add("Bar.ru.txt", "Hello text!");
			files.Add("BarEmpty.en.txt", "");
			files.Add("BarDefault.en.txt", "Hello default!");
			files.Add("StringTable.en.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /><item name=\"InfoTitle\" value=\"Information!\" /><item name=\"FooEnumFooItem1\" value=\"Foo item text\" /></items>");
			files.Add("StringTable.ru.xml", "<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок сайта!\" /></items>");

			var fs = new Mock<IFileSystem>();
			fs.Verify();

			return new MockFileSystem(files, "C:/WebSites/FooSite/ExtensionsData");
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

			return new Manager(page, httpContext, fs);
		}

		[Test]
		public void VerifyManagerInitializeExceptionsThrownCorrectly()
		{
			Assert.Throws<ArgumentNullException>(() => new Manager(null));
			Assert.Throws<ArgumentNullException>(() => new Manager(null, null, null));
			Assert.Throws<ArgumentNullException>(() => new Manager(new Page(), null, null));
			Assert.Throws<ArgumentNullException>(() => new Manager(new Page(), GetTestHttpContext(), null));
		}

		[Test]
		public void VerifyManagerParametersInitializedCorrectly()
		{
			var manager = GetTestManager();
			
			Assert.IsNotNull(manager.Context);
			Assert.IsNotNull(manager.Request);
			Assert.IsNotNull(manager.Page);
			Assert.IsNotNull(manager.Response);
			Assert.IsNotNull(manager.StopWatch);
			Assert.IsNotNull(manager.Settings);
			Assert.IsNotNull(manager.Environment);
			Assert.IsNotNull(manager.StringTable);
			Assert.IsNotNull(manager.DataCollector);
			Assert.AreEqual("C:/WebSites/FooSite/", manager.SitePhysicalPath);
		}

		[Test]
		public void VerifyEnvironmentParametersInitializedCorrectly()
		{
			var manager = GetTestManager();

			Assert.AreEqual("en", manager.Environment.Language);

			Assert.IsNull(manager.Environment.SiteStyle);
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

			Assert.AreEqual(3, manager.StringTable.Items.Count);

			Assert.AreEqual("Your site title!", manager.StringTable["SiteTitle"]);
			Assert.AreEqual("Information!", manager.StringTable["InfoTitle"]);

			manager.Environment.SetCurrentLanguage("ru");
			manager.StringTable.Reload();

			Assert.AreEqual(3, manager.StringTable.Items.Count);
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

			Assert.IsNull(manager.TemplateFactory.Load(null));
			Assert.IsNull(manager.TemplateFactory.Load("Not"));
			var tpl = manager.TemplateFactory.Load("Not");

			Assert.IsNotNull(tpl);

			Assert.AreEqual("Hello world!!!", tpl.Get());
		}

		[Test]
		public void TestDataCollectorBehaviour()
		{
			var manager = GetTestManager();

			manager.DataCollector.Set(null, null);
			manager.DataCollector.Set("Foo", null);
			Assert.Throws<AcspNetException>(() => manager.DataCollector.Set("Foo", null));
			manager.DataCollector.Set("Foo", null, DataCollectorAddType.AddFromBegin);
			manager.DataCollector.Set("Foo", null, DataCollectorAddType.AddFromEnd);
			manager.DataCollector.Set("Foo", "val", DataCollectorAddType.AddFromEnd);
			manager.DataCollector.Set("Foo2", "val");

			manager.DataCollector.Set("bar");
			manager.DataCollector.SetTitle("FooTitle");
			manager.DataCollector.SetSt("Foo", "FooValue");
			manager.DataCollector.SetSt("FooValue", DataCollectorAddType.AddFromEnd);
			manager.DataCollector.SetTitleSt("FooTitle", DataCollectorAddType.AddFromEnd);

			Assert.IsTrue(manager.DataCollector.IsDataExist("Foo"));
			Assert.IsFalse(manager.DataCollector.IsDataExist("Not"));

			manager.DataCollector.DisplaySite();

			manager.DataCollector.DisableSiteDisplay();
			manager.DataCollector.DisplaySite();
		}
    }

	public enum FooEnum
	{
		FooItem1,
		FooItem2
	}
}
