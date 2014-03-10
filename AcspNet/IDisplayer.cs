namespace AcspNet
{
	/// <summary>
	/// Represent web-site displayer
	/// </summary>
	public interface IDisplayer : IHideObjectMembers
	{
		/// <summary>
		/// Write data to the HTTP response
		/// </summary>
		/// <param name="data">Data to write</param>
		void Display(string data);

		/// <summary>
		/// Disable HTTP response caching and write data to the HTTP response (useful for ajax response)
		/// </summary>
		/// <param name="data">Data to write</param>
		void DisplayNoCache(string data);
	}
}