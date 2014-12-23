using System;
using System.Collections.Generic;
using AcspNet.ModelBinding;
using AcspNet.ModelBinding.Binders;
using AcspNet.Tests.TestEntities;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding.Binders
{
	[TestFixture]
	public class ListModelParserTests
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
		public void Parse_RequiredFieldDataTypeMismatch_ExceptionThrown()
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
		public void Parse_Numbers_ParsedCorrectly()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"1"}),
				new KeyValuePair<string, string[]>("Prop2", new []{"2"}),
				new KeyValuePair<string, string[]>("Prop3", new []{"3"}),
				new KeyValuePair<string, string[]>("Prop4", new []{"4"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelNumbers>(coll);

			// Act & Assert
			Assert.AreEqual(1, obj.Prop1);
			Assert.AreEqual(2, obj.Prop2);
			Assert.AreEqual(3, obj.Prop3);
			Assert.AreEqual((decimal?)4, obj.Prop4);
		}

		[Test]
		public void Parse_Bool_ParsedCorrectly()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"on"}),
				new KeyValuePair<string, string[]>("Prop2", new []{"False"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelBool>(coll);

			// Act & Assert
			Assert.IsTrue(obj.Prop1);
			Assert.IsFalse(obj.Prop2 != null && obj.Prop2.Value);
		}

		[Test]
		public void Parse_String_ParsedCorrectly()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"test"}),
				new KeyValuePair<string, string[]>("Prop2", new []{"asDd"}),
				new KeyValuePair<string, string[]>("Prop3", new []{"test@test.test"})
			};

			// Act
			var obj = ListToModelParser.Parse<TestModelString>(coll);

			// Act & Assert
			Assert.AreEqual("test", obj.Prop1);
			Assert.AreEqual("asDd", obj.Prop2);
			Assert.AreEqual("test@test.test", obj.Prop3);
		}

		[Test]
		public void Parse_DateTimeNormal_Binded()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string[]>>
			{
				new KeyValuePair<string, string[]>("Prop1", new []{"15.03.2014"}),
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