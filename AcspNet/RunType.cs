namespace AcspNet
{
	/// <summary>
	/// Executable extension run parameters
	/// </summary>
	public enum RunType
	{
		/// <summary>
		/// Indicates what extension should run on specified action/mode or on any page
		/// </summary>
		OnAction = 0,
		/// <summary>
		/// Indicates what extension should run only on default page
		/// </summary>
		MainPage = 1
	}
}
