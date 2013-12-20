namespace AcspNet.CoreExtensions.Library.Html
{
	/// <summary>
	/// MessageBox control
	/// Usable template files:
	/// "AcspNet/MessageBox/InfoMessageBox.tpl"
	/// "AcspNet/MessageBox/ErrorMessageBox.tpl"
	/// "AcspNet/MessageBox/OkMessageBox.tpl"
	/// "AcspNet/MessageBox/InlineInfoMessageBox.tpl"
	/// "AcspNet/MessageBox/InlineErrorMessageBox.tpl"
	/// "AcspNet/MessageBox/InlineOkMessageBox.tpl"
	/// Usable <see cref="StringTable"/> items:
	/// "FormTitleMessageBox"
	/// Template variables:
	/// "Message"
	/// "Title"
	/// <see cref="DataCollector"/> variables:
	/// "MainContent"
	/// "Title"
	/// </summary>
	[Priority(-6)]
	[Version("2")]
	public sealed class MessageBox : LibExtension
	{
		private const string MessageBoxTemplatesPath = "AcspNet/MessageBox/";
		private static string DataCollectorVariableNameInstance = "MainContent";

		/// <summary>
		/// Gets or sets the data collector variable name to put message box to
		/// </summary>
		/// <value>
		/// The data collector variable name to put message box to
		/// </value>
		public static string DataCollectorVariableName
		{
			get { return DataCollectorVariableNameInstance; }
			set { DataCollectorVariableNameInstance = value; }
		}

		/// <summary>
		/// Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		public void Show(string text, MessageBoxStatus status = MessageBoxStatus.Information, string title = "")
		{
			var dataCollector = Manager.Get<DataCollector>();
			if (dataCollector.IsDataExist("MainContainerData") || dataCollector.IsDataExist("Title"))
				return;

			var st = Manager.Get<StringTable>();
			var templateFactory = Manager.Get<TemplateFactory>();

			var templateFile = MessageBoxTemplatesPath;

			switch (status)
			{
				case MessageBoxStatus.Information:
					templateFile += "InfoMessageBox.tpl";
					break;
				case MessageBoxStatus.Error:
					templateFile += "ErrorMessageBox.tpl";
					break;
				case MessageBoxStatus.Ok:
					templateFile += "OkMessageBox.tpl";
					break;
			}

			var tpl = templateFactory.Load(templateFile);

			tpl.Set("Message", text);
			tpl.Set("Title", title == "" ? st.Items["FormTitleMessageBox"] : title);

			dataCollector.Set(DataCollectorVariableName, tpl.Text);
			dataCollector.Set("Title", title == "" ? st.Items["FormTitleMessageBox"] : title);
		}

		/// <summary>
		///Generate message box HTML and set to data collector
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <param name="title">Title of message box</param>
		public void ShowSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Information, string title = "")
		{
			Show(Manager.Get<StringTable>()[stringTableItemName], status, title);
		}

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="text">Text of message box</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		public string GetInline(string text, MessageBoxStatus status = MessageBoxStatus.Information)
		{
			var tf = Manager.Get<TemplateFactory>();

			var templateFile = MessageBoxTemplatesPath;

			switch (status)
			{
				case MessageBoxStatus.Information:
					templateFile += "InlineInfoMessageBox.tpl";
					break;
				case MessageBoxStatus.Error:
					templateFile += "InlineErrorMessageBox.tpl";
					break;
				case MessageBoxStatus.Ok:
					templateFile += "InlineOkMessageBox.tpl";
					break;
			}

			var tpl = tf.Load(templateFile);

			tpl.Set("Message", text);
			return tpl.Text;
		}

		/// <summary>
		/// Get inline message box HTML
		/// </summary>
		/// <param name="stringTableItemName">Show message from string table item</param>
		/// <param name="status">Status of the information</param>
		/// <returns>Message box html</returns>
		public string GetInlineSt(string stringTableItemName, MessageBoxStatus status = MessageBoxStatus.Error)
		{
			return GetInline(Manager.Get<StringTable>()[stringTableItemName], status);
		}
	}

	/// <summary>
	/// The status of message box
	/// </summary>
	public enum MessageBoxStatus
	{
		/// <summary>
		/// The information status
		/// </summary>
		Information = 0,
		/// <summary>
		/// The error status
		/// </summary>
		Error = 1,
		/// <summary>
		/// The OK status
		/// </summary>
		Ok = 2
	}
}
