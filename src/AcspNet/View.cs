namespace AcspNet
{
	/// <summary>
	/// AcspNet view base class 
	/// </summary>
	public abstract class View : ModulesAccessor
	{
		/// <summary>
		/// Gets the current language.
		/// </summary>
		/// <value>
		/// The current language.
		/// </value>
		public virtual string Language { get; internal set; }
	}
}