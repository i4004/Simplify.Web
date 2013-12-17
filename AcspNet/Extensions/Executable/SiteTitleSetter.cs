using AcspNet.Extensions.Library;

namespace AcspNet.Extensions.Executable
{
	/// <summary>
	/// Site title
	/// Usable <see cref="StringTable"/> items:
	/// "SiteTitle"
	/// <see cref="DataCollector"/> variables:
	/// "Title"
	/// </summary>
	[Priority(8)]
	[Version("1.0.3")]
	internal sealed class SiteTitleSetter : IExecExtension
	{
		public void Invoke(Manager manager)
		{
			var st = manager.Get<StringTable>();
			var dc = manager.Get<DataCollector>();

			if (string.IsNullOrEmpty(manager.CurrentAction) && string.IsNullOrEmpty(manager.CurrentMode) && !dc.IsDataExist("Title"))
				dc.Set("Title", st["SiteTitle"]);
			else
				dc.Set("Title", " - " + st["SiteTitle"], DataCollectorAddType.AddFromEnd);
		}
	}
}
