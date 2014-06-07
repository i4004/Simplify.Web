using System;
using System.Web;

namespace AcspNet
{
	/// <summary>
	/// Displays web-site
	/// </summary>
	public class Displayer
	{
		private readonly HttpResponseBase _httpResponse;

		/// <summary>
		/// Initializes a new instance of the <see cref="Displayer"/> class.
		/// </summary>
		/// <param name="httpResponse">The HTTP response.</param>
		/// <exception cref="System.ArgumentNullException">httpResponse</exception>
		internal Displayer(HttpResponseBase httpResponse)
		{
			_httpResponse = httpResponse;
		}

		/// <summary>
		/// Write data to the HTTP response
		/// </summary>
		/// <param name="data">Data to write</param>
		public void Display(string data)
		{
			_httpResponse.Write(data);
		}

		/// <summary>
		/// Disable HTTP response caching and write data to the HTTP response (useful for ajax response)
		/// </summary>
		/// <param name="data">Data to write</param>
		public void DisplayNoCache(string data)
		{
			_httpResponse.Cache.SetExpires(DateTime.Now);
			_httpResponse.Cache.SetNoStore();
			_httpResponse.Write(data);
		}
	}
}