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
	}
}