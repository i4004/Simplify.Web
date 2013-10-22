// /////////////////////////////////////////////////////////////

namespace AcspNet
{
	/// <summary>
	/// Library extension interface 
	/// Provides interface for ACSPNET engine extensions
	/// </summary>
	public interface ILibExtension
	{
		ExtensionInfo Info { get; }

		int Priority { get; }

		void Initialize(Manager manager);
	}
}

// /////////////////////////////////////////////////////////////