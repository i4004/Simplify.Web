using System.Collections.Specialized;
using AcspNet.Html;

namespace AcspNet.Extensions
{
	/// <summary>
	/// Class that is used to parse 'ID' field from request query string or form.
	/// Usable <see cref="StringTable" /> items:
	/// "NotifyPageDataError"
	/// </summary>
	public sealed class IdVerifier : IIdVerifier
	{
		private readonly NameValueCollection _queryString;
		private readonly NameValueCollection _form;
		private readonly IMessageBox _messageBox;
		private readonly IDisplayer _displayer;

		/// <summary>
		/// Initializes a new instance of the <see cref="IdVerifier"/> class.
		/// </summary>
		internal IdVerifier(NameValueCollection queryString, NameValueCollection form, IMessageBox messageBox, IDisplayer displayer)
		{
			_queryString = queryString;
			_form = form;
			_messageBox = messageBox;
			_displayer = displayer;
		}

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
				_displayer.DisplayNoCache(_messageBox.GetInlineSt("NotifyPageDataError", MessageBoxStatus.Error));
			else
			{
				int parsedID;

				if (!int.TryParse(id, out parsedID))
					_displayer.DisplayNoCache(_messageBox.GetInlineSt("NotifyPageDataError", MessageBoxStatus.Error));
				else
					return parsedID;
			}

			return null;
		}
	}
}
