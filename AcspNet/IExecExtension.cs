namespace AcspNet
{
	/// <summary>
	/// Executable extensions interface
	/// Provides interface for ACSP.NET extensions
	/// </summary>
	public interface IExecExtension
	{
		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		void Invoke(Manager manager);
	}
}
