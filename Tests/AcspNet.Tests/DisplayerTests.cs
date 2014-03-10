using System;
using System.IO;
using System.Web;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class DisplayerTests
	{
		[Test]
		public void Display_SomeData_DataSent()
		{
			var httpCacheContext = new HttpContext(new HttpRequest("TestRequest", "http://localhost", ""), new HttpResponse(new StringWriter()));
			var httpResponse = new Mock<HttpResponseBase>();
			httpResponse.SetupGet(r => r.Cache).Returns(new HttpCachePolicyWrapper(httpCacheContext.Response.Cache));

			httpResponse.Setup(x => x.Write(It.IsAny<string>()))
								.Callback<string>(DataCollectorResponseWriteWriteDataIsCorrect);

			var d = new Displayer(httpResponse.Object);

			d.Display("Test data!");

			httpResponse.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));

			d.DisplayNoCache("Test data!");

			httpResponse.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(2));
		}
		
		public void DataCollectorResponseWriteWriteDataIsCorrect(string s)
		{
			Assert.AreEqual("Test data!", s);
		}
	}
}
