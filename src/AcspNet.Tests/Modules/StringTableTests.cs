
using System.Collections.Generic;
using System.Xml.Linq;
using AcspNet.Modules;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	public class StringTableTests
	{
		private StringTable _stringTable;
		private Mock<IFileReader> _fileReader;
		private string _defaultLanguage = "en";
		private string _currentLanguage = "ru";

		[SetUp]
		public void Initialize()
		{
			_fileReader = new Mock<IFileReader>();
		}

		[Test]
		public void Constructor_NoStringTable_NoItemsLoaded()
		{
			// Act
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);

			// Assert
			Assert.AreEqual(0, ((IDictionary<string, object>)_stringTable.Items).Count);
		}

		[Test]
		public void Constructor_StringTableFound_ItemsLoadedCorrectly()
		{
			// Assign
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

			// Act
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);

			// Assert
			Assert.AreEqual("Your site title!", _stringTable.Items.SiteTitle);
		}

		[Test]
		public void Constructor_CurrentLanguageEqualToDefaultLanguage_DefaultItemsNotLoaded()
		{
			// Assign

			_currentLanguage = "en";
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

			// Act
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);

			// Assert
			_fileReader.Verify(x => x.LoadXDocument(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Constructor_StringTableNotFound_DefaultLoaded()
		{
			_defaultLanguage = "ru";

			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>())).Returns((XDocument)null);
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.Is<string>(d => d == _defaultLanguage))).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /></items>"));

			// Act
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);

			// Assert
			Assert.AreEqual("Your site title!", _stringTable.Items.SiteTitle);
		}

		[Test]
		public void Constructor_StringTableWithMissingItems_MissingItemsLoadedFromDefaultStringTable()
		{
			_defaultLanguage = "ru";

			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"Item1\" value=\"Foo\" /></items>"));
			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>(), It.Is<string>(d => d == _defaultLanguage))).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"Item1\" value=\"FooDef\" /><item name=\"Item2\" value=\"BarDef\" /></items>"));

			// Act
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);

			// Assert

			Assert.AreEqual("Foo", _stringTable.Items.Item1);
			Assert.AreEqual("BarDef", _stringTable.Items.Item2);
		}

		[Test]
		public void GetAssociatedValue_EnumItems_GetCorrectly()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);
			
			// Act & Assert
			Assert.AreEqual("Foo", _stringTable.GetAssociatedValue(FooEnum.FooItem1));
			Assert.IsNull(_stringTable.GetAssociatedValue(FooEnum.FooItem2));
		}

		[Test]
		public void GetItem_ItemFound_Returned()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);

			// Act & Assert
			Assert.AreEqual("Foo", _stringTable.GetItem("FooEnum.FooItem1"));
		}

		[Test]
		public void GetItem_ItemNotFound_Null()
		{
			// Assign

			_fileReader.Setup(x => x.LoadXDocument(It.IsAny<string>())).Returns(XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"FooEnum.FooItem1\" value=\"Foo\" /></items>"));
			_stringTable = new StringTable(_defaultLanguage, _currentLanguage, _fileReader.Object);

			// Act & Assert
			Assert.IsNull(_stringTable.GetItem("Foo"));
		}
	}

	public enum FooEnum
	{
		FooItem1,
		FooItem2
	}
}