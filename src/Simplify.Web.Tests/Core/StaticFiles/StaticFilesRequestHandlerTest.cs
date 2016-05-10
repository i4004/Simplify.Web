using System.Collections.Generic;
using System.IO.Abstractions;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core;
using Simplify.Web.Core.StaticFiles;

namespace Simplify.Web.Tests.Core.StaticFiles
{
	[TestFixture]
	public class StaticFilesRequestHandlerTest
	{
		private Mock<IFileSystem> _fileSystem;

		private Mock<IResponseWriter> _responseWriter;
		private StaticFilesRequestHandler _requestHandler;

		private Mock<IOwinContext> _context;
		private Mock<IOwinResponse> _response;
		private IHeaderDictionary _headers;

		//[TestFixtureSetUp]
		//public void SetUpFileSystem()
		//{
		//	var files = new Dictionary<string, MockFileData>
		//	{
		//		{"C:/WebSites/MySite/Foo/Bar.css", new MockFileData(new byte[]{13})}
		//	};

		//}

		[SetUp]
		public void Initialize()
		{
			_fileSystem = new Mock<IFileSystem>();
			//StaticFilesRequestHandler.FileSystem = _fileSystem.Object;

			_responseWriter = new Mock<IResponseWriter>();
			//_requestHandler = new StaticFilesRequestHandler(new List<string> { "Foo" }, "C:/WebSites/MySite/", _responseWriter.Object);

			_headers = new HeaderDictionary(new Dictionary<string, string[]>());

			_response = new Mock<IOwinResponse>();
			_response.SetupGet(x => x.Headers).Returns(_headers);

			_context = new Mock<IOwinContext>();
			_context.SetupGet(x => x.Request.Path).Returns(new PathString("/Foo/Bar.css"));
			_context.SetupGet(x => x.Response).Returns(_response.Object);
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
			//_requestHandler = new StaticFilesRequestHandler(new List<string> { "Test" }, "C:/WebSites/MySite/", _responseWriter.Object);

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
		public void ProcessRequest_CorrectFile_LastModifiedSet()
		{
			// Assign

			// Act
			var result = _requestHandler.ProcessRequest(_context.Object);

			result.Wait();

			// Assert
			//_context.Object.Response.Headers["Last-Modified"] =
		}

		//[Test]
		//public void ProcessRequest_CorrectFile_LastModifiedSet()
		//{
		//	// Act
		//	var result = _requestHandler.ProcessRequest(_context.Object);

		//	result.Wait();

		//	// Assert
		//	_responseWriter.Verify(x => x.WriteAsync(It.Is<byte[]>(d => d[0] == 13), It.IsAny<IOwinResponse>()));
		//}
	}
}