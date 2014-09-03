namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Represent view model from POST/GET request data or JSON to object serializer
	/// </summary>
	public interface IModelDeserializer
	{
		/// <summary>
		/// Deserializes view model from POST/GET request data or JSON to object
		/// </summary>
		/// <typeparam name="T">View model type</typeparam>
		/// <returns></returns>
		T Deserialize<T>();
	}
}