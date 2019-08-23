using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Simplify.Web.Modules
{
	/// <summary>
	/// Provides web context
	/// </summary>
	public class WebContext : IWebContext
	{
		private readonly Lazy<IFormCollection> _form;
		private readonly Lazy<string> _requestBody;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebContext"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public WebContext(HttpContext context)
		{
			Context = context;
			Request = context.Request;
			Response = context.Response;
			Query = context.Request.Query;

			_form = new Lazy<IFormCollection>(() => Task.Run(() => context.Request.ReadFormAsync()).Result);
			_requestBody = new Lazy<string>(() => new StreamReader(Context.Request.Body).ReadToEnd());

			VirtualPath = string.IsNullOrEmpty(Request.PathBase.Value) ? "" : Request.PathBase.Value;

			SiteUrl = Request.Scheme + "://" + Request.Host.Value + VirtualPath + "/";

			IsAjax = Request.Headers.ContainsKey("X-Requested-With");

			Route = Request.Path.Value;
		}

		/// <summary>
		/// Current web-site route, for example: "/" or "/user/delete/15"
		/// </summary>
		public string Route { get; }

		/// <summary>
		/// Site root url, for example: http://mysite.com or http://localhost/mysite/
		/// </summary>
		public string SiteUrl { get; }

		/// <summary>
		/// Gets the virtual path.
		/// </summary>
		/// <value>
		/// The virtual path.
		/// </value>
		public string VirtualPath { get; }

		/// <summary>
		/// Gets the context for the current HTTP request.
		/// </summary>
		public HttpContext Context { get; }

		/// <summary>
		/// Gets the request for the current HTTP request.
		/// </summary>
		public HttpRequest Request { get; }

		/// <summary>
		/// Gets the response for the current HTTP request.
		/// </summary>
		public HttpResponse Response { get; }

		/// <summary>
		/// Gets the query string for current HTTP request.
		/// </summary>
		public IQueryCollection Query { get; }

		/// <summary>
		/// Gets the form data of post HTTP request.
		/// </summary>
		public IFormCollection Form => _form.Value;

		/// <summary>
		/// Gets a value indicating whether this request is ajax request.
		/// </summary>
		/// <value>
		/// <c>true</c> if current request is ajax request; otherwise, <c>false</c>.
		/// </value>
		public bool IsAjax { get; }

		/// <summary>
		/// Gets or sets the request body.
		/// </summary>
		/// <value>
		/// The request body.
		/// </value>
		public string RequestBody => _requestBody.Value;
	}
}