namespace AcspNet.Meta
{
	/// <summary>
	/// Provides controller security information
	/// </summary>
	public class ControllerSecurity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerSecurity" /> class.
		/// </summary>
		/// <param name="isAuthenticationRequired">if set to <c>true</c> then indicates what controller requires what user should be authenticated to access controller.</param>
		/// <param name="isHttpGet">if set to <c>true</c> then indicates what controller handler only HTTP GET requests.</param>
		/// <param name="isHttpPost">if set to <c>true</c>then indicates what controller handler only HTTP POST requests.</param>
		public ControllerSecurity(bool isAuthenticationRequired = false, bool isHttpGet = false, bool isHttpPost = false)
		{
			IsAuthenticationRequired = isAuthenticationRequired;
			IsHttpGet = isHttpGet;
			IsHttpPost = isHttpPost;
		}

		/// <summary>
		/// Gets or sets a value indicating whether authentication required for controller.
		/// </summary>
		/// 
		public bool IsAuthenticationRequired { get; private set; }
		/// <summary>
		/// Gets a value indicating whether controller handles only HTTP GET requests.
		/// </summary>
		public bool IsHttpGet { get; private set; }

		/// <summary>
		/// Gets a value indicating whether controller handles only HTTP POST requests.
		/// </summary>
		public bool IsHttpPost { get; private set; }		 
	}
}