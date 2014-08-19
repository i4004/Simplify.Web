using AcspNet.Responses;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represent website navigation manager, which controls current user location, link to previous page and link to specified page
	/// </summary>
	public interface INavigator : IHideObjectMembers
	{
		/// <summary>
		/// Gets or sets the previous page link.
		/// </summary>
		/// <value>
		/// The previous page link.
		/// </value>
		string PreviousPageLink { get; set; }

		/// <summary>
		/// Gets or sets the redirect link.
		/// </summary>
		/// <value>
		/// The redirect link.
		/// </value>
		string RedirectLink { get; set; }

		/// <summary>
		/// Gets or sets the previous navigated URL.
		/// </summary>
		/// <value>
		/// The previous navigated URL.
		/// </value>
		string PreviousNavigatedUrl { get; set; }

		/// <summary>
		/// Sets the redirect link to current page.
		/// </summary>
		void SetRedirectLinkToCurrentPage();

		/// <summary>
		/// Navigates the client by specified navigation type.
		/// </summary>
		/// <param name="navigationType">Type of the navigation.</param>
		void Navigate(NavigationType navigationType);

		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		void Redirect(string url);
	}
}