using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelMinLength
	{
		[MinLength(2)]
		public string Prop1 { get; set; }		 
	}
}