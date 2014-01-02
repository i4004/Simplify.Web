using System;
using System.Collections.Generic;
using System.Linq;

namespace AcspNet.Html
{
	/// <summary>
	/// HTML select control lists generator
	/// Usable <see cref="StringTable"/> items:
	/// "HtmlListNotSelectedMessage"
	/// </summary>
	public sealed class ListsGenerator : IListsGenerator
	{
		internal Manager Manager;

		internal ListsGenerator(Manager manager)
		{
			Manager = manager;
		}

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
			var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedNumber == null) : "";

			for (var i = startNumber; i < startNumber + length; i++)
				data += string.Format("<option value='{0}'{1}>{0}</option>", i, i == selectedNumber ? " selected='selected'" : "");

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
			var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedHour == -1) : "";

			for (var i = 0; i < 24; i++)
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
			var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMinute == -1) : "";

			for (var i = 0; i < 60; i++)
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
			var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedDay == -1) : "";

			for (var i = 1; i < 32; i++)
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
			var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMonth == -1) : "";

			var month = Convert.ToDateTime("1/1/2010");

			for (var i = 0; i < 12; i++)
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
			var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedMonth == -1) : "";

			var month = Convert.ToDateTime("1/1/2010");

			for (var i = 1; i < 13; i++)
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
			var data = displayNotSelectedMessage ? GenerateDefaultListItem(selectedYear == -1) : "";

			for (var i = DateTime.Now.Year; i >= DateTime.Now.Year - numberOfYears; i--)
				data += string.Format("<option value=\"{0}\" {2}>{1}</option>", i, i, i == selectedYear ? "selected='selected'" : "");

			return data;
		}

		/// <summary>
		/// Generate years selector HTML list (from current year to +<paramref name="numberOfYears"/>)
		/// </summary>
		/// <param name="numberOfYears">Number of years in list</param>
		/// <returns>HTML list</returns>
		public string GenerateYearsListToFuture(int numberOfYears)
		{
			var data = "";

			for (var i = DateTime.Now.Year; i <= DateTime.Now.Year + numberOfYears; i++)
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
		public string GenerateList<T>(IList<T> items, Func<T, int> id, Func<T, string> name, T selectedItem = null, bool generateEmptyListItem = false)
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
		/// Generate HTML list from enum items
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="selectedItem">Selected enum item</param>
		/// <param name="displayNotSelectedMessage">Display not selected message in list or not</param>
		/// <returns>HTML list</returns>
		public string GenerateListFromEnum<T>(T selectedItem, bool displayNotSelectedMessage = true)
			where T : struct
		{
			var data = displayNotSelectedMessage ? GenerateDefaultListItem() : "";

			return Enum.GetValues(typeof(T))
				.Cast<T>()
				.Aggregate(data,
					(current, item) =>
						current +
						string.Format("<option value=\"{0}\" {2}>{1}</option>", Convert.ToInt32(item), Manager.StringTable.GetAssociatedValue(item),
							selectedItem.ToString() == item.ToString() ? "selected='selected'" : ""));
		}

		/// <summary>
		/// Generating empty HTML list item
		/// </summary>
		/// <returns></returns>
		public string GenerateEmptyListItem()
		{
			return "<option value=''>&nbsp;</option>";
		}

		/// <summary>
		/// Generating HTML list default item
		/// </summary>
		/// <returns></returns>
		public string GenerateDefaultListItem(bool isSelected = true)
		{
			return string.Format("<option value=''{1}>{0}</option>", Manager.StringTable["HtmlListDefaultItemLabel"], isSelected ? " selected='selected'" : "");
		}
	}
}