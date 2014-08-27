using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using AcspNet.Core;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	[Category("Windows")]
	public class StaticFilesRequestHandlerTest
	{
		private Mock<IResponseWriter> _responseWriter;
		private StaticFilesRequestHandler _requestHandler;

		private Mock<IOwinContext> _context;

		[TestFixtureSetUp]
		public void SetUpFileSystem()
		{
			var files = new Dictionary<string, MockFileData>
			{
				{"Foo/Bar.css", "Hello!"}
			};

			StaticFilesRequestHandler.FileSystem = new MockFileSystem(files, "C:/WebSites/MySite/");
		}
		
		[SetUp]
		public void Initialize()
		{
			_responseWriter = new Mock<IResponseWriter>();
			_requestHandler = new StaticFilesRequestHandler(null, null, _responseWriter.Object);

			_requestHandler = new StaticFilesRequestHandler(new List<string> { "Foo" }, "C:/WebSites/MySite/", _responseWriter.Object);

			_context = new Mock<IOwinContext>();

			_context.SetupGet(x => x.Request.Path).Returns(new PathString("/Foo/Bar.css"));
		}

		[Test]
		public void IsStaticFileRoutePath_Matched_True()
		{
			// Act
			var result = _requestHandler.IsStaticFileRoutePath(_context.Object);

			// Assert
			Assert.IsTrue(result);
		}
		
		[Test]
		public void IsStaticFileRoutePath_NoMatchedPaths_False()
		{
			// Assign
			_requestHandler = new StaticFilesRequestHandler(new List<string> { "Test" }, "C:/WebSites/MySite/", _responseWriter.Object);

			// Act
			var result = _requestHandler.IsStaticFileRoutePath(_context.Object);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void IsStaticFileRoutePath_PrefixMatchedNotExistingFile_False()
		{
			// Assign
			_context.SetupGet(x => x.Request.Path).Returns(new PathString("/Foo/Bar2.css"));

			// Act
			var result = _requestHandler.IsStaticFileRoutePath(_context.Object);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void ProcessRequest_CorrectFile_TaskReturnedDataLoaded()
		{
			// Act
			var result = _requestHandler.ProcessRequest(_context.Object);

			result.Wait();

			// Assert
			_responseWriter.Verify(x => x.Write(It.Is<string>(d => d == "Hello!"), It.IsAny<IOwinResponse>()));
		}
	}
}