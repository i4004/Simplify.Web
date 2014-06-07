namespace AcspNet
{
	/// <summary>
	/// Security violation result types
	/// </summary>
	public enum SecurityViolationResult
	{
		/// <summary>
		/// OK
		/// </summary>
		Ok,
		/// <summary>
		/// Authentication required
		/// </summary>
		AuthenticationRequired,
		/// <summary>
		/// The request type (POST/GET) violated
		/// </summary>
		RequestTypeViolated
	}
}