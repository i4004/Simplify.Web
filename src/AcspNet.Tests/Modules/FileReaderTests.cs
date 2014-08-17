using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using AcspNet.Modules;
using NUnit.Framework;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	[Category("Windows")]
	public class FileReaderTests
	{
		private const string DataPath = "C:/WebSites/FooSite/App_Data";

		[TestFixtureSetUp]
		public void SetUpFileSystem()
		{
			var files = new Dictionary<string, MockFileData>
			{
				{"App_Data/Foo.en.xml", "Dummy data"},
				{"App_Data/Foo.en", "Dummy data"},
				{
					"App_Data/FooX.en.xml",
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"
				}
			};
			
			FileReader.FileSystem = new MockFileSystem(files, "C:/WebSites/FooSite");
		}

		[Test]
		public void FileSystem_NullsPassed_ArgumentNullExceptionThrown()
		{
			// Assert
			Assert.Throws<ArgumentNullException>(() => FileReader.FileSystem = null);
		}

		[Test]
		public void GetFilePath_NullsPassed_ArgumentNullExceptionsThrown()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "en", "en");

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => fileReader.GetFilePath(null, null));
			Assert.Throws<ArgumentNullException>(() => fileReader.GetFilePath("File", null));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileExist_PathIsCorrect()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "en", "en");

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/Foo.en.xml", fileReader.GetFilePath("Foo.xml", "en"));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileNotExist_PathIsCorrect()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "en", "ru");

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/FooNot.en.xml", fileReader.GetFilePath("FooNot.xml", "ru"));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileExistWithoutExtension_PathIsCorrect()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "en", "en");

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/Foo.en", fileReader.GetFilePath("Foo", "en"));
		}

		[Test]
		public void GetFilePath_FileNotExist_PathIsCorrect()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "en", "ru");

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/FooNot.en.xml", fileReader.GetFilePath("FooNot.xml"));
		}

		[Test]
		public void GetFilePath_FileExist_PathIsCorrect()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "ru", "en");

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/Foo.en.xml", fileReader.GetFilePath("Foo.xml"));
		}

		[Test]
		public void LoadTextDocument_FileExist_DocumentLoaded()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "ru", "en");

			// Act & Assert
			Assert.AreEqual("Dummy data", fileReader.LoadTextDocument("Foo.xml"));
		}

		[Test]
		public void LoadTextDocument_FileNotExist_NullReturned()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "ru", "en");

			// Act & Assert
			Assert.IsNull(fileReader.LoadTextDocument("FooNot.xml"));
		}

		[Test]
		public void LoadXDocument_FileExist_DocumentLoaded()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "ru", "en");

			// Act

			var xmlDoc = fileReader.LoadXDocument("FooX");
			var root = xmlDoc.Root;

			// Assert

			Assert.IsNotNull(root);
			Assert.AreEqual("items", root.Name.ToString());
		}

		[Test]
		public void LoadXDocument_FileNotExist_NullReturned()
		{
			// Act
			var fileReader = new FileReader(DataPath, "ru", "en");

			// Act & Assert
			Assert.IsNull(fileReader.LoadXDocument("FooNot.xml"));
		}
	}
}