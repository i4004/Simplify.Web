using System;
using AcspNet.Diagnostics;
using NUnit.Framework;

namespace AcspNet.Tests.Diagnostics
{
	[TestFixture]
	public class ExceptionInfoPageGeneratorTests
	{
		[Test]
		public void Generate_Null_Null()
		{
			Assert.IsNull(ExceptionInfoPageGenerator.Generate(null));
		}

		[Test]
		public void Generate_WithInnerException_HtmlPageText()
		{
			try
			{
				string text = null;
				text.IndexOf("test");
			}
			catch (Exception e)
			{
				try
				{
					throw new Exception("test 2", e);
				}
				catch (Exception ex)
				{
					Assert.IsTrue(ExceptionInfoPageGenerator.Generate(ex).Contains("html"));
				}
			}
		}

		[Test]
		public void Generate_ExceptionNoFrames_Null()
		{
			Assert.IsNull(ExceptionInfoPageGenerator.Generate(new Exception("test")));
		}
	}
}