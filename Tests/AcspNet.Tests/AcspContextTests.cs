using System;
using System.Web.Routing;
using AcspNet.TestingHelpers;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class AcspContextTests
	{
		[Test]
		public void Constructor_NullValues_ArgumentNullExceptionsThown()
		{
			Assert.Throws<ArgumentNullException>(() => new AcspContext(null, null));
			Assert.Throws<ArgumentNullException>(() => new AcspContext(new RouteData(), null));
		}

		[Test]
		public void Constructor_CorrectValues_PropertiesInitializedCorrectly()
		{
			var httpContext = AcspNetTestingHelper.CreateTestHttpContext();
			var context = new AcspContext(new RouteData(), httpContext.Object);

			Assert.IsNotNull(context.HttpContext);
			Assert.IsNotNull(context.Request);
			Assert.IsNotNull(context.Response);
			Assert.IsNotNull(context.RouteData);
		}

		[Test]
		public void Constructor_CorrectValuesFromRouteData_ActionModeIdInitializedCorrectly()
		{
			var httpContext = AcspNetTestingHelper.CreateTestHttpContext();
			var routeData = new RouteData();

			routeData.Values.Add("action", "Foo");
			routeData.Values.Add("mode", "Bar");
			routeData.Values.Add("id", "5");

			var context = new AcspContext(routeData, httpContext.Object);

			Assert.AreEqual("Foo", context.CurrentAction);
			Assert.AreEqual("Bar", context.CurrentMode);
			Assert.AreEqual("5", context.CurrentID);
		}

		[Test]
		public void Constructor_CorrectValuesFromQueryString_ActionAndModeInitializedCorrectly()
		{
			var httpContext = AcspNetTestingHelper.CreateTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "Foo");
			httpContext.Object.Request.QueryString.Add("mode", "Bar");
			httpContext.Object.Request.QueryString.Add("id", "5");

			var context = new AcspContext(new RouteData(), httpContext.Object);

			Assert.AreEqual("Foo", context.CurrentAction);
			Assert.AreEqual("Bar", context.CurrentMode);
			Assert.AreEqual("5", context.CurrentID);
		}

		[Test]
		public void Constructor_CorrectValuesFromQueryString_ActionModeIdUrlCalculatedCorrectly()
		{
			var httpContext = AcspNetTestingHelper.CreateTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "Foo");
			httpContext.Object.Request.QueryString.Add("mode", "Bar");
			httpContext.Object.Request.QueryString.Add("id", "5");

			var context = new AcspContext(new RouteData(), httpContext.Object);

			Assert.AreEqual("?act=Foo&amp;mode=Bar&amp;id=5", context.GetActionModeUrl());
		}

		[Test]
		public void Constructor_CorrectValuesFromQueryString_ActionIdUrlCalculatedCorrectly()
		{
			var httpContext = AcspNetTestingHelper.CreateTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "Foo");
			httpContext.Object.Request.QueryString.Add("id", "5");

			var context = new AcspContext(new RouteData(), httpContext.Object);

			Assert.AreEqual("?act=Foo&amp;id=5", context.GetActionModeUrl());
		}

		[Test]
		public void Constructor_ActionIsNull_EmptyStringReturned()
		{
			var httpContext = AcspNetTestingHelper.CreateTestHttpContext();

			var context = new AcspContext(new RouteData(), httpContext.Object);

			Assert.AreEqual("", context.GetActionModeUrl());
		}
	}
}