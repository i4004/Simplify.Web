namespace AcspNet
{
	/// <summary>
	/// Library extension base class 
	/// Base class for ACSP.NET library extensions
	/// </summary>
	public abstract class LibExtension
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
		/// Initializes the library extension.
		/// </summary>
		public virtual void Initialize()
		{
			
		}
	}
}
