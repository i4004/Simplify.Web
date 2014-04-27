using System;
using System.Web;

namespace AcspNet.Modules
{
	/// <summary>
	/// Website navigation manager, controls current user location, link to previous page or link specific page
	/// </summary>
	public sealed class Navigator : INavigator
	{
		private readonly HttpSessionStateBase _session;
		private readonly HttpResponseBase _response;
		private readonly HttpRequestBase _request;

		/// <summary>
		/// Initializes a new instance of the <see cref="Navigator" /> class.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="response">The response.</param>
		/// <param name="request">The request.</param>
		internal Navigator(HttpSessionStateBase session, HttpResponseBase response, HttpRequestBase request)
		{
			_session = session;
			_response = response;
			_request = request;
		}

		/// <summary>
		/// Gets or sets the previous page link.
		/// </summary>
		/// <value>
		/// The previous page link.
		/// </value>
		public string PreviousPageLink
		{
			get { return (string)_session["NavigatorPreviousPageLink"]; }
			set { _session.Add("NavigatorPreviousPageLink", value); }
		}

		/// <summary>
		/// Gets or sets the redirect link.
		/// </summary>
		/// <value>
		/// The redirect link.
		/// </value>
		public string RedirectLink
		{
			get { return (string)_session["NavigatorRedirectLink"]; }
			set { _session.Add("NavigatorRedirectLink", value); }
		}

		/// <summary>
		/// Gets or sets the previous navigated URL.
		/// </summary>
		/// <value>
		/// The previous navigated URL.
		/// </value>
		public string PreviousNavigatedUrl
		{
			get { return (string)_session["NavigatorPreviousNavigatedUrl"]; }
			set { _session.Add("NavigatorPreviousNavigatedUrl", value); }
		}

		/// <summary>
		/// Navigates to previous page.
		/// </summary>
		public void NavigateToPreviousPage()
		{
			PreviousNavigatedUrl = _request.RawUrl;

			Redirect(string.IsNullOrEmpty(PreviousPageLink) ? _request.ApplicationPath : PreviousPageLink);
		}

		/// <summary>
		/// Navigates to previous page with bookmark.
		/// </summary>
		/// <param name="bookmarkName">Name of the bookmark.</param>
		public void NavigateToPreviousPageWithBookmark(string bookmarkName)
		{
			PreviousNavigatedUrl = _request.RawUrl;

			Redirect(!string.IsNullOrEmpty(PreviousPageLink) ? PreviousPageLink + "#" + bookmarkName : _request.ApplicationPath);
		}

		/// <summary>
		/// Navigates to redirect link.
		/// </summary>
		public void NavigateToRedirectLink()
		{
			PreviousNavigatedUrl = _request.RawUrl;

			Redirect(string.IsNullOrEmpty(RedirectLink) ? _request.ApplicationPath : RedirectLink);
		}

		/// <summary>
		/// Sets the redirect link to current page.
		/// </summary>
		public void SetRedirectLinkToCurrentPage()
		{
			if (_request.Url != null)
				RedirectLink = _request.Url.AbsoluteUri;
		}

		public void NavigateToDefaultPage()
		{
			Redirect(_request.ApplicationPath);
		}

		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		public void Redirect(string url)
		{
			if (string.IsNullOrEmpty(url))
				throw new ArgumentNullException("url");

			_response.Redirect(url, true);
		}
	}
}
