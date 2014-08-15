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
		/// <param name="response">The response.</param>
		void Process(IControllerResponse response);
	}
}