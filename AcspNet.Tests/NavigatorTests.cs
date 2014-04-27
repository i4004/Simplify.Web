using System;
using System.Web;
using AcspNet.Modules;
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
			var nav = new Navigator(null, null, null);

			Assert.Throws<ArgumentNullException>(() => nav.Redirect(null));
		}

		[Test]
		public void Redirect_NormalUrl_ResponseRedirectCalled()
		{
			var response = new Mock<HttpResponseBase>();
			var nav = new Navigator(null, response.Object, null);

			nav.Redirect("http://testwebsite.com");

			response.Verify(x => x.Redirect(It.Is<string>(c => c == "http://testwebsite.com"), It.Is<bool>(c => c)), Times.Once);
		}

		[Test]
		public void NavigateToRedirectLink_NormalLink_RedirectCalledWithCorrectLink()
		{
			// Arrange

			var response = new Mock<HttpResponseBase>();
			var request = new Mock<HttpRequestBase>();
			var session = new Mock<HttpSessionStateBase>();

			session.SetupGet(x => x[It.IsAny<string>()]).Returns("foo");

			var nav = new Navigator(session.Object, response.Object, request.Object);

			// Act
			nav.NavigateToRedirectLink();

			// Assert
			response.Verify(x => x.Redirect(It.Is<string>(c => c == "foo"), It.Is<bool>(c => c)), Times.Once);
		}

		[Test]
		public void NavigateToRedirectLink_NoLink_RedirectCalledToSiteVirtualPath()
		{
			// Arrange

			var response = new Mock<HttpResponseBase>();
			var request = new Mock<HttpRequestBase>();

			request.SetupGet(x => x.ApplicationPath).Returns("test");

			var session = new Mock<HttpSessionStateBase>();

			var nav = new Navigator(session.Object, response.Object, request.Object);

			// Act
			nav.NavigateToRedirectLink();

			// Assert
			response.Verify(x => x.Redirect(It.Is<string>(c => c == "test"), It.Is<bool>(c => c)), Times.Once);
		}

		[Test]
		public void NavigateToPreviosPage_NormalLink_RedirectCalledWithCorrectLink()
		{
			// Arrange

			var response = new Mock<HttpResponseBase>();
			var request = new Mock<HttpRequestBase>();
			var session = new Mock<HttpSessionStateBase>();

			session.SetupGet(x => x[It.IsAny<string>()]).Returns("foo");

			var nav = new Navigator(session.Object, response.Object, request.Object);

			// Act
			nav.NavigateToPreviousPage();

			// Assert
			response.Verify(x => x.Redirect(It.Is<string>(c => c == "foo"), It.Is<bool>(c => c)), Times.Once);
		}

		[Test]
		public void NavigateToPreviosPageWithBookmark_NormalLink_RedirectCalledWithCorrectLink()
		{
			// Arrange

			var response = new Mock<HttpResponseBase>();
			var request = new Mock<HttpRequestBase>();
			var session = new Mock<HttpSessionStateBase>();

			session.SetupGet(x => x[It.IsAny<string>()]).Returns("foo");

			var nav = new Navigator(session.Object, response.Object, request.Object);

			// Act
			nav.NavigateToPreviousPageWithBookmark("test");

			// Assert
			response.Verify(x => x.Redirect(It.Is<string>(c => c == "foo#test"), It.Is<bool>(c => c)), Times.Once);
		}
	}
}