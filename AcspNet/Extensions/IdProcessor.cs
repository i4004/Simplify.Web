using AcspNet.Html;

namespace AcspNet.Extensions
{
	/// <summary>
	/// Query and form data helper
	/// Usable <see cref="StringTable"/> items:
	/// "NotifyPageDataError"
	/// </summary>
	public sealed class IdProcessor : IIdProcessor
	{
		private readonly Manager _manager;

		public IdProcessor(Manager manager)
		{
			_manager = manager;
		}

		/// <summary>
		/// Checking and getting query string "ID" field value and displaying <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		public int? CheckAndGetQueryStringID()
		{
			var id = _manager.QueryString["id"];

			if ((string.IsNullOrEmpty(id)))
				_manager.HtmlWrapper.MessageBox.ShowSt("NotifyPageDataError");
			else
			{
				int parsedID;

				if (!int.TryParse(id, out parsedID))
					_manager.HtmlWrapper.MessageBox.ShowSt("NotifyPageDataError");
				else
					return parsedID;
			}

			return null;
		}

		/// <summary>
		/// Checking and getting query string "id" field value and displaying <see cref="MessageBox"/> small message in case of error
		/// </summary>
		/// <returns>"id" field value</returns>
		public int? CheckAndGetQueryStringIdAjax()
		{
			var id = _manager.QueryString["id"];

			if ((string.IsNullOrEmpty(id)))
				_manager.DataCollector.DisplayPartial(_manager.HtmlWrapper.MessageBox.GetInlineSt("NotifyPageDataError"));
			else
			{
				int parsedID;

				if (!int.TryParse(id, out parsedID))
					_manager.DataCollector.DisplayPartial(_manager.HtmlWrapper.MessageBox.GetInlineSt("NotifyPageDataError"));
				else
					return parsedID;
			}

			return null;
		}

		/// <summary>
		/// Checking and getting form string "ID" field value and displaying <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		public int? CheckAndGetFormID()
		{
			var id = _manager.Form["ID"];

			if ((string.IsNullOrEmpty(id)))
				_manager.HtmlWrapper.MessageBox.ShowSt("NotifyPageDataError");
			else
			{
				int parsedID;

				if (!int.TryParse(id, out parsedID))
					_manager.HtmlWrapper.MessageBox.ShowSt("NotifyPageDataError");
				else
					return parsedID;
			}

			return null;
		}
	}
}
