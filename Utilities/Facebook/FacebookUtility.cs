using System;
using System.IO;
using System.Web.Hosting;

namespace Facebook
{
    /// <summary>Provides miscellaneous utility functionality for the client framework.</summary>
    public static class FacebookUtility
    {
        /// <summary>Gets the full physical path of the file at the specified relative path.</summary>
        /// <param name="path">A relative path to a file.</param>
        /// <returns>A <see cref="String" /> containing the full physical path of the file at the specified relative path.</returns>
        public static String MapPath(String path)
        {
            if (HostingEnvironment.IsHosted) return HostingEnvironment.MapPath("~/" + path);
            else return Path.Combine(Environment.CurrentDirectory, path);
        }
    }
}
