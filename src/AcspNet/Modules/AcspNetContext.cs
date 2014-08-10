using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides AcspNet context
	/// </summary>
	public class AcspNetContext : IAcspNetContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetContext"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public AcspNetContext(IOwinContext context)
		{
			Context = context;
		}

		/// <summary>
		/// Gets the context for the current HTTP request.
		/// </summary>
		public IOwinContext Context { get; private set; }
	}
}