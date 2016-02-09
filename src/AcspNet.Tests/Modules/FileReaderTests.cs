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

		private FileReader _fileReader;

		[SetUp]
		public void Initialize()
		{
			_languageManagerProvider = new Mock<ILanguageManagerProvider>();
			_languageManager = new Mock<ILanguageManager>();

			_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);
			_languageManager.SetupGet(x => x.Language).Returns("ru");

			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns("Dummy");

			// ReSharper disable once StringLiteralTypo
			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt")))
				.Returns("Тест");

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.en.txt")))
				.Returns(true);

			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt")))
				.Returns(true);

			FileReader.FileSystem = _fs.Object;

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();
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
			// Act
			_fileReader.Setup();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => _fileReader.GetFilePath(null, null));
			Assert.Throws<ArgumentNullException>(() => _fileReader.GetFilePath("File", null));
		}

		[Test]
		public void GetFilePath_CommonFile_PathIsCorrect()
		{
			// Assign
			_languageManager.SetupGet(x => x.Language).Returns("en");

			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/My.Project/Foo.en.xml", _fileReader.GetFilePath("My.Project/Foo.xml"));
		}

		[Test]
		public void GetFilePath_FileWithoutExtension_PathIsCorrect()
		{
			// Act & Assert
			Assert.AreEqual("C:/WebSites/FooSite/App_Data/MyProject/Foo.en", _fileReader.GetFilePath("MyProject/Foo", "en"));
		}

		[Test]
		public void LoadTextDocument_FileExist_Loaded()
		{
			// ReSharper disable once StringLiteralTypo

			// Act & Assert
			Assert.AreEqual("Тест", _fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_FileNotExistButDefaultFileExist_DefaultFile()
		{
			// Assign
			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt"))).Returns(false);

			// Act & Assert
			Assert.AreEqual("Dummy", _fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_FileNotExist_Null()
		{
			// Assign
			_fs.Setup(x => x.File.Exists(It.IsAny<string>())).Returns(false);

			// Act & Assert
			Assert.IsNull(_fileReader.LoadTextDocument("Foo.txt"));
		}

		[Test]
		public void LoadTextDocument_CacheEnabled_SecondTimeFromCache()
		{
			// Act

			_fileReader.LoadTextDocument("Foo.txt", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadTextDocument("Foo.txt", true);

			// Assert

			_fs.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
			Assert.AreEqual("Тест", result);
		}

		[Test]
		public void LoadTextDocument_CacheEnabledDefaultFile_DefaultFileFromCache()
		{
			// Assign
			_fs.Setup(x => x.File.Exists(It.Is<string>(d => d == "C:/WebSites/FooSite/App_Data/Foo.ru.txt"))).Returns(false);

			// Act

			_fileReader.LoadTextDocument("Foo.txt", true);

			_fileReader = new FileReader(DataPath, "en", _languageManagerProvider.Object);
			_fileReader.Setup();

			var result = _fileReader.LoadTextDocument("Foo.txt", true);

			// Assert

			_fs.Verify(x => x.File.ReadAllText(It.IsAny<string>()), Times.Once);
			Assert.AreEqual("Dummy", result);
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