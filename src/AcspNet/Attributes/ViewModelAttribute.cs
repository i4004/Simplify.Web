using System;

namespace AcspNet.Attributes
{
	/// <summary>
	/// Indicates type of view model which should be created from request data to controller
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ViewModelAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the type of the view model.
		/// </summary>
		/// <value>
		/// The type of the view model.
		/// </value>
		public Type ViewModelType { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelAttribute"/> class.
		/// </summary>
		/// <param name="viewModelType">Type of the view model.</param>
		public ViewModelAttribute(Type viewModelType)
		{
			ViewModelType = viewModelType;
		}
	}
}