using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides controller response handler
	/// </summary>
	public class ControllerResponseHandler : IControllerResponseHandler
	{
		private readonly IControllerResponseBuilder _builder;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerResponseHandler"/> class.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public ControllerResponseHandler(IControllerResponseBuilder builder)
		{
			_builder = builder;
		}

		/// <summary>
		/// Processes the specified response.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="response">The response.</param>
		public void Process(IDIContainerProvider containerProvider, ControllerResponse response)
		{
			_builder.BuildControllerResponseProperties(containerProvider, response);
			response.Process();
		}
	}
}