namespace AcspNet.Core
{
	/// <summary>
	/// Represent controller response builder
	/// </summary>
	public interface IControllerResponseBuilder
	{
		/// <summary>
		/// Builds the controller response properties.
		/// </summary>
		void BuildControllerResponseProperties(ControllerResponse controllerResponse);
	}
}