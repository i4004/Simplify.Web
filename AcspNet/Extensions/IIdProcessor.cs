using AcspNet.Html;

using ApplicationHelper;

namespace AcspNet.Extensions
{
	public interface IIdProcessor : IHideObjectMembers
	{
		/// <summary>
		/// Checking and getting query string "ID" field value and displaying <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		int? CheckAndGetQueryStringID();

		/// <summary>
		/// Checking and getting query string "id" field value and displaying <see cref="MessageBox"/> small message in case of error
		/// </summary>
		/// <returns>"id" field value</returns>
		int? CheckAndGetQueryStringIdAjax();

		/// <summary>
		/// Checking and getting form string "ID" field value and displaying <see cref="MessageBox"/> message in case of error
		/// </summary>
		/// <returns>"ID" field value</returns>
		int? CheckAndGetFormID();
	}
}