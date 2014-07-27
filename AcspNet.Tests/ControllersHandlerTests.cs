using System;
using System.Collections.Generic;
using AcspNet.Meta;
using AcspNet.Routing;
using AcspNet.Tests.TestEntities;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class ControllersHandlerTests
	{
		private Mock<IControllersAgent> _agent;
		private ControllersHandler _handler;
		private Mock<IControllerFactory> _factory;
		private Mock<Controller> _controller;

		[SetUp]
		public void Initialize()
		{
			_agent = new Mock<IControllersAgent>();
			_factory = new Mock<IControllerFactory>();

			_handler = new ControllersHandler(_agent.Object, _factory.Object);

			_controller = new Mock<Controller>();
			
			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
		}

		[Test]
		public void CreateAndInvokeControllers_MatchControllerRoute_InvokedWithCorrectParameters()
		{
			var metaData = new ControllerMetaData(typeof(TestController1),
				new ControllerExecParameters(new ControllerRouteInfo("/foo")));

			_agent.Setup(x => x.GetStandartControllersMetaData()).Returns(() => new List<IControllerMetaData>
			{
				metaData
			});

			// Act

			_handler.Execute("/foo/bar", "GET");
			
			// Assert

			_agent.Verify(x => x.GetStandartControllersMetaData());
			_agent.Verify(x => x.MatchControllerRoute(It.Is<IControllerMetaData>(d => d == metaData), It.Is<string>(d => d == "/foo/bar"), It.Is<string>(d => d == "GET")));			
		}

		[Test]
		public void CreateAndInvokeControllers_ControllerMatched_CreatedCorrectly()
		{
			// Arrange

			var metaData = new ControllerMetaData(typeof (TestController1),
				new ControllerExecParameters(new ControllerRouteInfo("/foo/bar")));

			_agent.Setup(x => x.GetStandartControllersMetaData()).Returns(() => new List<IControllerMetaData>
			{
				metaData
			});

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult(true));

			_factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(_controller.Object);

			// Act

			_handler.Execute("/foo/bar", "GET");

			// Assert

			_factory.Verify(x => x.CreateController(It.Is<Type>(t => t == typeof(TestController1))), Times.Exactly(1));
		}
	
		[Test]
		public void CreateAndInvokeControllers_ControllerMatched_InvokedCorrectly()
		{
			// Arrange

			var metaData = new ControllerMetaData(typeof (TestController1),
				new ControllerExecParameters(new ControllerRouteInfo("/foo/bar")));

			_agent.Setup(x => x.GetStandartControllersMetaData()).Returns(() => new List<IControllerMetaData>
			{
				metaData
			});

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult(true));
			
			_factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(_controller.Object);

			// Act

			var result = _handler.Execute("/foo/bar", "GET");

			// Assert

			Assert.AreEqual(ControllersHandlerResult.Ok, result);
			_controller.Verify(x => x.Invoke(), Times.Exactly(1));
		}
	}
}