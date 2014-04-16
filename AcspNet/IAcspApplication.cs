using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// Represent ACSP application
	/// </summary>
	public interface IAcspApplication : IHideObjectMembers
	{
		/// <summary>
		/// Gets or sets the assembly which contains LoadExtensionsFromAssemblyOf or LoadIndividualExtensions attributes.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">value</exception>
		Assembly MainAssembly { get; set; }

		///// <summary>
		///// Gets or sets the ACSP settings.
		///// </summary>
		///// <value>
		///// The ACSP settings.
		///// </value>
		///// <exception cref="System.ArgumentNullException">value</exception>
		//IAcspSettings Settings { get; set; }

		/// <summary>
		/// Setup ACSP application.
		/// </summary>
		void SetUp();

		/////// <summary>
		/////// Creates an ACSP extensions processor.
		/////// </summary>
		/////// <param name="routeData">The route data.</param>
		/////// <returns></returns>
		////IAcspProcessor CreateProcessor(RouteData routeData);

		///// <summary>
		///// Get currently loaded executable extensions meta-data
		///// </summary>
		///// <returns></returns>
		//IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData();

		///// <summary>
		///// Gets the library extensions meta data.
		///// </summary>
		///// <returns></returns>
		//IList<LibExtensionMetaContainer> GetLibExtensionsMetaData();
	}
}