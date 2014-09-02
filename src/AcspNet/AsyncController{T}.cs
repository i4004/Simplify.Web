using System.Threading.Tasks;

namespace AcspNet
{
	/// <summary>
	/// AcspNet asynchronous controllers class
	/// </summary>
	public abstract class AsyncController<T> : ModelController<T>
		where T : class
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public abstract Task<ControllerResponse> Invoke();
	}
}