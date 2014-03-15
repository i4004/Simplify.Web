using AcspNet.Html;

namespace AcspNet.Extensions
{
	/// <summary>
	/// Class that is used to parse 'ID' field from request query string or form.
	/// Usable <see cref="StringTable" /> items:
	/// "NotifyPageDataError"
	/// </summary>
	public interface IIdVerifier : IHideObjectMembers
	{
		/// <summary>
		/// Verify and get query string "ID" field value and display the <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		int? VeiryQueryStringID();

		/// <summary>
		/// Verify and get form string "ID" field value and display the <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		int? VerifyFormID();

		/// <summary>
		/// Verify and gett query string "id" field value and just display <see cref="MessageBox"/> an inline message box to user via <see cref="Displayer"/> in case of error
		/// </summary>
		/// <returns>"id" field value</returns>
		int? VerifyQueryStringIdAjax();
	}
}