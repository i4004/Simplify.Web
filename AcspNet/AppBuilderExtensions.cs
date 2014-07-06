using Owin;

namespace AcspNet
{
	public static class AppBuilderExtensions
	{
		public static IAppBuilder UseAcspNet(this IAppBuilder builder/* options = null*/)
		{
			return builder.Use(null);
		}		 
	}
}