using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelWithExcludedProperty
	{
		[ExcludeAttribute]
		public string Prop1 { get; set; }		 
	}
}