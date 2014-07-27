namespace AcspNet
{
	/// <summary>
	/// Represent controller response processor
	/// </summary>
	public interface IControllerResponseProcessor
	{
		/// <summary>
		/// Processes the specified response.
		/// </summary>
		/// <param name="response">The response.</param>
		void Process(IControllerResponse response);
	}
}