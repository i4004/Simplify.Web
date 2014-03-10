namespace AcspNet.Tests.TestExtensions.Extensions.Executable
{
	[Action("stopExtensionsExecution")]
	[Priority(10)]
	public class StopExtensionsExecutionTest : ExecExtension
	{
		public override void Invoke()
		{
			ProcessorContoller.StopExecution();
		}
	}
}