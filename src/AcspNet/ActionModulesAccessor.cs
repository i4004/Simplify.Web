using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Action modules accessor base class
	/// </summary>
	public class ActionModulesAccessor : ModulesAccessor
	{
		/// <summary>
		/// Current AcspNet context
		/// </summary>
		public virtual IAcspNetContext Context { get; internal set; }

		/// <summary>
		/// Gets the data collector.
		/// </summary>
		/// <value>
		/// The data collector.
		/// </value>
		public virtual IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Gets the redirector.
		/// </summary>
		/// <value>
		/// The redirector.
		/// </value>
		public virtual IRedirector Redirector { get; internal set; }
		
		/// <summary>
		/// Gets the language manager.
		/// </summary>
		/// <value>
		/// The language manager.
		/// </value>
		public virtual ILanguageManager LanguageManager { get; internal set; }

		/// <summary>
		/// Text files reader.
		/// </summary>
		public virtual IFileReader FileReader { get; internal set; }
	}
}