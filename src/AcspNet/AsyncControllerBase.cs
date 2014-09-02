using System.Threading.Tasks;

namespace AcspNet
{
	/// <summary>
	/// AcspNet asynchronous controllers base class
	/// </summary>
	public abstract class AsyncControllerBase : ControllerBase
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public abstract Task<ControllerResponse> Invoke();
	}
}