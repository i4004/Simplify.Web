using AcspNet.Modules;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class TplDataTests
	{
		Mock<IDataCollector> _dataCollector;
		
		[SetUp]
		public void Initialize()
		{
			_dataCollector = new Mock<IDataCollector>();			
		}

		[Test]
		public void Process_NullData_NoDataAddedtoDataCollector()
		{
			// Assign
			var tplData = new Mock<TplData>(null) { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert
			_dataCollector.Verify(x => x.Add(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Process_NormalData_DataAddedtoDataCollector()
		{
			// Assign
			var tplData = new Mock<TplData>("test") {CallBase = true};
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
			
			// Act
			tplData.Object.Process();

			// Assert
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		}

		[Test]
		public void Process_NormalDataAndTitle_DataAddedtoDataCollector()
		{
			// Assign
			var tplData = new Mock<TplData>("test", "foo title") { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
			_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
		}

		[Test]
		public void Process_NormalDataAndNullTitle_NoDataAddedtoDataCollector()
		{
			// Assign
			var tplData = new Mock<TplData>("test", null) { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert
			_dataCollector.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
			_dataCollector.Verify(x => x.AddTitle(It.IsAny<string>()), Times.Never);
		}
	}
}