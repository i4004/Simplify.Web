// /////////////////////////////////////////////////////////////

namespace AcspNet
{
	/// <summary>
	/// Extensions info class
	/// </summary>
	public class ExtensionInfo
	{
		public readonly string Author = "";
		public readonly string Description = "";
		public readonly string Version = "";

		public ExtensionInfo(string version, string author = "", string description = "")
		{
			Version = version;
			Author = author;
			Description = description;
		}
	}
}

// /////////////////////////////////////////////////////////////