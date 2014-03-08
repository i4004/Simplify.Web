using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class PageBuilderTests
	{
		[Test]
		public void Constructor_NullsPassed_ArgumentNullExceptionsThrown()
		{
			Assert.Throws<ArgumentNullException>(() => new PageBuilder(null, false));
		}

		[Test]
		public void Build_CorrectData_BuilderCorrectly()
		{
			var b = new PageBuilder("Index.tpl", false);
			var data = new Dictionary<string, string>();

			data.Add("Var1", "data1");
			data.Add("Var2", "data2");

			var result = b.Buid(data);

			Assert.AreEqual("data1data2", result);
		}	
	}
}

//	//			var routeData = AcspNetTestingHelper.GetTestRouteData();
//	//			var fs = GetTestFileSystem();
//	//			var httpContext = GetTestHttpContext();
//	//			var userAssembly = GetTestUserAssembly();
//	//			Template.FileSystem = fs;

//	//			_httpResponse.Setup(x => x.Write(It.IsAny<string>()))
//	//				.Callback<string>(DataCollectorResponseWriteWriteDataIsCorrect);

//	//			var manager = new Manager(routeData, httpContext.Object, fs, AcspNetTestingHelper.GetTestHttpRuntime(), userAssembly);