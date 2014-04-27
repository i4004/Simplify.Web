namespace AcspNet.Web
{
	/// <summary>
	/// Parses URL for AcspNet parameters
	/// </summary>
	public class AcspRouteParser
	{
		/// <summary>
		/// Parses URL for AcspNet parameters.
		/// </summary>
		/// <param name="absolutePath">The URL absolute path.</param>
		/// <param name="virtualPath">The application virtual path.</param>
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="id">The identifier.</param>
		public static void ParseRoute(string absolutePath, string virtualPath, out string action, out string mode, out string id)
		{
			action = null;
			mode = null;
			id = null;

			if (string.IsNullOrEmpty(absolutePath) || absolutePath.Length <= virtualPath.Length)
				return;

			var path = absolutePath.Substring(virtualPath.Length + 1);

			// Removing query string
			if (path.Contains("?"))
				path = path.Substring(0, path.IndexOf('?'));

			var items = path.Split('/');

			if (items.Length == 1 && !string.IsNullOrEmpty(items[0]))
				action = items[0];
			else if (items.Length == 2 && !string.IsNullOrEmpty(items[0]) && !string.IsNullOrEmpty(items[1]))
			{
				action = items[0];
				id = items[1];
			}
			else if (items.Length == 3 && !string.IsNullOrEmpty(items[0]) && !string.IsNullOrEmpty(items[1]) && !string.IsNullOrEmpty(items[2]))
			{
				action = items[0];
				mode = items[1];
				id = items[2];
			}
		}
	}
}