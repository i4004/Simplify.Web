using System;
using System.Collections.Generic;
using System.Linq;

namespace AcspNet.Meta
{
	/// <summary>
	/// Loads and stores AcspNet controllers meta information
	/// </summary>
	public class ControllersMetaStore : IControllersMetaStore
	{
		private readonly IControllerMetaDataFactory _metaDataFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
		/// </summary>
		/// <param name="metaDataFactory">The meta data factory.</param>
		public ControllersMetaStore(IControllerMetaDataFactory metaDataFactory)
		{
			_metaDataFactory = metaDataFactory;
		}

		/// <summary>
		/// Get controllers meta-data
		/// </summary>
		/// <returns></returns>
		public IList<IControllerMetaData> GetControllersMetaData()
		{
			var controllersMetaContainers = new List<IControllerMetaData>();

			var types = AcspTypesFinder.FindTypesDerivedFrom<Controller>();
			var typesToIgnore = new List<Type>();

			var ignoreContainingClass = AcspTypesFinder.GetAllTypes().FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

			if (ignoreContainingClass != null)
			{
				var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

				typesToIgnore.AddRange(((IgnoreControllersAttribute)attributes[0]).Types);
			}

			LoadMetaData(controllersMetaContainers, types, typesToIgnore);
			return SortControllersMetaContainers(controllersMetaContainers);
		}

		private void LoadMetaData(ICollection<IControllerMetaData> controllersMetaContainers, IEnumerable<Type> types, IEnumerable<Type> typesToIgnore)
		{
			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.Controller"
											   && typesToIgnore.All(x => x.FullName != t.FullName) &&
											   controllersMetaContainers.All(x => x.ControllerType != t)))
			{
				BuildControllerMetaData(controllersMetaContainers, t);
			}
		}

		private void BuildControllerMetaData(ICollection<IControllerMetaData> controllersMetaContainers, Type controllerType)
		{
			controllersMetaContainers.Add(_metaDataFactory.CreateControllerMetaData(controllerType));
		}

		private static IList<IControllerMetaData> SortControllersMetaContainers(IEnumerable<IControllerMetaData> controllersMetaContainers)
		{
			return controllersMetaContainers.OrderBy(x => x.ExecParameters == null ? 0 : x.ExecParameters.RunPriority).ToList();
		}
	}
}