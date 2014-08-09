namespace AcspNet
{
	/// <summary>
	/// Represent view accessor
	/// </summary>
	public interface IViewAccessor : IHideObjectMembers
	{
		/// <summary>
		/// Gets view instance
		/// </summary>
		/// <typeparam name="T">View instance to get</typeparam>
		/// <returns>View instance</returns>
		T GetView<T>()
			where T : IView;
	}
}