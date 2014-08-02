using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;
using AcspNet.Routing;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class ControllersAgentTests
	{
		private ControllersAgent _agent;
		private Mock<IControllersMetaStore> _metaStore;
		private IRouteMatcher _routeMatcher;

		[SetUp]
		public void Initialize()
		{
			_metaStore = new Mock<IControllersMetaStore>();
			_routeMatcher = new RouteMatcher();
			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher);
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

			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher);

			// Act
			var items = _agent.GetStandardControllersMetaData().ToList();

			// Assert
			Assert.AreEqual(1, items.Count());
			Assert.IsNull(items.First().Role);
		}

		[Test]
		public void MatchControllerRoute_NoControllerRouteData_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null), "/test", "GET");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_GetControllerRouteGetMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/test"))), "/test", "GET");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_PostControllerRoutePostMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/test"))), "/test", "POST");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_PutControllerRoutePutMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, "/test"))), "/test",
				"PUT");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_DeleteControllerRouteDeleteMethodMatched_Success()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, null, null, "/test"))),
				"/test", "DELETE");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void MatchControllerRoute_PostControllerRouteGetMethodMatched_Null()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo(null, "/test"))), "/test", "GET");

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public void MatchControllerRoute_GetControllerRouteGetMethodNotMatched_NoSuccess()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/test"))), "/foo", "GET");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void MatchControllerRoute_UndefinedMethod_Null()
		{
			// Act
			var result = _agent.MatchControllerRoute(
				new ControllerMetaData(null, new ControllerExecParameters(new ControllerRouteInfo("/test"))), "/foo", "FOO");

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public void GetHandlerController_NoController_Null()
		{
			// Assign

			_metaStore.SetupGet(x => x.ControllersMetaData).Returns(new List<IControllerMetaData>());
			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher);

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

			_agent = new ControllersAgent(_metaStore.Object, _routeMatcher);

			// Act

			var metaData = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

			// Assert

			Assert.IsTrue(metaData.Role.Is404Handler);
		}
	}
}