using AcspNet.Bootstrapper;
using AcspNet.Meta;
using AcspNet.Tests.TestEntities;
using NUnit.Framework;

namespace AcspNet.Tests.Meta
{
	[TestFixture]
	public class AcspTypesFinderTests
	{
		[Test]
		public void FindTypeDerivedFrom_BaseAcspNetBootstrapper_TestBootstrapperReturned()
		{
			// Assign
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			// Act
			var type = AcspTypesFinder.FindTypeDerivedFrom<BaseAcspNetBootstrapper>();

			// Assert
			Assert.AreEqual("AcspNet.Tests.TestEntities.TestBootstrapper", type.FullName);
		}

		[Test]
		public void FindTypeDerivedFrom_TestBootstrapper_NullReturned()
		{
			// Act
			var type = AcspTypesFinder.FindTypeDerivedFrom<TestBootstrapper>();

			// Assert
			Assert.IsNull(type);
		}
	}
}