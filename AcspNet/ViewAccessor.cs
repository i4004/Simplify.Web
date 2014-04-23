namespace AcspNet
{
	public abstract class ViewAccessor : IHideObjectMembers
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
	}
}