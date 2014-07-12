using System.Linq;
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

		[Test]
		public void FindTypesDerivedFrom_Controller_2TestControllersReturned()
		{
			// Assign
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			// Act
			var types = AcspTypesFinder.FindTypesDerivedFrom<Controller>();

			// Assert

			Assert.AreEqual(2, types.Count);
			Assert.AreEqual("AcspNet.Tests.TestEntities.TestController2", types[0].FullName);
			Assert.AreEqual("AcspNet.Tests.TestEntities.TestController1", types[1].FullName);
		}

		[Test]
		public void FindTypesDerivedFrom_TestBootstrapper_NullReturned()
		{
			// Act
			var types = AcspTypesFinder.FindTypesDerivedFrom<TestBootstrapper>();

			// Assert
			Assert.AreEqual(0, types.Count);
		}
	}
}