using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class NavigatorTests
	{
		[Test]
		public void T()
		{
			
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