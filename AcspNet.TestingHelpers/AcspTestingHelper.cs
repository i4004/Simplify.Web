using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Web;
using System.Web.Routing;
using Moq;

namespace AcspNet.TestingHelpers
{
	/// <summary>
	/// Provides default ACSP Manager parameters for unit tests
	/// </summary>
	public static class AcspTestingHelper
	{
		/// <summary>
		/// Gets the default test HTTP context for AcspNet unit tests.
		/// </summary>
		/// <returns></returns>
		public static Mock<HttpContextBase> CreateTestHttpContext()
		{

			var httpContext = new Mock<HttpContextBase>();
			var httpRequest = new Mock<HttpRequestBase>();
			var httpResponse = new Mock<HttpResponseBase>();

			httpContext.SetupGet(r => r.Request).Returns(httpRequest.Object);
			httpContext.SetupGet(r => r.Response).Returns(httpResponse.Object);

			var httpSession = new Mock<HttpSessionStateBase>();
			httpContext.SetupGet(r => r.Session).Returns(httpSession.Object);

			var requestCookies = new HttpCookieCollection();
			httpRequest.SetupGet(r => r.Cookies).Returns(requestCookies);

			httpRequest.SetupGet(r => r.PhysicalApplicationPath).Returns(@"C:\WebSites\TestSite\");
			httpRequest.SetupGet(r => r.QueryString).Returns(new NameValueCollection());

			httpRequest.SetupGet(r => r.Form).Returns(new NameValueCollection());
			//httpRequest.SetupGet(r => r.Url).Returns(new Uri("http://localhost"));
			//httpRequest.SetupGet(r => r.RawUrl).Returns("http://localhost/TestSite/");
			//httpRequest.SetupGet(r => r.ApplicationPath).Returns("/TestSite");

			var responseCookies = new HttpCookieCollection();
			httpResponse.SetupGet(r => r.Cookies).Returns(responseCookies);

			var httpCacheContext = new HttpContext(new HttpRequest("TestRequest", "http://localhost", ""), new HttpResponse(new StringWriter()));
			httpResponse.SetupGet(r => r.Cache).Returns(new HttpCachePolicyWrapper(httpCacheContext.Response.Cache));

			//var sessions = new Dictionary<string, object>();
			//httpSession.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<object>()))
			//	.Callback((string key, object value) =>
			//	{
			//		if (!sessions.ContainsKey(key))
			//			sessions.Add(key, value);
			//	});

			//httpSession.Setup(x => x[It.IsAny<string>()])
			//	.Returns((string key) => sessions.ContainsKey(key) ? sessions[key] : null);
			//httpSession.Setup(x => x.Remove(It.IsAny<string>())).Callback((string key) => sessions.Remove(key));

			return httpContext;
		}

		/// <summary>
		/// Gets the default route data for AcspNet unit tests.
		/// </summary>
		/// <returns></returns>
		public static RouteData CreateTestRouteData()
		{
			return new RouteData();
		}

		/// <summary>
		/// Creates the route data with action
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static RouteData CreateRouteDataWithActionAndId(string action)
		{
			var routeData = new RouteData();
			routeData.Values.Add("action", action);
			return routeData;
		}

		/// <summary>
		/// Creates the route data with action and identifier.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public static RouteData CreateRouteDataWithActionAndId(string action, string id)
		{
			var routeData = new RouteData();

			routeData.Values.Add("action", action);
			routeData.Values.Add("id", id);

			return routeData;
		}

		/// <summary>
		/// Creates the route data with action, mode and identifier.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public static RouteData CreateRouteDataWithActionModeAndId(string action, string mode, string id)
		{
			var routeData = new RouteData();

			routeData.Values.Add("action", action);
			routeData.Values.Add("mode", mode);
			routeData.Values.Add("id", id);

			return routeData;
		}

		/// <summary>
		/// Gets the default test file system for AcspNet unit tests.
		/// </summary>
		/// <returns></returns>
		public static IFileSystem GetTestFileSystem()
		{
			var files = new Dictionary<string, MockFileData>();
			files.Add("Templates/Index.tpl", "");

			return new MockFileSystem(files, "C:/WebSites/TestSite");
		}


//		/// <summary>
//		/// Gets the default test HTTP runtime for AcspNet unit tests.
//		/// </summary>
//		/// <returns></returns>
//		public static IHttpRuntime GetTestHttpRuntime()
//		{
//			var httpRuntime = new Mock<IHttpRuntime>();

//			httpRuntime.SetupGet(x => x.AppDomainAppVirtualPath).Returns("/FooSite");

//			return httpRuntime.Object;
//		}
	}
}