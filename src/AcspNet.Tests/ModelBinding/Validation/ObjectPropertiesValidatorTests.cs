using System.Collections.Generic;
using AcspNet.ModelBinding;
using AcspNet.ModelBinding.Validation;
using AcspNet.Tests.TestEntities;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding.Validation
{
	[TestFixture]
	public class ObjectPropertiesValidatorTests
	{
		private ObjectPropertiesValidator _validator;

		[SetUp]
		public void Initialize()
		{
			_validator = new ObjectPropertiesValidator();
		}

		[Test]
		public void Validate_RequiredFieldIsNull_ExceptionThrown()
		{
			// Assign
			var model = new TestModelRequired { Prop2 = new List<string>() };

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => _validator.Validate(model));
		}

		[Test]
		public void Validate_RequiredListFieldIsNull_ExceptionThrown()
		{
			// Assign
			var model = new TestModelRequired { Prop1 = "test" };

			// Act & Assert
			Assert.Throws<ModelBindingException>(() => _validator.Validate(model));
		}

		[Test]
		public void Validate_RequiredFieldsNormal_Ok()
		{
			// Assign
			var model = new TestModelRequired { Prop1 = "test", Prop2 = new List<string>() };

			// Act & Assert
			_validator.Validate(model);
		}
	}
}