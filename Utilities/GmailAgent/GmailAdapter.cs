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
using System.Collections;
using System.Data;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Johnvey.GmailAgent;

namespace Johnvey.GmailAgent
{
    /// <summary>
    /// Represents a set of tools used to communicate with the Gmail system.
    /// </summary>
    public class GmailAdapter
    {

        #region Enumerations
        /// <summary>
        /// Defines the result of a Gmail request.
        /// </summary>
        public enum RequestResponseType
        {

            /// <summary>
            /// The request was successful.
            /// </summary>
            Success,

            /// <summary>
            /// The Google Accounts login information did not validate.
            /// </summary>
            LoginFailed,

            /// <summary>
            /// The DataPack request was not successful.
            /// </summary>
            RefreshFailed

        }

        /// <summary>
        /// Defines the type of threads to retrieve.
        /// </summary>
        public enum ThreadFetchType
        {

            /// <summary>
            /// All unread threads.
            /// </summary>
            AllUnread,

            /// <summary>
            /// All unread inbox threads.
            /// </summary>
            Inbox

        }
        #endregion

        #region Constants
        /// <summary>
        /// Defines the URL to POST Google Accounts login information.
        /// </summary>
        // modified login url to compensate for new login procedure
        // Fix from Eric Larson [larson.eric@gmail.com]; 4/21/2005
        public const string GOOGLE_LOGIN_URL = "https://www.google.com/accounts/ServiceLoginAuth";

        /// <summary>
        /// Defines the URL to fake as the GOOGLE_LOGIN_URL's referrer. (I don't know if Google is checking this, but it can't hurt.)
        /// </summary>
        // Changed login referrer according to what is currently being used (from my tests)
        // Modified by Eric Larson [larson.eric@gmail.com]; 4/21/2005
        public const string GOOGLE_LOGIN_REFERRER_URL = "https://www.google.com/accounts/ServiceLogin?service=mail&passive=true&rm=false&continue=https%3A%2F%2Fmail.google.com%2Fmail%3Fui%3Dhtml%26zy%3Dl";

        /// <summary>
        /// Defines the base URL for Gmail requests.
        /// </summary>
        public const string GMAIL_HOST_URL = "https://mail.google.com";

        /// <summary>
        /// Defines the URL to use in the post data "continue" variable on the initial connection
        /// </summary>
        // modified login url to compensate for new login procedure
        // Fix from Eric Larson [larson.eric@gmail.com]; 4/21/2005
        public const string GOOGLE_LOGIN_CONTINUE = "https://mail.google.com/mail?";

        /// <summary>
        /// Defines the URL to use when sending an invite.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public const string GMAIL_INVITE_URL = "http://mail.google.com/mail?&view=ii";

        /// <summary>
        /// Defines the URL to fake as the GOOGLE_INVITE_URL's referrer. (I don't know if Google is checking this, but it can't hurt.)
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public const string GMAIL_INVITE_REFERRER_URL = "http://mail.google.com/mail?&ik=&search=inbox&view=tl&start=0&init=1&zx=vik357vuzj2r";

        /// <summary>
        /// Defines the URL to use when adding, editing, or deleting a label.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public const string GMAIL_LABEL_URL = "http://mail.google.com/mail?&ik=&view=up";

        /// <summary>
        /// Defines the URL to fake as the GOOGLE_LABEL_URL's referrer. (I don't know if Google is checking this, but it can't hurt.)
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public const string GMAIL_LABEL_REFERRER_URL = "http://mail.google.com/mail?&view=pr&pnl=l";

        /// <summary>
        /// Defines the URL to use when adding, editing, or deleting a filter.
        /// </summary>
        public const string GMAIL_FILTER_URL = "https://mail.google.com/mail?";

        /// <summary>
        /// Defines the URL to fake as the GOOGLE_FILTER_URL's referrer. (I don't know if Google is checking this, but it can't hurt.)
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public const string GMAIL_FILTER_REFERRER_URL = "https://mail.google.com/mail?&pnl=f";

        /// <summary>
        /// Defines the URL to use when adding or editing a contact.
        /// </summary>
        public const string GMAIL_CONTACT_URL = "http://mail.google.com/mail?&ik=&view=up";

        /// <summary>
        /// Defines the URL to use when deleting a contact.
        /// </summary>
        public const string GMAIL_CONTACT_DELETE_URL = "http://mail.google.com/mail?";

        /// <summary>
        /// Defines the URL to fake as the GOOGLE_CONTACT_URL's and GOOGLE_CONTACT_DELETE_URL's referrer. (I don't know if Google is checking this, but it can't hurt.)
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public const string GMAIL_CONTACT_REFERRER_URL = "http://mail.google.com/mail?&search=contacts&ct_id=1&cvm=2&view=ct";

        #endregion

        #region Properties
        private string _jsVersion;
        private GmailSession _session;
        private string _rawLoginResponse;
        private string _rawHomeFrameResponse;
        private string _rawDataPackResponse;
        private string _lastErrorMessage;
        private ThreadFetchType _threadFetchMode;
        private WebProxy _proxy;
        private bool _commandSuccess;
        private string _commandResponse;

        /// <summary>
        /// Gets or sets the Gmail JS engine version.
        /// </summary>
        public string JsVersion { get { return _jsVersion; } set { _jsVersion = value; } }

        /// <summary>
        /// Gets the raw HTML content returned from the Google Accounts login request.
        /// </summary>
        public string RawLoginResponse { get { return _rawLoginResponse; } }

        /// <summary>
        /// Gets the raw HTML content returned from the Gmail base launch request.
        /// </summary>
        public string RawHomeFrameResponse { get { return _rawHomeFrameResponse; } }

        /// <summary>
        /// Gets the raw HTML content returned from a DataPack request.
        /// </summary>
        public string RawDataPackResponse { get { return _rawDataPackResponse; } }

        /// <summary>
        /// Gets the last error message generated by the GmailAdapter methods.  Will be null if there are no errors.
        /// </summary>
        public string LastErrorMessage { get { return _lastErrorMessage; } }

        /// <summary>
        /// Gets or sets the <see cref="ThreadFetchType"/> for the adapter. The default is <c>Inbox</c>.
        /// </summary>
        public ThreadFetchType ThreadFetchMode { get { return _threadFetchMode; } set { _threadFetchMode = value; } }

        /// <summary>
        /// Gets or sets the proxy for HTTP requests.  Leave null for no proxy support.
        /// </summary>
        public WebProxy Proxy { get { return this._proxy; } set { this._proxy = value; } }

        /// <summary>
        /// Gets or sets the Gmail success status of the current request.
        /// </summary>
        public bool CommandSuccess { get { return _commandSuccess; } set { _commandSuccess = value; } }

        /// <summary>
        /// Gets or sets the Gmail success message of the current request.
        /// </summary>
        public string CommandResponse { get { return _commandResponse; } set { _commandResponse = value; } }

        #endregion

        /// <summary>
        /// Initializes a new instance of the GmailAdapter class.
        /// </summary>
        public GmailAdapter()
        {
            /**********************************************************************
			 * These ServicePointManager settings are here because the .NET
			 * Framework (1.0 and 1.1) don't like to play well with other web
			 * servers.  Some of these are arbitrary hacks that have been
			 * suggested over the newsgroups.  The one that seems to work
			 * constistently is using TLS instead of SSL3.  Go figure.
			 * NOTE: the Expect100Continue property is not supportted in .NET 1.0.
			 * *******************************************************************/
            // ServicePointManager.CertificatePolicy = new GmailCertificatePolicy();
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            this._lastErrorMessage = null;
            this._threadFetchMode = ThreadFetchType.Inbox;
            this._proxy = null;
        }

        /// <summary>
        /// Represents a delegate for the <see cref="Refresh"/> method.
        /// </summary>
        public delegate RequestResponseType RefreshDelegate(GmailSession session);

        /// <summary>
        /// Queries Gmail to get latest mailbox information.
        /// </summary>
        /// <param name="session">The <see cref="GmailSession"/> object to query.</param>
        /// <returns>The <see cref="RequestResponseType"/>.</returns>
        public RequestResponseType Refresh(GmailSession session)
        {
            // bring focus to active session
            this._session = session;

            // make sure there is proper login information
            if (this._session.Username == "" || this._session.Username == null || this._session.Password == "" || this._session.Password == null)
            {
                return RequestResponseType.LoginFailed;
            }

            // if it's been a while since we logged in, do it again to keep the cookie fresh
            if (session.LastLoginTime == new DateTime(0) || session.LastLoginTime < DateTime.Now.AddHours(-1))
            {
                if (Login())
                {
                    return RequestResponseType.Success;
                }
                else
                {
                    return RequestResponseType.LoginFailed;
                }
            }
            else
            {
                if (RefreshDataPack(false))
                {
                    return RequestResponseType.Success;
                }
                else
                {
                    return RequestResponseType.RefreshFailed;
                }
            }

        }

        /// <summary>
        /// Sends Google Accounts login stored in the current <see cref="GmailSession"/> and establishes a session with Gmail.
        /// </summary>
        /// <returns>True if login was successful; false otherwise.</returns>
        private bool Login()
        {
            // grab the user's cookie store
            CookieCollection cookieJar = this._session.Cookies;

            // put some cookies in it
            GmailCookieFactory tollHouse = new GmailCookieFactory();
            cookieJar.Add(tollHouse.GenerateCookie("GMAIL_LOGIN"));
            cookieJar.Add(tollHouse.GenerateCookie("TZ"));

            // instantiate the key pieces
            Uri location;
            string rawResponse;
            int currentCursor;

            /**********************************************************************
			 * Login to Google Accounts
			 * -- parse response to get the GV cookie (don't know what it's for)
			 * *******************************************************************/
            // modified post data to compensate for new login procedure
            // service=mail is necessary
            // rm=false is not necessary, but appeared during testing so I left it
            // Fix from Eric Larson [larson.eric@gmail.com]; 4/21/2005
            string loginPostData = "continue=" + System.Web.HttpUtility.UrlEncode(GOOGLE_LOGIN_CONTINUE)
                                + "&service=mail"
                                + "&rm=false"
                                + "&Email=" + System.Web.HttpUtility.UrlEncode(this._session.Username)
                                + "&Passwd=" + System.Web.HttpUtility.UrlEncode(this._session.Password)
                                + "&null=Sign+in";
            location = new Uri(GOOGLE_LOGIN_URL);

            // try the request; catch any unhandled exceptions
            try
            {
                rawResponse = MakeWebRequest(location, "POST", GOOGLE_LOGIN_REFERRER_URL, loginPostData, false);

            }
            catch (Exception ex)
            {
                this._session.HasConnectionError = true;
                this._lastErrorMessage = "Unable to log in to Google Accounts: " + ex.Message;
                return false;
            }

            // catch empty response and bad login
            if (rawResponse == null || rawResponse == "")
            {
                this._session.HasConnectionError = true;
                this._lastErrorMessage = "Unable to log in to Google Accounts: empty response document!";
                return false;
            }
            else if (Regex.Match(rawResponse, "password.+not.+match", RegexOptions.Compiled).Success)
            {
                this._session.HasConnectionError = true;
                this._lastErrorMessage = "Unable to log in to Google Accounts: Username (" + this._session.Username + ") and password do not match.";
                return false;
            }

            this._rawLoginResponse = rawResponse;

            // get _sgh variable from login response
            string var_sgh = "";
            currentCursor = rawResponse.IndexOf("_sgh");
            if (currentCursor > -1)
            {
                int varDeclStart = rawResponse.IndexOf("_sgh%3D", currentCursor) + 7;
                int varDeclEnd = rawResponse.IndexOf("&", varDeclStart + 5, 40);
                var_sgh = rawResponse.Substring(varDeclStart, varDeclEnd - varDeclStart);
            }
            else
            {
                this._lastErrorMessage = "Unable to find _sgh GET variable.";
            }

            /**********************************************************************
			 * Request Gmail home application frame
			 * -- store GMAIL_AT and S cookies passed in the header
			 * -- parse response to get Gmail engine version (jsVersion)
			 * *******************************************************************/
            location = new Uri(GMAIL_HOST_URL + "/mail?_sgh=" + var_sgh);

            try
            {
                rawResponse = MakeWebRequest(location, "GET", GOOGLE_LOGIN_URL, null, false);
            }
            catch (Exception ex)
            {
                this._lastErrorMessage = "Error retrieving Gmail home frame page: " + ex.Message;
                return false;
            }

            this._rawHomeFrameResponse = rawResponse;

            // get JS version ID
            this._jsVersion = "jsVersionNotFound";
            Match m = Regex.Match(rawResponse, @"\&ver=([a-z0-9]+)", RegexOptions.Compiled);
            if (m.Success)
            {
                this._jsVersion = m.Groups[1].Value;
            }
            else
            {
                this._lastErrorMessage = "Unable to find JS Gmail engine version.";
            }

            /**********************************************************************
			 * Request initial dataPack page and extract mailbox information
			 * *******************************************************************/
            switch (this._threadFetchMode)
            {
                case ThreadFetchType.AllUnread:
                    location = new Uri(GMAIL_HOST_URL + "/mail?search=query&q=is%3Aunread&view=tl&start=0&init=1&zx=" + MakeUniqueUrl());
                    break;
                case ThreadFetchType.Inbox:
                    location = new Uri(GMAIL_HOST_URL + "/mail?search=inbox&view=tl&start=0&init=1&zx=" + MakeUniqueUrl());
                    break;
                default:
                    break;
            }

            try
            {
                rawResponse = MakeWebRequest(location, "GET", null, null, false);
            }
            catch (Exception ex)
            {
                this._session.HasConnectionError = true;
                this._lastErrorMessage = "Unable to retrieve initial DataPack document: " + ex.Message;
                return false;
            }

            if (rawResponse == "" || rawResponse == null)
            {
                this._session.HasConnectionError = true;
                this._lastErrorMessage = "Initial DataPack document did not contain any data.";
                return false;
            }

            Debug.WriteLine(rawResponse);

            this._rawDataPackResponse = rawResponse;

            // mark login time
            this._session.LastLoginTime = DateTime.Now;

            // parse the mailbox information
            ParseDataPack();
            this._session.LastRefreshTime = DateTime.Now;

            this._session.HasConnectionError = false;
            return true;
        }

        /// <summary>
        /// Requests the auto-refresh DataPack.
        /// </summary>
        /// <remarks>
        /// If the threadlist timestamp has not changed, Gmail will only send a short DataPack.
        /// </remarks>
        /// <param name="forceRefresh">DEBUG: Indicates whether to pass an old timestamp, which forces Gmail to resend a full DataPack.</param>
        public bool RefreshDataPack(bool forceRefresh)
        {
            string tlt;

            // DEBUG: setting the timestamp to an older time forces Gmail to return a full DataPack
            if (forceRefresh)
            {
                tlt = "fd44c8cfc2";
            }
            else
            {
                tlt = this._session.ThreadListTimestamp;
            }

            string fp = this._session.Fingerprint;

            Uri location = null;
            switch (this._threadFetchMode)
            {
                case ThreadFetchType.AllUnread:
                    location = new Uri(GMAIL_HOST_URL + "/mail?view=tl&search=query&start=0&q=is%3Aunread&tlt=" + tlt + "&fp=" + fp + "&auto=1&zx=" + MakeUniqueUrl());
                    break;
                case ThreadFetchType.Inbox:
                    location = new Uri(GMAIL_HOST_URL + "/mail?view=tl&search=inbox&start=0&tlt=" + tlt + "&fp=" + fp + "&auto=1&zx=" + MakeUniqueUrl());
                    break;
                default:
                    break;
            }

            try
            {
                this._rawDataPackResponse = MakeWebRequest(location, "GET", "http://mail.google.com/mail/html/hist2.html", null, false);
            }
            catch (Exception ex)
            {
                this._session.HasConnectionError = true;
                this._lastErrorMessage = "Unable to refresh DataPack document: " + ex.Message;
                return false;
            }

            if (this._rawDataPackResponse == "" || this._rawDataPackResponse == null)
            {
                this._lastErrorMessage = "Initial DataPack document did not contain any data.";
                return false;
            }

            ParseDataPack();
            this._session.LastRefreshTime = DateTime.Now;

            return true;
        }

        #region Contacts
        /// <summary>
        /// Retrieves all the contacts in the user's Gmail address book.
        /// </summary>
        /// <returns>A <see cref="GmailContactCollection"/> of contacts in address book.</returns>
        public GmailContactCollection GetContacts()
        {
            // instantiate output vars
            GmailContactCollection output = new GmailContactCollection();

            // Added the IK the URI to the fit the current protocol
            // Fix by Eric Larson [larson.eric@gmail.com]; 5/2/2005
            Uri location = new Uri(GMAIL_HOST_URL + "/mail?&ik=" + System.Web.HttpUtility.UrlEncode(this._session.IdentificationKey) + "&view=cl&search=contacts&pnl=a&zx=" + MakeUniqueUrl());
            this._rawDataPackResponse = MakeWebRequest(location, "GET", null, null, true);

            // sanitize the incoming _rawDataPackResponse
            this._rawDataPackResponse = this._rawDataPackResponse.Replace("\n", "");

            if (this._rawDataPackResponse.Length > 128)
            {
                // Looping through the data pack response looking for multiple arrays
                // Gmail only puts 15 address in a single array, so search all 'a' (address) arrays.
                // Fix by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                //int addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"a\"");

                // Fix by Billy Roebuck [billyroebuck@yahoo.com.au];  6/24/2005
                // Changed the line below (and again at the end of this method) and some of the offset values
                //int addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"a\"");
                int addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"cl\",");

                while (addressBlockStart > -1)
                {
                    // find the address block
                    addressBlockStart = this._rawDataPackResponse.IndexOf("[", addressBlockStart + 6);
                    int addressBlockEnd = this._rawDataPackResponse.IndexOf("]]);", addressBlockStart) + 1;

                    string addressBlock = "[" + this._rawDataPackResponse.Substring(addressBlockStart, addressBlockEnd - addressBlockStart) + "]";
                    //addressBlock = addressBlock.Replace("\\\\\\\"", "\"");

                    // parse the address block into an ArrayList
                    ArrayList addresses = Utilities.ParseJSArray(addressBlock);

                    // loop through ArrayList of contacts and insert into collection
                    foreach (ArrayList contact in addresses)
                    {
                        GmailContact tmpContact = new GmailContact();

                        // Using GmailContact Indeces enum for easy changing
                        // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005

                        // Store the contact ID so that we can edit or delete it.
                        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                        tmpContact.id = int.Parse((string)contact[(int)GmailContact.Indeces.id], System.Globalization.NumberStyles.HexNumber);

                        tmpContact.Email = contact[(int)GmailContact.Indeces.Email].ToString();
                        tmpContact.Name = contact[(int)GmailContact.Indeces.DefaultName].ToString();
                        if (contact.Count > (int)GmailContact.Indeces.Notes)
                        {
                            tmpContact.Notes = contact[(int)GmailContact.Indeces.Notes].ToString();
                        }

                        // EmailUnescaped is no longer sent by Gmail, so fake it.
                        // to keep backwards compatibility with 0.6.1 and before
                        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                        tmpContact.EmailUnescaped = System.Web.HttpUtility.HtmlEncode(contact[(int)GmailContact.Indeces.Email].ToString());

                        // Frequently mailed can no longer be be determined in a single request.
                        // So initialize all contacts to false and then make second request later.
                        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                        tmpContact.IsFrequentlyMailed = false;

                        output.Add(tmpContact);
                    }

                    // Check to see if there is another block of contacts.
                    // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                    //addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"a\"", addressBlockStart);
                    // Fix by Billy Roebuck [billyroebuck@yahoo.com.au];  6/24/2005
                    addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"cl\",", addressBlockStart);
                } ;
            }

            // Pass the current list of contacts to a helper function to find the frequently mailed contacts.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
            GetFrequentlyMailedContacts(output);

            return output;
        }

        /// <summary>
        /// Updates IsFrequentlyMailed flag in the contacts collection.
        /// If the contact is not found in the current collection, it is added.
        /// Helper function of GetContacts to update the contacts collection.
        /// </summary>
        /// <param name="contacts">List of Gmail contacts; should be all contacts.</param>
        /// <returns>A <see cref="GmailContactCollection"/> of contacts in address book.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        private void GetFrequentlyMailedContacts(GmailContactCollection contacts)
        {
            //frequently mailed
            Uri location = new Uri(GMAIL_HOST_URL + "/mail?&ik=" + System.Web.HttpUtility.UrlEncode(this._session.IdentificationKey) + "&view=cl&search=contacts&pnl=d&zx=" + MakeUniqueUrl());
            this._rawDataPackResponse = MakeWebRequest(location, "GET", null, null, true);

            // sanitize the incoming _rawDataPackResponse
            this._rawDataPackResponse = this._rawDataPackResponse.Replace("\n", "");

            if (this._rawDataPackResponse.Length > 128)
            {
                // find the beginning of the address block
                // Fix by Billy Roebuck [billyroebuck@yahoo.com.au];  6/24/2005
                // Changed the line below (and again at the end of this method) and some of the offset values
                //int addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"a\"");
                int addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"cl\",");

                // make multiple passes to parse all address arrays
                while (addressBlockStart > -1)
                {
                    // find the address block beginning and end
                    addressBlockStart = this._rawDataPackResponse.IndexOf("[", addressBlockStart + 8);
                    int addressBlockEnd = this._rawDataPackResponse.IndexOf("]]);", addressBlockStart) + 2;

                    // Need to enclose address block in quotes for ParseJSArray to parse it correctly.
                    // Fixed by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                    // get the address block
                    string addressBlock = "[" + this._rawDataPackResponse.Substring(addressBlockStart, addressBlockEnd - addressBlockStart) + "]";

                    // parse the address block into an ArrayList
                    ArrayList addresses = Utilities.ParseJSArray(addressBlock);

                    // loop through ArrayList of contacts and insert into collection
                    foreach (ArrayList contact in addresses)
                    {
                        // Using GmailContact Indeces enum for easy changing
                        // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005

                        GmailContact currentContact = new GmailContact();
                        int i;

                        // loop through current contacts to update conatct collection
                        for (i = 0; i < contacts.Count; i++)
                        {
                            // Check the contact list to see if the frequently mailed contact is in the collection
                            currentContact = (GmailContact)contacts[i];
                            if (currentContact.id == int.Parse((string)contact[(int)GmailContact.Indeces.id], System.Globalization.NumberStyles.HexNumber))
                            {
                                currentContact.IsFrequentlyMailed = true;
                                break;
                            }
                        }

                        // If contact was not in the collection, add it
                        if (i == contacts.Count)
                        {
                            GmailContact tmpContact = new GmailContact();
                            tmpContact.id = int.Parse((string)contact[(int)GmailContact.Indeces.id], System.Globalization.NumberStyles.HexNumber);
                            tmpContact.Email = contact[(int)GmailContact.Indeces.Email].ToString();
                            tmpContact.Name = contact[(int)GmailContact.Indeces.DefaultName].ToString();
                            if (contact.Count > (int)GmailContact.Indeces.Notes)
                            {
                                tmpContact.Notes = contact[(int)GmailContact.Indeces.Notes].ToString();
                            }

                            // to keep backwards compatibility with 0.6.1 and before
                            tmpContact.EmailUnescaped = System.Web.HttpUtility.HtmlEncode(contact[(int)GmailContact.Indeces.Email].ToString());
                            tmpContact.IsFrequentlyMailed = true;

                            contacts.Add(tmpContact);
                        }
                    }

                    // Check to see if there is another block of contacts.
                    //addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"a\"", addressBlockStart);
                    // Fix by Billy Roebuck [billyroebuck@yahoo.com.au];  6/24/2005
                    addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"cl\",", addressBlockStart);
                } ;
            }
            return;
        }

        /// <summary>
        /// Retrieves all the frequently mailed contacts in the user's Gmail address book.
        /// </summary>
        /// <returns>A <see cref="GmailContactCollection"/> of frequently mailed contacts in address book.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        // Can be used in place of GetContacts if you don't care about all the contacts.
        public GmailContactCollection GetFrequentlyMailedContacts()
        {
            // instantiate output vars
            GmailContactCollection output = new GmailContactCollection();

            Uri location = new Uri(GMAIL_HOST_URL + "/mail?&ik=" + System.Web.HttpUtility.UrlEncode(this._session.IdentificationKey) + "&view=cl&search=contacts&pnl=d&zx=" + MakeUniqueUrl());
            this._rawDataPackResponse = MakeWebRequest(location, "GET", null, null, true);

            // sanitize the incoming _rawDataPackResponse
            this._rawDataPackResponse = this._rawDataPackResponse.Replace("\n", "");

            if (this._rawDataPackResponse.Length > 128)
            {
                // find the beginning of the address block
                //int addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"a\"");
                // Fix by Billy Roebuck [billyroebuck@yahoo.com.au];  6/24/2005
                int addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"cl\",");                 

                // make multiple passes to parse all address arrays
                while (addressBlockStart > -1)
                {
                    // find the address block beginning and end
                    addressBlockStart = this._rawDataPackResponse.IndexOf("[", addressBlockStart + 8);
                    int addressBlockEnd = this._rawDataPackResponse.IndexOf("]]);", addressBlockStart) + 2;

                    // get the address block
                    string addressBlock = this._rawDataPackResponse.Substring(addressBlockStart, addressBlockEnd - addressBlockStart);

                    // parse the address block into an ArrayList
                    ArrayList addresses = Utilities.ParseJSArray(addressBlock);

                    // loop through ArrayList of contacts and insert into collection
                    foreach (ArrayList contact in addresses)
                    {
                        GmailContact currentContact = new GmailContact();

                        currentContact.id = int.Parse((string)contact[0], System.Globalization.NumberStyles.HexNumber);

                        GmailContact tmpContact = new GmailContact();
                        tmpContact.id = int.Parse((string)contact[0], System.Globalization.NumberStyles.HexNumber);
                        tmpContact.Email = contact[3].ToString();
                        tmpContact.Name = contact[1].ToString();
                        if (contact.Count > 4)
                        {
                            tmpContact.Notes = contact[4].ToString();
                        }

                        // to keep backwards compatibility with 0.6.1 and before
                        tmpContact.EmailUnescaped = System.Web.HttpUtility.HtmlEncode(contact[3].ToString());
                        tmpContact.IsFrequentlyMailed = true;

                        output.Add(tmpContact);
                    }

                    // Check to see if there is another block of contacts.
                    //addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"a\"", addressBlockStart);
                    // Fix by Billy Roebuck [billyroebuck@yahoo.com.au];  6/24/2005
                    addressBlockStart = this._rawDataPackResponse.IndexOf("D([\"cl\",", addressBlockStart); 
                } ;
            }

            return output;
        }

        /// <summary>
        /// Adds a contact into the address book. Emails that already exist will be updated with the new information.
        /// </summary>
        /// <param name="name">Contact display name.</param>
        /// <param name="email">Contact email address.</param>
        /// <param name="notes">Optional notes.</param>
        /// <returns>True if Gmail accepted the command; false otherwise.</returns>
        public bool AddContact(string name, string email, string notes)
        {
            // Limit labels to the 100 characters.
            // Not sure if 100 is correct, I need to test actual limit.
            if (name.Length > 100) name = name.Substring(0, 100);
            if (email.Length > 100) email = email.Substring(0, 100);

            string contactPostData = "act=ec"
                + "&at=" + this._session.Cookies["GMAIL_AT"].Value
                + "&ct_id=-1"
                + "&ct_nm=" + HttpUtility.UrlEncode(name)
                + "&ct_em=" + HttpUtility.UrlEncode(email)
                + "&ctf_n=" + HttpUtility.UrlEncode(notes);

            Uri location = new Uri(GMAIL_CONTACT_URL);
            this._rawDataPackResponse = MakeWebRequest(location, "POST", GMAIL_CONTACT_REFERRER_URL, contactPostData, false);

            // Parse the data pack to determine if contact was stored successfully.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }

        /// <summary>
        /// Edits a contact in the address book.
        /// </summary>
        /// <param name="id">ID of the contact on Gmail system.</param>
        /// <param name="name">Contact display name.</param>
        /// <param name="email">Contact email address.</param>
        /// <param name="notes">Optional notes.</param>
        /// <returns>True if Gmail accepted the command; false otherwise.</returns>
        /// // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool EditContact(int id, string name, string email, string notes)
        {
            if (id > -1)
            {
                string contactPostData = "act=ec"
                    + "&at=" + this._session.Cookies["GMAIL_AT"].Value
                    + "&ct_id=" + id
                    + "&ct_nm=" + System.Web.HttpUtility.UrlEncode(name)
                    + "&ct_em=" + System.Web.HttpUtility.UrlEncode(email)
                    + "&ctf_n=" + System.Web.HttpUtility.UrlEncode(notes);

                Uri location = new Uri(GMAIL_CONTACT_URL);
                this._rawDataPackResponse = MakeWebRequest(location, "POST", GMAIL_LABEL_REFERRER_URL, contactPostData, false);

                // Parse the data pack to determine if contact was edited successfully.
                // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
                ParseDataPack();

                // Return Gmail's response to whether the request was successful.
                // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
                return this.CommandSuccess;
            }

            else
                return false;
        }

        /// <summary>
        /// Edits a contact in the address book.
        /// </summary>
        /// <param name="oldEmail">Email of the contact as it is on Gmail.</param>
        /// <param name="name">Contact display name.</param>
        /// <param name="email">Contact email address.</param>
        /// <param name="notes">Optional notes.</param>
        /// <returns>True if Gmail accepted the command; false otherwise.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool EditContact(string oldEmail, string name, string email, string notes)
        {
            // loop through contacts to find the old email address
            foreach (GmailContact contact in this._session.Contacts)
            {
                if (oldEmail == contact.Email)
                {
                    // if Gmail provided an ID for this contact, pass it off to other EditContact function
                    if (contact.id > -1)
                        return EditContact(contact.id, name, email, notes);
                    else
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Delete a contact from the address book.
        /// </summary>
        /// <param name="id">ID of the contact to delete.</param>
        /// <returns>True if Gmail accepted the command; false otherwise.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool DeleteContact(int id)
        {
            if (id > -1)
            {
                string contactGetData = "&ik=" + this._session.IdentificationKey
                    + "&search=contacts"
                    + "&ct_id=" + Convert.ToString(id, 16)
                    + "&cvm=2"
                    + "&view=up"
                    + "&act=dc"
                    + "&at=" + this._session.Cookies["GMAIL_AT"].Value
                    + "&c=" + Convert.ToString(id, 16)
                    + "&zx=" + MakeUniqueUrl();

                Uri location = new Uri(GMAIL_CONTACT_DELETE_URL + contactGetData);
                this._rawDataPackResponse = MakeWebRequest(location, "GET", GMAIL_LABEL_REFERRER_URL, null, false);

                // Parse the data pack to determine if contact was deleted successfully.
                // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
                ParseDataPack();

                // Return Gmail's response to whether the request was successful.
                // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
                return this.CommandSuccess;
            }

            else
                return false;
        }

        /// <summary>
        /// Delete a contact from the address book.
        /// </summary>
        /// <param name="email">Email of the contact to delete.</param>
        /// <returns>True if Gmail accepted the command; false otherwise.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool DeleteContact(string email)
        {
            foreach (GmailContact contact in this._session.Contacts)
            {
                if (email == contact.Email)
                {
                    if (contact.id > -1)
                        return DeleteContact(contact.id);
                    else
                        return false;
                }
            }

            return false;
        }
        #endregion

        #region Filters
        /// <summary>
        /// Retrieves all the filters in the user's Gmail account.
        /// </summary>
        /// <returns>A <see cref="GmailFilterCollection"/> of filters.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public GmailFilterCollection GetFilters()
        {
            // instantiate output vars
            GmailFilterCollection output = new GmailFilterCollection();

            Uri location = new Uri(GMAIL_HOST_URL + "/mail?&ik=" + System.Web.HttpUtility.UrlEncode(this._session.IdentificationKey) + "&view=pr&pnl=f&zx=" + MakeUniqueUrl());
            this._rawDataPackResponse = MakeWebRequest(location, "GET", null, null, false);

            // sanitize the incoming _rawDataPackResponse
            this._rawDataPackResponse = this._rawDataPackResponse.Replace("\n", "");

            if (this._rawDataPackResponse.Length > 128)
            {
                int filterBlockStart = this._rawDataPackResponse.IndexOf("D([\"fi\"");

                if (filterBlockStart > -1)
                {
                    // find the filter block
                    filterBlockStart = this._rawDataPackResponse.IndexOf("[", filterBlockStart + 7);
                    int filterBlockEnd = this._rawDataPackResponse.IndexOf("]]);", filterBlockStart) + 1;

                    string filterBlock = this._rawDataPackResponse.Substring(filterBlockStart, filterBlockEnd - filterBlockStart);

                    // parse the filter block into an ArrayList
                    ArrayList filters = Utilities.ParseJSArray(filterBlock);

                    // loop through ArrayList of Filters and insert into collection
                    foreach (ArrayList Filter in filters)
                    {
                        // Using GmailFilter Indeces enum for easy changing
                        // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005

                        GmailFilter tmpFilter = new GmailFilter();

                        tmpFilter.id = Int64.Parse((string)Filter[(int)GmailFilter.Indeces.id]);
                        tmpFilter.Name = (string)Filter[(int)GmailFilter.Indeces.Name];

                        // Get subarray of filter settings
                        ArrayList tmpArray = (ArrayList)Filter[(int)GmailFilter.Indeces.SubArray];
                        tmpFilter.From = (string)tmpArray[(int)GmailFilter.SubIndeces.From];
                        tmpFilter.To = (string)tmpArray[(int)GmailFilter.SubIndeces.To];
                        tmpFilter.Subject = (string)tmpArray[(int)GmailFilter.SubIndeces.Subject];
                        tmpFilter.HasWords = (string)tmpArray[(int)GmailFilter.SubIndeces.HasWords];
                        tmpFilter.DoesntHave = (string)tmpArray[(int)GmailFilter.SubIndeces.DoesntHave];
                        tmpFilter.HasAttachment = tmpArray[(int)GmailFilter.SubIndeces.HasAttachment].ToString().Length > 0 && bool.Parse((string)tmpArray[(int)GmailFilter.SubIndeces.HasAttachment]);
                        tmpFilter.SkipInbox = tmpArray[(int)GmailFilter.SubIndeces.SkipInbox].ToString().Length > 0 && bool.Parse((string)tmpArray[(int)GmailFilter.SubIndeces.SkipInbox]);
                        tmpFilter.StarIt = tmpArray[(int)GmailFilter.SubIndeces.StarIt].ToString().Length > 0 && bool.Parse((string)tmpArray[(int)GmailFilter.SubIndeces.StarIt]);
                        tmpFilter.ApplyLabel = tmpArray[(int)GmailFilter.SubIndeces.ApplayLabel].ToString().Length > 0 && bool.Parse((string)tmpArray[(int)GmailFilter.SubIndeces.ApplayLabel]);
                        tmpFilter.LabelToApply = (string)tmpArray[(int)GmailFilter.SubIndeces.LabelToApply];
                        tmpFilter.Forward = tmpArray[(int)GmailFilter.SubIndeces.Forward].ToString().Length > 0 && bool.Parse((string)tmpArray[(int)GmailFilter.SubIndeces.Forward]);
                        tmpFilter.ForwardTo = (string)tmpArray[(int)GmailFilter.SubIndeces.ForwardTo];
                        tmpFilter.MoveToTrash = tmpArray[(int)GmailFilter.SubIndeces.MoveToTrash].ToString().Length > 0 && bool.Parse((string)tmpArray[(int)GmailFilter.SubIndeces.MoveToTrash]);

                        // I'm not sure why the following fields are sent twice.
                        // If Gmail changes the filter format, these may need to be used.
                        //Filter[GmailFilter.Indeces.StarIt] = StarIt
                        //Filter[GmailFilter.Indeces.LabelToApply] = LabelToApply
                        //Filter[GmailFilter.Indeces.MoveToTrash] = Move To Trash
                        //Filter[GmailFilter.Indeces.MoveToTrash2] = Move To Trash
                        //Filter[GmailFilter.Indeces.ForwardTo] = ForwardTo
                        //Filter[GmailFilter.Indeces.PerpetualFalse] = always seems to be false

                        output.Add(tmpFilter);
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Adds a filter into the set of Gmail filters.
        /// </summary>
        /// <returns>A <see cref="bool"/> true if completed successfully.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool AddFilter(GmailFilter filter)
        {
            string strLocation = GMAIL_FILTER_URL
                + "&ik=" + this._session.IdentificationKey
                + "&view=pr"
                + "&pnl=f"
                + "&at=" + this._session.Cookies["GMAIL_AT"].Value
                + "&act=cf"
                + "&cf_t=cf"
                + "&cf1_from=" + System.Web.HttpUtility.UrlEncode(filter.From)
                + "&cf1_to=" + System.Web.HttpUtility.UrlEncode(filter.To)
                + "&cf1_subj=" + System.Web.HttpUtility.UrlEncode(filter.Subject)
                + "&cf1_has=" + System.Web.HttpUtility.UrlEncode(filter.HasWords)
                + "&cf1_hasnot=" + System.Web.HttpUtility.UrlEncode(filter.DoesntHave)
                + "&cf1_attach=" + System.Web.HttpUtility.UrlEncode(filter.HasAttachment.ToString().ToLower())
                + "&cf2_ar=" + System.Web.HttpUtility.UrlEncode(filter.SkipInbox.ToString().ToLower())
                + "&cf2_st=" + System.Web.HttpUtility.UrlEncode(filter.StarIt.ToString().ToLower())
                + "&cf2_cat=" + System.Web.HttpUtility.UrlEncode(filter.ApplyLabel.ToString().ToLower())
                + "&cf2_sel=" + System.Web.HttpUtility.UrlEncode(filter.LabelToApply)
                + "&cf2_emc=" + System.Web.HttpUtility.UrlEncode(filter.Forward.ToString().ToLower())
                + "&cf2_email=" + System.Web.HttpUtility.UrlEncode(filter.ForwardTo)
                + "&cf2_tr=" + System.Web.HttpUtility.UrlEncode(filter.MoveToTrash.ToString().ToLower())
                + "&zx=" + MakeUniqueUrl();
            Uri location = new Uri(strLocation);

            string referrer = GMAIL_FILTER_REFERRER_URL
                + "&pnl=f"
                + "&ik=" + this._session.IdentificationKey
                + "&search=cf"
                + "&view=tl"
                + "&start=0"
                + "&cf_f=cf1"
                + "&cf_t=cf2"
                + "&cf1_from=" + System.Web.HttpUtility.UrlEncode(filter.From)
                + "&cf1_to=" + System.Web.HttpUtility.UrlEncode(filter.To)
                + "&cf1_subj=" + System.Web.HttpUtility.UrlEncode(filter.Subject)
                + "&cf1_has=" + System.Web.HttpUtility.UrlEncode(filter.HasWords)
                + "&cf1_hasnot=" + System.Web.HttpUtility.UrlEncode(filter.DoesntHave)
                + "&cf1_attach=" + System.Web.HttpUtility.UrlEncode(filter.HasAttachment.ToString().ToLower())
                + "&zx=" + MakeUniqueUrl();

            this._rawDataPackResponse = MakeWebRequest(location, "GET", referrer, string.Empty, false);

            // Parse the data pack to determine if filter was stored successfully.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }

        /// <summary>
        /// Edits a filter into the set of Gmail filters.
        /// </summary>
        /// <returns>A <see cref="bool"/> true if completed successfully.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool EditFilter(GmailFilter filter)
        {
            string strLocation = GMAIL_FILTER_URL
                + "&ik=" + this._session.IdentificationKey
                + "&view=pr"
                + "&pnl=f"
                + "&at=" + this._session.Cookies["GMAIL_AT"].Value
                + "&act=rf"
                + "&cf_t=rf"
                + "&cf1_from=" + System.Web.HttpUtility.UrlEncode(filter.From)
                + "&cf1_to=" + System.Web.HttpUtility.UrlEncode(filter.To)
                + "&cf1_subj=" + System.Web.HttpUtility.UrlEncode(filter.Subject)
                + "&cf1_has=" + System.Web.HttpUtility.UrlEncode(filter.HasWords)
                + "&cf1_hasnot=" + System.Web.HttpUtility.UrlEncode(filter.DoesntHave)
                + "&cf1_attach=" + System.Web.HttpUtility.UrlEncode(filter.HasAttachment.ToString().ToLower())
                + "&cf2_ar=" + System.Web.HttpUtility.UrlEncode(filter.SkipInbox.ToString().ToLower())
                + "&cf2_st=" + System.Web.HttpUtility.UrlEncode(filter.StarIt.ToString().ToLower())
                + "&cf2_cat=" + System.Web.HttpUtility.UrlEncode(filter.ApplyLabel.ToString().ToLower())
                + "&cf2_sel=" + System.Web.HttpUtility.UrlEncode(filter.LabelToApply)
                + "&cf2_emc=" + System.Web.HttpUtility.UrlEncode(filter.Forward.ToString().ToLower())
                + "&cf2_email=" + System.Web.HttpUtility.UrlEncode(filter.ForwardTo)
                + "&cf2_tr=" + System.Web.HttpUtility.UrlEncode(filter.MoveToTrash.ToString().ToLower())
                + "&ofid=" + System.Web.HttpUtility.UrlEncode(filter.id.ToString())
                + "&zx=" + MakeUniqueUrl();
            Uri location = new Uri(strLocation);

            string referrer = GMAIL_FILTER_REFERRER_URL
                + "&ik=" + this._session.IdentificationKey
                + "&search=cf"
                + "&view=tl"
                + "&start=0"
                + "&cf_f=cf1"
                + "&cf_t=cf2"
                + "&cf1_from=" + System.Web.HttpUtility.UrlEncode(filter.From)
                + "&cf1_to=" + System.Web.HttpUtility.UrlEncode(filter.To)
                + "&cf1_subj=" + System.Web.HttpUtility.UrlEncode(filter.Subject)
                + "&cf1_has=" + System.Web.HttpUtility.UrlEncode(filter.HasWords)
                + "&cf1_hasnot=" + System.Web.HttpUtility.UrlEncode(filter.DoesntHave)
                + "&cf1_attach=" + System.Web.HttpUtility.UrlEncode(filter.HasAttachment.ToString().ToLower())
                + "&cf2_ar=" + System.Web.HttpUtility.UrlEncode(filter.SkipInbox.ToString().ToLower())
                + "&cf2_st=" + System.Web.HttpUtility.UrlEncode(filter.StarIt.ToString().ToLower())
                + "&cf2_cat=" + System.Web.HttpUtility.UrlEncode(filter.ApplyLabel.ToString().ToLower())
                + "&cf2_sel=" + System.Web.HttpUtility.UrlEncode(filter.LabelToApply)
                + "&cf2_emc=" + System.Web.HttpUtility.UrlEncode(filter.Forward.ToString().ToLower())
                + "&cf2_email=" + System.Web.HttpUtility.UrlEncode(filter.ForwardTo)
                + "&cf2_tr=" + System.Web.HttpUtility.UrlEncode(filter.MoveToTrash.ToString().ToLower())
                + "&ofid=" + System.Web.HttpUtility.UrlEncode(filter.id.ToString())
                + "&zx=" + MakeUniqueUrl();

            this._rawDataPackResponse = MakeWebRequest(location, "GET", referrer, string.Empty, false);

            // Parse the data pack to determine if filter was edited successfully.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }

        /// <summary>
        /// Deletes a filter from the set of Gmail filters.
        /// </summary>
        /// <returns>A <see cref="bool"/> true if completed successfully.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool DeleteFilter(Int64 id)
        {
            string labelPostData = "act=df_" + id.ToString() +
                "&at=" + this._session.Cookies["GMAIL_AT"].Value;

            Uri location = new Uri(GMAIL_FILTER_URL + "&ik=&view=up");
            string referrer = GMAIL_FILTER_REFERRER_URL + "&view=pr&ik= " + this._session.IdentificationKey + "&zx=" + MakeUniqueUrl();

            this._rawDataPackResponse = MakeWebRequest(location, "POST", referrer, labelPostData, false);

            // Parse the data pack to determine if filter was deleted successfully.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }

        /// <summary>
        /// Deletes a filter from the set of Gmail filters.
        /// </summary>
        /// <returns>A <see cref="bool"/> true if completed successfully.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool DeleteFilter(GmailFilter filter)
        {
            return DeleteFilter(filter.id);
        }
        #endregion

        #region Labels
        /// <summary>
        /// Adds a label to the set of Gmail labels.
        /// </summary>
        /// <returns>A <see cref="bool"/> true if completed successfully.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool AddLabel(string label)
        {
            // Limit labels to the 100 characters.
            // Not sure if 100 is correct, I need to test actual limit.
            if (label.Length > 100) label = label.Substring(0, 100);

            string labelPostData = "act=cc_" + System.Web.HttpUtility.UrlEncode(label) +
                "&at=" + this._session.Cookies["GMAIL_AT"].Value;

            Uri location = new Uri(GMAIL_LABEL_URL);
            string referrer = GMAIL_LABEL_REFERRER_URL + "&ik= " + this._session.IdentificationKey + "&zx=" + MakeUniqueUrl();

            this._rawDataPackResponse = MakeWebRequest(location, "POST", referrer, labelPostData, false);

            // Parse the data pack to determine if label was stored successfully.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }

        /// <summary>
        /// Delete a label from the set of Gmail labels.
        /// </summary>
        /// <returns>A <see cref="bool"/> true if completed successfully.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool DeleteLabel(string label)
        {
            // Limit labels to the 100 characters.
            // Not sure if 100 is correct, I need to test actual limit.
            if (label.Length > 100) label = label.Substring(0, 100);

            string labelPostData = "act=dc_" + System.Web.HttpUtility.UrlEncode(label) +
                "&at=" + this._session.Cookies["GMAIL_AT"].Value;

            Uri location = new Uri(GMAIL_LABEL_URL);
            string referrer = GMAIL_LABEL_REFERRER_URL + "&ik= " + this._session.IdentificationKey + "&zx=" + MakeUniqueUrl();

            this._rawDataPackResponse = MakeWebRequest(location, "POST", referrer, labelPostData, false);

            // Parse the data pack to determine if label was deleted successfully.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }

        /// <summary>
        /// Rename a label in the set of Gmail labels.
        /// </summary>
        /// <returns>A <see cref="bool"/> true if completed successfully.</returns>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool RenameLabel(string oldName, string newName)
        {
            // Limit labels to the 100 characters.
            // Not sure if 100 is correct, I need to test actual limit.
            if (oldName.Length > 100) oldName = oldName.Substring(0, 100);
            if (newName.Length > 100) newName = newName.Substring(0, 100);

            string labelPostData = "act=nc_" + System.Web.HttpUtility.UrlEncode(oldName + "^" + newName) +
                "&at=" + this._session.Cookies["GMAIL_AT"].Value;

            Uri location = new Uri(GMAIL_LABEL_URL);
            string referrer = GMAIL_LABEL_REFERRER_URL + "&ik= " + this._session.IdentificationKey + "&zx=" + MakeUniqueUrl();

            this._rawDataPackResponse = MakeWebRequest(location, "POST", referrer, labelPostData, false);

            // Parse the data pack to determine if label was renamed successfully.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }
        #endregion

        #region Invites
        /// <summary>
        /// Send a Gmail invite to an email address.
        /// </summary>
        /// <param name="email">Email of the receipient of the invite.</param>
        /// <returns>True if Gmail accepted the command; false otherwise.</returns>
        // This may be useless after beta stages.
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public bool SendInvite(string email)
        {
            string invitePostData = "act=ii"
                + "&at=" + this._session.Cookies["GMAIL_AT"].Value
                + "&em=" + System.Web.HttpUtility.UrlEncode(email);

            Uri location = new Uri(GMAIL_INVITE_URL + "&ik=" + this._session.IdentificationKey);

            this._rawDataPackResponse = MakeWebRequest(location, "POST", GMAIL_INVITE_REFERRER_URL, invitePostData, false);

            // Parse the data pack to determine if invite was sent.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            ParseDataPack();

            // Return Gmail's response to whether the request was successful.
            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
            return this.CommandSuccess;
        }
        #endregion

        #region Communication and Parsing
        /// <summary>
        /// Reads in the DataPack and extracts relevant mailbox data.
        /// </summary>
        private void ParseDataPack()
        {
            // sanitize the incoming _rawDataPackResponse
            _rawDataPackResponse = _rawDataPackResponse.Replace("\n", "");
            _rawDataPackResponse = _rawDataPackResponse.Replace("D([", "\nD([");
            _rawDataPackResponse = _rawDataPackResponse.Replace("]);", "]);\n");

            // extract the fingerprint, i.e. var fp='6c8abc683047b5bc'
            Match fp = Regex.Match(this._rawDataPackResponse, "var fp='([A-Za-z0-9]+)'", RegexOptions.Compiled);
            if (fp.Success)
            {
                this._session.Fingerprint = fp.Groups[1].Value;

                // clear internal thread store
                this._session.UnreadThreads.Clear();
            }
            else
            {
                Debug.WriteLine("DataPack error: Could not find the DataPack fingerprint.");
            }

            // capture all the dataItems
            Regex r = new Regex(@"D\((?<dataItem>\[.+\])\)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
            Match m = r.Match(_rawDataPackResponse);

            // loop through all the dataItems and insert accordingly
            while (m.Success)
            {
                // get ArrayList version of dataPack JS array
                ArrayList tmpArray = Utilities.ParseJSArray(m.Groups[1].Value);

                // get name of DataItem
                string settingName = (string)tmpArray[0];

                // strip the name element; reindexes the array
                tmpArray.RemoveAt(0);

                SortedList sl;
                switch (settingName)
                {
                    case "a":
                        Debug.WriteLine("Test");
                        break;
                    case "ds":		// default searches
                        if (tmpArray.Count == 7)
                        {
                            sl = this._session.DefaultSearchCounts;
                            sl["Inbox"] = Int32.Parse((string)tmpArray[0]);
                            sl["Starred"] = Int32.Parse((string)tmpArray[1]);
                            sl["Sent"] = Int32.Parse((string)tmpArray[2]);
                            sl["Drafts"] = Int32.Parse((string)tmpArray[3]);		// fix by Brian Hampson [brian.hampson@gmail.com]; 10/6/2004
                            sl["All"] = Int32.Parse((string)tmpArray[4]);
                            sl["Spam"] = Int32.Parse((string)tmpArray[5]);
                            sl["Trash"] = Int32.Parse((string)tmpArray[6]);
                        }
                        else
                        {
                            Debug.WriteLine("DataPack error: 'ds' did not have expected number of elements (6).");
                        }
                        break;
                    case "ct":		// categories
                        sl = this._session.CategoryCounts;
                        foreach (ArrayList sub in (ArrayList)tmpArray[0])
                        {
                            sl[(string)sub[0]] = Int32.Parse((string)sub[1]);
                        }
                        break;
                    // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                    case "ar":      // Check Gmail's success/failure response
                        if (tmpArray.Count == 3)
                        {
                            this.CommandSuccess = Int32.Parse(tmpArray[0].ToString()) == 1;
                            this.CommandResponse = tmpArray[1].ToString();
                        }
                        break;
                    case "ts":		// threadlist summary
                        if (tmpArray.Count == 9)
                        {
                            this._session.ThreadListTimestamp = tmpArray[6].ToString();
                            this._session.TotalMessages = Int32.Parse(tmpArray[2].ToString());
                        }
                        else
                        {
                            Debug.WriteLine("DataPack error: 'ts' did not have expected number of elements (7).");
                        }
                        break;
                    case "qu":      // quota information
                        this._session.Quota = int.Parse(((string)tmpArray[1]).Substring(0, tmpArray[1].ToString().Length - 3));
                        this._session.MailboxSize = int.Parse(((string)tmpArray[0]).Substring(0, tmpArray[0].ToString().Length -3));
                        break;
                    case "fi":      // filters

                        break;
                    case "t":		// message listings *** NOTE: If Gmail changes their spec, this will definitely need to be updated. ***
                        foreach (ArrayList message in tmpArray)
                        {
                            // Using GmailThread Indeces enum for easy changing
                            // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005

                            // we really only want the unread messages
                            if ((string)message[(int)GmailThread.Indeces.IsRead] == "1")
                            {
                                GmailThread newMessage = new GmailThread();
                                newMessage.ThreadID = (string)message[(int)GmailThread.Indeces.ID];
                                newMessage.IsRead = ((string)message[(int)GmailThread.Indeces.IsRead] == "1" ? false : true);	// Gmail reports isUnread, so we swap
                                newMessage.IsStarred = ((string)message[(int)GmailThread.Indeces.IsStarred] == "1" ? true : false);
                                newMessage.DateHtml = (string)message[(int)GmailThread.Indeces.DateHtml];
                                newMessage.AuthorsHtml = (string)message[(int)GmailThread.Indeces.AuthorsHtml];
                                newMessage.Flags = (string)message[(int)GmailThread.Indeces.Flags];
                                newMessage.SubjectHtml = (string)message[(int)GmailThread.Indeces.SubjectHtml];
                                newMessage.SnippetHtml = (string)message[(int)GmailThread.Indeces.SnippetHtml];
                                newMessage.Categories = (ArrayList)message[(int)GmailThread.Indeces.Categories];
                                newMessage.AttachHtml = (string)message[(int)GmailThread.Indeces.AttachHtml];
                                newMessage.MatchingMessageID = (string)message[(int)GmailThread.Indeces.MatchingMessageID];
                                this._session.UnreadThreads.Add(newMessage);
                            }
                        }
                        break;
                    // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                    case "ud":      // IK value.  What's IK stand for?  Anyone?
                        this._session.IdentificationKey = (string)tmpArray[2];
                        break;
                    // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
                    case "i":       // invitations
                        this._session.Invites.Count = int.Parse((string)tmpArray[0]);
                        break;
                    default:
                        break;
                }

                // advance to next dataItem
                m = m.NextMatch();
            }

            this._session.FinalizeUpdate();
        }


        /// <summary>
        /// Attempts an HTTP request and returns the response document.
        /// </summary>
        /// <param name="location">Resource to request.</param>
        /// <param name="method">"GET" or "POST".</param>
        /// <param name="referrer">The HTTP referer (it's spelled 'referrer', dammit!).</param>
        /// <param name="postData">If method if POST, pass the request document; null otherwise.</param>
        /// <param name="allowAutoRedirect">Set to true to allow client to follow redirect.</param>
        /// <returns></returns>
        private string MakeWebRequest(Uri location, string method, string referrer, string postData, bool allowAutoRedirect)
        {
            Debug.WriteLine("Initiating " + method + " request at: " + location.ToString());

            // reset last request's success response
            // Added by Eric larson [larson.eric@gmail.com]; 5/10/2005
            this.CommandResponse = string.Empty;
            this.CommandSuccess = false;

            // prepare HTTP request
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(location);

            // enable proxy support if the user has specified it, or init an empty one
            // Fix from Brian Hampson [brian.hampson@gmail.com]; 10/6/2004
            if (this._proxy != null)
            {
                webRequest.Proxy = this._proxy;
            }
            else
            {
                webRequest.Proxy = GlobalProxySelection.GetEmptyWebProxy();
            }

            // if POSTing, add request page and modify the headers
            byte[] encodedPostData = new byte[0];
            if (method == "POST")
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                encodedPostData = encoding.GetBytes(postData);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = encodedPostData.Length;
            }
            else
            {
                webRequest.Method = "GET";
                webRequest.ContentType = "text/html";
            }

            webRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
            webRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            webRequest.ProtocolVersion = HttpVersion.Version11;
            webRequest.AllowAutoRedirect = allowAutoRedirect;
            webRequest.KeepAlive = true;
            webRequest.Referer = referrer;

            webRequest.CookieContainer = new CookieContainer();
            // adding cookies one-by-one because certain cookies were being added
            // that did not fit the uri path
            // Fix from Eric Larson [larson.eric@gmail.com]; 4/21/2005
            //webRequest.CookieContainer.Add(location, this._session.Cookies);
            foreach (Cookie tmpCookie in this._session.Cookies)
            {
                if ((location.Host.IndexOf(tmpCookie.Domain) > -1) && (location.AbsolutePath.IndexOf(tmpCookie.Path) > -1))
                {
                    Debug.WriteLine("Cookie: " + tmpCookie.Name + " " + tmpCookie.Value + " " + tmpCookie.Domain + " " + tmpCookie.Path);
                    webRequest.CookieContainer.Add(tmpCookie);
                }
            }

            // updated UserAgent to the most recent version of Mozilla FireFox
            // updated the Accept paramater to match the (my) latest version of Mozilla FireFox
            // updated by Eric Larson [larson.eric@gmail.com]; 4/21/2005
            webRequest.UserAgent = "User-Agent: Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.7.7) Gecko/20050414 Firefox/1.0.3";
            webRequest.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

            // Attempt to send request stream to server if POSTing
            if (method == "POST")
            {
                Stream requestStream = null;
                try
                {
                    requestStream = webRequest.GetRequestStream();
                    requestStream.Write(encodedPostData, 0, encodedPostData.Length);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    if (requestStream != null)
                    {
                        requestStream.Close();
                    }
                }
            }

            // Attempt to get response from server
            HttpWebResponse webResponse = null;
            string output = "";
            try
            {
                // get response
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                string headers = "";
                foreach (string key in webResponse.Headers.Keys)
                {
                    headers += key + "=" + webResponse.Headers[key] + " | ";
                }
                Debug.WriteLine("   Headers: " + headers);

                // manually set the GV cookie because ASP.NET doesn't like the subdomain
                if (webResponse.Headers["Set-Cookie"] != null)
                {
                    int tmpCursor = webResponse.Headers["Set-Cookie"].IndexOf("GV=");
                    if (tmpCursor > -1)
                    {
                        int tmpEndCursor = webResponse.Headers["Set-Cookie"].IndexOf(";", tmpCursor);
                        string tmpVal = webResponse.Headers["Set-Cookie"].Substring(tmpCursor + 3, tmpEndCursor - tmpCursor - 3);
                        Debug.WriteLine("   Adding GV cookie: " + tmpVal);
                        Cookie tmpCookie = new Cookie("GV", tmpVal, "/", ".google.com");
                        this._session.Cookies.Add(tmpCookie);
                    }
                }

                // add new cookies to cookie jar
                this._session.Cookies.Add(webResponse.Cookies);
                foreach (Cookie ck in webResponse.Cookies)
                {
                    Debug.WriteLine("   Adding cookie: " + ck.ToString());
                }

                // read response stream and dump to string
                Stream streamResponse = webResponse.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);

                output = streamRead.ReadToEnd();

                streamRead.Close();
                streamResponse.Close();

                // the initial login tries to redirect the browser to a temporarily moved page (HTTP 302)
                // but if you set allowAutoRedirect = true, you receive an error about not being able to
                // securely handshaking or something like that (I don't exactly remember)
                // so if google tries to redirect the browser, form a new uri and make a new MakeWebRequest call
                // Fix from Eric Larson [larson.eric@gmail.com]; 4/21/2005
                if (webResponse.Headers["Location"] != null)
                {
                    string redirect = webResponse.Headers["Location"];
                    Uri uriRedirect;

                    // if redirected to absolute uri
                    if (redirect.IndexOf("http") == 0)
                    {
                        uriRedirect = new Uri(redirect);
                    }

                    // if redirected to the current directory of the current uri
                    else if (redirect.IndexOf("/") != 0)
                    {
                        for (int i = location.Segments.GetLength(0) - 2; i >= 0; i--)
                        {
                            redirect = location.Segments[i] + redirect;
                        }
                        uriRedirect = new Uri(location.Scheme + "://" + location.Host + redirect);
                    }

                    // else if redirected to the root of the current uri's host
                    else
                    {
                        uriRedirect = new Uri(location.Scheme + "://" + location.Host + redirect);
                    }

                    // make recursive web request call
                    output = MakeWebRequest(uriRedirect, "GET", GOOGLE_LOGIN_REFERRER_URL, string.Empty, false);
                }

                Debug.WriteLine("Received response (" + output.Length + " char(s))");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (webResponse != null)
                {
                    webResponse.Close();
                }
            }

            // return the response document
            return output;
        }

        /// <summary>
        /// Generates a proxy defeating random string (passed as the 'zx' GET variable).
        /// </summary>
        /// <returns>Random string composed of JS version and random string.</returns>
        private string MakeUniqueUrl()
        {
            Random rnd = new Random();

            // The significance of 2147483648 is that it's equal to 2^32, or 2GB.
            return this._jsVersion + Convert.ToString((Math.Round(rnd.Next(1, 999) * 2147483.648)));
        }
        #endregion
    }
}
