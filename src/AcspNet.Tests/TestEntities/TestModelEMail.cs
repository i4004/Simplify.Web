using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelEMail
	{
		[EMail]
		public string Prop1 { get; set; }		 
	}
}