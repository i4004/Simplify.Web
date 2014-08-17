using System;
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
		/// <param name="response">The response.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		public ControllerResponseResult Process(ControllerResponse response, IDIContainerProvider containerProvider)
		{
			if(response == null)
				return ControllerResponseResult.Ok;

			_builder.BuildControllerResponseProperties(response, containerProvider);
			return response.Process();
		}
	}
}