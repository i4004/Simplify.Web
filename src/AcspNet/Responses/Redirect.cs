using AcspNet.Modules;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides controller redirect response (redirects the client to specified URL.)
	/// </summary>
	public class Redirect : ControllerResponse
	{
		private readonly RedirectionType _redirectionType;
		private readonly string _bookmarkName;
		private readonly string _url;

		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		public Redirect(string url)
		{
			_url = url;
		}

		/// <summary>
		/// Redirects the client by specifying redirection type.
		/// </summary>
		/// <param name="redirectionType">Type of the navigation.</param>
		/// <param name="bookmarkName">Name of the bookmark.</param>
		public Redirect(RedirectionType redirectionType, string bookmarkName = null)
		{
			_redirectionType = redirectionType;
			_bookmarkName = bookmarkName;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			if (!string.IsNullOrEmpty(_url))
				Redirector.Redirect(_url);
			else
				Redirector.Redirect(_redirectionType, _bookmarkName);

			return ControllerResponseResult.Ok;
		}
	}
}