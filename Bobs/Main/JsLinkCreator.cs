using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using Common;

namespace Bobs.Main
{
	public static class JsLinkCreator
	{
		static bool initialised = false;
		static Dictionary<string, string> PathVersions = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
		static string appPath;
		public static void LoadJsFiles(string directoryPath)
		{
			if (directoryPath[0] == '\\')
			{
				
				directoryPath = directoryPath.Substring(1);
			}
			DirectoryInfo di = new DirectoryInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, directoryPath));
			
			foreach (var fileInfo in di.GetFiles("*.js"))
			{
#if DEBUG
				PathVersions[fileInfo.FullName] = ThreadSafeRandom.Next(100000000).ToString();
#else
				PathVersions[fileInfo.FullName] = ComputeHash(fileInfo, new SHA1CryptoServiceProvider());
#endif
			}
		}

		public static string GetRegisterScriptHtml(string jsPath)
		{
			string s = GetRegisterScriptUrl(jsPath);
			if (s.Length > 0)
				return String.Format("<script src='{0}'></script>", s);
			else
				return "";
		}
		public static string GetRegisterScriptUrl(string jsPath)
		{
			if (!initialised)
			{
				appPath = System.AppDomain.CurrentDomain.BaseDirectory.Substring(0, System.AppDomain.CurrentDomain.BaseDirectory.Length - 1);
				LoadJsFiles(@"\misc\SpottedScript\");
				initialised = true;
			}
			if (HttpContext.Current.Items["Scripts"] == null)
			{
				HttpContext.Current.Items.Add("Scripts", new Dictionary<string, string>());
			}
			var scripts = (Dictionary<string, string>) HttpContext.Current.Items["Scripts"];
			if (!scripts.ContainsKey(jsPath))
			{
				scripts.Add(jsPath, null);
				string path = appPath + jsPath.Replace('/', '\\');
				if (PathVersions.ContainsKey(path))
				{
					return jsPath + "?" + PathVersions[path];
				}
				else
				{
					return jsPath;
				}
			}else
			{
				return "";
			}
		}
			
		static string ComputeHash(FileInfo fileInfo, HashAlgorithm hashAlgorithm)
		{
			using (FileStream stmcheck = fileInfo.OpenRead())
			{
				byte[] hash = hashAlgorithm.ComputeHash(stmcheck);
				return BitConverter.ToString(hash).Replace("-", "");
			}
		}
	}
}
