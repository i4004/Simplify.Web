namespace AcspNet
{
	/// <summary>
	/// Controllers handler result types
	/// </summary>
	public enum ControllersHandlerResult
	{
		/// <summary>
		/// OK
		/// </summary>
		Ok,
		/// <summary>
		/// Execution should be stopped
		/// </summary>
		RawOutput,
		/// <summary>
		/// The HTTP 400 error should be sent to response
		/// </summary>
		Http400,
		/// <summary>
		/// The HTTP 403 error should be sent to response
		/// </summary>
		Http403,
		/// <summary>
		/// The HTTP 404 error should be sent to response
		/// </summary>
		Http404
	}
}