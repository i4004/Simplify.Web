using System;

namespace AcspNet.Tests.TestExtensions.Extensions.Executable
{
	[Action("stopExtensionsExecution")]
	[Priority(11)]
	public class NotReachedException : ExecExtension
	{
		public override void Invoke()
		{
			throw new Exception("Unit test error: extension should not be called!");
		}
	}
}