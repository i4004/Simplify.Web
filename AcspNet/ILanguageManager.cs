namespace AcspNet
{
	public interface ILanguageManager : IHideObjectMembers
	{
		/// <summary>
		/// Site current language, for example: "en", "ru", "de" etc.
		/// </summary>
		string Language { get; }
	}
}