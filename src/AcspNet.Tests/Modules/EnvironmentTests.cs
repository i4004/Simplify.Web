using AcspNet.Modules;
using NUnit.Framework;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	public class EnvironmentTests
	{
		AcspNetSettings _settings;
		
		[SetUp]
		public void Initialize()
		{
			_settings = new AcspNetSettings();		
		}

		[Test]
		public void Constructor_DefaultParameters_PropertiesSetCorrectly()
		{
			// Act
			var env = new Environment("C:/Test", _settings);

			// Assert

			Assert.AreEqual("Main", env.SiteStyle);
			Assert.AreEqual("Templates", env.TemplatesPath);
			Assert.AreEqual("Index.tpl", env.MasterTemplateFileName);
			Assert.AreEqual("Templates", env.TemplatesPath);
			Assert.AreEqual("C:/Test/Templates/", env.TemplatesPhysicalPath);
			Assert.AreEqual("App_Data", env.DataPath);
			Assert.AreEqual("C:/Test/App_Data/", env.DataPhysicalPath);
		}

		[Test]
		public void Constructor_BackslashPath_BackslashAdded()
		{
			// Act
			var env = new Environment(@"C:\Test\", _settings);

			// Assert
			Assert.AreEqual("C:/Test/Templates/", env.TemplatesPhysicalPath);
		}
	}
}