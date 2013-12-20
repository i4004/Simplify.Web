namespace AcspNet
{
	/// <summary>
	/// Executable extensions base class
	/// Provides interface for ACSP.NET executable extensions
	/// </summary>
	public abstract class ExecExtension
	{
		internal Manager ManagerInstance;

		/// <summary>
		/// Current AcspNet.Manager instance
		/// </summary>
		public Manager Manager
		{
			get { return ManagerInstance; }
		}

		/// <summary>
		/// Invokes the executable extension.
		/// </summary>
		public virtual void Invoke()
		{
		}
	}
}
