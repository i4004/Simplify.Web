namespace AcspNet
{
	/// <summary>
	/// Executable extensions base class
	/// Provides interface for ACSP executable extensions
	/// </summary>
	public abstract class ExecExtension : ExtensionsBase
	{
		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		public virtual void Invoke()
		{
		}
	}
}
