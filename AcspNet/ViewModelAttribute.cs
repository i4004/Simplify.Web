using System;

namespace AcspNet
{
	/// <summary>
	/// Sets view model class type for controller
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ViewModelAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelAttribute"/> class.
		/// </summary>
		public ViewModelAttribute(Type viewModelType)
		{
			ViewModelType = viewModelType;
		}

		public Type ViewModelType { get; private set; }
	}
}