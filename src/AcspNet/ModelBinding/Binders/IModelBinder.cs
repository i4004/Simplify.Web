namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Represent model binder
	/// </summary>
	public interface IModelBinder
	{
		/// <summary>
		/// Binds model to object
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <returns></returns>
		T Bind<T>();
	}
}