using System;
using NUnit.Framework;
using Simplify.Web.Util;

namespace Simplify.Web.Tests.Util
{
	[TestFixture]
	public class TimeOperationsTests
	{
		[Test]
		public void TrimMilliseconds_DateTime_MillisecondsTrimmed()
		{
			// Act
			var result = TimeOperations.TrimMilliseconds(new DateTime(2015, 02, 03, 14, 22, 13, 456));

			// Assert
			Assert.AreEqual(0, result.Millisecond);
		}
	}
}