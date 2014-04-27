namespace AcspNet.Modules
{
	/// <summary>
	/// Site environment properties, initialized from <see cref="IAcspSettings" />
	/// </summary>
	public sealed class Environment : IEnvironment
	{
		private readonly string _sitePhysicalPath;

		internal Environment(string sitePhysicalPath, IAcspSettings settings)
		{
			_sitePhysicalPath = sitePhysicalPath;

			TemplatesPath = settings.DefaultTemplatesPath;
			SiteStyle = settings.DefaultStyle;
			DataPath = settings.DefaultDataPath;
			TemplatesMemoryCache = settings.TemplatesMemoryCache;
			MasterTemplateFileName = settings.DefaultMasterTemplateFileName;
			MainContentVariableName = settings.DefaultMainContentVariableName;
			TitleVariableName = settings.DefaultTitleVariableName;
		}

		/// <summary>
		/// Site current templates directory relative path
		/// </summary>
		public string TemplatesPath { get; set; }

		/// <summary>
		/// Site current templates directory physical path
		/// </summary>
		public string TemplatesPhysicalPath
		{
			get
			{
				return _sitePhysicalPath + "/" + TemplatesPath;
			}
		}

		/// <summary>
		/// Site current style
		/// </summary>
		public string SiteStyle { get; set; }

		/// <summary>
		/// Site current data directory relative path
		/// </summary>
		public string DataPath { get; set; }

		/// <summary>
		/// Site templates memory cache status
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		public bool TemplatesMemoryCache { get; set; }

		/// <summary>
		/// Gets or sets the current master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		public string MasterTemplateFileName { get; set; }

		/// <summary>
		/// Gets or sets the current master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		public string MainContentVariableName { get; set; }

		/// <summary>
		/// Gets or sets the current master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		public string TitleVariableName { get; set; }
	}
}
