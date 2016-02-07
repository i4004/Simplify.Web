using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using AcspNet.Modules;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	public class FileReaderTests
	{
		private const string DataPath = "C:/WebSites/FooSite/App_Data/";

		private Mock<ILanguageManagerProvider> _languageManagerProvider;
		private Mock<ILanguageManager> _languageManager;

		private MockFileSystem _fs1;
		private readonly Mock<IFileSystem> _fs2 = new Mock<IFileSystem>();

		[TestFixtureSetUp]
		public void SetUpFileSystem()
		{
			var files = new Dictionary<string, MockFileData>
			{
				{"C:/WebSites/FooSite/App_Data/My.Project/Foo.en.xml", "Dummy data"},
				{"C:/WebSites/FooSite/App_Data/Foo.en", "Dummy data"},
				{
					"C:/WebSites/FooSite/App_Data/FooX.en.xml",
					"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"
				}
			};

			_fs1 = new MockFileSystem(files);
		}

		[SetUp]
		public void Initialize()
		{
			_languageManagerProvider = new Mock<ILanguageManagerProvider>();
			_languageManager = new Mock<ILanguageManager>();

			_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);
			_languageManager.SetupGet(x => x.Language).Returns("en");

			_fs2.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns("Dummy");

			_fs2.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns(true);
		}

		[Test]
		public void FileSystem_NullsPassed_ArgumentNullExceptionThrown()
		{
			// Assign
			FileReader.FileSystem = _fs1;

			// Assert
			Assert.Throws<ArgumentNullException>(() => FileReader.FileSystem = null);
		}

		[Test]
		public void GetFilePath_NullsPassed_ArgumentNullExceptionsThrown()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => fileReader.GetFilePath(null, null));
			Assert.Throws<ArgumentNullException>(() => fileReader.GetFilePath("File", null));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileExist_PathIsCorrect()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/My.Project/Foo.en.xml", fileReader.GetFilePath("My.Project/Foo.xml", "en"));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileNotExist_PathIsCorrect()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			_languageManager.SetupGet(x => x.Language).Returns("ru");
			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/FooNot.en.xml", fileReader.GetFilePath("FooNot.xml", "ru"));
		}

		[Test]
		public void GetFilePathWithExactLanguage_FileExistWithoutExtension_PathIsCorrect()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/Foo.en", fileReader.GetFilePath("Foo", "en"));
		}

		[Test]
		public void GetFilePath_FileNotExist_PathIsCorrect()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			_languageManager.SetupGet(x => x.Language).Returns("ru");
			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/FooNot.en.xml", fileReader.GetFilePath("FooNot.xml"));
		}

		[Test]
		public void GetFilePath_FileExist_PathIsCorrect()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/My.Project/Foo.en.xml", fileReader.GetFilePath("My.Project/Foo.xml"));
		}

		[Test]
		public void LoadTextDocument_FileExist_DocumentLoaded()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.AreEqual("Dummy data", fileReader.LoadTextDocument("My.Project/Foo.xml"));
		}

		[Test]
		public void LoadTextDocument_FileNotExist_NullReturned()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.IsNull(fileReader.LoadTextDocument("FooNot.xml"));
		}

		[Test]
		public void LoadXDocument_FileExist_DocumentLoaded()
		{
			// Assign

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);

			// Act

			fileReader.Setup();
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

			FileReader.FileSystem = _fs1;
			var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.IsNull(fileReader.LoadXDocument("FooNot.xml"));
		}

		[Test]
		public void LoadTextDocument_CacheEnabled_SecondFromCache()
		{
			// Assign

			FileReader.FileSystem = _fs2.Object;

			_languageManager.SetupGet(x => x.Language).Returns("ru");
			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			fileReader.Setup();

			// Act

			fileReader.LoadTextDocument("Foo.txt");

			fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			fileReader.Setup();

			var result = fileReader.LoadTextDocument("Foo.txt");

			// Assert

			_fs2.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
			Assert.AreEqual("Dummy", result);
		}
	}
}