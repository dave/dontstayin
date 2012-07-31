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
using System.Collections;

namespace Johnvey.GmailAgent
{
	/// <summary>
	/// Represents an in-use Gmail mailbox account.
	/// </summary>
	public class GmailSession
	{
		private string _username;
		private string _password;
		private string _threadListTimestamp;
		private string _fingerprint;
		private DateTime _lastLoginTime;
		private DateTime _lastRefreshTime;
		private SortedList _defaultSearchCounts;
		private SortedList _categoryCounts;
		private CookieCollection _cookies;
		private int _totalMessages;
        private int _quota; // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        private int _mailboxSize; // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        private GmailThreadCollection _threads;
        private GmailThreadCollection _unreadThreads;
        private bool _hasConnectionError;
        private GmailInvite _invites; // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        private GmailContactCollection _contacts;
        private GmailFilterCollection _filters; // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005

        private string _ik;

        /// <summary>
		/// Gets or sets the user's Google Accounts username.
		/// </summary>
		public string Username						{ get { return _username; } set { _username = value; } }

		/// <summary>
		/// Gets or sets the user's Google Accounts password.
		/// </summary>
		public string Password						{ get { return _password; } set { _password = value; } }

		/// <summary>
		/// Gets or sets the Gmail mailbox threadlist timestamp.
		/// </summary>
		public string ThreadListTimestamp			{ get { return _threadListTimestamp; } set { _threadListTimestamp = value; } }

		/// <summary>
		/// Gets or sets the Gmail fingerprint.
		/// </summary>
		public string Fingerprint					{ get { return _fingerprint; } set { _fingerprint = value; } }

		/// <summary>
		/// Gets or sets the last timestamp the user was authenticated with Google Accounts.
		/// </summary>
		public DateTime LastLoginTime				{ get { return _lastLoginTime; } set { _lastLoginTime = value; } }

		/// <summary>
		/// Gets or sets the last timestamp when the mailbox status was refreshed.
		/// </summary>
		public DateTime LastRefreshTime				{ get { return _lastRefreshTime; } set { _lastRefreshTime = value; } }

		/// <summary>
		/// Gets or sets the list of new message counts in each of the default searches: Inbox, All, Spam, Starred, Trash, Sent Items.
		/// </summary>
		public SortedList DefaultSearchCounts		{ get { return _defaultSearchCounts; } set { _defaultSearchCounts = value; } }

		/// <summary>
		/// Gets or sets the list of new message counts in all the user-defined categories.
		/// </summary>
		public SortedList CategoryCounts			{ get { return _categoryCounts; } set { _categoryCounts = value; } }

		/// <summary>
		/// Gets or sets the cookie collection associated with the current session.
		/// </summary>
		public CookieCollection Cookies				{ get { return _cookies; } set { _cookies = value; } }

        /// <summary>
        /// Gets or sets the total number of messages in the mailbox.
        /// </summary>
        public int TotalMessages { get { return _totalMessages; } set { _totalMessages = value; } }

        /// <summary>
        /// Gets or sets the mailbox quota size in MB.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public int Quota { get { return _quota; } set { _quota = value; } }

        /// <summary>
        /// Gets or sets the mailbox size in MB.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public int MailboxSize { get { return _mailboxSize; } set { _mailboxSize = value; } }

        /// <summary>
        /// Gets or sets the collection of unread threads.
        /// </summary>
        // The unreadThreads is being phased out but the property is being kept for backward compatibility.
        // Storing the unread threads overwrites the threads collection.
        // Retrieving the unread threads filters through the threads collection and returns only the unread threads.
        // Retrieving the unread threads may be an inefficient method depending on what you're trying to do.
        // It may be more efficient to request only the unread threads via the search method.
        // Eric Larson [larson.eric@gmail.com]; 5/12/2005
        public GmailThreadCollection UnreadThreads
        {
            get
            {
                GmailThreadCollection tmpThreadCollection = new GmailThreadCollection();

                foreach (GmailThread thread in _threads)
                {
                    if (!thread.IsRead)
                        tmpThreadCollection.Add(thread);
                }

                return tmpThreadCollection;
            }
            
            set
            {
                _threads = value;
            }
        }

        /// <summary>
        /// Gets or sets a collection of threads.
        /// </summary>
        public GmailThreadCollection Threads { get { return _threads; } set { _threads = value; } }

        /// <summary>
        /// Gets or sets the invite object associated with the current session.
        /// </summary>
        public GmailInvite Invites { get { return _invites; } set { _invites = value; } }

        /// <summary>
        /// Gets or sets the contact collection.
        /// </summary>
        public GmailContactCollection Contacts { get { return _contacts; } set { _contacts = value; } }

        /// <summary>
        /// Gets or sets the contact collection.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public GmailFilterCollection Filters { get { return _filters; } set { _filters = value; } }

        /// <summary>
        /// Gets or sets the IK value for contacts associated with the current session.
        /// </summary>
        public string IdentificationKey { get { return _ik; } set { _ik = value; } }

        /// <summary>
		/// Gets or sets the flag indicating a connection error with Gmail during the last request.
		/// </summary>
		public bool HasConnectionError				{ get { return _hasConnectionError; } set { _hasConnectionError = value; } }


		/// <summary>
		/// Initializes a new instance of the GmailSession class.
		/// </summary>
		public GmailSession()
		{
			this._cookies = new CookieCollection();
            this._invites = new GmailInvite(); // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005

            this._defaultSearchCounts = new SortedList();
			this._defaultSearchCounts["Inbox"] = 0;
			this._defaultSearchCounts["Starred"] = 0;
			this._defaultSearchCounts["All"] = 0;
			this._defaultSearchCounts["Sent"] = 0;
			this._defaultSearchCounts["Spam"] = 0;
			this._defaultSearchCounts["Trash"] = 0;

			this._categoryCounts = new SortedList();
            this._unreadThreads = new GmailThreadCollection();
            this._threads = new GmailThreadCollection();
            this._hasConnectionError = false;
		}

		/// <summary>
		/// Signals the end of the session refresh and raises any appropriate events.
		/// </summary>
		public void FinalizeUpdate() {

			// check to see if there are any new messages; raise NewMessage event.
			if((int)this._defaultSearchCounts["Inbox"] > 0) {
				this.OnNewMessage();
			}
		}

		#region Events (and related components)
		/// <summary>
		/// Default EventHandler for GmailSession events.
		/// </summary>
		public delegate void EventHandler(object sender, EventArgs e);

		/// <summary>
		/// Occurs when a new message has been received by the Gmail account.
		/// </summary>
		public event EventHandler NewMessageEventHandler;

		/// <summary>
		/// Occurs when a new message has been received by the Gmail account.
		/// </summary>
		protected virtual void OnNewMessage() {
			if(NewMessageEventHandler != null) {
				NewMessageEventHandler(this, null);
			}
		}
		#endregion

	}
}
