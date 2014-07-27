namespace AcspNet
{
	/// <summary>
	/// Template data result
	/// </summary>
	public class TplData : ControllerResponse
	{
		private readonly string _data;
		private readonly string _title;

		/// <summary>
		/// Initializes a new instance of the <see cref="TplData"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		public TplData(string data)
		{
			_data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TplData"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		/// <param name="title">The site title.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public TplData(string data, string title)
		{
			_data = data;
			_title = title;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public override void Process()
		{
			if (!string.IsNullOrEmpty(_data))
			{
				DataCollector.Add(_data);

				if (!string.IsNullOrEmpty(_title))
					DataCollector.AddTitle(_title);
			}
		}
	}
}