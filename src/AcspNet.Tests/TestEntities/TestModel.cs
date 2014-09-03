using AcspNet.ModelBinding;

namespace AcspNet.Tests.TestEntities
{
	public class TestModel
	{
		[Required]
		public string Prop1 { get; set; }
		public int Prop2 { get; set; }
		public bool Prop3 { get; set; }
	}
}