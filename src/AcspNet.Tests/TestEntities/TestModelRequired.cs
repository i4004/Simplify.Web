using AcspNet.ModelBinding;
using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelRequired
	{
		[Required]
		public string Prop1 { get; set; }
	}
}