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
			AcspTypesFinder.CleanLoadedTypesAndAssenbliesInfo();

			// Act
			var type = AcspTypesFinder.FindTypeDerivedFrom<BaseAcspNetBootstrapper>();

			// Assert
			Assert.AreEqual("AcspNet.Tests.TestEntities.TestBootstrapper", type.FullName);
		}

		[Test]
		public void FindTypeDerivedFrom_NoDerivedTypes_NullReturned()
		{
			// Act
			var type = AcspTypesFinder.FindTypeDerivedFrom<TestBootstrapper>();

			// Assert
			Assert.IsNull(type);
		}

		[Test]
		public void FindTypesDerivedFrom_TypeHave3TypesDerived_3TestControllersReturned()
		{
			// Assign

			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");
			AcspTypesFinder.ExcludedAssembliesPrefixes.Add("DynamicProxyGenAssembly2");
			AcspTypesFinder.CleanLoadedTypesAndAssenbliesInfo();

			// Act
			var types = AcspTypesFinder.FindTypesDerivedFrom<Controller>();
			var types2 = AcspTypesFinder.FindTypesDerivedFrom<AsyncController>();

			// Assert

			Assert.AreEqual(2, types.Count);
			Assert.AreEqual(1, types2.Count);
			Assert.AreEqual("AcspNet.Tests.TestEntities.TestController1", types[0].FullName);
			Assert.AreEqual("AcspNet.Tests.TestEntities.TestController3", types[1].FullName);
			Assert.AreEqual("AcspNet.Tests.TestEntities.TestController2", types2[0].FullName);
		}

		[Test]
		public void FindTypesDerivedFrom_NoDerivedTypes_NullReturned()
		{
			// Act
			var types = AcspTypesFinder.FindTypesDerivedFrom<TestBootstrapper>();

			// Assert
			Assert.AreEqual(0, types.Count);
		}
	}
}