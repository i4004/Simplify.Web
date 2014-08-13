namespace AcspNet
{
	/// <summary>
	/// Represent view
	/// </summary>
	public interface IView : IModulesAccessor
	{
		/// <summary>
		/// Gets the current language.
		/// </summary>
		/// <value>
		/// The current language.
		/// </value>
		string Language { get; }
	}
}