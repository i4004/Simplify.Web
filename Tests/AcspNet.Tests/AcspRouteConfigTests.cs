using System.Web.Routing;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class AcspRouteConfigTests
	{
		[Test]
		public void RegisterRoutes_AcspRoutes_RegisteredCorrectly()
		{
			Assert.AreEqual(0, RouteTable.Routes.Count);
			AcspRouteConfig.RegisterRoutes("index.aspx");
			Assert.AreEqual(3, RouteTable.Routes.Count);
		}
	}
}