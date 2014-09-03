using System.Collections.Generic;
using AcspNet.ModelBinding;
using AcspNet.Tests.TestEntities;
using Microsoft.Owin;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding
{
	[TestFixture]
	public class FormDeserializerTests
	{
		private IFormCollection _form;

		[Test]
		public void Deserialize_NormalData_Deserialized()
		{
			// Assign

			var coll = new Dictionary<string, string[]>
			{
				{"Prop1", new[] {"Foo"}},
				{"Prop2", new[] {"15"}},
				{"Prop3", new[] {"on"}}
			};

			_form = new FormCollection(coll);

			// Act
			var obj = FormDeserializer.Deserialize<TestModel>(_form);

			// Assert

			Assert.AreEqual("Foo", obj.Prop1);
			Assert.AreEqual(15, obj.Prop2);
			Assert.IsTrue(obj.Prop3);
		}

		[Test]
		public void Deserialize_RequiredIsAbsent_ExceptionThrown()
		{
			// Assign

			var coll = new Dictionary<string, string[]>
			{
				{"Prop2", new[] {"15"}},
				{"Prop3", new[] {"on"}}
			};

			_form = new FormCollection(coll);

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => FormDeserializer.Deserialize<TestModel>(_form));
		}

		[Test]
		public void Deserialize_DataTypeMismatch_0()
		{
			// Assign

			var coll = new Dictionary<string, string[]>
			{
				{"Prop1", new[] {"Foo"}},
				{"Prop2", new[] {"test"}},
				{"Prop3", new[] {"on"}}
			};

			_form = new FormCollection(coll);

			// Act
			var obj = FormDeserializer.Deserialize<TestModel>(_form);

			// Assert

			Assert.AreEqual("Foo", obj.Prop1);
			Assert.AreEqual(0, obj.Prop2);
			Assert.IsTrue(obj.Prop3);
		}


		[Test]
		public void Deserialize_RequiredFieldDataTypeMismatch_ExceptionThrown()
		{
			// Assign

			var coll = new Dictionary<string, string[]>
			{
				{"Prop1", new[] {"test"}},
			};

			_form = new FormCollection(coll);

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => FormDeserializer.Deserialize<TestModel2>(_form));
		}

		[Test]
		public void Deserialize_UnrecognizedPropertyType_ExceptionThrown()
		{
			// Assign

			var coll = new Dictionary<string, string[]>
			{
				{"Prop1", new[] {"3"}},
				{"Prop2", new[] {"asd"}},
			};

			_form = new FormCollection(coll);

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => FormDeserializer.Deserialize<TestModel2>(_form));
		}
	}
}