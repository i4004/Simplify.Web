using AcspNet.CoreExtensions.Library;
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
	internal sealed class SiteTitleSetter : ExecExtension
	{
		public override void Invoke()
		{
			var st = Manager.Get<StringTable>();
			var dc = Manager.Get<DataCollector>();

			if (string.IsNullOrEmpty(Manager.CurrentAction) && string.IsNullOrEmpty(Manager.CurrentMode) && !dc.IsDataExist("Title"))
				dc.Set("Title", st["SiteTitle"]);
			else
				dc.Set("Title", " - " + st["SiteTitle"], DataCollectorAddType.AddFromEnd);
		}
	}
}
