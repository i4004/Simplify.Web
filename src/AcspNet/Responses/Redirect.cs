using System;
using AcspNet.Modules;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides controller redirect response (redirects the client to specified URL.)
	/// </summary>
	public class Redirect : ControllerResponse
	{
		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		public Redirect(string url)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Redirects the client by specifying redirection type.
		/// </summary>
		/// <param name="redirectionType">Type of the navigation.</param>
		/// <param name="bookmarkName">Name of the bookmark.</param>
		public Redirect(RedirectionType redirectionType, string bookmarkName = null)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			throw new NotImplementedException();
		}
	}
}