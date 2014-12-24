using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelWithBindProperty
	{
		[BindProperty("Prop2")]
		public string Prop1 { get; set; }		 
	}
}