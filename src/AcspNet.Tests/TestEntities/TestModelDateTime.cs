using System;
using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelDateTime
	{
		[Format("dd--yyyy--MM")]
		public DateTime? Prop1 { get; set; }

		public DateTime Prop2 { get; set; }
	}
}