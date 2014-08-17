using System;
using System.Collections.Generic;
using AcspNet.Modules;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Modules
{
	[TestFixture]
	public class LanguageManagerTests
	{
		private LanguageManager _languageManager;
		private Mock<IAcspNetSettings> _settings;
		private Mock<IOwinContext> _context;
		private Mock<IHeaderDictionary> _headerDictionary;
			
		[SetUp]
		public void Initialize()
		{
			_settings = new Mock<IAcspNetSettings>();
			_context = new Mock<IOwinContext>();

			_settings.SetupGet(x => x.DefaultLanguage).Returns("ru");
			_headerDictionary = new Mock<IHeaderDictionary>();

			_context.SetupGet(x => x.Request.Cookies).Returns(new RequestCookieCollection(new Dictionary<string, string>()));
			_context.SetupGet(x => x.Response.Cookies).Returns(new ResponseCookieCollection(_headerDictionary.Object));

			_languageManager = new LanguageManager(_settings.Object, _context.Object);
		}

		[Test]
		public void Constructor_NoRequestCookieLanguage_DefaultLanguageSet()
		{
			// Assert
			Assert.AreEqual("ru", _languageManager.Language);
		}

		[Test]
		public void Constructor_HaveRequestCookie_CurrentLanguageSet()
		{
			// Assign

			var cookies = new Dictionary<string, string> {{LanguageManager.CookieLanguageFieldName, "ru"}};
			_context.SetupGet(x => x.Request.Cookies).Returns(new RequestCookieCollection(cookies));
			_settings.SetupGet(x => x.DefaultLanguage).Returns("en");

			// Act
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			// Assert
			Assert.AreEqual("ru", _languageManager.Language);
		}
		
		[Test]
		public void SetCookieLanguage_EmptyLanguageString_CorrectCookieCreated()
		{
			// Act
			Assert.Throws<ArgumentNullException>(() => _languageManager.SetCookieLanguage(null));
		}

		[Test]
		public void SetCookieLanguage_CorrectLanguage_CorrectCookieCreated()
		{
			// Assign

			_settings.SetupGet(x => x.DefaultLanguage).Returns("en");
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			_headerDictionary.Setup(x => x.AppendValues(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string[]>((key, values) =>
			{
				Assert.AreEqual("Set-Cookie", key);
				Assert.IsTrue(values[0].Contains("language=ru"));
			});

			// Act
			_languageManager.SetCookieLanguage("ru");
		}
	}
}