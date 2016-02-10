using Simplify.Web.Bootstrapper;
using Simplify.Web.Meta;

namespace Simplify.Web.Tests.Bootstrapper
{
	[TestFixture]
	public class BootstrapperFactoryTests
	{
		[Test]
		public void CreateBootstrapper_NoUserType_BaseAcspNetBootstrapperReturned()
		{
			// Assign

			if(!AcspTypesFinder.ExcludedAssembliesPrefixes.Contains("AcspNet"))
				AcspTypesFinder.ExcludedAssembliesPrefixes.Add("AcspNet");

			AcspTypesFinder.CleanLoadedTypesAndAssenbliesInfo();

			// Act
			var bootstrapper = BootstrapperFactory.CreateBootstrapper();

			// Assert

			Assert.AreEqual("AcspNet.Bootstrapper.BaseAcspNetBootstrapper", bootstrapper.GetType().FullName);
		}

		[Test]
		public void CreateBootstrapper_HaveUserType_TestBootstrapperReturned()
		{
			// Assign

			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");
			AcspTypesFinder.CleanLoadedTypesAndAssenbliesInfo();

			// Act
			var bootstrapper = BootstrapperFactory.CreateBootstrapper();

			// Assert

			Assert.AreEqual("AcspNet.Tests.TestEntities.TestBootstrapper", bootstrapper.GetType().FullName);
		}
	}
}