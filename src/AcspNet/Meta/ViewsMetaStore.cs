using System;
using System.Collections.Generic;

namespace AcspNet.Meta
{
	/// <summary>
	/// Loads and stores AcspNet views meta information
	/// </summary>
	public class ViewsMetaStore : IViewsMetaStore
	{
		private static IViewsMetaStore _current;
		private IList<Type> _viewsTypes;

		/// <summary>
		/// Current views meta store
		/// </summary>
		public static IViewsMetaStore Current
		{
			get
			{
				return _current ?? (_current = new ViewsMetaStore());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_current = value;
			}
		}

		/// <summary>
		/// Current domain views types
		/// </summary>
		/// <returns></returns>
		public IList<Type> ViewsTypes
		{
			get
			{
				if (_viewsTypes != null)
					return _viewsTypes;

				_viewsTypes = AcspTypesFinder.FindTypesDerivedFrom<View>();

				return _viewsTypes;
			}
		}
	}
}