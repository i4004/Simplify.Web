using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules
{
	/// <summary>
	/// Provides web context
	/// </summary>
	public class WebContext : IWebContext
	{
		private readonly Lazy<IFormCollection> _form;

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

			VirtualPath = string.IsNullOrEmpty(Request.PathBase.Value) ? "" : Request.PathBase.Value;

			// TODO
			//SiteUrl = Request.Uri.Scheme + "://" + Request.Uri.Authority + VirtualPath + "/";

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

		// TODO check correct class
		/// <summary>
		/// Gets the request for the current HTTP request.
		/// </summary>
		public HttpRequest Request { get; }

		// TODO check correct class
		/// <summary>
		/// Gets the response for the current HTTP request.
		/// </summary>
		public HttpResponse Response { get; }

		// TODO check correct interface
		/// <summary>
		/// Gets the query string for current HTTP request.
		/// </summary>
		public IQueryCollection Query { get; }

		// TODO check correct interface
		/// <summary>
		/// Gets the form data of post HTTP request.
		/// </summary>
		public IFormCollection Form
		{
			get { return _form.Value; }
		}

		/// <summary>
		/// Gets a value indicating whether this request is ajax request.
		/// </summary>
		/// <value>
		/// <c>true</c> if current request is ajax request; otherwise, <c>false</c>.
		/// </value>
		public bool IsAjax { get; }
	}
}