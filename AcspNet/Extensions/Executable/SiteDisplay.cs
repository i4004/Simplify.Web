using System;

using AcspNet.Extensions.Library;

namespace AcspNet.Extensions.Executable
{
	/// <summary>
	/// Site displaying
	/// </summary>
	[Priority(11)]
	[Version("1.1")]
	public sealed class SiteDisplay : IExecExtension
	{
		/// <summary>
		/// Site generation time templates variable name
		/// </summary>
		public const string SiteVariableNameSiteGenerationTime = "SV:SiteGenerationTime";

		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public void Invoke(Manager manager)
		{
			var dataCollector = manager.Get<DataCollector>();

			manager.StopWatch.Stop();
			dataCollector.Set(SiteVariableNameSiteGenerationTime, manager.StopWatch.Elapsed.ToString("mm\\:ss\\:fff"));

			manager.Response.Cache.SetExpires(DateTime.Now);
			manager.Response.Cache.SetNoStore();
			dataCollector.DisplaySite();
		}
	}
}

