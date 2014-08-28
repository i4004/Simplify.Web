using AcspNet.Core;
using AcspNet.Modules;
using AcspNet.Responses;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Responses
{
	[TestFixture]
	public class AjaxTests
	{
		Mock<IResponseWriter> _responseWriter;
		private Mock<IAcspNetContext> _context;

		[SetUp]
		public void Initialize()
		{
			_responseWriter = new Mock<IResponseWriter>();
			_context = new Mock<IAcspNetContext>();
		}

		[Test]
		public void Process_NormalData_DataAddedtoDataCollector()
		{
			// Assign
			var ajax = new Mock<Ajax>("test") { CallBase = true };
			ajax.SetupGet(x => x.ResponseWriter).Returns(_responseWriter.Object);
			ajax.SetupGet(x => x.Context).Returns(_context.Object);

			// Act
			var result = ajax.Object.Process();

			// Assert

			_responseWriter.Verify(x => x.Write(It.Is<string>(d => d == "test"), It.IsAny<IOwinResponse>()));
			Assert.AreEqual(ControllerResponseResult.RawOutput, result);
		}

		//[Test]
		//public void Process_NormalTemplate_DataAddedtoDataCollector()
		//{
		//	// Assign

		//	var tplData = new Mock<Tpl>(Template.FromString("test")) { CallBase = true };
		//	tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

		//	// Act
		//	tplData.Object.Process();

		//	// Assert
		//	_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		//}

		//[Test]
		//public void Process_NormalTemplateAndTitle_DataAndTitleAddedtoDataCollector()
		//{
		//	// Assign

		//	var tplData = new Mock<Tpl>(Template.FromString("test"), "foo title") { CallBase = true };
		//	tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

		//	// Act
		//	tplData.Object.Process();

		//	// Assert

		//	_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		//	_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
		//}

		//[Test]
		//public void Process_NormalDataAndTitle_DataAndTitleAddedtoDataCollector()
		//{
		//	// Assign

		//	var tplData = new Mock<Tpl>("test", "foo title") { CallBase = true };
		//	tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

		//	// Act
		//	tplData.Object.Process();

		//	// Assert

		//	_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		//	_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
		//}

		//[Test]
		//public void Process_NormalDataAndNullTitle_DataAddedTitleNotAddedtoDataCollector()
		//{
		//	// Assign

		//	var tplData = new Mock<Tpl>("test", null) { CallBase = true };
		//	tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

		//	// Act
		//	tplData.Object.Process();

		//	// Assert

		//	_dataCollector.Verify(x => x.AddTitle(It.IsAny<string>()), Times.Never);
		//}
	}
}