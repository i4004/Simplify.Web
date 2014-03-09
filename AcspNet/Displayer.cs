using System;
using System.Web;

namespace AcspNet
{
	/// <summary>
	/// Displays web-site
	/// </summary>
	public class Displayer : IDisplayer
	{
		private readonly HttpResponseBase _httpResponse;

		/// <summary>
		/// Prevent site to be displayed via DataCollector
		/// </summary>
		private bool _isDisplayDisabled;

		/// <summary>
		/// Initializes a new instance of the <see cref="Displayer"/> class.
		/// </summary>
		/// <param name="httpResponse">The HTTP response.</param>
		/// <exception cref="System.ArgumentNullException">httpResponse</exception>
		public Displayer(HttpResponseBase httpResponse)
		{
			if (httpResponse == null) throw new ArgumentNullException("httpResponse");

			_httpResponse = httpResponse;
		}

		/// <summary>
		/// Prevent data sent to displayer to be displayed
		/// </summary>
		public void DisableDisplay()
		{
			_isDisplayDisabled = true;
		}

		/// <summary>
		/// Write data to the HTTP response
		/// </summary>
		/// <param name="data">Data to write</param>
		public void Display(string data)
		{
			if (!_isDisplayDisabled)
				_httpResponse.Write(data);
		}

		/// <summary>
		/// Disable HTTP response caching and write data to the HTTP response (useful for ajax response)
		/// </summary>
		/// <param name="data">Data to write</param>
		public void DisplayNoCache(string data)
		{
			if (!_isDisplayDisabled)
			{
				_httpResponse.Cache.SetExpires(DateTime.Now);
				_httpResponse.Cache.SetNoStore();
				_httpResponse.Write(data);
			}
		}
	}
}