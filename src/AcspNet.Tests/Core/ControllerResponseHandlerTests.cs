using AcspNet.Core;
using Moq;
using NUnit.Framework;

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
		public void Test()
		{
			// Assign
	
			// Act

			// Assert
		}
	}
}