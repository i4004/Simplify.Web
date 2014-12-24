using System;
using System.Collections.Generic;
using AcspNet.ModelBinding;
using AcspNet.ModelBinding.Binders.Parsers;
using AcspNet.Tests.TestEntities;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding.Binders.Parsers
{
	[TestFixture]
	public class ListToModelParserTests
	{
		[Test]
		public void Parse_DefaultKeyValuePair_Null()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				default(KeyValuePair<string, string[]>)
			};

			// Act
			var model = ListToModelParser.Parse<TestModelUndefinedType>(coll);

			// Assert
			Assert.IsNull(model.Prop1);
		}

		[Test]
		public void Parse_EmptyArray_Null()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new string[0])
			};

			// Act
			var model = ListToModelParser.Parse<TestModelUndefinedType>(coll);

			// Assert
			Assert.IsNull(model.Prop1);
		}

		[Test]
		public void Parse_DataTypeMismatch_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"test"})
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListToModelParser.Parse<TestModelUndefinedType>(coll));
		}

		[Test]
		public void Parse_DateTimeNormal_Binded()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"15--2014--03"}),
				new KeyValuePair<string, string[]>("Prop2", new []{"16.03.2014"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelDateTime>(coll);

			// Assert

			Assert.AreEqual(new DateTime(2014, 03, 15), obj.Prop1);
			Assert.AreEqual(new DateTime(2014, 03, 16), obj.Prop2);
		}

		[Test]
		public void Parse_StringArray_Parsed()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"asd", "qwe"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelStringsList>(coll);

			// Assert

			Assert.AreEqual("asd", obj.Prop1[0]);
			Assert.AreEqual("qwe", obj.Prop1[1]);
		}
	}
}