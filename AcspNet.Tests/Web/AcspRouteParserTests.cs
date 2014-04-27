using AcspNet.Web;
using NUnit.Framework;

namespace AcspNet.Tests.Web
{
	[TestFixture]
	public class AcspRouteParserTests
	{
		[Test]
		public void ParseRoute_Root_NullValues()
		{
			string action;
			string mode;
			string id;

			AcspRouteParser.ParseRoute("/", "", out action, out mode, out id);

			Assert.IsNull(action);
			Assert.IsNull(mode);
			Assert.IsNull(id);
		}

		[Test]
		public void ParseRoute_Action_NullValues()
		{
			string action;
			string mode;
			string id;

			AcspRouteParser.ParseRoute("/foo", "", out action, out mode, out id);

			Assert.AreEqual("foo", action);
			Assert.IsNull(mode);
			Assert.IsNull(id);
		}

		[Test]
		public void ParseRoute_ActionId_NullValues()
		{
			string action;
			string mode;
			string id;

			AcspRouteParser.ParseRoute("/MySite/foo/15", "/MySite", out action, out mode, out id);

			Assert.AreEqual("foo", action);
			Assert.IsNull(mode);
			Assert.AreEqual("15", id);
		}

		[Test]
		public void ParseRoute_ActionModeId_NullValues()
		{
			string action;
			string mode;
			string id;

			AcspRouteParser.ParseRoute("/foo/bar/2", "", out action, out mode, out id);

			Assert.AreEqual("foo", action);
			Assert.AreEqual("bar", mode);
			Assert.AreEqual("2", id);
		}

		[Test]
		public void ParseRoute_ActionModeIdQueryString_NullValues()
		{
			string action;
			string mode;
			string id;

			AcspRouteParser.ParseRoute("/FooSite/foo/bar/2", "/FooSite", out action, out mode, out id);

			Assert.AreEqual("foo", action);
			Assert.AreEqual("bar", mode);
			Assert.AreEqual("2", id);
		}

		[Test]
		public void ParseRoute_QueryString_NullValues()
		{
			string action;
			string mode;
			string id;

			AcspRouteParser.ParseRoute("/TestSite/?act=test", "/TestSite", out action, out mode, out id);

			Assert.IsNull(action);
			Assert.IsNull(mode);
			Assert.IsNull(id);
		}
	}
}