using System;

namespace AcspNet
{
	/// <summary>
	/// Template data result
	/// </summary>
	public class TplData : IControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TplData"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		public TplData(string data)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TplData"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		/// <param name="title">The site title.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public TplData(string data, string title)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Process()
		{
			throw new NotImplementedException();
		}
	}
}