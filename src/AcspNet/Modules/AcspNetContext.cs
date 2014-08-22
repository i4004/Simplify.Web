using System;
using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides AcspNet context
	/// </summary>
	public class AcspNetContext : IAcspNetContext
	{
		private readonly Lazy<IFormCollection> _form;

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetContext"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public AcspNetContext(IOwinContext context)
		{
			Context = context;
			Request = context.Request;
			Response = context.Response;
			Query = context.Request.Query;

			_form = new Lazy<IFormCollection>(() =>
			{
				var task = context.Request.ReadFormAsync();
				task.Wait();
				return task.Result;
			});

			if (string.IsNullOrEmpty(Request.PathBase.Value))
				SiteUrl = Request.Uri.AbsoluteUri.Substring(0,
					Request.Uri.AbsoluteUri.IndexOf(Request.Uri.Host, StringComparison.Ordinal) + Request.Uri.Host.Length);
			else
				SiteUrl =
					Request.Uri.AbsoluteUri.Substring(0, Request.Uri.AbsoluteUri.IndexOf(Request.PathBase.Value,
						StringComparison.Ordinal)) + Request.PathBase.Value + "/";

			VirtualPath = string.IsNullOrEmpty(Request.PathBase.Value) ? "" : Request.PathBase.Value + "/";
		}
		
		/// <summary>
		/// Site root url, for example: http://mysite.com or http://localhost/mysite/
		/// </summary>
		public string SiteUrl { get; private set; }

		/// <summary>
		/// Gets the virtual path.
		/// </summary>
		/// <value>
		/// The virtual path.
		/// </value>
		public string VirtualPath { get; private set; }

		/// <summary>
		/// Gets the context for the current HTTP request.
		/// </summary>
		public IOwinContext Context { get; private set; }

		/// <summary>
		/// Gets the request for the current HTTP request.
		/// </summary>
		public IOwinRequest Request { get; private set; }

		/// <summary>
		/// Gets the response for the current HTTP request.
		/// </summary>
		public IOwinResponse Response { get; private set; }

		/// <summary>
		/// Gets the query string for current HTTP request.
		/// </summary>
		public IReadableStringCollection Query { get; private set; }

		/// <summary>
		/// Gets the form data of post HTTP request.
		/// </summary>
		public IFormCollection Form
		{
			get { return _form.Value; }
		}
	}
}
