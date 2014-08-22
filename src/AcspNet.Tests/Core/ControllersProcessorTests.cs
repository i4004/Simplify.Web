﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcspNet.Core;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.DI;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	public class ControllersProcessorTests
	{
		private ControllersProcessor _processor;
		private Mock<IControllerFactory> _controllerFactory;
		private Mock<IControllerResponseBuilder> _controllerResponseBuilder;

		private Mock<Controller> _syncController;
		private Mock<AsyncController> _asyncController;
		private Mock<ControllerResponse> _controllerResponse;

		[SetUp]
		public void Initialize()
		{
			_controllerFactory = new Mock<IControllerFactory>();
			_controllerResponseBuilder = new Mock<IControllerResponseBuilder>();
			_processor = new ControllersProcessor(_controllerFactory.Object, _controllerResponseBuilder.Object);

			_syncController = new Mock<Controller>();
			_asyncController = new Mock<AsyncController>();
			_controllerResponse = new Mock<ControllerResponse>();
		}

		[Test]
		public void Process_StandardControllerNoResponse_CreatedDefaultReturned()
		{
			// Assign
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
						It.IsAny<IDictionary<string, Object>>())).Returns(_syncController.Object);
			// Act
			var result = _processor.Process(null, null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
					It.IsAny<IDictionary<string, Object>>()));
			_syncController.Verify(x => x.Invoke());
		}

		[Test]
		public void Process_StandardControllerHaveDefaultResponse_CreatedProcessedDefaultReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.Default);
			_syncController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
						It.IsAny<IDictionary<string, Object>>())).Returns(_syncController.Object);
			// Act
			var result = _processor.Process(null, null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
					It.IsAny<IDictionary<string, Object>>()));
			_syncController.Verify(x => x.Invoke());
			_controllerResponse.Setup(x => x.Process());
		}

		[Test]
		public void Process_StandardControllerHaveRawDataResponse_CreatedProcessedRawDataReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.RawOutput);
			_syncController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
						It.IsAny<IDictionary<string, Object>>())).Returns(_syncController.Object);
			// Act
			var result = _processor.Process(null, null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.RawOutput, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
					It.IsAny<IDictionary<string, Object>>()));
			_syncController.Verify(x => x.Invoke());
			_controllerResponse.Setup(x => x.Process());
		}

		[Test]
		public void Process_AsyncControllerHaveRawDataResponse_CreatedDefaultReturnedRawDataReturnedAfterGetProcessResponse()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.RawOutput);
			_asyncController.Setup(x => x.Invoke()).Returns(Task.FromResult(_controllerResponse.Object));
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
						It.IsAny<IDictionary<string, Object>>())).Returns(_asyncController.Object);
			// Act
			var result = _processor.Process(null, null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_asyncController.Verify(x => x.Invoke());
			_controllerResponse.Verify(x => x.Process(), Times.Never());
			_controllerFactory.Verify(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>(),
						It.IsAny<IDictionary<string, Object>>()));
			
			// Act & Assert

			foreach (var response in _processor.ProcessAsyncControllersResponses(null))
			{
				_controllerResponse.Verify(x => x.Process());
				Assert.AreEqual(ControllerResponseResult.RawOutput, response);
			}
		}
	}
}