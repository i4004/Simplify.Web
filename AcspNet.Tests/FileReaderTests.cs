using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class FileReaderTests
	{
		[TestFixtureSetUp]
		public void SetUpFileSystem()
		{
			var files = new Dictionary<string, MockFileData>
			{
				{"ExtensionsData/Foo.en.xml", "Dummy data"},
				{"ExtensionsData/Foo.en", "Dummy data"},
				{
					"ExtensionsData/FooX.en.xml",
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"
				}
			};


			FileReader.FileSystem = new MockFileSystem(files, "C:/WebSites/FooSite");
		}

		[Test]
		public void FileSystem_NullsPassed_ArgumentNullExceptionThrown()
		{
			Assert.Throws<ArgumentNullException>(() => FileReader.FileSystem = null);	
		}
		
		[Test]
		public void GetFilePath_NullsPassed_ArgumentNullExceptionsThrown()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "en");
			Assert.Throws<ArgumentNullException>(() => dl.GetFilePath(null, null));
			Assert.Throws<ArgumentNullException>(() => dl.GetFilePath("File", null));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileExist_PathIsCorrect()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "en");
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Foo.en.xml", dl.GetFilePath("Foo.xml", "en"));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileNotExist_PathIsCorrect()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "ru", "en");
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/FooNot.en.xml", dl.GetFilePath("FooNot.xml", "ru"));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileExistWithoutExtension_PathIsCorrect()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "en");
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Foo.en", dl.GetFilePath("Foo", "en"));
		}

		[Test]
		public void GetFilePath_FileNotExist_PathIsCorrect()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "ru", "en");
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/FooNot.en.xml", dl.GetFilePath("FooNot.xml"));
		}

		[Test]
		public void GetFilePath_FileExist_PathIsCorrect()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "ru");
			Assert.AreEqual("C:/WebSites/FooSite/ExtensionsData/Foo.en.xml", dl.GetFilePath("Foo.xml"));
		}

		[Test]
		public void LoadTextDocument_FileExist_DocumentLoaded()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "ru");
			Assert.AreEqual("Dummy data", dl.LoadTextDocument("Foo.xml"));
		}

		[Test]
		public void LoadTextDocument_FileNotExist_NullReturned()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "ru");
			Assert.IsNull(dl.LoadTextDocument("FooNot.xml"));
		}

		[Test]
		public void LoadXDocument_FileExist_DocumentLoaded()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "ru");

			var xmlDoc = dl.LoadXDocument("FooX.xml");
			var root = xmlDoc.Root;

			Assert.IsNotNull(root);
			Assert.AreEqual("items", root.Name.ToString());
		}

		[Test]
		public void LoadXDocument_FileNotExist_NullReturned()
		{
			var dl = new FileReader("ExtensionsData", "C:/WebSites/FooSite", "en", "ru");
			Assert.IsNull(dl.LoadXDocument("FooNot.xml"));
		}	
	}
}
