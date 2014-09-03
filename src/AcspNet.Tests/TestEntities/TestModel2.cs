using AcspNet.ModelBinding;

namespace AcspNet.Tests.TestEntities
{
	public class TestModel2
	{
		[Required]
		public int Prop1 { get; set; }

		public TestModel2 Prop2 { get; set; }
	}
}