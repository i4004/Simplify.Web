namespace AcspNet.Modules
{
	/// <summary>
	/// Represent site enviroment properties
	/// </summary>
	public interface IEnvironment : IHideObjectMembers
	{
		/// <summary>
		/// Site current templates directory relative path
		/// </summary>
		string TemplatesPath { get; set; }

		/// <summary>
		/// Site current templates directory physical path
		/// </summary>
		string TemplatesPhysicalPath { get; }

		/// <summary>
		/// Site current style
		/// </summary>
		string SiteStyle { get; set; }

		/// <summary>
		/// Site current data directory relative path
		/// </summary>
		string DataPath { get; set; }

		/// <summary>
		/// Site templates memory cache status
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		bool TemplatesMemoryCache { get; set; }

		/// <summary>
		/// Gets or sets the current master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		string MasterTemplateFileName { get; set; }

		/// <summary>
		/// Gets or sets the current master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		string MainContentVariableName { get; set; }

		/// <summary>
		/// Gets or sets the current master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		string TitleVariableName { get; set; }
	}
}