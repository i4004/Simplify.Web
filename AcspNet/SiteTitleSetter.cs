namespace AcspNet
{
	/// <summary>
	/// Set site title
	/// </summary>
	public static class SiteTitleSetter
	{
		/// <summary>
		/// Sets the site title.
		/// </summary>
		/// <param name="dataCollector">The data collector.</param>
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		public static void SetSiteTitleFromStringTable(IDataCollector dataCollector, string action, string mode)
		{
			if (string.IsNullOrEmpty(action) && string.IsNullOrEmpty(mode)
				&& !dataCollector.IsDataExist(dataCollector.TitleVariableName))
				dataCollector.Add(dataCollector.TitleVariableName, dataCollector.StringTable["SiteTitle"]);
			else
				dataCollector.Add(dataCollector.TitleVariableName, " - " + dataCollector.StringTable["SiteTitle"]);
		}
	}
}
