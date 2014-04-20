using System;

namespace AcspNet
{
	/// <summary>
	/// View factory
	/// </summary>
	public class ViewFactory : ContainerFactory, IViewFactory
	{
		internal ViewFactory(SourceContainer sourceContainer) : base(sourceContainer)
		{
		}

		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public View CreateView(Type controllerType)
		{
			var view = (View)base.CreateContainer(controllerType);

			return view;
		}
	}
}