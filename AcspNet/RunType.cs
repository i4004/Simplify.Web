namespace AcspNet
{
	/// <summary>
	/// Executable extension run parameters
	/// </summary>
	public enum RunType
	{
		/// <summary>
		/// Indicates what extension should run on specified action and mode or on any page
		/// </summary>
		OnAction = 0,
		/// <summary>
		/// Indicates what extension should run only on main page
		/// </summary>
		MainPage = 1
	}
}
