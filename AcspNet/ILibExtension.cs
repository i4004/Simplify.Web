namespace AcspNet
{
	/// <summary>
	/// Library extension interface 
	/// Provides interface for ACSP.NET library extensions
	/// </summary>
	public interface ILibExtension
	{
		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		void Initialize(Manager manager);
	}
}
