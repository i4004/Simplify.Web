using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelWithExcludedProperty
	{
		[Exclude]
		public string Prop1 { get; set; }		 
	}
}