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
		/// <exception cref="System.NotImplementedException"></exception>
		public IAcspNetContext Create(IOwinContext context)
		{
			var ascpNetContext = new AcspNetContext(context);

			return ascpNetContext;
		}
	}
}