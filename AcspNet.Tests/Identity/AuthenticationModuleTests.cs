using System;
using AcspNet.Modules.Identity;
using NUnit.Framework;

namespace AcspNet.Tests.Identity
{
	[TestFixture]
	public class AuthenticationModuleTests
	{
		[Test]
		public void SetAuthenticated_RegularData_DataSet()
		{
			var module = new Authentication(null, null, null);

			Assert.IsFalse(module.IsAuthenticatedAsUser);
			Assert.AreEqual(-1, module.AuthenticatedUserID);
			Assert.IsNull(module.AuthenticatedUserName);

			module.SetAuthenticated(2, "Test");

			Assert.IsTrue(module.IsAuthenticatedAsUser);
			Assert.AreEqual(2, module.AuthenticatedUserID);
			Assert.AreEqual("Test", module.AuthenticatedUserName);
		}

		[Test]
		public void SetAuthenticated_NullData_DataSet()
		{
			var module = new Authentication(null, null, null);

			Assert.Throws<ArgumentException>(() =>module.SetAuthenticated(-1));
		}

		[Test]
		public void Reset_RegularData_DataReset()
		{
			var module = new Authentication(null, null, null);

			module.SetAuthenticated(2, "Test");
			module.Reset();

			Assert.IsFalse(module.IsAuthenticatedAsUser);
			Assert.AreEqual(-1, module.AuthenticatedUserID);
			Assert.IsNull(module.AuthenticatedUserName);
		}
	}
}