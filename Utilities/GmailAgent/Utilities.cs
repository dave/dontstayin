/**************************************************************************
Gmail Agent API
Copyright (C) 2005 Johnvey Hwang, Eric Larson
http://sourceforge.net/projects/gmail-api/

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
**************************************************************************/

using System;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Johnvey.GmailAgent
{
	/// <summary>
	/// A collection of auxilliary tools used to deal with Gmail data.
	/// </summary>
	public class Utilities
	{

		/// <summary>
		/// Converts a string representation of a JavaScript array to an ArrayList() object.
		/// </summary>
		/// <param name="input">String containing a JavaScript array.</param>
		/// <returns>The converted ArrayList.</returns>
		public static ArrayList ParseJSArray(string input) {
			int outReturnOffset = 0;
			return ParseJSArrayRecurse(input, ref outReturnOffset);
		}


		/// <summary>
		/// Internal recursive sub-function to ParseJSArray
		/// </summary>
		/// <param name="input">Incoming string.</param>
		/// <param name="outReturnOffset">Reference to cursor state tracker</param>
		/// <returns></returns>
		private static ArrayList ParseJSArrayRecurse(string input, ref int outReturnOffset) {
			// instantiate output
			ArrayList output = new ArrayList();

			// state variables
			bool isQuoted = false;		// track when we are inside quotes
			string dataHold = "";		// temporary data container
			char lastCharacter = ' ';

			// loop through the entire string
			for(int i=1; i < input.Length; i++) {
				switch(input[i]) {

					case '[':	// handle start of array marker
						if(!isQuoted) {
							// recurse any nested arrays
							output.Add(ParseJSArrayRecurse(input.Substring(i), ref outReturnOffset));

							// the returning recursive function write out the total length of the characters consumed
							i += outReturnOffset;

							// assume that the last character is a closing bracket
							lastCharacter = ']';
						} else {
							dataHold += "[";
						}
						break;

					case ']':	// handle end of array marker
						if(!isQuoted) {
							if(dataHold != "") {
								output.Add(dataHold);
							}

							// determine total number of characters consumed (write to reference)
							outReturnOffset = i;
							return output;
						} else {
							dataHold += "]";
							break;
						}

					case '"':	// toggle quoted state
						if(isQuoted) {
							isQuoted = false;
						} else {
							isQuoted = true;
							lastCharacter = '"';
						}
						break;

					case ',':	// find end of element marker and add to array
						if(!isQuoted) {
							if(dataHold != "") {	// this is to filter out adding extra elements after an empty array
								output.Add(dataHold);
								dataHold = "";
							} else if(lastCharacter == '"') {	// this is to catch empty strings
								output.Add("");
							}
						} else {
							dataHold += ",";
						}
						break;
					case '\\':
                        // Check if a double quote was escaped inside a string
                        // Added by Eric Larson [larson.eric]; 5/10/2005
                        if (i < input.Length - 1)
                        {
                            switch (input[i + 1])
                            {
                                case '\\':
                                    dataHold += '\\';
                                    lastCharacter = '\\';
                                    break;
                                case '"':
                                    dataHold += '"';
                                    lastCharacter = '\"';
                                    i++;
                                    break;
                                case '\'':
                                    dataHold += '\'';
                                    lastCharacter = '\'';
                                    i++;
                                    break;
                                case 'n':
                                    dataHold += '\n';
                                    lastCharacter = '\n';
                                    i++;
                                    break;
                                case 'r':
                                    dataHold += '\r';
                                    lastCharacter = '\r';
                                    i++;
                                    break;
                                case 't':
                                    dataHold += '\t';
                                    lastCharacter = '\t';
                                    i++;
                                    break;
                                default:
                                    // TODO: the right thing to do here is to catch all the escape characters, i.e. \n, \r ... maybe another time.
                                    break;
                            }
                        }
                        break;

					default:	// regular characters are added to the data container
						dataHold += input[i];
						break;
				}
			}

			return output;
		}

		/// <summary>
		/// Removes HTML tags and decodes HTML entities.
		/// </summary>
		/// <param name="dirtyHtml">The HTML string to clean.</param>
		/// <returns>The cleansed string.</returns>
		public static string CleanHtml(string dirtyHtml) {
			string output = Regex.Replace(dirtyHtml, "<[^>]+>", "");
			output = System.Web.HttpUtility.HtmlDecode(output);
			return output;
		}
    }
}
