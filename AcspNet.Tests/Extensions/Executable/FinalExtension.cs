namespace AcspNet.Tests.Extensions.Executable
{
	[Action("foo2")]
	[Priority(10)]
	public class FinalExtension : ExecExtension
	{
		public override void Invoke()
		{
			Manager.StopExtensionsExecution();
		}
	}
}