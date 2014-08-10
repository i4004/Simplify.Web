using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represent AcspNet context factory
	/// </summary> 
	public interface IAcspNetContextFactory
	{
		/// <summary>
		/// Creates the AcspNet context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		IAcspNetContext Create(IOwinContext context);
	}
}