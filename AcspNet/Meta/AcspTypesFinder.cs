using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AcspNet.Meta
{
	/// <summary>
	/// Provides AcspNet types finder among all loaded assemblies
	/// </summary>
	public static class AcspTypesFinder
	{
		private static IList<string> _excludedAssembliesPrefixes = new List<string> { "mscorlib", "System", "Microsoft", "AspNet", "AcspNet", "DotNet", "Simplify" };

		/// <summary>
		/// Gets or sets the excluded assemblies prefixes.
		/// </summary>
		/// <value>
		/// The excluded assemblies prefixes.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IList<string> ExcludedAssembliesPrefixes
		{
			get { return _excludedAssembliesPrefixes; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_excludedAssembliesPrefixes = value;
			}
		}

		public static Type FindTypeDerivedFrom<T>()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var types = GetAssembliesTypes(assemblies);

			var type = typeof (T);

			return types.FirstOrDefault(t => t.BaseType != null && t.BaseType.FullName == type.FullName);
		}

		private static IEnumerable<Type> GetAssembliesTypes(IEnumerable<Assembly> assemblies)
		{
			var types = new List<Type>();

			foreach (var assembly in assemblies.Where(assembly => !ExcludedAssembliesPrefixes.Any(prefix => assembly.FullName.StartsWith(prefix))))
				types.AddRange(assembly.GetTypes());

			return types;
		}
	}
}