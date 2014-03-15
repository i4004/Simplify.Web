using System;
using System.Web;
using AcspNet.Extensions;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class NavigatorTests
	{
		[Test]
		public void Redirect_NullUrl_ArgumentNullExceptionThrown()
		{
			var nav = new Navigator(null, null);

			Assert.Throws<ArgumentNullException>(() => nav.Redirect(null));
		}

		[Test]
		public void Redirect_NormalUrl_ResponseRedirectCalled()
		{
			var response = new Mock<HttpResponseBase>();
			var nav = new Navigator(null, response.Object);

			nav.Redirect("http://testwebsite.com");

			response.Verify(x => x.Redirect(It.Is<string>(c => c == "http://testwebsite.com"), It.Is<bool>(c => c)), Times.Once);
		}
	}
}

//		[Test]
//		public void Navigator_NavigateToPreviosPage_IsCorrect()
//		{
//			var manager = GetTestManager("navigatorTest");

//			manager.ExtensionsWrapper.Navigator.NavigateToPreviousPage();

//			Assert.IsNotNull(manager.ExtensionsWrapper.Navigator.PreviousNavigatedUrl);
//		}

//		[Test]
//		public void Navigator_NavigateToPreviosPageWithBookmark_IsCorrect()
//		{
//			var manager = GetTestManager("navigatorTest");

//			manager.ExtensionsWrapper.Navigator.NavigateToPreviousPageWithBookmark("foo");
//		}

//		[Test]
//		public void Navigator_NavigateToRedirectLink_IsCorrect()
//		{
//			var manager = GetTestManager("navigatorTest");

//			manager.ExtensionsWrapper.Navigator.RedirectLink = "foo";
//			manager.ExtensionsWrapper.Navigator.NavigateToRedirectLink();
//		}

//		[Test]
//		public void Navigator_SetRedirectLinkToCurrentPage_IsCorrect()
//		{
//			var manager = GetTestManager("navigatorTest");

//			manager.ExtensionsWrapper.Navigator.SetRedirectLinkToCurrentPage();
//		}