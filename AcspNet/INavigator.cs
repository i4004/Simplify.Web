namespace AcspNet.Extensions
{
	/// <summary>
	/// Website navigation manager, controls current user location, link to previous page and link to specified page
	/// </summary>
	public interface INavigator : IHideObjectMembers
	{
		///// <summary>
		///// Gets or sets the previous page link.
		///// </summary>
		///// <value>
		///// The previous page link.
		///// </value>
		//string PreviousPageLink { get; set; }

		///// <summary>
		///// Gets or sets the redirect link.
		///// </summary>
		///// <value>
		///// The redirect link.
		///// </value>
		//string RedirectLink { get; set; }

		///// <summary>
		///// Gets or sets the previous navigated URL.
		///// </summary>
		///// <value>
		///// The previous navigated URL.
		///// </value>
		//string PreviousNavigatedUrl { get; set; }

		///// <summary>
		///// Redirects a client to a new URL
		///// </summary>
		//void Redirect(string url);

		///// <summary>
		///// Navigates to previous page.
		///// </summary>
		//void NavigateToPreviousPage();

		///// <summary>
		///// Navigates to previous page with bookmark.
		///// </summary>
		///// <param name="bookmarkName">Name of the bookmark.</param>
		//void NavigateToPreviousPageWithBookmark(string bookmarkName);

		///// <summary>
		///// Navigates to redirect link.
		///// </summary>
		//void NavigateToRedirectLink();

		///// <summary>
		///// Sets the redirect link to current page.
		///// </summary>
		//void SetRedirectLinkToCurrentPage();

		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		void Redirect(string url);
	}
}