namespace AcspNet
{
	/// <summary>
	/// Controller base class
	/// </summary>
	public abstract class Controller : Container
	{
		internal virtual IViewFactory ViewFactory { get; set; }

		/// <summary>
		/// Gets library extension instance
		/// </summary>
		/// <typeparam name="T">Library extension instance to get</typeparam>
		/// <returns>Library extension</returns>
		public T GetView<T>()
			where T : View
		{
			var type = typeof(T);

			return (T)ViewFactory.CreateView(type);
		}

		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public virtual void Invoke()
		{
		}
	}
}
