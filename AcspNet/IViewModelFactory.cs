using System;

namespace AcspNet
{
	public interface IViewModelFactory
	{
		object CreateViewModel(Type viewModelType, out CreateViewModelResult result);
	}
}