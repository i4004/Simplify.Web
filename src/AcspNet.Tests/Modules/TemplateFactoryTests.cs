using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using AcspNet.Modules;
using Moq;
using NUnit.Framework;
using Simplify.Templates;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	[Category("Windows")]
	public class TemplateFactoryTests
	{
		[TestFixtureSetUp]
		public void SetUpFileSystem()
		{
			var files = new Dictionary<string, MockFileData> { { "Templates/Foo.tpl", "Dummy data" } };

			Template.FileSystem = new MockFileSystem(files, "C:/WebSites/FooSite");
		}

		[Test]
		public void Load_NullFileName_ArgumentNullExceptionThrown()
		{
			// Assign
			var tf = new TemplateFactory("C:/WebSites/FooSite/Templates", "en", "en");

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => tf.Load(null));
		}

		[Test]
		public void Load_NoCache_TemplateLoadedCorrectly()
		{
			// Assign
			var tf = new TemplateFactory("C:/WebSites/FooSite/Templates", "en", "en");

			// Act
			var data = tf.Load("Foo.tpl");

			// Assert
			Assert.AreEqual("Dummy data", data.Get());
		}

		[Test]
		public void Load_NameWithoutTpl_TemplateLoadedCorrectly()
		{
			// Assign
			var tf = new TemplateFactory("C:/WebSites/FooSite/Templates", "en", "en");

			// Act
			var data = tf.Load("Foo");

			// Assert
			Assert.AreEqual("Dummy data", data.Get());
		}

		[Test]
		public void Load_WithCache_TemplateLoadedCorrectly()
		{
			// Assign
			var tf = new TemplateFactory("C:/WebSites/FooSite/Templates", "en", "en", true);

			// Act
			var data = tf.Load("Foo.tpl");

			// Asset
			Assert.AreEqual("Dummy data", data.Get());

			// Assign
			Template.FileSystem = new Mock<IFileSystem>().Object;

			// Act
			data = tf.Load("Foo.tpl");

			// Assert
			Assert.AreEqual("Dummy data", data.Get());
		}
	}
}