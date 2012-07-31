using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace SpottedTests
{
    internal class Site
    {
        internal static Uri Uri = new Uri(ConfigurationManager.AppSettings["SpottedUri"]);
    }
}
