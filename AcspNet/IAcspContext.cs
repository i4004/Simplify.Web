using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// Represent HTTP and ACSP context
	/// </summary>
	public interface IAcspContext : IHideObjectMembers
	{
		/// <summary>
		///  Gets the <see cref="T:System.Web.HttpContextBase"/> object for the current HTTP request.
		/// </summary>
		HttpContextBase HttpContext { get; }

		/// <summary>
		/// Gets the System.Web.HttpRequest object for the current HTTP request
		/// </summary>
		HttpRequestBase Request { get; }

		/// <summary>
		/// Gets the System.Web.HttpResponse object for the current HTTP response
		/// </summary>
		HttpResponseBase Response { get; }

		/// <summary>
		/// Gets the System.Web.HttpSessionState object for the current HTTP request
		/// </summary>
		HttpSessionStateBase Session { get; }

		/// <summary>
		/// Gets the connection of  HTTP query string variables
		/// </summary>
		NameValueCollection QueryString { get; }

		/// <summary>
		/// Gets the connection of HTTP post request form variables
		/// </summary>
		NameValueCollection Form { get; }

		/// <summary>
		/// Indicating whether session was created with the current request
		/// </summary>
		bool IsNewSession { get; }

		/// <summary>
		/// Gets the current web-site request action parameter (/someAction or ?act=someAction).
		/// </summary>
		/// <value>
		/// The current action (?act=someAction).
		/// </value>
		string CurrentAction { get; }

		/// <summary>
		/// Gets the current web-site mode request parameter (/someAction/someMode/SomeID or ?act=someAction&amp;mode=somMode).
		/// </summary>
		/// <value>
		/// The current mode (?act=someAction&amp;mode=somMode).
		/// </value>
		string CurrentMode { get; }

		/// <summary>
		/// Gets the current web-site ID request parameter (/someAction/someID or ?act=someAction&amp;id=someID).
		/// </summary>
		/// <value>
		/// The current mode (?act=someAction&amp;mode=somMode).
		/// </value>
		string CurrentID { get; }

		/// <summary>
		/// Gets the web-site physical path, for example: C:/inetpub/wwwroot/YourSite
		/// </summary>
		/// <value>
		/// The site physical path.
		/// </value>
		string SitePhysicalPath { get; }

		/// <summary>
		/// Gets the web-site virtual relative path, for example: /site1 if your web-site url is http://yoursite.com/site1/
		/// </summary>
		string SiteVirtualPath { get; }

		/// <summary>
		/// Gets the web-site URL, for example: http://yoursite.com/site1/
		/// </summary>
		/// <value>
		/// The site URL.
		/// </value>
		string SiteUrl { get; }

		/// <summary>
		/// Gets current action/mode URL in formal like ?act={0}&amp;mode={1}&amp;id={2}.
		/// </summary>
		/// <returns></returns>
		string GetActionModeUrl();
	}
}