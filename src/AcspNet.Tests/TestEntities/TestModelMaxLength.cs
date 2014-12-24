using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelMaxLength
	{
		[MaxLength(2)]
		public string Prop1 { get; set; }		 
	}
}