namespace Simplify.Web.Modules
{
	/// <summary>
	/// Represent AcspNet context provider
	/// </summary> 
	public interface IAcspNetContextProvider
	{
		/// <summary>
		/// Creates the AcspNet context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		void Setup(IOwinContext context);

		/// <summary>
		/// Gets the AcspNet context.
		/// </summary>
		/// <returns></returns>
		IAcspNetContext Get();
	}
}