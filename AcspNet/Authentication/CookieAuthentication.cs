namespace AcspNet.Authentication
{
	public class CookieAuthentication
	{
		//private const string CookieUserNameFieldName = "AcspUserName";
		//private const string CookieUserPasswordFieldName = "AcspUserPassword";

		///// <summary>
		///// Gets the authenticated user name from cookie.
		///// </summary>
		///// <value>
		///// The authenticated user name from cookie.
		///// </value>
		//public string UserNameFromCookie
		//{
		//	get
		//	{
		//		var cookie = Manager.Request.Cookies[CookieUserNameFieldName];

		//		if (cookie != null)
		//			return cookie.Value ?? "";

		//		return null;
		//	}
		//}

		///// <summary>
		///// Gets the authenticated user password from cookie.
		///// </summary>
		///// <value>
		///// The authenticated user password from cookie.
		///// </value>
		//public string UserPasswordFromCookie
		//{
		//	get
		//	{
		//		var cookie = Manager.Request.Cookies[CookieUserPasswordFieldName];

		//		if (cookie != null)
		//			return cookie.Value ?? "";

		//		return null;
		//	}
		//}		


		///// <summary>
		///// Create user authentication cookies (login user via cookies)
		///// </summary>
		//public void LogInCookieUser(string name, string password, bool autoLogin = false)
		//{
		//	if(string.IsNullOrEmpty(name))
		//		throw new ArgumentNullException("name");

		//	if (string.IsNullOrEmpty(password))
		//		throw new ArgumentNullException("password");

		//	var cookie = new HttpCookie(CookieUserNameFieldName, name);

		//	if (autoLogin)
		//		cookie.Expires = DateTime.Now.AddDays(256);

		//	Manager.Response.Cookies.Add(cookie);

		//	cookie = new HttpCookie(CookieUserPasswordFieldName, password);

		//	if (autoLogin)
		//		cookie.Expires = DateTime.Now.AddDays(256);

		//	Manager.Response.Cookies.Add(cookie);
		//}

		///// <summary>
		///// Remove user authentication data cookies
		///// </summary>
		//public void LogOutCookieUser()
		//{
		//	var myCookie = new HttpCookie(CookieUserNameFieldName)
		//					{
		//						Expires = DateTime.Now.AddDays(-1d)
		//					};

		//	Manager.Response.Cookies.Add(myCookie);

		//	myCookie = new HttpCookie(CookieUserPasswordFieldName)
		//				{
		//					Expires = DateTime.Now.AddDays(-1d)
		//				};

		//	Manager.Response.Cookies.Add(myCookie);

		//	IsAuthenticatedAsUser = false;
		//	AuthenticatedUserID = -1;
		//	AuthenticatedUserName = null;
		//}

		///// <summary>
		///// Checking user cookies authentication data and updating authentication status if success
		///// </summary>
		//public void AuthenticateCookieUser(int userID, string name, string password)
		//{
		//	if (userID <= 0)
		//		throw new ArgumentException("User ID is invalid");

		//	if (string.IsNullOrEmpty(name))
		//		throw new ArgumentNullException("name");

		//	if (string.IsNullOrEmpty(password))
		//		throw new ArgumentNullException("password");

		//	var userNameCookie = Manager.Request.Cookies[CookieUserNameFieldName];
		//	var userPasswordCookie = Manager.Request.Cookies[CookieUserPasswordFieldName];

		//	if (userNameCookie != null &&
		//	   userPasswordCookie != null &&
		//	   userNameCookie.Value == name &&
		//	   userPasswordCookie.Value == password)
		//	{
		//		IsAuthenticatedAsUser = true;
		//		AuthenticatedUserID = userID;
		//		AuthenticatedUserName = name;
		//	}
		//	else
		//	{
		//		Manager.Request.Cookies.Remove(CookieUserNameFieldName);
		//		Manager.Request.Cookies.Remove(CookieUserPasswordFieldName);
		//	}
		//}
	}
}