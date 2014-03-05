using System.Collections.Generic;

namespace AcspNet
{
	public interface IAcspApplication : IHideObjectMembers
	{
		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData();

		/// <summary>
		/// Gets the library extensions meta data.
		/// </summary>
		/// <returns></returns>
		IList<LibExtensionMetaContainer> GetLibExtensionsMetaData();
	}
}