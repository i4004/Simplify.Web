using System.Collections.Specialized;
using AcspNet.Modules.Html;

namespace AcspNet.Modules
{
	/// <summary>
	/// Class that is used to parse and act on 'ID' field from request query string or form.
	/// Usable <see cref="StringTable" /> items:
	/// "NotifyPageDataError"
	/// </summary>
	public sealed class IdVerifier : IIdVerifier
	{
		private readonly NameValueCollection _queryString;
		private readonly NameValueCollection _form;
		private readonly IMessageBox _messageBox;

		/// <summary>
		/// Initializes a new instance of the <see cref="IdVerifier"/> class.
		/// </summary>
		internal IdVerifier(NameValueCollection queryString, NameValueCollection form, IMessageBox messageBox)
		{
			_queryString = queryString;
			_form = form;
			_messageBox = messageBox;
		}

		/// <summary>
		/// Gets the message box html code to display message box, in case of verify method failed.
		/// </summary>
		/// <value>
		/// The message box to display.
		/// </value>
		public string MessageBoxToDisplay { get; private set; }

		/// <summary>
		/// Verify and get query string "ID" field value and display the <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		public int? VeiryQueryStringID()
		{
			var id = _queryString["id"];

			if ((string.IsNullOrEmpty(id)))
				_messageBox.ShowSt("NotifyPageDataError", MessageBoxStatus.Error);
			else
			{
				int parsedID;

				if (!int.TryParse(id, out parsedID))
					_messageBox.ShowSt("NotifyPageDataError", MessageBoxStatus.Error);
				else
					return parsedID;
			}

			return null;
		}

		/// <summary>
		/// Verify and get form string "ID" field value and display the <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		public int? VerifyFormID()
		{
			var id = _form["ID"];

			if ((string.IsNullOrEmpty(id)))
				_messageBox.ShowSt("NotifyPageDataError", MessageBoxStatus.Error);
			else
			{
				int parsedID;

				if (!int.TryParse(id, out parsedID))
					_messageBox.ShowSt("NotifyPageDataError", MessageBoxStatus.Error);
				else
					return parsedID;
			}

			return null;
		}

		/// <summary>
		/// Verify and gett query string "id" field value and just display <see cref="MessageBox"/> an inline message box to user via <see cref="Displayer"/> in case of error
		/// </summary>
		/// <returns>"id" field value</returns>
		public int? VerifyQueryStringIdAjax()
		{
			var id = _queryString["id"];

			if ((string.IsNullOrEmpty(id)))
				MessageBoxToDisplay = _messageBox.GetInlineSt("NotifyPageDataError", MessageBoxStatus.Error);
			else
			{
				int parsedID;

				if (!int.TryParse(id, out parsedID))
					MessageBoxToDisplay = _messageBox.GetInlineSt("NotifyPageDataError", MessageBoxStatus.Error);
				else
					return parsedID;
			}

			return null;
		}
	}
}
