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
		private RequestCookieCollection _requestCookies;
		private ResponseCookieCollection _responseCookies;

		private Mock<IHeaderDictionary> _headerDictionary;
			
		[SetUp]
		public void Initialize()
		{
			_headerDictionary = new Mock<IHeaderDictionary>();
			_requestCookies = new RequestCookieCollection(new Dictionary<string, string>());
			_responseCookies = new ResponseCookieCollection(_headerDictionary.Object);

			_languageManager = new LanguageManager("ru", _requestCookies, _responseCookies);
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
			_requestCookies = new RequestCookieCollection(cookies);

			// Act
			_languageManager = new LanguageManager("en", _requestCookies, _responseCookies);

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

			_languageManager = new LanguageManager("en", _requestCookies, _responseCookies);
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