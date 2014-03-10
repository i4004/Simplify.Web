using System.Reflection;
using AcspNet.TestingHelpers;
using AcspNet.Tests.TestExtensions.Extensions.Executable;
using NUnit.Framework;
using Simplify.Templates;

namespace AcspNet.Tests
{
	[TestFixture]
	public class AcspProcessorTests
	{
		private readonly AcspApplication _app = new AcspApplication();

		[TestFixtureSetUp]
		public void SetupAcspApplication()
		{
			_app.MainAssembly = Assembly.GetAssembly(typeof(ActionIdTest));
			_app.SetUp();
		}

		[Test]
		public void AcspProcessor_RunActionIdExtension_ExtensionExecuted()
		{
			_app.HttpContext = AcspTestingHelper.CreateTestHttpContext().Object;
			Template.FileSystem = AcspTestingHelper.GetTestFileSystem();
			var processor = _app.CreateProcessor(AcspTestingHelper.CreateRouteDataWithActionAndId("ActionIdTest", "2"));
			processor.Execute();
		}

		[Test]
		public void AcspProcessor_RunActionModeIdExtension_ExtensionExecuted()
		{
			_app.HttpContext = AcspTestingHelper.CreateTestHttpContext().Object;
			var processor = _app.CreateProcessor(AcspTestingHelper.CreateRouteDataWithActionModeAndId("ActionModeIdTest", "ActionModeIdTestMode", "15"));
			processor.Execute();
		}

		[Test]
		public void AcspProcessor_StopExtensionsExecution_SubsequentExtensionIsNotExecuted()
		{
			_app.HttpContext = AcspTestingHelper.CreateTestHttpContext().Object;
			var processor = _app.CreateProcessor(AcspTestingHelper.CreateRouteDataWithActionAndId("stopExtensionsExecution"));
			processor.Execute();
		}
	}
}
