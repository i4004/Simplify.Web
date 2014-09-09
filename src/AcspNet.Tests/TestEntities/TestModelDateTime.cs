using System;
using AcspNet.ModelBinding;
using AcspNet.ModelBinding.Attributes;

namespace AcspNet.Tests.TestEntities
{
	public class TestModelDateTime
	{
		[DateTimeFormat("dd.MM.yyyy")]
		public DateTime? Prop1 { get; set; }

		[DateTimeFormat("dd.MM.yyyy")]
		public DateTime Prop2 { get; set; }
	}
}