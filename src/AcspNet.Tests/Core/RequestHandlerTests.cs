using AcspNet.Core;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.DI;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	public class RequestHandlerTests
	{
		private RequestHandler _requestHandler;
		private Mock<IControllersHandler> _controllersHandler;
		private Mock<IOwinContext> _context;
		
		[SetUp]
		public void Initialize()
		{
			_controllersHandler = new Mock<IControllersHandler>();
			_requestHandler = new RequestHandler(_controllersHandler.Object);
			_context = new Mock<IOwinContext>();
			_context.SetupGet(x => x.Response.StatusCode);
		}

		[Test]
		public void ProcessRequest_OkFromControllersHandler_Ok()
		{
			// Assign
			_controllersHandler.Setup(x => x.Execute(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersHandlerResult.Ok);

			// Act
			_requestHandler.ProcessRequest(null, null);
		}

		[Test]
		public void ProcessRequest_404FromControllersHandler_404StatusCodeSet()
		{
			// Assign
			_controllersHandler.Setup(x => x.Execute(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersHandlerResult.Http404);

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 404));
		}
	}
}