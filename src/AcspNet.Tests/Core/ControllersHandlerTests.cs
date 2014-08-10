using System;
using System.Collections.Generic;
using AcspNet.Core;
using AcspNet.DI;
using AcspNet.Meta;
using AcspNet.Routing;
using AcspNet.Tests.TestEntities;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	public class ControllersHandlerTests
	{
		private Mock<IControllersAgent> _agent;
		private ControllersHandler _handler;
		private Mock<IControllerFactory> _factory;

		private Mock<Controller> _controller;
		private ControllerMetaData _metaData;

		private readonly IDIContainerProvider _containerProvider = null;

		[SetUp]
		public void Initialize()
		{
			_agent = new Mock<IControllersAgent>();
			_factory = new Mock<IControllerFactory>();
			_handler = new ControllersHandler(_agent.Object, _factory.Object);

			_controller = new Mock<Controller>();
			_metaData = new ControllerMetaData(typeof (TestController1),
				new ControllerExecParameters(new ControllerRouteInfo("/foo/bar")));

			_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
			{
				_metaData
			});

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult(true));

			_factory.Setup(x => x.CreateController(It.IsAny<IDIContainerProvider>(), It.IsAny<Type>())).Returns(_controller.Object);
		}

		[Test]
		public void CreateAndInvokeControllers_ControllerMatched_InvokedCorrectly()
		{
			// Act

			var result = _handler.Execute(_containerProvider, "/foo/bar", "GET");

			// Assert

			Assert.AreEqual(ControllersHandlerResult.Ok, result);
			_agent.Verify(x => x.GetStandardControllersMetaData());
			_agent.Verify(x => x.MatchControllerRoute(It.Is<IControllerMetaData>(d => d == _metaData), It.Is<string>(d => d == "/foo/bar"), It.Is<string>(d => d == "GET")));
			_factory.Verify(x => x.CreateController(It.IsAny<IDIContainerProvider>(), It.Is<Type>(t => t == typeof(TestController1))), Times.Exactly(1));
			_controller.Verify(x => x.Invoke(), Times.Exactly(1));
		}

		[Test]
		public void CreateAndInvokeControllers_NoControllersMatchedNo404Controller_404Returned()
		{
			// Assign
			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
	
			// Act

			var result = _handler.Execute(_containerProvider, "/foo/test", "GET");

			// Assert

			Assert.AreEqual(ControllersHandlerResult.Http404, result);
			_controller.Verify(x => x.Invoke(), Times.Never);
			_factory.Verify(x => x.CreateController(It.IsAny<IDIContainerProvider>(), It.Is<Type>(t => t == typeof(TestController1))), Times.Never);
		}

		[Test]
		public void CreateAndInvokeControllers_NoControllersMatchedButHave404Controller_404ControllerExecuted()
		{
			// Assign

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
			_agent.Setup(
				x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
				.Returns(new ControllerMetaData(typeof(TestController2)));

			// Act

			var result = _handler.Execute(_containerProvider, "/foo/test", "GET");

			// Assert

			Assert.AreEqual(ControllersHandlerResult.Ok, result);
			_controller.Verify(x => x.Invoke());
			_factory.Verify(x => x.CreateController(It.IsAny<IDIContainerProvider>(), It.Is<Type>(t => t == typeof(TestController2))));
		}
	}
}