using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class SiteTitleSetterTests
	{
		private IFileReader _dataLoader;

		[TestFixtureSetUp]
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
		}

		[Test]
		public void SetSiteTitle_DefaultPage_DataSet()
		{
			var dc = new DataCollector("Test", "Title", new StringTable(_dataLoader));

			SiteTitleSetter.SetSiteTitleFromStringTable(dc, "", "");

			Assert.AreEqual("Your site title!", dc["Title"]);
		}

		[Test]
		public void SetSiteTitle_SomePage_DataSet()
		{
			var dc = new DataCollector("Test", "Title", new StringTable(_dataLoader));

			dc.AddTitle("Test!");

			SiteTitleSetter.SetSiteTitleFromStringTable(dc, "", "");

			Assert.AreEqual("Test! - Your site title!", dc["Title"]);
		}
	}
}