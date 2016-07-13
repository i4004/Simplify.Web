using System;

namespace Simplify.Web.Util
{
	public static class TimeOperations
	{
		public static DateTime TrimMilliseconds(DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
		}
	}
}