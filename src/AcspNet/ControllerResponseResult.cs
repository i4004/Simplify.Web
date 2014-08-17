namespace AcspNet
{
	/// <summary>
	/// Controller response result types
	/// </summary>
	public enum ControllerResponseResult
	{
		/// <summary>
		/// Default result
		/// </summary>
		Ok,
		/// <summary>
		/// Execution should be stopped, becase raw output will be sent to client
		/// </summary>
		RawOutput
	}
}
