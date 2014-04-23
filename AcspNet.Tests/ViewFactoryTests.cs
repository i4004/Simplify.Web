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

			var languageManager = new Mock<ILanguageManager>();
			languageManager.SetupGet(x => x.Language).Returns("en");
			modulesContainer.SetupProperty(x => x.LanguageManager, languageManager.Object);

			var acspContext = new Mock<IAcspContext>();
			acspContext.SetupGet(x => x.SiteUrl).Returns("foo");
			acspContext.SetupGet(x => x.SiteVirtualPath).Returns("bar");
			modulesContainer.SetupProperty(x => x.Context, acspContext.Object);

			// Act

			var factory = new ViewFactory(modulesContainer.Object);

			var view = factory.CreateView(typeof(TestView));

			// Assert

			Assert.IsNotNull(view.StringTable);
			Assert.IsNotNull(view.TemplateFactory);
			Assert.IsNotNull(view.ViewFactory);
			Assert.IsNotNull(view.Html);
			Assert.IsNotNull(view.Html.ListsGenerator);
			Assert.AreEqual("en", view.Language);
			Assert.AreEqual("foo", view.SiteUrl);
			Assert.AreEqual("bar", view.SiteVirtualPath);
		}
	}
}