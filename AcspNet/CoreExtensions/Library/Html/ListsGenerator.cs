using System;
using System.Collections.Generic;
using System.Linq;

namespace AcspNet.CoreExtensions.Library.Html
{
	/// <summary>
	/// HTML select control lists generator
	/// Usable <see cref="StringTable"/> items:
	/// "HtmlListNotSelectedMessage"
	/// </summary>
	[Version("1.1.3")]
	public class ListsGenerator : LibExtension
	{
		/// <summary>
		/// Generate number selected HTML list
		/// </summary>
		/// <param name="length">Length of a list</param>
		/// <param name="selectedNumber">Selected list number</param>
		/// <param name="startNumber">Start number of a list</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns></returns>
		public string GenerateNumbersList(int length, int? selectedNumber = 0, int startNumber = 0, bool displayNotSelectedMessage = false)
		{
			var data = displayNotSelectedMessage ? GenerateNotSelectedListItem(selectedNumber == null) : "";

			for(var i = startNumber; i < startNumber + length; i++)
				data += string.Format("<option value=\"{0}\"{1}>{0}</option>", i, i == selectedNumber ? " selected='selected'" : "");

			return data;			
		}

		/// <summary>
		/// Generate hours selector HTML list in 24 hour format (from 0 to 23)
		/// </summary>
		/// <param name="selectedHour">Selected hour</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateHoursList(int selectedHour = -1, bool displayNotSelectedMessage = false)
		{
			var data = displayNotSelectedMessage ? GenerateNotSelectedListItem(selectedHour == -1) : "";

			for(var i = 0; i < 24; i++)
				data += string.Format("<option value=\"{0}\"{2}>{1}</option>", i, i.ToString("00"), i == selectedHour ? " selected='selected'" : "");

			return data;
		}

		/// <summary>
		/// Generate minutes selector HTML list (from 0 to 59)
		/// </summary>
		/// <param name="selectedMinute">Selected minute</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateMinutesList(int selectedMinute = -1, bool displayNotSelectedMessage = false)
		{
			var data = displayNotSelectedMessage ? GenerateNotSelectedListItem(selectedMinute == -1) : "";

			for(var i = 0; i < 60; i++)
				data += string.Format("<option value=\"{0}\"{2}>{1}</option>", i, i.ToString("00"), i == selectedMinute ? " selected='selected'" : "");

			return data;
		}


		/// <summary>
		/// Generate days selector HTML list (from 1 to 31)
		/// </summary>
		/// <param name="selectedDay">Selected day</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateDaysList(int selectedDay = -1, bool displayNotSelectedMessage = true)
		{
			var data = displayNotSelectedMessage ? GenerateNotSelectedListItem(selectedDay == -1) : "";

			for(var i = 1; i < 32; i++)
				data += string.Format("<option value=\"{0}\" {2}>{1}</option>", i, i.ToString("00"), i == selectedDay ? "selected='selected'" : "");

			return data;
		}

		/// <summary>
		/// Generate months selector HTML list (from 0 to 11)
		/// </summary>
		/// <param name="selectedMonth">Selected month</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateMonthsList(int selectedMonth = -1, bool displayNotSelectedMessage = true)
		{
			var data = displayNotSelectedMessage ? GenerateNotSelectedListItem(selectedMonth == -1) : "";

			var month = Convert.ToDateTime("1/1/2010");

			for(var i = 0; i < 12; i++)
				data += string.Format("<option value=\"{0}\" {2}>{1}</option>", i, month.AddMonths(i).ToString("MMMM"), i == selectedMonth ? "selected='selected'" : "");

			return data;
		}

		/// <summary>
		/// Generate months selector HTML list (from 1 to 12)
		/// </summary>
		/// <param name="selectedMonth">Selected month</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateMonthsListFrom1(int selectedMonth = -1, bool displayNotSelectedMessage = true)
		{
			var data = displayNotSelectedMessage ? GenerateNotSelectedListItem(selectedMonth == -1) : "";

			var month = Convert.ToDateTime("1/1/2010");

			for(var i = 1; i < 13; i++)
				data += string.Format("<option value=\"{0}\" {2}>{1}</option>", i, month.AddMonths(i).ToString("MMMM"), i == selectedMonth ? "selected='selected'" : "");

			return data;
		}

		/// <summary>
		/// Generate years selector HTML list (from current year to -<paramref name="numberOfYears"/>)
		/// </summary>
		/// <param name="numberOfYears">Number of years in list</param>
		/// <param name="selectedYear">Selected year</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateYearsListToPast(int numberOfYears, int selectedYear = -1, bool displayNotSelectedMessage = true)
		{
			var data = displayNotSelectedMessage ? GenerateNotSelectedListItem(selectedYear == -1) : "";

			for(var i = DateTime.Now.Year; i >= DateTime.Now.Year - numberOfYears; i--)
				data += string.Format("<option value=\"{0}\" {2}>{1}</option>", i, i, i == selectedYear ? "selected='selected'" : "");

			return data;
		}

		/// <summary>
		/// Generate years selector HTML list (from current year to +<paramref name="numberOfYears"/>)
		/// </summary>
		/// <param name="numberOfYears">Number of years in list</param>
		/// <returns>HTML list</returns>
		public static string GenerateYearsListToFuture(int numberOfYears)
		{
			var data = "";

			for(var i = DateTime.Now.Year; i <= DateTime.Now.Year + numberOfYears; i++)
				data += string.Format("<option value=\"{0}\">{1}</option>", i, i);

			return data;
		}

		/// <summary>
		/// Generic list generator
		/// </summary>
		/// <typeparam name="T">Item type to generate list from</typeparam>
		/// <param name="items">List of items</param>
		/// <param name="id">Item ID field</param>
		/// <param name="name">Item name field</param>
		/// <param name="selectedItem">Selected item</param>
		/// <param name="generateEmptyListItem">Generate empty list item</param>
		/// <returns></returns>
		public static string GenerateList<T>(IList<T> items, Func<T, int> id, Func<T, string> name, T selectedItem = null, bool generateEmptyListItem = false)
			where T : class
		{
			var data = generateEmptyListItem ? GenerateEmptyListItem() : "";

			return items.Aggregate(data,
				(current, item) =>
					current +
					string.Format("<option value=\"{0}\" {2}>{1}</option>", id(item), name(item),
						(selectedItem == item ? "selected='selected'" : "")));
		}

		/// <summary>
		/// Generating empty HTML list item
		/// </summary>
		/// <returns></returns>
		public static string GenerateEmptyListItem()
		{
			return "<option value=''>&nbsp;</option>";
		}

		/// <summary>
		/// Generating HTML list item with "Not selected" text
		/// </summary>
		/// <returns></returns>
		public string GenerateNotSelectedListItem(bool isSelected = true)
		{
			var st = Manager.Get<StringTable>();
			return string.Format("<option value=''{1}>{0}</option>", st["HtmlListNotSelectedMessage"], isSelected ? " selected='selected'" : "");
		}
	}
}