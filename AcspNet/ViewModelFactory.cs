using System;
using System.Web;

namespace AcspNet
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly HttpRequestBase _httpRequest;

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
		/// </summary>
		/// <param name="httpRequest">The HTTP request.</param>
		public ViewModelFactory(HttpRequestBase httpRequest)
		{
			_httpRequest = httpRequest;
		}

		public object CreateViewModel(Type viewModelType, out CreateViewModelResult result)
		{
			result = CreateViewModelResult.Ok;

			var viewModel = DependencyResolver.Current.Resolve(viewModelType);
			return viewModel;
		}
	}
}