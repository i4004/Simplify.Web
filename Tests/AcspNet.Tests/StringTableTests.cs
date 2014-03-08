using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class StringTableTests
	{
		[Test]
		public void Constructor_NullsPassed_ArgumentNullExceptionsThrown()
		{
			Assert.Throws<ArgumentNullException>(() => new StringTable(null));
		}

		[Test]
		public void Constructor_NoStringTable_NoItemsLoaded()
		{
			ExtensionsDataLoader.FileSystem = new MockFileSystem(null, "C:/WebSites/FooSite");;

			var st = new StringTable(new ExtensionsDataLoader("Test", "Test", "Test", "Test"));
			Assert.AreEqual(0, st.Items.Count);
		}

		[Test]
		public void Constructor_StringTables_ItemsLoadedAndGetCorrectly()
		{
			var files = new Dictionary<string, MockFileData>();

			files.Add("ExtensionsData/StringTable.en.xml",
				"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /><item name=\"InfoTitle\" value=\"Information!\" /><item name=\"FooEnumFooItem1\" value=\"Foo item text\" /><item name=\"HtmlListDefaultItemLabel\" value=\"Default label\" /><item name=\"NotifyPageDataError\" value=\"Page data error!\" /></items>");
			files.Add("ExtensionsData/StringTable.ru.xml",
				"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок сайта!\" /></items>");

			ExtensionsDataLoader.FileSystem = new MockFileSystem(files, "C:/WebSites/FooSite");

			var st = new StringTable(new ExtensionsDataLoader("ExtensionsData", "C:/WebSites/FooSite", "ru", "en"));

			Assert.AreEqual(5, st.Items.Count);
			Assert.AreEqual("Information!", st["InfoTitle"]);
			Assert.AreEqual("Заголовок сайта!", st.Items["SiteTitle"]);
			Assert.AreEqual("Foo item text", st.GetAssociatedValue(FooEnum.FooItem1));
		}
	}

	public enum FooEnum
	{
		FooItem1,
		FooItem2
	}
}
