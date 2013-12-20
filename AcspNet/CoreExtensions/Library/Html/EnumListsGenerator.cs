using System;
using System.Linq;

namespace AcspNet.CoreExtensions.Library.Html
{
	/// <summary>
	/// Html select control lists generator from Enums
	/// Usable <see cref="StringTable"/> items:
	/// "HtmlListNotSelectedMessage"
	/// </summary>
	[Version("1.0.1")]
	public sealed class EnumListsGenerator : LibExtension
	{
		/// <summary>
		/// Generate HTML list from enum items
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="selectedItem">Selected enum item</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateList<T>(T selectedItem, bool displayNotSelectedMessage = true)
			where T : struct 
		{
			var st = Manager.Get<StringTable>();
			var data = displayNotSelectedMessage ? Manager.Get<ListsGenerator>().GenerateNotSelectedListItem() : "";

			return Enum.GetValues(typeof (T))
				.Cast<T>()
				.Aggregate(data,
					(current, item) =>
						current +
						string.Format("<option value=\"{0}\" {2}>{1}</option>", Convert.ToInt32(item), st.GetAssociatedValue(item),
							selectedItem.ToString() == item.ToString() ? "selected='selected'" : ""));
		}
	}
}