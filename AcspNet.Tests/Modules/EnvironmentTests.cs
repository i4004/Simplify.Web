using AcspNet.Modules;
using NUnit.Framework;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	public class EnvironmentTests
	{
		[Test]
		public void Constructor_DefaultParamenters_PropertiesSetCorrecly()
		{
			// Assign
			var settings = new AcspNetSettings();

			// Act
			var env = new Environment("C:/Test", settings);

			// Assert

			Assert.AreEqual("Main", env.SiteStyle);
			Assert.AreEqual("Templates", env.TemplatesPath);
			Assert.AreEqual("App_Data", env.DataPath);
			Assert.AreEqual("MainContent", env.MainContentVariableName);
			Assert.AreEqual("Index.tpl", env.MasterTemplateFileName);
			Assert.IsFalse(env.TemplatesMemoryCache);
			Assert.AreEqual("Templates", env.TemplatesPath);
			Assert.AreEqual("C:/Test/Templates/", env.TemplatesPhysicalPath);
			Assert.AreEqual("Title", env.TitleVariableName);
		}
	}
}