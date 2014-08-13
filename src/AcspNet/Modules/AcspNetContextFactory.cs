using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides AcspNet context factory
	/// </summary>
	public class AcspNetContextFactory : IAcspNetContextFactory
	{
		/// <summary>
		/// Creates the AcspNet context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public IAcspNetContext Create(IOwinContext context)
		{
			var ascpNetContext = new AcspNetContext(context);

			return ascpNetContext;
		}
	}
}