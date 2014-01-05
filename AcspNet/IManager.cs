using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;
using System.Web.Routing;

using ApplicationHelper;

namespace AcspNet
{
	/// <summary>
	/// ACSP.NET manager class interface
	/// </summary>
	public interface IManager : IHideObjectMembers
	{
		/// <summary>
		///  Gets the <see cref="T:System.Web.HttpContextBase"/> object for the current HTTP request.
		/// </summary>
		HttpContextBase Context { get; }

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
		NameValueCollection Form { get;}

		/// <summary>
		/// Gets the route data.
		/// </summary>
		RouteData RouteData { get; }

		/// <summary>
		/// The stop watch (for web-page build measurement)
		/// </summary>
		Stopwatch StopWatch { get; }

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
		/// Gets the current executing extensions types.
		/// </summary>
		/// <value>
		/// The current executing extensions types.
		/// </value>
		IList<Type> ExecExtensionsTypes { get; }

		/// <summary>
		/// Stop ACSP subsequent extensions execution
		/// </summary>
		void StopExtensionsExecution();

		/// <summary>
		/// Gets library extension instance
		/// </summary>
		/// <typeparam name="T">Library extension instance to get</typeparam>
		/// <returns>Library extension</returns>
		T Get<T>() where T : LibExtension;

		/// <summary>
		/// Gets current action/mode URL in formal like ?act={0}&amp;mode={1}&amp;id={2}.
		/// </summary>
		/// <returns></returns>
		string GetActionModeUrl();

		/// <summary>
		/// Redirects a client to a new URL
		/// </summary>
		void Redirect(string url);

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData();

		/// <summary>
		/// Gets the library extensions meta data.
		/// </summary>
		/// <returns></returns>
		IList<LibExtensionMetaContainer> GetLibExtensionsMetaData();
	}
}