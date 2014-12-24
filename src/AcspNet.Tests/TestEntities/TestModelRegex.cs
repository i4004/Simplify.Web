using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelRegex
	{
		[Regex("^[a-zA-Z]+$")]
		public string Prop1 { get; set; }
	}
}