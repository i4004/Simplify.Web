using System;
using AcspNet.Core;
using AcspNet.Modules;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.DI;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	public class PageProcessorTests
	{
		private PageProcessor _processor;
		private Mock<IPageBuilder> _pageBuilder;
		private Mock<IResponseWriter> _responseWriter;
		private Mock<IRedirector> _redirector;

		private Mock<IOwinContext> _context;

		[SetUp]
		public void Initialize()
		{
			_pageBuilder = new Mock<IPageBuilder>();
			_responseWriter = new Mock<IResponseWriter>();
			_redirector = new Mock<IRedirector>();
			_processor = new PageProcessor(_pageBuilder.Object, _responseWriter.Object, _redirector.Object);

			_context = new Mock<IOwinContext>();
		}


		[Test]
		public void Process_Ok_PageBuiltWithOutput()
		{
			// Assign

			_pageBuilder.Setup(x => x.Build(It.IsAny<IDIContainerProvider>())).Returns("Foo");
			_context.SetupGet(x => x.Request.Uri).Returns(new Uri("http://localhost:8080/test"));

			// Act
			_processor.ProcessPage(null, _context.Object);

			// Assert

			_pageBuilder.Verify(x => x.Build(It.IsAny<IDIContainerProvider>()));
			_responseWriter.Verify(x => x.Write(It.Is<string>(d => d == "Foo"), It.Is<IOwinResponse>(d => d == _context.Object.Response)));
			_redirector.SetupSet(x => x.PreviousPageUrl = It.Is<string>(d => d == "http://localhost:8080/test"));
		}
	}
}