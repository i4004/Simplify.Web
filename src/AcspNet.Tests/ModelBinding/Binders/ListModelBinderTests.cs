using System;
using System.Collections.Generic;
using AcspNet.ModelBinding;
using AcspNet.ModelBinding.Binders;
using AcspNet.Tests.TestEntities;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding.Binders
{
	[TestFixture]
	public class ListModelBinderTests
	{
		[Test]
		public void Bind_RequiredIsAbsent_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>();

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModelRequired>(coll));
		}

		[Test]
		public void Bind_RequiredFieldDataTypeMismatch_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "test")
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModelUndefinedType>(coll));
		}

		[Test]
		public void Bind_Numbers_ParsedCorrectly()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "1"),
				new KeyValuePair<string, string>("Prop2", "2"),
				new KeyValuePair<string, string>("Prop3", "3"),
				new KeyValuePair<string, string>("Prop4", "4")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModelNumbers>(coll);

			// Act & Assert
			Assert.AreEqual(1, obj.Prop1);
			Assert.AreEqual(2, obj.Prop2);
			Assert.AreEqual(3, obj.Prop3);
			Assert.AreEqual((decimal?)4, obj.Prop4);
		}

		[Test]
		public void Bind_Bool_ParsedCorrectly()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "on"),
				new KeyValuePair<string, string>("Prop2", "False")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModelBool>(coll);

			// Act & Assert
			Assert.IsTrue(obj.Prop1);
			Assert.IsFalse(obj.Prop2 != null && obj.Prop2.Value);
		}

		[Test]
		public void Bind_String_ParsedCorrectly()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "test"),
				new KeyValuePair<string, string>("Prop2", "asDd"),
				new KeyValuePair<string, string>("Prop3", "test@test.test")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModelString>(coll);

			// Act & Assert
			Assert.AreEqual("test", obj.Prop1);
			Assert.AreEqual("asDd", obj.Prop2);
			Assert.AreEqual("test@test.test", obj.Prop3);
		}

		[Test]
		public void Bind_DateTimeNormal_Binded()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "15.03.2014"),
				new KeyValuePair<string, string>("Prop2", "16.03.2014")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModelDateTime>(coll);

			// Assert

			Assert.AreEqual(new DateTime(2014, 03, 15), obj.Prop1);
			Assert.AreEqual(new DateTime(2014, 03, 16), obj.Prop2);
		}
	}
}