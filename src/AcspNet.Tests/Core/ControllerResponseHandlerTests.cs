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
		public void Process_BuildAndProcessInvoked()
		{
			// Assign
			var response = new Mock<ControllerResponse>();
	
			// Act
			_controllerResponseHandler.Process(response.Object, null);

			// Assert
			response.Verify(x => x.Process());
			_controllerResponseBuilder.Verify(x => x.BuildControllerResponseProperties(It.IsAny<IDIContainerProvider>(), It.IsAny<ControllerResponse>()));
		}
	}
}