using System;
using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// Contains HTTP and ACSP context information
	/// </summary>
	public class AcspContext : IAcspContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AcspContext"/> class.
		/// </summary>
		/// <param name="routeData">The current page route data.</param>
		/// <param name="httpContext">The HTTP context.</param>
		internal AcspContext(RouteData routeData, HttpContextBase httpContext)
		{
			if (routeData == null)
				throw new ArgumentNullException("routeData");

			if (httpContext == null)
				throw new ArgumentNullException("httpContext");

			RouteData = routeData;
			HttpContext = httpContext;
			//	FileSystem = fileSystem;
			//	HttpRuntime = httpRuntime;
			Request = HttpContext.Request;
			Response = HttpContext.Response;
			//	Session = Context.Session;
			//	QueryString = Request.QueryString;
			//	Form = Request.Form;

			CalculateCurrentAction();
			CalculateCurrentMode();
			CalculateCurrentID();
			CalculateSitePhysicalPath();
		}

		/// <summary>
		/// Gets the route data.
		/// </summary>
		public RouteData RouteData { get; private set; }

		/// <summary>
		///  Gets the <see cref="T:System.Web.HttpContextBase"/> object for the current HTTP request.
		/// </summary>
		public HttpContextBase HttpContext { get; private set; }

		/// <summary>
		/// Gets the System.Web.HttpRequest object for the current HTTP request
		/// </summary>
		public HttpRequestBase Request { get; private set; }

		/// <summary>
		/// Gets the System.Web.HttpResponse object for the current HTTP response
		/// </summary>
		public HttpResponseBase Response { get; private set; }

		/// <summary>
		/// Gets the current web-site request action parameter (/someAction or ?act=someAction).
		/// </summary>
		/// <value>
		/// The current action (?act=someAction or yourSite/someAction).
		/// </value>
		public string CurrentAction { get; private set; }

		/// <summary>
		/// Gets the current web-site mode request parameter (/someAction/someMode/SomeID or ?act=someAction&amp;mode=somMode).
		/// </summary>
		/// <value>
		/// The current mode (?act=someAction&amp;mode=somMode).
		/// </value>
		public string CurrentMode { get; private set; }

		/// <summary>
		/// Gets the current web-site ID request parameter (/someAction/someID or ?act=someAction&amp;id=someID).
		/// </summary>
		/// <value>
		/// The current mode (?act=someAction&amp;mode=somMode).
		/// </value>
		public string CurrentID { get; private set; }

		/// <summary>
		/// Gets the web-site physical path, for example: C:/inetpub/wwwroot/YourSite
		/// </summary>
		/// <value>
		/// The site physical path.
		/// </value>
		public string SitePhysicalPath { get; private set; }

		/// <summary>
		/// Gets current action/mode URL in formal like ?act={0}&amp;mode={1}&amp;id={2}.
		/// </summary>
		/// <returns></returns>
		public string GetActionModeUrl()
		{
			if (String.IsNullOrEmpty(CurrentAction)) return "";

			var url = "?act=" + CurrentAction;

			if (!String.IsNullOrEmpty(CurrentMode))
				url += "&amp;mode=" + CurrentMode;

			if (!String.IsNullOrEmpty(CurrentID))
				url += "&amp;id=" + CurrentID;

			return url;
		}

		private void CalculateCurrentAction()
		{
			string action;

			if (RouteData != null && RouteData.Values.ContainsKey("action"))
				action = (string)RouteData.Values["action"];
			else
				action = Request.QueryString["act"];

			CurrentAction = action ?? "";
		}

		private void CalculateCurrentMode()
		{
			string mode;

			if (RouteData != null && RouteData.Values.ContainsKey("mode"))
				mode = (string)RouteData.Values["mode"];
			else
				mode = Request.QueryString["mode"];

			CurrentMode = mode ?? "";
		}

		private void CalculateCurrentID()
		{
			string id;

			if (RouteData != null && RouteData.Values.ContainsKey("id"))
				id = (string)RouteData.Values["id"];
			else
				id = Request.QueryString["id"];

			CurrentID = id ?? "";
		}
		private void CalculateSitePhysicalPath()
		{
			if (Request.PhysicalApplicationPath != null)
			{
				SitePhysicalPath = Request.PhysicalApplicationPath.Replace("\\", "/");

				if (SitePhysicalPath.EndsWith("/"))
					SitePhysicalPath = SitePhysicalPath.Substring(0, SitePhysicalPath.Length - 1);
			}
		}
	}
}