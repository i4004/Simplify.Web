namespace AcspNet.Authentication
{
	/// <summary>
	/// Executable extension protection type
	/// </summary>
	public enum Protection
	{
		/// <summary>
		/// Any client can access extension
		/// </summary>
		None = 0,
		/// <summary>
		/// Only authenticated users can access extension
		/// </summary>
		User = 1
	}
}
