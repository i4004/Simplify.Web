using System;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.Web.Owin;

namespace Simplify.Web.Tests.Owin
{
	[TestFixture]
	public class OwinRequestHelperTests
	{
		[Test]
		public void GetIfModifiedSinceTime_Exists_Parsed()
		{
			// Assign

			var time = new DateTime(2016, 03, 04);
			var headers = new Mock<IHeaderDictionary>();
			headers.SetupGet(x => x[It.Is<string>(p => p == "If-Modified-Since")]).Returns(time.ToString("r"));
			headers.Setup(x => x.ContainsKey(It.Is<string>(p => p == "If-Modified-Since"))).Returns(true);

			// Act
			var result = OwinRequestHelper.GetIfModifiedSinceTime(headers.Object);

			// Assert

			Assert.IsNotNull(result);
			Assert.AreEqual(time, result);
		}

		[Test]
		public void GetIfModifiedSinceTime_NoExists_Null()
		{
			// Assign

			var headers = new Mock<IHeaderDictionary>();
			headers.Setup(x => x.ContainsKey(It.Is<string>(p => p == "If-Modified-Since"))).Returns(false);

			// Act
			var result = OwinRequestHelper.GetIfModifiedSinceTime(headers.Object);

			// Assert

			Assert.IsNull(result);
		}

		[Test]
		public void IsNoCacheRequested_NullHeader_False()
		{
			// Act
			var result = OwinRequestHelper.IsNoCacheRequested(null);

			// Assert

			Assert.IsFalse(result);
		}

		[Test]
		public void IsNoCacheRequested_ContainsNoCache_True()
		{
			// Act
			var result = OwinRequestHelper.IsNoCacheRequested("no-cache");

			// Assert

			Assert.IsTrue(result);
		}
	}
}