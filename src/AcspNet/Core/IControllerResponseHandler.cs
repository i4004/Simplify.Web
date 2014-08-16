using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent controller response handler
	/// </summary>
	public interface IControllerResponseHandler
	{
		/// <summary>
		/// Processes the specified response.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="response">The response.</param>
		void Process(IDIContainerProvider containerProvider, ControllerResponse response);
	}
}