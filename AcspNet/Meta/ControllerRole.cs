namespace AcspNet.Meta
{
	/// <summary>
	/// Controller specific roles
	/// </summary>
	public class ControllerRole
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerRole" /> class.
		/// </summary>
		/// <param name="is403Handler">if set to <c>true</c> then indicates what contoller handles HTTP 403 errors.</param>
		/// <param name="is404Handler">if set to <c>true</c> then indicates what contoller handles HTTP 404 errors.</param>
		public ControllerRole(bool is403Handler = false, bool is404Handler = false)
		{
			Is403Handler = is403Handler;
			Is404Handler = is404Handler;
		}

		public bool Is403Handler { get; set; }
		public bool Is404Handler { get; set; }
	}
}