using System.Collections.Generic;
using Simplify.Web.Routing;

namespace Simplify.Web.Tests.Routing
{
	[TestFixture]
	public class RouteMatcherTests
	{
		private IRouteMatcher _matcher;
		private Mock<IControllerPathParser> _controllerPathParser;

		[SetUp]
		public void Initialize()
		{
			_controllerPathParser = new Mock<IControllerPathParser>();
			_matcher = new RouteMatcher(_controllerPathParser.Object);
		}

		[Test]
		public void Match_SourceEmptyOrNull_False()
		{
			// Act
			var result = _matcher.Match(null, "/test");
			var result2 = _matcher.Match("", "/test");

			// Assert
			Assert.IsFalse(result.Success);
			Assert.IsFalse(result2.Success);
		}

		[Test]
		public void Match_SingleSegmentWithNullString_True()
		{
			// Act
			var result = _matcher.Match("/test", null);

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_RootWithRoot_True()
		{
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>())).Returns(new ControllerPath(new List<PathItem>()));

			// Act
			var result = _matcher.Match("/", "/");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_SingleSegments_True()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> {new PathSegment("foo")}));

			// Act
			var result = _matcher.Match("/foo", "/foo");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_SingleSegmentsNotMatching_False()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathSegment("bar") }));

			// Act
			var result = _matcher.Match("/foo", "/bar");

			// Assert
			Assert.IsFalse(result.Success);
		}
		
		[Test]
		public void Match_MultipleSegmentsWithFirstMatchedSegment_False()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> {new PathSegment("foo")}));

			// Act
			var result = _matcher.Match("/foo/bar/test", "/foo");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_SingleSegmentsWithMultipleSegments_False()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(
					new ControllerPath(new List<PathItem> {new PathSegment("foo"), new PathSegment("bar"), new PathSegment("test")}));

			// Act
			var result = _matcher.Match("/foo", "/foo/bar/test");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_TwoSegmentsWithSegmentAndParameter_TrueValueParsed()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathSegment("user"), new PathParameter("userName", typeof(string)) }));

			// Act
			var result = _matcher.Match("/user/testuser", "/user/{userName}");

			// Assert

			Assert.IsTrue(result.Success);
			Assert.AreEqual("testuser", result.RouteParameters.userName);
		}

		[Test]
		public void Match_TwoSegmentsWithSegmentAndParameterNotMatched_False()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathSegment("bar"), new PathParameter("userName", typeof(string)) }));

			// Act
			var result = _matcher.Match("/user/testuser", "/bar/{userName}");

			// Assert

			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_TwoSegmentsWithOneSegmentAndTwoParameters_False()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(
					new ControllerPath(new List<PathItem>
					{
						new PathSegment("foo"),
						new PathParameter("test", typeof (string)),
						new PathParameter("userName", typeof (string))
					}));

			// Act
			var result = _matcher.Match("/user/testuser", "/foo/{test}/{userName}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_OneSegmentWithOneParameter_True()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathParameter("userName", typeof(string)) }));

			// Act
			var result = _matcher.Match("/user", "/{userName}");

			// Assert

			Assert.IsTrue(result.Success);
			Assert.AreEqual("user", result.RouteParameters.userName);
		}

		[Test]
		public void Match_ParameterTypeMismatch_False()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathParameter("userName", typeof(int)) }));

			// Act
			var result = _matcher.Match("/foo/bar", "/foo/{id:int}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_TwoSegmentsWithTwoParameters_TrueParsed()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathParameter("test", typeof(string)), new PathParameter("name", typeof(string)) }));

			// Act
			var result = _matcher.Match("/foo/bar", "/{test}/{name}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual("foo", result.RouteParameters.test);
			Assert.AreEqual("bar", result.RouteParameters.name);
		}

		[Test]
		public void Match_OneSegmentWithOneIntegerParameter_True()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathParameter("id", typeof(int)) }));

			// Act
			var result = _matcher.Match("/15", "/{id}");

			// Assert

			Assert.IsTrue(result.Success);
			Assert.AreEqual(15, result.RouteParameters.id);
		}

		[Test]
		public void Match_OneSegmentWithOneDecimalParameter_True()
		{
			// Assign
			_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ControllerPath(new List<PathItem> { new PathParameter("id", typeof(decimal)) }));

			// Act
			var result = _matcher.Match("/15", "/{id}");

			// Assert

			Assert.IsTrue(result.Success);
			Assert.AreEqual((decimal)15, result.RouteParameters.id);
		}
	}
}