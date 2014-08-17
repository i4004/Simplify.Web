using AcspNet.Core;
using Moq;
using NUnit.Framework;
using Simplify.DI;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	public class ControllerResponseHandlerTests
	{
		private Mock<IControllerResponseBuilder> _controllerResponseBuilder;
		private ControllerResponseHandler _controllerResponseHandler;

		[SetUp]
		public void Initialize()
		{
			_controllerResponseBuilder = new Mock<IControllerResponseBuilder>();
			_controllerResponseHandler = new ControllerResponseHandler(_controllerResponseBuilder.Object);
		}

		[Test]
		public void Process_NoResponse_BuildAndProcessInvoked()
		{
			// Act
			var result = _controllerResponseHandler.Process(null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Ok, result);
			_controllerResponseBuilder.Verify(x => x.BuildControllerResponseProperties(It.IsAny<ControllerResponse>(), It.IsAny<IDIContainerProvider>()), Times.Never);
		}

		[Test]
		public void Process_NormalResponse_BuildAndProcessInvoked()
		{
			// Assign
			var response = new Mock<ControllerResponse>();

			// Act
			var result = _controllerResponseHandler.Process(response.Object, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Ok, result);
			response.Verify(x => x.Process());
			_controllerResponseBuilder.Verify(x => x.BuildControllerResponseProperties(It.IsAny<ControllerResponse>(), It.IsAny<IDIContainerProvider>()));
		}
	}
}