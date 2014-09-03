using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides AcspNet context
	/// </summary>
	public class AcspNetContext : IAcspNetContext
	{
		private readonly object _locker = new object();

		private IFormCollection _form;

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

			SiteUrl = Request.Uri.Scheme + "://" + Request.Uri.Authority;

			if (!string.IsNullOrEmpty(Request.PathBase.Value))
				SiteUrl += Request.PathBase.Value + "/";

			VirtualPath = string.IsNullOrEmpty(Request.PathBase.Value) ? "" : Request.PathBase.Value + "/";

			IsAjax = Request.Headers.ContainsKey("X-Requested-With");
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
			get
			{
				lock (_locker)
					if (_form == null)
					{
						var task = Request.ReadFormAsync();
						task.Wait();
						_form = task.Result;
					}

				return _form;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this request is ajax request.
		/// </summary>
		/// <value>
		/// <c>true</c> if current request is ajax request; otherwise, <c>false</c>.
		/// </value>
		public bool IsAjax { get; private set; }
	}
}
