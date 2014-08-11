using System.Collections.Generic;
using System.Linq;
using AcspNet.Core;
using AcspNet.Meta;
using AcspNet.Routing;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	public class ControllersAgentTests
	{
		private ControllersAgent _agent;
		private Mock<IControllersMetaStore> _metaStore;
		private Mock<IRouteMatcher> _routeMatcher;

		[SetUp]
		public void Initialize()
		{
			_metaStore = new Mock<IControllersMetaStore>();
			_routeMatcher = new Mock<IRouteMatcher>();//_controllerPathParser.Object);
			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);
		}

		[Test]
		public void GetStandardControllersMetaData_StandartControllerAndAll40xControllers_OnlyStandartReturned()
		{
			// Assign

			_metaStore.SetupGet(x => x.ControllersMetaData)
				.Returns(new List<IControllerMetaData>
				{
					new ControllerMetaData(null),
					new ControllerMetaData(null, null, new ControllerRole(true)),
					new ControllerMetaData(null, null, new ControllerRole(false, true)),
					new ControllerMetaData(null, null, new ControllerRole(false, false, true))
				});

			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);

			// Act
			var items = _agent.GetStandardControllersMetaData().ToList();

			// Assert
			Assert.AreEqual(1, items.Count());
			Assert.IsNull(items.First().Role);
		}

		[Test]
		public void MatchControllerRoute_NoControllerRouteData_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(new ControllerMetaData(null), "/foo", "GET");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/foo"), It.Is<string>(s => s == null)));
		}

		[Test]
		public void MatchControllerRoute_GetControllerRouteGetMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/foo"))), "/bar", "GET");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_PostControllerRoutePostMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/foo"))), "/bar", "POST");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_PutControllerRoutePutMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, "/foo"))), "/bar",
				"PUT");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_DeleteControllerRouteDeleteMethod_MatchCalled()
		{
			// Act
			_agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, null, "/foo"))),
				"/bar", "DELETE");

			// Assert
			_routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == "/bar"), It.Is<string>(s => s == "/foo")));
		}

		[Test]
		public void MatchControllerRoute_PostControllerRouteGetMethod_MatchNotCalled()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/foo"))), "/bar", "GET");

			// Assert

			Assert.IsNull(result);
			_routeMatcher.Verify(x => x.Match(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void MatchControllerRoute_UndefinedMethod_MatchNotCalled()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/foo"))), "/bar", "FOO");

			// Assert

			Assert.IsNull(result);
			_routeMatcher.Verify(x => x.Match(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void GetHandlerController_NoController_Null()
		{
			// Assign

			_metaStore.SetupGet(x => x.ControllersMetaData).Returns(new List<IControllerMetaData>());
			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);

			// Act & Assert
			Assert.IsNull(_agent.GetHandlerController(HandlerControllerType.Http404Handler));
		}

		[Test]
		public void GetHandlerController_HaveController_ControllerMetaDataReturned()
		{
			// Assign

			_metaStore.SetupGet(x => x.ControllersMetaData).Returns(new List<IControllerMetaData>
			{
				new ControllerMetaData(null, null, new ControllerRole(false, false, true))
			});

			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher.Object);

			// Act

			var metaData = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

			// Assert

			Assert.IsTrue(metaData.Role.Is404Handler);
		}
	}
}