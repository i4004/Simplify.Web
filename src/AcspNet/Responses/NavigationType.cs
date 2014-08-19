namespace AcspNet.Responses
{
	/// <summary>
	/// Navigation types
	/// </summary>
	public enum NavigationType
	{
		/// <summary>
		/// Navigate to default page
		/// </summary>
		DefaultPage,
		/// <summary>
		/// Navigate to redirect URL specified in Navigator
		/// </summary>
		RedirectUrl,
		/// <summary>
		/// Navigate to previous page URL
		/// </summary>
		PreviousPage,
		/// <summary>
		/// The previous page URL with bookmark
		/// </summary>
		PreviousPageWithBookmark,
		/// <summary>
		/// Navigate to current page (refresh the page)
		/// </summary>
		CurrentPage
	}
}