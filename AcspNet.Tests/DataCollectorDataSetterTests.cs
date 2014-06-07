using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using AcspNet.Modules;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class DataCollectorDataSetterTests
	{
		private IFileReader _dataLoader;
		private IDataCollector _dataCollector;

		[SetUp]
		public void SetUpFileSystem()
		{
			var files = new Dictionary<string, MockFileData>
			{
				{
					"ExtensionsData/StringTable.en.xml",
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"
				}
			};

			FileReader.FileSystem = new MockFileSystem(files, "C:/WebSites/FooSite");

			_dataLoader = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "en");
			_dataCollector = new DataCollector("Test", "Title", new StringTable(_dataLoader));
		}

		[Test]
		public void SetSiteTitle_DefaultPage_DataSet()
		{
			// Act

			var dcSetter = new DataCollectorDataSetter(_dataCollector);
			dcSetter.SetSiteTitleFromStringTable("", "");

			// Assert
			Assert.AreEqual("Your site title!", _dataCollector["Title"]);
		}

		[Test]
		public void SetSiteTitle_SomePage_DataSet()
		{
			// Arrange
			_dataCollector.AddTitle("Test!");

			// Act

			var dcSetter = new DataCollectorDataSetter(_dataCollector);
			dcSetter.SetSiteTitleFromStringTable("", "");

			// Assert
			Assert.AreEqual("Test! - Your site title!", _dataCollector["Title"]);
		}

		[Test]
		public void SetEnviromentVariables_ValuesSetCorrectly()
		{
			// Arrange

			var env = new Mock<IEnvironment>();
			env.SetupGet(x => x.TemplatesPath).Returns("Path");
			env.SetupGet(x => x.SiteStyle).Returns("Style");

			// Act

			var dcSetter = new DataCollectorDataSetter(_dataCollector);
			dcSetter.SetEnvironmentVariables(env.Object);

			// Assert

			Assert.AreEqual("Path", _dataCollector[DataCollectorDataSetter.SiteVariableNameTemplatesPath]);
			Assert.AreEqual("Style", _dataCollector[DataCollectorDataSetter.SiteVariableNameCurrentStyle]);
		}

		[Test]
		public void SetLanguageVariables_CorrectLanguge_ValuesSetCorrectly()
		{
			// Act

			var dcSetter = new DataCollectorDataSetter(_dataCollector);
			dcSetter.SetLanguageVariables("en");

			// Assert

			Assert.AreEqual("en", _dataCollector[DataCollectorDataSetter.SiteVariableNameCurrentLanguage]);
			Assert.AreEqual(".en", _dataCollector[DataCollectorDataSetter.SiteVariableNameCurrentLanguageExtension]);
		}

		[Test]
		public void SetLanguageVariables_NullValue_ValuesSetCorrectly()
		{
			// Act

			var dcSetter = new DataCollectorDataSetter(_dataCollector);
			dcSetter.SetLanguageVariables(null);

			// Assert

			Assert.AreEqual(null, _dataCollector[DataCollectorDataSetter.SiteVariableNameCurrentLanguage]);
			Assert.AreEqual("", _dataCollector[DataCollectorDataSetter.SiteVariableNameCurrentLanguageExtension]);
		}

		[Test]
		public void SetContextVariables_ValuesSetCorrectly()
		{
			// Arrange

			var context = new Mock<IAcspContext>();
			context.SetupGet(x => x.SiteUrl).Returns("http://localhost/testsite");
			context.SetupGet(x => x.SiteVirtualPath).Returns("/testsite");

			// Act

			var dcSetter = new DataCollectorDataSetter(_dataCollector);
			dcSetter.SetContextVariables(context.Object);

			// Assert

			Assert.AreEqual("http://localhost/testsite", _dataCollector[DataCollectorDataSetter.SiteVariableNameSiteUrl]);
			Assert.AreEqual("/testsite", _dataCollector[DataCollectorDataSetter.SiteVariableNameSiteVirtualPath]);
		}

		[Test]
		public void SetExecutionTimeVariable_ValuesSetCorrectly()
		{
			// Act

			var dcSetter = new DataCollectorDataSetter(_dataCollector);
			dcSetter.SetExecutionTimeVariable(new TimeSpan(0, 0, 1, 15, 150));

			// Assert

			Assert.AreEqual("01:15:150", _dataCollector[DataCollectorDataSetter.SiteVariableNameSiteExecutionTime]);
		}
	}
}