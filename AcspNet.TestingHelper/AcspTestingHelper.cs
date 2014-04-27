using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Moq;

namespace AcspNet.TestingHelper
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
			httpRequest.SetupGet(r => r.Url).Returns(new Uri("http://localhost/TestSite/"));
			httpRequest.SetupGet(r => r.ApplicationPath).Returns("/TestSite");

			var responseCookies = new HttpCookieCollection();
			httpResponse.SetupGet(r => r.Cookies).Returns(responseCookies);

			var httpCacheContext = new HttpContext(new HttpRequest("TestRequest", "http://localhost", ""), new HttpResponse(new StringWriter()));
			httpResponse.SetupGet(r => r.Cache).Returns(new HttpCachePolicyWrapper(httpCacheContext.Response.Cache));

			return httpContext;
		}
	}
}