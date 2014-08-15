namespace AcspNet.Core
{
	/// <summary>
	/// Represent controller response builder
	/// </summary>
	public interface IControllerResponseBuilder
	{
		/// <summary>
		/// Builds the controller response base properties.
		/// </summary>
		void BuildBaseProperties(IControllerResponse controllerResponse);
	}
}