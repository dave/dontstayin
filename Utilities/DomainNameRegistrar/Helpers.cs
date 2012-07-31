using System;
using System.Collections.Generic;
using System.Text;

namespace DomainNameRegistrar
{
	public static class Helpers
	{
		#region DomainName level splitters
		/// <summary>
		/// </summary>
		/// <param name="domain">e.g. "www.dontstayin.com"</param>
		/// <returns>"dontstayin"</returns>
		public static string GetSecondLevelDomain(string domain)
		{
			string withoutTopLevel = domain.Substring(0, domain.Length - GetTopLevelDomain(domain).Length - 1);
			int lastIndexOfDot = withoutTopLevel.LastIndexOf(".");
			if (lastIndexOfDot > 0)
			{
				return withoutTopLevel.Substring(lastIndexOfDot + 1);
			}
			return withoutTopLevel;
		}

		/// <summary>
		/// </summary>
		/// <param name="domain">e.g. "www.dontstayin.com"</param>
		/// <returns>"com"</returns>
		public static string GetTopLevelDomain(string domain)
		{
			return domain.Substring(domain.LastIndexOf(".") + 1);
		}
		#endregion
	}
}
