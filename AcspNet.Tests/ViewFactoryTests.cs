using AcspNet.Html;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	public class TestView : View
	{
	}

	[TestFixture]
	public class ViewFactoryTests
	{
		[Test]
		public void CreateView_DataFilledCorrectly()
		{
			// Arrange

			var modulesContainer = new Mock<ModulesContainer>();
			modulesContainer.SetupProperty(x => x.StringTable, new Mock<IStringTable>().Object);
			modulesContainer.SetupProperty(x => x.TemplateFactory, new Mock<ITemplateFactory>().Object);

			var htmlWrapper = new Mock<IHtmlWrapper>();
			htmlWrapper.SetupGet(x => x.ListsGenerator).Returns(new Mock<IListsGenerator>().Object);

			modulesContainer.SetupProperty(x => x.Html, htmlWrapper.Object);

			// Act

			var factory = new ViewFactory(modulesContainer.Object);

			var controller = factory.CreateView(typeof(TestView));

			// Assert

			Assert.IsNotNull(controller.StringTable);
			Assert.IsNotNull(controller.TemplateFactory);
			Assert.IsNotNull(controller.ViewFactory);
			Assert.IsNotNull(controller.Html);
			Assert.IsNotNull(controller.Html.ListsGenerator);
		}
	}
}