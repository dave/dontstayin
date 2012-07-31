/* *********************************************************************
 * The contents of this file are subject to the Mozilla Public License 
 * Version 1.1 (the "License"); you may not use this file except in 
 * compliance with the License. You may obtain a copy of the License at 
 * http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an "AS IS" 
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
 * See the License for the specific language governing rights and 
 * limitations under the License.
 *
 * The Original Code is named .NET Mobile API, first released under 
 * this licence on 11th March 2009.
 * 
 * The Initial Developer of the Original Code is owned by 
 * 51 Degrees Mobile Experts Limited. Portions created by 51 Degrees 
 * Mobile Experts Limited are Copyright (C) 2009 - 2011. All Rights Reserved.
 * 
 * Contributor(s):
 *     James Rosewell <james@51degrees.mobi>
 * 
 * ********************************************************************* */

#region Usings

using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace FiftyOne.Foundation.Mobile.Detection
{
    /// <summary>
    /// Used to manipulate user agent strings prior to matching.
    /// </summary>
    public class UserAgentParser
    {
        #region Classes

        /// <summary>
        /// Filter used to identify a section of a user agent to be replaced.
        /// </summary>
        private class ReplaceFilter
        {
            private readonly Regex _regex;
            private readonly string _replacement;

            /// <summary>
            /// Constructs a new instance of <see cref="ReplaceFilter"/>.
            /// </summary>
            /// <param name="expression">Regular expression to use for the replace.</param>
            /// <param name="replacement">String to use to replace.</param>
            internal ReplaceFilter(string expression, string replacement)
            {
                _regex = new Regex(expression, RegexOptions.Compiled | RegexOptions.CultureInvariant);
                _replacement = replacement;
            }

            /// <summary>
            /// Processes the useragent passed returning a clean string.
            /// </summary>
            /// <param name="useragent">User agent string to clean.</param>
            /// <returns>Returns a clearned user agent string.</returns>
            internal string ParseString(string useragent)
            {
                return _regex.Replace(useragent, _replacement);
            }
        }

        #endregion

        #region Static Fields

        private static readonly List<ReplaceFilter> ReplaceFilters = new List<ReplaceFilter>();

        #endregion

        #region Private Methods

        /// <summary>
        /// Create the regular expressions that are used to filter out common
        /// problems with useragent strings.
        /// </summary>
        private static void InitReplaceFilters()
        {
            if (ReplaceFilters.Count != 0) return;
            {
                lock (ReplaceFilters)
                {
                    if (ReplaceFilters.Count != 0) return;
                    // Removes UP.Link from user agents strings.
                    ReplaceFilters.Add(new ReplaceFilter(@"UP.Link/[\d\.]+", ""));
                    // Removes IMEI numbers.
                    ReplaceFilters.Add(new ReplaceFilter(@"/IMEI/SN[\d|X]+|/SN[\d|X]+", ""));
                    // Removes leading and trailing spaces.
                    ReplaceFilters.Add(new ReplaceFilter(@"^\s+|\s+$", ""));
                    // Removes STM substrings.
                    ReplaceFilters.Add(new ReplaceFilter(@"STM/[\d-\.]+", ""));
                    // Removes the UNTRUSTED substring.
                    ReplaceFilters.Add(new ReplaceFilter(@"UNTRUSTED/\d\.\d", ""));
                    // Removes the [TFXXXXXX] substring from LG strings.
                    ReplaceFilters.Add(new ReplaceFilter(@"\[TF\d+\]", ""));
                    // Removes IMEI/XXXXXXXXXXXX with a preceding space substring.
                    ReplaceFilters.Add(new ReplaceFilter(@"\s+IMEI/\d+", ""));
                    // Removes any sequence of numbers longer than 5 digits.
                    ReplaceFilters.Add(new ReplaceFilter(@"\d{6,}", "X"));
                }
            }
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Check the user agent string for common errors that hinder matching. 
        /// </summary>
        /// <param name="userAgent">A useragent string to be cleaned.</param>
        /// <returns>A cleaned useragent string.</returns>
        public static string Parse(string userAgent)
        {
            InitReplaceFilters();
            foreach (ReplaceFilter filter in ReplaceFilters)
                userAgent = filter.ParseString(userAgent);
            return userAgent.Trim();
        }

        #endregion
    }
}
