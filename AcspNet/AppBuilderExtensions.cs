using Owin;

namespace AcspNet
{
	/// <summary>
	/// OWIN IAppBuilder AcspNet extensions
	/// </summary>
	public static class AppBuilderExtensions
	{
		/// <summary>
		/// Adds AcspNet in OWIN pipeline
		/// </summary>
		/// <param name="builder">The OWIN builder.</param>
		/// <returns></returns>
		public static IAppBuilder UseAcspNet(this IAppBuilder builder/* options = null*/)
		{
			return builder.Use(null);
		}		 
	}
}