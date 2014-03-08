using AcspNet.Tests.AcspApplicationTestData.Extensions.Executable;
using AcspNet.Tests.AcspApplicationTestData.Extensions.Library;

namespace AcspNet.Tests.AcspApplicationTestData
{
	[LoadIndividualExtensions(typeof(TestExecExtension1), typeof(TestExecExtension2), typeof(TestLibraryExtension1), typeof(TestLibraryExtension2))]
	public class Default
	{	 
	}
}