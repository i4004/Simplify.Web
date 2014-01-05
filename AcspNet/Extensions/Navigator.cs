namespace AcspNet.Extensions
{
	/// <summary>
	/// Website navigation manager, controls current user location, link to previous page or link specific page
	/// </summary>
	public sealed class Navigator : INavigator
	{
		private readonly Manager _manager;

		/// <summary>
		/// Initializes a new instance of the <see cref="Navigator"/> class.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public Navigator(Manager manager)
		{
			_manager = manager;
		}

		/// <summary>
		/// Gets or sets the previous page link.
		/// </summary>
		/// <value>
		/// The previous page link.
		/// </value>
		public string PreviousPageLink
		{
			get { return (string)_manager.Session["NavigatorPreviousPageLink"]; }
			set { _manager.Session.Add("NavigatorPreviousPageLink", value); }
		}

		/// <summary>
		/// Gets or sets the redirect link.
		/// </summary>
		/// <value>
		/// The redirect link.
		/// </value>
		public string RedirectLink
		{
			get { return (string)_manager.Session["NavigatorRedirectLink"]; }
			set { _manager.Session.Add("NavigatorRedirectLink", value); }
		}

		/// <summary>
		/// Gets or sets the previous navigated URL.
		/// </summary>
		/// <value>
		/// The previous navigated URL.
		/// </value>
		public string PreviousNavigatedUrl
		{
			get { return (string)_manager.Session["NavigatorPreviousNavigatedUrl"]; }
			set {_manager.Session.Add("NavigatorPreviousNavigatedUrl", value); }
		}

		/// <summary>
		/// Navigates to previous page.
		/// </summary>
		public void NavigateToPreviousPage()
		{
			PreviousNavigatedUrl = _manager.Request.RawUrl;

			_manager.Redirect(string.IsNullOrEmpty(PreviousPageLink) ? Manager.SiteVirtualPath : PreviousPageLink);
		}

		/// <summary>
		/// Navigates to previous page with bookmark.
		/// </summary>
		/// <param name="bookmarkName">Name of the bookmark.</param>
		public void NavigateToPreviousPageWithBookmark(string bookmarkName)
		{
			PreviousNavigatedUrl = _manager.Request.RawUrl;

			_manager.Redirect(!string.IsNullOrEmpty(PreviousPageLink) ? PreviousPageLink + "#" + bookmarkName : Manager.SiteVirtualPath);
		}

		/// <summary>
		/// Navigates to redirect link.
		/// </summary>
		public void NavigateToRedirectLink()
		{
			PreviousNavigatedUrl = _manager.Request.RawUrl;

			_manager.Redirect(string.IsNullOrEmpty(RedirectLink) ? Manager.SiteVirtualPath : RedirectLink);
		}

		/// <summary>
		/// Sets the redirect link to current page.
		/// </summary>
		public void SetRedirectLinkToCurrentPage()
		{
			PreviousNavigatedUrl = _manager.Request.RawUrl;

			if(_manager.Request.Url != null)
				RedirectLink = _manager.Request.Url.AbsoluteUri;
		}
	}
}
