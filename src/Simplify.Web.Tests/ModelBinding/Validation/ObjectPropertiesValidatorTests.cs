using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.ModelBinding;
using Simplify.Web.ModelBinding.Validation;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.ModelBinding.Validation
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
			Assert.Throws<ModelValidationException>(() => _validator.Validate(model));
		}

		[Test]
		public void Validate_RequiredListFieldIsNull_ExceptionThrown()
		{
			// Assign
			var model = new TestModelRequired { Prop1 = "test" };

			// Act & Assert
			Assert.Throws<ModelValidationException>(() => _validator.Validate(model));
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