// /////////////////////////////////////////////////////////////

namespace AcspNet
{
	/// <summary>
	/// Executable extensions interface
	/// Provides interface for ACSPNET engine extensions
	/// </summary>
	public interface IExtension
	{
		ExtensionInfo Info { get; }

		ExtensionExecParams ExecParams { get; }

		void Invoke(Manager manager);
	}
}

// /////////////////////////////////////////////////////////////