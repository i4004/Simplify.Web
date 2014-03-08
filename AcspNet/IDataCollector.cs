using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Represents web-site master page data collector
	/// </summary>
	public interface IDataCollector : IHideObjectMembers
	{
		/// <summary>
		/// Gets the data collector items which will be inserted into master template file.
		/// </summary>
		IDictionary<string, string> Items { get; }

		/// <summary>
		///  Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name in master template file</param>
		/// <param name="value">Value to set</param>
		/// <returns></returns>
		void Add(string variableName, string value);

		/// <summary>
		/// Set template main content variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="value">Value to set</param>
		/// <returns></returns>
		void Add(string value);

		/// <summary>
		/// Set template title variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="value">Value to set</param>
		/// <returns></returns>
		void AddTitle(string value);

		/// <summary>
		/// Set template variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <param name="variableName">Variable name in master template file</param>
		/// <returns></returns>
		void AddSt(string variableName, string stringTableKey);

		/// <summary>
		/// Set template main content variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <returns></returns>
		void AddSt(string stringTableKey);

		/// <summary>
		/// Set template title variable value from StringTable (all occurrences will be replaced)
		/// </summary>
		/// <param name="stringTableKey">StringTable key</param>
		/// <returns></returns>
		void AddTitleSt(string stringTableKey);

		/// <summary>
		/// Checking if some variable data is already exist in a data collector
		/// </summary>
		/// <param name="variableName">Variable name</param>
		bool IsDataExist(string variableName);
	}
}