using System;

namespace AcspNet.ModelBinding.Attributes
{
	/// <summary>
	/// Sets date time format for parsing
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class DateTimeFormatAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeFormatAttribute"/> class.
		/// </summary>
		/// <param name="format">The format.</param>
		public DateTimeFormatAttribute(string format)
		{
			Format = format;
		}

		/// <summary>
		/// Gets or sets the format.
		/// </summary>
		/// <value>
		/// The format.
		/// </value>
		public string Format { get; set; }
	}
}