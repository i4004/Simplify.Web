using AcspNet.Modules;
using AcspNet.Modules.Controllers;
using AcspNet.Modules.Html;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Modules.Controllers
{
	[TestFixture]
	public class MessagePageDisplayTests
	{
		[Test]
		public void Invoke_EmptyMessage_NavigateToDefaultPage()
		{
			// Arrange

			var controller = new Mock<MessagePageDisplay> { CallBase = true };
			controller.SetupGet(x => x.MessagePage.Message).Returns((string)null);

			var navigator = new Mock<INavigator>();
			controller.SetupGet(x => x.Navigator).Returns(navigator.Object);

			// Act
			controller.Object.Invoke();

			// Assert
			navigator.Verify(x => x.NavigateToDefaultPage(), Times.Once);
		}

		[Test]
		public void Invoke_NormalMessage_MessageBoxIsDisplayedMessageRemoved()
		{
			// Arrange

			var controller = new Mock<MessagePageDisplay> { CallBase = true };
			controller.SetupGet(x => x.MessagePage.Message).Returns("foo");
			controller.SetupGet(x => x.MessagePage.MessageStatus).Returns(MessageBoxStatus.Information);

			var messageBox = new Mock<IMessageBox>();
			controller.SetupGet(x => x.MessageBox).Returns(messageBox.Object);

			// Act
			controller.Object.Invoke();

			// Assert

			controller.Verify(x => x.MessagePage.RemoveMessage(), Times.Once);
			messageBox.Verify(x => x.Show(It.Is<string>(s => s == "foo"), It.Is<MessageBoxStatus>(s => s == MessageBoxStatus.Information), It.IsAny<string>()), Times.Once);
		}
	}
}