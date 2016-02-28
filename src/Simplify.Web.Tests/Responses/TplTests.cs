using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses
{
	[TestFixture]
	public class TplTests
	{
		private Mock<IDataCollector> _dataCollector;

		[SetUp]
		public void Initialize()
		{
			_dataCollector = new Mock<IDataCollector>();
		}

		[Test]
		public void Process_NormalData_DataAddedtoDataCollector()
		{
			// Assign

			var tplData = new Mock<Tpl>("test") { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			var result = tplData.Object.Process();

			// Assert

			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
			Assert.AreEqual(ControllerResponseResult.Default, result);
		}

		[Test]
		public void Process_NormalTemplate_DataAddedtoDataCollector()
		{
			// Assign

			var tplData = new Mock<Tpl>(Template.FromString("test")) { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		}

		[Test]
		public void Process_NormalTemplateAndTitle_DataAndTitleAddedtoDataCollector()
		{
			// Assign

			var tplData = new Mock<Tpl>(Template.FromString("test"), "foo title") { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert

			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
			_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
		}

		[Test]
		public void Process_NormalDataAndTitle_DataAndTitleAddedtoDataCollector()
		{
			// Assign

			var tplData = new Mock<Tpl>("test", "foo title") { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert

			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
			_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
		}

		[Test]
		public void Process_NormalDataAndNullTitle_DataAddedTitleNotAddedtoDataCollector()
		{
			// Assign

			var tplData = new Mock<Tpl>("test", null) { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert

			_dataCollector.Verify(x => x.AddTitle(It.IsAny<string>()), Times.Never);
		}
	}
}