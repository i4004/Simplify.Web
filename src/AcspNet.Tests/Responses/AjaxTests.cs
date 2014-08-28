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
		public void Process_NormalData_DataWrittenToResponse()
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
	}
}