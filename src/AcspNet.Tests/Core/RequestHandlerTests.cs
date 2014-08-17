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
		private Mock<IControllersHandler> _controllersHandler;
		private Mock<IPageBuilder> _pageBuilder;
		private Mock<IResponseWriter> _responseWriter;
		private RequestHandler _requestHandler;
		private Mock<IOwinContext> _context;
		
		[SetUp]
		public void Initialize()
		{
			_controllersHandler = new Mock<IControllersHandler>();
			_pageBuilder = new Mock<IPageBuilder>();
			_responseWriter = new Mock<IResponseWriter>();
			_requestHandler = new RequestHandler(_controllersHandler.Object, _pageBuilder.Object, _responseWriter.Object);
			_context = new Mock<IOwinContext>();
			_context.SetupGet(x => x.Response.StatusCode);
		}

		[Test]
		public void ProcessRequest_Ok_PageBuiltWithOutput()
		{
			// Assign

			_controllersHandler.Setup(x => x.Execute(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersHandlerResult.Ok);
			_pageBuilder.Setup(x => x.Build(It.IsAny<IDIContainerProvider>())).Returns("Foo");

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);

			// Assert

			_pageBuilder.Verify(x => x.Build(It.IsAny<IDIContainerProvider>()));
			_responseWriter.Verify(x => x.Write(It.Is<string>(d => d == "Foo"), It.Is<IOwinResponse>(d => d == _context.Object.Response)));
		}


		[Test]
		public void ProcessRequest_RawOutput_NoPageBuildsWithOutput()
		{
			// Assign

			_controllersHandler.Setup(x => x.Execute(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersHandlerResult.RawOutput);

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);

			// Assert

			_pageBuilder.Verify(x => x.Build(It.IsAny<IDIContainerProvider>()), Times.Never);
			_responseWriter.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<IOwinResponse>()), Times.Never);
			_context.VerifySet(x => x.Response.StatusCode = It.IsAny<int>(), Times.Never);
		}

		[Test]
		public void ProcessRequest_Http404_404StatusCodeSetNoPageBuiltWithOutput()
		{
			// Assign
			_controllersHandler.Setup(x => x.Execute(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersHandlerResult.Http404);

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);

			// Assert

			_pageBuilder.Verify(x => x.Build(It.IsAny<IDIContainerProvider>()), Times.Never);
			_responseWriter.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<IOwinResponse>()), Times.Never);
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 404));
		}
	}
}