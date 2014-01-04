using AcspNet.Authentication;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("extensionsProtectorTest")]
	[Protection(Protection.User)]
	public class ExtensionsProtectorTest : ExecExtension
	{
		 
	}
}