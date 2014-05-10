using System;
using System.Collections.Generic;
using AcspNet.Meta;
using AcspNet.Tests.TestControllers;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class ControllersHandlerTests
	{
		[Test]
		public void CreateAndInvokeControllers_Http404Returned()
		{
			// Arrange

			var metaStore = new Mock<IControllersMetaStore>();
			metaStore.Setup(x => x.GetControllersMetaData()).Returns(new List<ControllerMetaContainer>());

			var handler = new ControllersHandler(metaStore.Object, null, null, null);

			// Assert
			Assert.AreEqual(ControllersHandlerResult.Http404, handler.Execute());
		}

		[Test]
		public void CreateAndInvokeControllers_NoCurrentControllers_Http404Returned()
		{
			// Arrange

			var controllers = new List<ControllerMetaContainer>
			{
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("test"))
			};

			var controller = new Mock<Controller>();

			var metaStore = new Mock<IControllersMetaStore>();
			metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

			var factory = new Mock<IControllerFactory>();
			factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

			var handler = new ControllersHandler(metaStore.Object, factory.Object, "foo", "bar", null);

			// Assert
			Assert.AreEqual(ControllersHandlerResult.Http404, handler.Execute());
		}

		[Test]
		public void CreateAndInvokeControllers_DefaultPage_InvokedCorrectly()
		{
			// Arrange

			var controllers = new List<ControllerMetaContainer>
			{
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 0, true)),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar")),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters()),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(true)),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, true)),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, false, true))
			};

			var controller = new Mock<Controller>();

			var metaStore = new Mock<IControllersMetaStore>();
			metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

			var factory = new Mock<IControllerFactory>();
			factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

			// Act

			var handler = new ControllersHandler(metaStore.Object, factory.Object, null, null);
			var result = handler.Execute();

			// Assert

			Assert.AreEqual(ControllersHandlerResult.Ok, result);
			factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(2));
			controller.Verify(x => x.Invoke(), Times.Exactly(2));
		}

		[Test]
		public void CreateAndInvokeControllers_SomePage_InvokedCorrectly()
		{
			// Arrange

			var controllers = new List<ControllerMetaContainer>
			{
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 0, true)),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar")),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters()),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(true)),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, true)),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, false, true))
			};

			var controller = new Mock<Controller>();

			var metaStore = new Mock<IControllersMetaStore>();
			metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

			var factory = new Mock<IControllerFactory>();
			factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

			// Act

			var handler = new ControllersHandler(metaStore.Object, factory.Object, "foo", "bar");
			var result = handler.Execute();

			// Assert

			Assert.AreEqual(ControllersHandlerResult.Ok, result);
			factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(2));
			controller.Verify(x => x.Invoke(), Times.Exactly(2));
		}

		[Test]
		public void CreateAndInvokeControllers_AjaxRequest_InvokedCorrectly()
		{
			// Arrange

			var controllers = new List<ControllerMetaContainer>
			{
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, -1)),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar", 0, false, true)),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1)),
			};

			var controller = new Mock<Controller>();
			controller.SetupGet(x => x.AjaxResult).Returns("test");

			var metaStore = new Mock<IControllersMetaStore>();
			metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

			var factory = new Mock<IControllerFactory>();
			factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

			// Act

			var handler = new ControllersHandler(metaStore.Object, factory.Object, "foo", "bar");
			var result = handler.Execute();

			// Assert

			Assert.AreEqual(ControllersHandlerResult.AjaxRequest, result);
			Assert.AreEqual("test", handler.AjaxResult);
			factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(2));
			controller.Verify(x => x.Invoke(), Times.Exactly(2));
		}

		[Test]
		public void CreateAndInvokeControllers_StopExecution_InvokedCorrectly()
		{
			// Arrange

			var controllers = new List<ControllerMetaContainer>
			{
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar")),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1))
			};

			var controller = new Mock<Controller>();
			controller.SetupGet(x => x.StopExecution).Returns(true);

			var metaStore = new Mock<IControllersMetaStore>();
			metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

			var factory = new Mock<IControllerFactory>();
			factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

			// Act

			var handler = new ControllersHandler(metaStore.Object, factory.Object, "foo", "bar");
			var result = handler.Execute();

			// Assert

			Assert.AreEqual(ControllersHandlerResult.StopExecution, result);
			factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(1));
			controller.Verify(x => x.Invoke(), Times.Exactly(1));
		}

		[Test]
		public void CreateAndInvokeControllers_HttpGetOnlyController_ErrorReturned()
		{
			// Arrange

			var controllers = new List<ControllerMetaContainer>
			{
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, -1)),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar"), new ControllerSecurity(true)),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters("foo", "bar")),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1)),
				new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters()),
				new ControllerMetaContainer(typeof (TestController2), null, null, new ControllerRole(true)),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, true)),
				new ControllerMetaContainer(typeof (TestController), null, null, new ControllerRole(false, false, true))
			};

			var controller = new Mock<Controller>();
			var controller2 = new Mock<Controller>();

			var metaStore = new Mock<IControllersMetaStore>();
			metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

			var factory = new Mock<IControllerFactory>();
			factory.Setup(x => x.CreateController(It.Is<Type>(t => t.Name == "TestController"))).Returns(controller.Object);
			factory.Setup(x => x.CreateController(It.Is<Type>(t => t.Name == "TestController2"))).Returns(controller2.Object);

			// Act

			var handler = new ControllersHandler(metaStore.Object, factory.Object, "foo", "bar");
			var result = handler.Execute();

			// Assert

			Assert.AreEqual(ControllersHandlerResult.Ok, result);
			factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(4));
			controller.Verify(x => x.Invoke(), Times.Exactly(3));
			controller2.Verify(x => x.Invoke(), Times.Once);
		}

		[Test]
		public void CreateAndInvokeControllers_NoPage_404PagesCalled()
		{
		//	// Arrange

		//	var controllers = new List<ControllerMetaContainer>
		//	{
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, -1)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 0, false, false, true)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 0, false, false, true)),
		//		new ControllerMetaContainer(typeof (TestController), new ControllerExecParameters(null, null, 1))
		//	};

		//	var controller = new Mock<Controller>();

		//	var metaStore = new Mock<IControllersMetaStore>();
		//	metaStore.Setup(x => x.GetControllersMetaData()).Returns(controllers);

		//	var factory = new Mock<IControllerFactory>();
		//	factory.Setup(x => x.CreateController(It.IsAny<Type>())).Returns(controller.Object);

		//	// Act

		//	var handler = new ControllersHandler(metaStore.Object, factory.Object, "foo", "bar");
		//	var result = handler.Execute();

		//	// Assert

		//	Assert.AreEqual(ControllersHandlerResult.Ok, result);
		//	factory.Verify(x => x.CreateController(It.IsAny<Type>()), Times.Exactly(4));
		//	controller.Verify(x => x.Invoke(), Times.Exactly(4));
		}
	}
}