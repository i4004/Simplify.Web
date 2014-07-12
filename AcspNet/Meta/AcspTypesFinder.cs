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

		private static Assembly[] _currentDomainAssemblies;
		private static IEnumerable<Type> _currentDomainAssembliesTypes;

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

		private static IEnumerable<Assembly> CurrentDomainAssemblies
		{
			get { return _currentDomainAssemblies ?? (_currentDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies()); }
		}

		private static IEnumerable<Type> CurrentDomainAssembliesTypes
		{
			get { return _currentDomainAssembliesTypes ?? (_currentDomainAssembliesTypes = GetAssembliesTypes(CurrentDomainAssemblies)); }
		}

		/// <summary>
		/// Finds the type derived from specified type in current domain assemblies.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Type FindTypeDerivedFrom<T>()
		{
			var type = typeof (T);

			return CurrentDomainAssembliesTypes.FirstOrDefault(t => t.BaseType != null && t.BaseType.FullName == type.FullName);
		}
		/// <summary>
		/// Finds the all types derived from specified type in current domain assemblies.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static IList<Type> FindTypesDerivedFrom<T>()
		{
			var type = typeof(T);

			return CurrentDomainAssembliesTypes.Where(t => t.BaseType != null && t.BaseType.FullName == type.FullName).ToList();
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