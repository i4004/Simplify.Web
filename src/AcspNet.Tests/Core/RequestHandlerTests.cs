using System.Threading.Tasks;
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
		private Mock<IControllersProcessor> _controllersHandler;
		private Mock<IPageProcessor> _pageProcessor;
		private ControllersRequestHandler _requestHandler;
		private Mock<IOwinContext> _context;
		
		[SetUp]
		public void Initialize()
		{
			_controllersHandler = new Mock<IControllersProcessor>();
			_pageProcessor = new Mock<IPageProcessor>();
			_requestHandler = new ControllersRequestHandler(_controllersHandler.Object, _pageProcessor.Object);

			_context = new Mock<IOwinContext>();
			_context.SetupGet(x => x.Response.StatusCode);
		}

		[Test]
		public void ProcessRequest_Ok_PageBuiltWithOutput()
		{
			// Assign
			var task = Task.Delay(0);

			_controllersHandler.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersProcessorResult.Ok);
			_pageProcessor.Setup(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(task);

			// Act
			var result = _requestHandler.ProcessRequest(null, _context.Object);

			// Assert
			Assert.AreEqual(task, result);
			_pageProcessor.Verify(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>()));
		}
		
		[Test]
		public void ProcessRequest_RawOutput_NoPageBuildsWithOutput()
		{
			// Assign
			_controllersHandler.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersProcessorResult.RawOutput);

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);

			// Assert

			_pageProcessor.Verify(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>()), Times.Never);
			_context.VerifySet(x => x.Response.StatusCode = It.IsAny<int>(), Times.Never);
		}
		
		[Test]
		public void ProcessRequest_Http401_404StatusCodeSetNoPageBuiltWithOutput()
		{
			// Assign
			_controllersHandler.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersProcessorResult.Http401);

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);

			// Assert
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 401));
		}

		[Test]
		public void ProcessRequest_Http401_403StatusCodeSetNoPageBuiltWithOutput()
		{
			// Assign
			_controllersHandler.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersProcessorResult.Http403);

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);

			// Assert
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 403));
		}

		[Test]
		public void ProcessRequest_Http404_404StatusCodeSetNoPageBuiltWithOutput()
		{
			// Assign
			_controllersHandler.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>())).Returns(ControllersProcessorResult.Http404);

			// Act
			_requestHandler.ProcessRequest(null, _context.Object);

			// Assert

			_pageProcessor.Verify(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>()), Times.Never);
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 404));
		}
	}
}