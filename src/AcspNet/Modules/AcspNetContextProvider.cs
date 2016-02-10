namespace Simplify.Web.Modules
{
	/// <summary>
	/// Provides AcspNet context provider
	/// </summary>
	public class AcspNetContextProvider : IAcspNetContextProvider
	{
		private IAcspNetContext _acspNetContext;

		/// <summary>
		/// Creates the AcspNet context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public void Setup(IOwinContext context)
		{
			if(_acspNetContext == null)
				_acspNetContext = new AcspNetContext(context);
		}

		/// <summary>
		/// Gets the AcspNet context.
		/// </summary>
		/// <returns></returns>
		public IAcspNetContext Get()
		{
			return _acspNetContext;
		}
	}
}