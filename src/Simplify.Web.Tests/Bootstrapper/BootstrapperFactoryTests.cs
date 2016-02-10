using NUnit.Framework;
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

			if(!SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Contains("AcspNet"))
				SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Add("AcspNet");

			SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

			// Act
			var bootstrapper = BootstrapperFactory.CreateBootstrapper();

			// Assert

			Assert.AreEqual("AcspNet.Bootstrapper.BaseAcspNetBootstrapper", bootstrapper.GetType().FullName);
		}

		[Test]
		public void CreateBootstrapper_HaveUserType_TestBootstrapperReturned()
		{
			// Assign

			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");
			SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

			// Act
			var bootstrapper = BootstrapperFactory.CreateBootstrapper();

			// Assert

			Assert.AreEqual("AcspNet.Tests.TestEntities.TestBootstrapper", bootstrapper.GetType().FullName);
		}
	}
}