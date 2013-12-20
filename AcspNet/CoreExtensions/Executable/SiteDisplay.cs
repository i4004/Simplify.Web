using System;

using AcspNet.CoreExtensions.Library;

namespace AcspNet.CoreExtensions.Executable
{
	/// <summary>
	/// Site displaying
	/// </summary>
	[Priority(11)]
	[Version("1.1")]
	public sealed class SiteDisplay : ExecExtension
	{
		/// <summary>
		/// Site generation time templates variable name
		/// </summary>
		public const string SiteVariableNameSiteGenerationTime = "SV:SiteGenerationTime";

		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		public override void Invoke()
		{
			var dataCollector = Manager.Get<DataCollector>();

			Manager.StopWatch.Stop();
			dataCollector.Set(SiteVariableNameSiteGenerationTime, Manager.StopWatch.Elapsed.ToString("mm\\:ss\\:fff"));

			Manager.Response.Cache.SetExpires(DateTime.Now);
			Manager.Response.Cache.SetNoStore();
			dataCollector.DisplaySite();
		}
	}
}

