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
		public void Bind_NormalData_Binded()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "Foo"),
				new KeyValuePair<string, string>("Prop2", "15"),
				new KeyValuePair<string, string>("Prop3", "on")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModel>(coll);

			// Assert

			Assert.AreEqual("Foo", obj.Prop1);
			Assert.AreEqual(15, obj.Prop2);
			Assert.IsTrue(obj.Prop3);
		}

		[Test]
		public void Bind_RequiredIsAbsent_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop2", "15"),
				new KeyValuePair<string, string>("Prop3", "on")
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModel>(coll));
		}

		[Test]
		public void Bind_DataTypeMismatch_0()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "Foo"),
				new KeyValuePair<string, string>("Prop2", "test"),
				new KeyValuePair<string, string>("Prop3", "on")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModel>(coll);

			// Assert

			Assert.AreEqual("Foo", obj.Prop1);
			Assert.AreEqual(0, obj.Prop2);
			Assert.IsTrue(obj.Prop3);
		}

		[Test]
		public void Bind_BoolAsTrue_Parsed()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "Foo"),
				new KeyValuePair<string, string>("Prop2", "test"),
				new KeyValuePair<string, string>("Prop3", "trUe")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModel>(coll);

			// Assert

			Assert.IsTrue(obj.Prop3);
		}

		[Test]
		public void Bind_BoolAsFalse_Parsed()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "Foo"),
				new KeyValuePair<string, string>("Prop2", "test"),
				new KeyValuePair<string, string>("Prop3", "FalsE")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModel>(coll);

			// Assert

			Assert.IsFalse(obj.Prop3);
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
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModel2>(coll));
		}

		[Test]
		public void Bind_UnrecognizedPropertyType_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "3"),
				new KeyValuePair<string, string>("Prop2", "asd")
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModel2>(coll));
		}

		[Test]
		public void Bind_MinMaxBelowValue_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "a")
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModel3>(coll));
		}

		[Test]
		public void Bind_MinMaxAbowValue_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "example")
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModel3>(coll));
		}

		[Test]
		public void Bind_RegexpFalse_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "test"),
				new KeyValuePair<string, string>("Prop2", "asFd6"),
				new KeyValuePair<string, string>("Prop3", "test@test.test")
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModel3>(coll));
		}

		[Test]
		public void Bind_InvalidEmail_ExceptionThrown()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "test"),
				new KeyValuePair<string, string>("Prop2", "asDd"),
				new KeyValuePair<string, string>("Prop3", "asd@.")
			};

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => ListModelBinder.Bind<TestModel3>(coll));
		}

		[Test]
		public void Bind_ValidMinMaxRegexEmail_ParsedCorrectly()
		{
			// Assign

			var coll = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Prop1", "test"),
				new KeyValuePair<string, string>("Prop2", "asDd"),
				new KeyValuePair<string, string>("Prop3", "test@test.test")
			};

			// Act
			var obj = ListModelBinder.Bind<TestModel3>(coll);

			// Act & Assert
			Assert.AreEqual("test", obj.Prop1);
			Assert.AreEqual("asDd", obj.Prop2);
			Assert.AreEqual("test@test.test", obj.Prop3);
		}
	}
}