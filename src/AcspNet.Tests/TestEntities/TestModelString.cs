using AcspNet.ModelBinding;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelString
	{
		[MaxLength(5)]
		[MinLength(2)]
		public string Prop1 { get; set; }

		[Regex("^[a-zA-Z]+$")]
		public string Prop2 { get; set; }

		[EMail]
		public string Prop3 { get; set; }
	}
}