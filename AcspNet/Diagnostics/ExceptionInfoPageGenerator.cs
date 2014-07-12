using System;
using System.Diagnostics;
using System.Globalization;
using Simplify.Templates;

namespace AcspNet.Diagnostics
{
	/// <summary>
	/// Provides exception information HTML page generator
	/// </summary>
	public static class ExceptionInfoPageGenerator
	{
		/// <summary>
		/// Generates the HTML page with exception information
		/// </summary>
		/// <param name="e">Exception to get information from.</param>
		/// <returns></returns>
		public static string Generate(Exception e)
		{
			if (e == null)
				return null;

			var trace = new StackTrace(e, true);

			if (trace.FrameCount == 0)
				return null;

			var fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();

			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0 ? "" : String.Format("[{0}:{1}]", fileLineNumber, fileColumnNumber);
			var tpl = Template.FromManifest("Diagnostics.ExceptionInfoPage.html");

			tpl.Set("StackTrace", String.Format("{0} {1} : {2}{3}{4}{5}", positionPrefix, e.GetType(),
				e.Message, Environment.NewLine, trace, GetInnerExceptionData(1, e.InnerException)).Replace("\r\n", "<br />"));

			return tpl.Get();
		}

		private static string GetInnerExceptionData(int currentLevel, Exception e)
		{
			if (e == null)
				return null;

			var trace = new StackTrace(e, true);

			if (trace.FrameCount == 0)
				return null;

			var fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();
			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0 ? "" : String.Format("[{0}:{1}]", fileLineNumber, fileColumnNumber);
			var levelText = currentLevel > 1 ? " " + currentLevel.ToString(CultureInfo.InvariantCulture) : "";

			return String.Format("[Inner Exception{0}]{1} {2} : {3}{4}{5}{6}",
				levelText, positionPrefix, e.GetType(),
				e.Message, Environment.NewLine, trace,
				GetInnerExceptionData(currentLevel + 1, e.InnerException));
		}
	}
}