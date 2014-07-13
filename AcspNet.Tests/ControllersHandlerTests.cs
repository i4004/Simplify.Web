using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class ControllersHandlerTests
	{
		Mock<IControllersAgent> _agent;
		ControllersHandler _handler;

		[SetUp]
		public void Initialize()
		{
			_agent = new Mock<IControllersAgent>();
			_handler = new ControllersHandler(_agent.Object);

			//_metaStore.Setup(x => x.GetControllersMetaData()).Returns(new List<ControllerMetaData>());
		}

		[Test]
		public void CreateAndInvokeControllers_NoControllers_Http404Returned()
		{
			// Arrange

			// Act & Assert

			//Assert.AreEqual(ControllersHandlerResult.Http404, _handler.Execute(null));
		}

		[Test]
		public void CreateAndInvokeControllers_NoNonAnyPageControllers_Http404Returned()
		{
			// Arrange

			//var controllers = new List<ControllerMetaData>
			//{
			//	new ControllerMetaData(typeof (TestController1), new ControllerExecParameters(new ControllerRouteInfo("/test")))
			//};

			//var controller = new Mock<Controller>();

			//_metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);
			//_factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

			//// Act & Assert

			//Assert.AreEqual(ControllersHandlerResult.Http404, _handler.Execute("/"));
		}
		
		[Test]
		public void CreateAndInvokeControllers_SomePage_InvokedCorrectly()
		{
			// Arrange

			//var controllers = new List<ControllerMetaData>
			//{
			//	new ControllerMetaData(typeof (TestController1), new ControllerExecParameters(new ControllerRouteInfo("/"))),
			//	new ControllerMetaData(typeof (TestController1), new ControllerExecParameters(new ControllerRouteInfo("/foo/bar"))),
			//	new ControllerMetaData(typeof (TestController1), new ControllerExecParameters()),
			//	new ControllerMetaData(typeof (TestController1), null, new ControllerRole(true)),
			//	new ControllerMetaData(typeof (TestController1), null, new ControllerRole(false, true)),
			//	new ControllerMetaData(typeof (TestController1), null, new ControllerRole(false, false, true))
			//};

			//var controller = new Mock<Controller>();

			//_metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);
			//_factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

			//// Act

			//var result = _handler.Execute("/foo/bar");

			//// Assert

			//Assert.AreEqual(ControllersHandlerResult.Ok, result);
			//_factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(2));
			//controller.Verify(x => x.Invoke(), Times.Exactly(2));
		}

		//[Test]
		//public void CreateAndInvokeControllers_AjaxRequest_InvokedCorrectly()
		//{
		//	// Arrange

		//	var controllers = new List<ControllerMetaContainer>
		//	{
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, -1)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar", 0, false, true)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1)),
		//	};

		//	var controller = new Mock<Controller>();
		//	controller.SetupGet(x => x.AjaxResult).Returns("test");

		//	var metaStore = new Mock<IControllersMetaStore>();
		//	metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

		//	var factory = new Mock<IControllerFactory>();
		//	factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

		//	var executionAgent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo", "bar");

		//	// Act

		//	var handler = new ControllersHandler(metaStore.Object, factory.Object, executionAgent);
		//	var result = handler.Execute();

		//	// Assert

		//	Assert.AreEqual(ControllersHandlerResult.AjaxRequest, result);
		//	Assert.AreEqual("test", handler.AjaxResult);
		//	factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(2));
		//	controller.Verify(x => x.Invoke(), Times.Exactly(2));
		//}

		//[Test]
		//public void CreateAndInvokeControllers_StopExecution_InvokedCorrectly()
		//{
		//	// Arrange

		//	var controllers = new List<ControllerMetaContainer>
		//	{
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar")),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1))
		//	};

		//	var controller = new Mock<Controller>();
		//	controller.SetupGet(x => x.StopExecution).Returns(true);

		//	var metaStore = new Mock<IControllersMetaStore>();
		//	metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

		//	var factory = new Mock<IControllerFactory>();
		//	factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

		//	var executionAgent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo", "bar");

		//	// Act

		//	var handler = new ControllersHandler(metaStore.Object, factory.Object, executionAgent);
		//	var result = handler.Execute();

		//	// Assert

		//	Assert.AreEqual(ControllersHandlerResult.StopExecution, result);
		//	factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(1));
		//	controller.Verify(x => x.Invoke(), Times.Exactly(1));
		//}

		//[Test]
		//public void CreateAndInvokeControllers_HttpGetRequestViolated_Http400ControllerInvoked()
		//{
		//	// Arrange

		//	var controllers = new List<ControllerMetaContainer>
		//	{
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, -1)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar"), new ControllerSecurity(false, true)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar")),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters()),
		//		new ControllerMetaContainer(typeof (TestController2), null, null, new ControllerRole(true)),
		//		new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, true)),
		//		new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, false, true))
		//	};

		//	var controller = new Mock<Controller>();
		//	var controller2 = new Mock<Controller>();

		//	var metaStore = new Mock<IControllersMetaStore>();
		//	metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

		//	var factory = new Mock<IControllerFactory>();
		//	factory.Setup(x => x.CreateController(It.Is<Type>(t => t.Name == "TestController"))).Returns(controller.Object);
		//	factory.Setup(x => x.CreateController(It.Is<Type>(t => t.Name == "TestController2"))).Returns(controller2.Object);

		//	var executionAgent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo", "bar");

		//	// Act

		//	var handler = new ControllersHandler(metaStore.Object, factory.Object, executionAgent);
		//	var result = handler.Execute();

		//	// Assert

		//	Assert.AreEqual(ControllersHandlerResult.Ok, result);
		//	factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(4));
		//	controller.Verify(x => x.Invoke(), Times.Exactly(3));
		//	controller2.Verify(x => x.Invoke(), Times.Once);
		//}

		//[Test]
		//public void CreateAndInvokeControllers_AuthenticationViolated_Http403ControllerInvoked()
		//{
		//	// Arrange

		//	var controllers = new List<ControllerMetaContainer>
		//	{
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, -1)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar"), new ControllerSecurity(true)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar"), new ControllerSecurity(true)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1)),
		//		new ControllerMetaContainer(typeof (TestController2), null, null, new ControllerRole(false, true))
		//	};

		//	var controller = new Mock<Controller>();
		//	var controller2 = new Mock<Controller>();

		//	var metaStore = new Mock<IControllersMetaStore>();
		//	metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

		//	var factory = new Mock<IControllerFactory>();
		//	factory.Setup(x => x.CreateController(It.Is<Type>(t => t.Name == "TestController"))).Returns(controller.Object);
		//	factory.Setup(x => x.CreateController(It.Is<Type>(t => t.Name == "TestController2"))).Returns(controller2.Object);

		//	var executionAgent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo", "bar");

		//	// Act

		//	var handler = new ControllersHandler(metaStore.Object, factory.Object, executionAgent);
		//	var result = handler.Execute();

		//	// Assert

		//	Assert.AreEqual(ControllersHandlerResult.Ok, result);
		//	factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(3));
		//	controller.Verify(x => x.Invoke(), Times.Exactly(2));
		//	controller2.Verify(x => x.Invoke(), Times.Once);
		//}

		//[Test]
		//public void CreateAndInvokeControllers_AuthenticationViolatedNoHandler_Http403Returned()
		//{
		//	// Arrange

		//	var controllers = new List<ControllerMetaContainer>
		//	{
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar"), new ControllerSecurity(true)),
		//	};

		//	var controller = new Mock<Controller>();

		//	var metaStore = new Mock<IControllersMetaStore>();
		//	metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

		//	var factory = new Mock<IControllerFactory>();
		//	factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

		//	var executionAgent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo", "bar");

		//	// Act

		//	var handler = new ControllersHandler(metaStore.Object, factory.Object, executionAgent);
		//	var result = handler.Execute();

		//	// Assert

		//	Assert.AreEqual(ControllersHandlerResult.Http403, result);
		//	factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Never);
		//	controller.Verify(x => x.Invoke(), Times.Never);
		//}

		//[Test]
		//public void CreateAndInvokeControllers_SecurityViolatedNoHandler_Http400Returned()
		//{
		//	// Arrange

		//	var controllers = new List<ControllerMetaContainer>
		//	{
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar"), new ControllerSecurity(false, true)),
		//	};

		//	var controller = new Mock<Controller>();

		//	var metaStore = new Mock<IControllersMetaStore>();
		//	metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

		//	var factory = new Mock<IControllerFactory>();
		//	factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

		//	var executionAgent = new ControllerExecutionAgent(new Mock<IAuthenticationState>().Object, "foo", "bar");

		//	// Act

		//	var handler = new ControllersHandler(metaStore.Object, factory.Object, executionAgent);
		//	var result = handler.Execute();

		//	// Assert

		//	Assert.AreEqual(ControllersHandlerResult.Http400, result);
		//	factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Never);
		//	controller.Verify(x => x.Invoke(), Times.Never);
		//}
	}
}