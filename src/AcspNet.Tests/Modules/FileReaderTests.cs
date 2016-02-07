using System;
using System.IO.Abstractions;
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

		private readonly Mock<IFileSystem> _fs = new Mock<IFileSystem>();

		[SetUp]
		public void Initialize()
		{
			_languageManagerProvider = new Mock<ILanguageManagerProvider>();
			_languageManager = new Mock<ILanguageManager>();

			_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);
			_languageManager.SetupGet(x => x.Language).Returns("ru");

			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns("Dummy");

			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt")))
				.Returns("Тест");

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns(true);

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt")))
				.Returns(true);

			FileReader.FileSystem = _fs.Object;
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

			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);

			// Act
			fileReader.Setup();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => fileReader.GetFilePath(null, null));
			Assert.Throws<ArgumentNullException>(() => fileReader.GetFilePath("File", null));
		}

		[Test]
		public void GetFilePath_PathIsCorrect()
		{
			// Assign
			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/My.Project/Foo.en.xml", fileReader.GetFilePath("My.Project/Foo.xml", "en"));
		}

		[Test]
		public void LoadTextDocument_FileExist_DocumentLoaded()
		{
			// Assign

			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			fileReader.Setup();

			// Act & Assert
			Assert.AreEqual("Тест", fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_DefaultFileExist_DefaultDocumentLoaded()
		{
			// Assign

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt")))
				.Returns(false);

			var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			fileReader.Setup();

			// Act & Assert
			Assert.AreEqual("Dummy", fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_FileNotExist_NullReturned()
		{
			// Assign

			_fs.Setup(x => x.File.Exists(It.IsAny<string>()))
				.Returns(false);

			var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);
			fileReader.Setup();

			// Act & Assert
			Assert.IsNull(fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_CacheEnabled_SecondFromCache()
		{
			// Assign

			//FileReader.FileSystem = _fs.Object;

			//_languageManager.SetupGet(x => x.Language).Returns("ru");
			//var fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			//fileReader.Setup();

			//// Act

			//fileReader.LoadTextDocument("Foo.txt");

			//fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			//fileReader.Setup();

			//var result = fileReader.LoadTextDocument("Foo.txt");

			//// Assert

			//_fs.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
			//Assert.AreEqual("Dummy", result);
		}

		//[Test]
		//public void LoadXDocument_FileExist_DocumentLoaded()
		//{
		//	// Assign

		//	FileReader.FileSystem = _fs.Object;
		//	var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);

		//	// Act

		//	fileReader.Setup();
		//	var xmlDoc = fileReader.LoadXDocument("FooX");
		//	var root = xmlDoc.Root;

		//	// Assert

		//	Assert.IsNotNull(root);
		//	Assert.AreEqual("items", root.Name.ToString());
		//}

		//[Test]
		//public void LoadXDocument_FileNotExist_NullReturned()
		//{
		//	// Act

		//	FileReader.FileSystem = _fs.Object;
		//	var fileReader = new FileReader(DataPath, "ru", _languageManagerProvider.Object);

		//	// Act
		//	fileReader.Setup();

		//	// Act & Assert
		//	Assert.IsNull(fileReader.LoadXDocument("FooNot.xml"));
		//}
	}
}