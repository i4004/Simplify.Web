using AcspNet.Responses;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides website navigation manager, which controls current user location, link to previous page and link to specified page
	/// </summary>
	public class Navigator : INavigator
	{
		/// <summary>
		/// Gets or sets the previous page link.
		/// </summary>
		/// <value>
		/// The previous page link.
		/// </value>
		public string PreviousPageLink { get; set; }

		/// <summary>
		/// Gets or sets the redirect link.
		/// </summary>
		/// <value>
		/// The redirect link.
		/// </value>
		public string RedirectLink { get; set; }

		/// <summary>
		/// Gets or sets the previous navigated URL.
		/// </summary>
		/// <value>
		/// The previous navigated URL.
		/// </value>
		public string PreviousNavigatedUrl { get; set; }

		/// <summary>
		/// Sets the redirect link to current page.
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public void SetRedirectLinkToCurrentPage()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Navigates the client by specified navigation type.
		/// </summary>
		/// <param name="navigationType">Type of the navigation.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Navigate(NavigationType navigationType)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Redirect(string url)
		{
			throw new System.NotImplementedException();
		}
	}
}