﻿using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represent language manager provider
	/// </summary>
	public interface ILanguageManagerProvider
	{
		/// <summary>
		/// Creates the language manager instance.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		void Setup(IOwinContext context);

		/// <summary>
		/// Gets the language manager.
		/// </summary>
		/// <returns></returns>
		ILanguageManager Get();
	}
}