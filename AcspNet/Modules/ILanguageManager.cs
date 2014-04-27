namespace AcspNet.Modules
{
	/// <summary>
	/// Represent current language controller and information container
	/// </summary>
	public interface ILanguageManager : IHideObjectMembers
	{
		/// <summary>
		/// Site current language, for example: "en", "ru", "de" etc.
		/// </summary>
		string Language { get; }
	}
}