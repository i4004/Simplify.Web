//namespace AcspNet.Tests
//{
//	[TestFixture]
//	[LoadExtensionsFromAssemblyOf(typeof(ManagerTests))]
//	[LoadIndividualExtensions(typeof(ExternalExecExtensionTest), typeof(ExternalLibExtensionTest))]
//	public class ManagerTests
//	{
//		private Mock<HttpResponseBase> _httpResponse;

//		public Mock<HttpContextBase> GetTestHttpContext()
//		{
//			var context = new HttpContext(new HttpRequest("Foo", "http://localhost", ""), new HttpResponse(new StringWriter()));

//			var httpContext = new Mock<HttpContextBase>();
//			var httpRequest = new Mock<HttpRequestBase>();
//			_httpResponse = new Mock<HttpResponseBase>();
//			var httpSession = new Mock<HttpSessionStateBase>();
//			var cookieCollection = new HttpCookieCollection();

//			httpRequest.SetupGet(r => r.Url).Returns(new Uri("http://localhost"));
//			httpRequest.SetupGet(r => r.RawUrl).Returns("http://localhost/FooSite/");
//			httpRequest.SetupGet(r => r.ApplicationPath).Returns("/FooSite");

//			_httpResponse.SetupGet(r => r.Cookies).Returns(cookieCollection);
//			_httpResponse.SetupGet(r => r.Cache).Returns(new HttpCachePolicyWrapper(context.Response.Cache));

//			var sessions = new Dictionary<string, object>();
//			httpSession.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<object>()))
//				.Callback((string key, object value) =>
//						  {
//							  if (!sessions.ContainsKey(key))
//								  sessions.Add(key, value);
//						  });

//			httpSession.Setup(x => x[It.IsAny<string>()])
//				.Returns((string key) => sessions.ContainsKey(key) ? sessions[key] : null);
//			httpSession.Setup(x => x.Remove(It.IsAny<string>())).Callback((string key) => sessions.Remove(key));

//			return httpContext;
//		}

//		public IFileSystem GetTestFileSystem()
//		{
//			var files = new Dictionary<string, MockFileData>();

//			files.Add("ExtensionsData/Bar.en.xml",
//				"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>");
//			files.Add("ExtensionsData/Bar.ru.txt", "Hello text!");
//			files.Add("ExtensionsData/Empty.en.txt", "");
//			files.Add("ExtensionsData/BarDefault.en.txt", "Hello default!");

//			files.Add("ExtensionsData/StringTable.en.xml",
//				"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /><item name=\"InfoTitle\" value=\"Information!\" /><item name=\"FooEnumFooItem1\" value=\"Foo item text\" /><item name=\"HtmlListDefaultItemLabel\" value=\"Default label\" /><item name=\"NotifyPageDataError\" value=\"Page data error!\" /></items>");
//			files.Add("ExtensionsData/StringTable.ru.xml",
//				"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок сайта!\" /></items>");

//			files.Add("Templates/Foo.tpl", "Hello world!!!");

//			files.Add("Templates/Index.tpl", Template.FromManifest("TestData.Index.tpl").Get());

//			files.Add("Templates/AcspNet/MessageBox/OkMessageBox.tpl", "{Title}{Message}");
//			files.Add("Templates/AcspNet/MessageBox/ErrorMessageBox.tpl", "{Title}{Message}");
//			files.Add("Templates/AcspNet/MessageBox/InfoMessageBox.tpl", "{Title}{Message}");
//			files.Add("Templates/AcspNet/MessageBox/InlineInfoMessageBox.tpl", "{Message}");
//			files.Add("Templates/AcspNet/MessageBox/InlineErrorMessageBox.tpl", "{Message}");
//			files.Add("Templates/AcspNet/MessageBox/InlineOkMessageBox.tpl", "{Message}");

//			return new MockFileSystem(files, "C:/WebSites/FooSite");
//		}

//		public IFileSystem GetTestFileSystemForAsyncTesting()
//		{
//			var files = new Dictionary<string, MockFileData>();

//			for (var i = 0; i < 1000; i++)
//				files.Add("Templates/Async" + i + ".tpl",
//					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>");

//			return new MockFileSystem(files, "C:/WebSites/FooSite");
//		}

//		[Test]
//		public void Manager_Initialize_ParametersInitializedCorrectly()
//		{
//			var manager = GetTestManager();

//			Assert.IsNotNull(manager.StopWatch);
//			Assert.IsNotNull(manager.HtmlWrapper);
//			Assert.IsNotNull(manager.HtmlWrapper.ListsGenerator);
//			Assert.IsNotNull(manager.HtmlWrapper.MessageBox);
//			Assert.IsNotNull(manager.AuthenticationModule);
//			Assert.IsNotNull(manager.ExtensionsWrapper);
//			Assert.IsNotNull(manager.ExtensionsWrapper.MessagePage);
//			Assert.IsNotNull(manager.ExtensionsWrapper.IdProcessor);
//			Assert.IsNotNull(manager.ExtensionsWrapper.Navigator);
//			Assert.AreEqual("http://localhost/FooSite/", Manager.SiteUrl);
//		}

//		[Test]
//		public void MainPage_Execution_BehaviourIsCorrect()
//		{
//			RouteConfig.RegisterRoutes();
//			var manager = GetTestManager();

//			Assert.IsTrue(manager.IsNewSession);

//			manager.Run();

//			Assert.IsFalse(manager.IsNewSession);
//		}

//		[Test]
//		public void Manager_Usage_GetExtensionsMetadataIsCorrect()
//		{
//			var manager = GetTestManager("getExtensionsMetadataTest");
//			manager.Run();
//		}

//		[Test]
//		public void Manager_Redirect_BehaviourIsCorrect()
//		{
//			var manager = GetTestManager();
//			manager.Run();

//			Assert.Throws<ArgumentNullException>(() => manager.Redirect(""));

//			manager.Redirect("http://localhost");
//		}

//		[Test]
//		public void Manager_Usage_MessagePageBehaviourIsCorrect()
//		{
//			var routeData = AcspNetTestingHelper.GetTestRouteData();
//			var httpContext = GetTestHttpContext();
//			var fs = GetTestFileSystem();
//			var userAssembly = GetTestUserAssembly();
//			Template.FileSystem = fs;

//			var manager = new Manager(routeData, httpContext.Object, fs, AcspNetTestingHelper.GetTestHttpRuntime(), userAssembly);
//			manager.Run();

//			manager.ExtensionsWrapper.MessagePage.Message = "Hello!";
//			manager.ExtensionsWrapper.MessagePage.NavigateToMessagePage();

//			httpContext.Object.Request.QueryString.Add("act", "message");
//			manager = new Manager(routeData, httpContext.Object, fs, AcspNetTestingHelper.GetTestHttpRuntime(), userAssembly);
//			manager.Run();

//			manager.ExtensionsWrapper.MessagePage.NavigateToMessagePage("Hello!");
//			manager = new Manager(routeData, httpContext.Object, fs, AcspNetTestingHelper.GetTestHttpRuntime(), userAssembly);
//			manager.Run();

//			manager.Run();
//		}

//		[Test]
//		public void Manager_Usage_ExtensionsProtectorBehaviourIsCorrect()
//		{
//			var manager = GetTestManager("extensionsProtectorTest");
//			manager.Run();
//		}
//	}
//}
