using System;
using AcspNet.Core;
using AcspNet.Modules;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.DI;

namespace AcspNet.Tests.Core
{
	[TestFixture]
	public class ContextVariablesSetterTests
	{
		private Mock<IDataCollector> _dataCollector;
		private ContextVariablesSetter _setter;
		private Mock<IDIContainerProvider> _containerProvider;

		private Mock<IEnvironment> _environment;
		private Mock<ILanguageManagerProvider> _languageManagerProvider;
		private Mock<ILanguageManager> _languageManager;
		private Mock<IAcspNetContextProvider> _contextProvider;
		private Mock<IAcspNetContext> _context;
		private Mock<IStopwatchProvider> _stopwatchProvider;

		[SetUp]
		public void Initialize()
		{
			_dataCollector = new Mock<IDataCollector>();
			_setter = new ContextVariablesSetter(_dataCollector.Object);
			_containerProvider = new Mock<IDIContainerProvider>();

			_environment = new Mock<IEnvironment>();
			_languageManagerProvider = new Mock<ILanguageManagerProvider>();
			_languageManager = new Mock<ILanguageManager>();
			_contextProvider = new Mock<IAcspNetContextProvider>();
			_context = new Mock<IAcspNetContext>();
			_stopwatchProvider = new Mock<IStopwatchProvider>();

			_environment.SetupGet(x => x.TemplatesPath).Returns("Templates");
			_environment.SetupGet(x => x.SiteStyle).Returns("Main");

			_languageManager.SetupGet(x => x.Language).Returns("ru");
			_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);

			_context.SetupGet(x => x.SiteUrl).Returns("http://localhost/mysite/");
			_contextProvider.Setup(x => x.Get()).Returns(_context.Object);
			_context.SetupGet(x => x.Request.PathBase).Returns(new PathString("/mysite"));

			_stopwatchProvider.Setup(x => x.StopAndGetMeasurement()).Returns(new TimeSpan(0, 0, 1, 15, 342));

			_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IEnvironment)))).Returns(_environment.Object);
			_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(ILanguageManagerProvider)))).Returns(_languageManagerProvider.Object);
			_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IAcspNetContextProvider)))).Returns(_contextProvider.Object);
			_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IStopwatchProvider)))).Returns(_stopwatchProvider.Object);
		}

		[Test]
		public void SetVariables_NormalData_AllVariablesSetToDataCollector()
		{
			// Act
			_setter.SetVariables(_containerProvider.Object);

			// Assert

			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameTemplatesPath), It.Is<string>(d => d == "Templates")));
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameSiteStyle), It.Is<string>(d => d == "Main")));
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguage), It.Is<string>(d => d == "ru")));
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguageExtension), It.Is<string>(d => d == ".ru")));
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameSiteUrl), It.Is<string>(d => d == "http://localhost/mysite/")));
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameSiteVirtualPath), It.Is<string>(d => d == "/mysite")));
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameExecutionTime), It.Is<string>(d => d == "01:15:342")));
		}
	}
}