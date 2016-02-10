namespace Simplify.Web.Core
{
	/// <summary>
	/// Represent context variables setter
	/// </summary>
	public interface IContextVariablesSetter
	{
		/// <summary>
		/// Sets the context variables to data collector
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		void SetVariables(IDIContainerProvider containerProvider);
	}
}