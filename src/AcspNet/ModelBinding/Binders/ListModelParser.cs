﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides list of key value pair to model binding
	/// </summary>
	public static class ListModelParser
	{
		/// <summary>
		/// Parses list and creation a model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Parse<T>(IList<KeyValuePair<string, string>> source)
		{
			var type = typeof(T);

			var obj = Activator.CreateInstance<T>();

			foreach (var propInfo in type.GetProperties())
			{
				var propertyInfo = propInfo;
				propInfo.SetValue(obj, ParseProperty(propInfo, source.FirstOrDefault(x => x.Key == propertyInfo.Name)));
			}

			return obj;
		}

		private static object ParseProperty(PropertyInfo propertyInfo, KeyValuePair<string, string> keyValuePair)
		{
			string rawValue = null;

			if (!keyValuePair.Equals(default(KeyValuePair<string, string>)) && !string.IsNullOrEmpty(keyValuePair.Value))
				rawValue = keyValuePair.Value;

			return StringParser.ParseUndefined(rawValue, propertyInfo);
		}
	}
}