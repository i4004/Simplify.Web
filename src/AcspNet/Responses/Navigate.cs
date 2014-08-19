using System;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides controller navigation response (redirects the client to specified URL.)
	/// </summary>
	public class Navigate : ControllerResponse
	{
		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		public Navigate(string url)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Redirects the client to URL specified by navigation type
		/// </summary>
		/// <param name="navigationType">Type of the navigation.</param>
		/// <param name="bookmarkName">Name of the bookmark.</param>
		public Navigate(NavigationType navigationType, string bookmarkName = null)
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