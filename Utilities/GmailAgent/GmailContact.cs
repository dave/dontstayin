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

namespace Johnvey.GmailAgent
{
	/// <summary>
	/// Represents a Gmail address book contact.
	/// </summary>
	public class GmailContact
	{
        #region Enumerations
        /// <summary>
        /// Defintes the positions of the data in the DataPack array.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
        public enum Indeces
        {
            /// <summary>
            /// Unknown field, seems to always be "ce".
            /// </summary>
            ce = 0,

            /// <summary>
            /// Index
            /// </summary>
            id = 1,

            /// <summary>
            /// Name of contact or string preceeding @ symbol if no name specified.
            /// </summary>
            DefaultName = 2,

            /// <summary>
            /// Name
            /// </summary>
            Name = 3,

            /// <summary>
            /// Email Address
            /// </summary>
            Email = 4,

            /// <summary>
            /// Notes
            /// </summary>
            Notes = 5
        }
        #endregion

        // ID added so that we can update and delete contacts.
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        private int _id;
        private string _email;
		private string _emailUnescaped;
		private string _name;
		private string _notes;
		private bool _isFrequentlyMailed;

        /// <summary>
        /// Gets or sets the contact id, also the order the contact was added.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
        public int id { get { return _id; } set { _id = value; } }

        /// <summary>
        /// Gets or sets the contact email address.
        /// </summary>
        public string Email { get { return _email; } set { _email = value; } }

        /// <summary>
		/// Gets or sets the contact email address in HTML unescaped format (I've never noticed a difference with email).
		/// </summary>
		public string EmailUnescaped		{ get { return _emailUnescaped; } set { _emailUnescaped = value; } }

		/// <summary>
		/// Gets or sets the contact display name.
		/// </summary>
		public string Name					{ get { return _name; } set { _name = value; } }

		/// <summary>
		/// Gets or sets the contact note.
		/// </summary>
		public string Notes					{ get { return _notes; } set { _notes = value; } }

		/// <summary>
		/// Gets or sets the contact frequently mailed flag.
		/// </summary>
		public bool IsFrequentlyMailed		{ get { return _isFrequentlyMailed; } set { _isFrequentlyMailed = value; } }


		/// <summary>
		/// Initializes a new GmailContact class.
		/// </summary>
		public GmailContact()
		{
            this._id = -1; // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
            this._email = "";
            this._emailUnescaped = "";
			this._name = "";
			this._notes = "";
			this._isFrequentlyMailed = false;
		}

		/// <summary>
		/// Initializes a new GmailContact class with an initial email address and display name.
		/// </summary>
        /// <param name="email">The contact's email address.</param>
        /// <param name="name">The contact's display name.</param>
		public GmailContact(string email, string name) {
            this._id = -1; // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
            this._email = email;
			this._name = name;
			this._emailUnescaped = System.Web.HttpUtility.HtmlDecode(email);
			this._notes = "";
			this._isFrequentlyMailed = false;
		}

        /// <summary>
        /// Initializes a new GmailContact class with an initial id, email address, and display name.
        /// </summary>
        /// <param name="id">The contact's id.</param>
        /// <param name="email">The contact's email address.</param>
        /// <param name="name">The contact's display name.</param>
        public GmailContact(int id, string email, string name)
        {
            this._id = id; // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
            this._email = email;
            this._name = name;
            this._emailUnescaped = System.Web.HttpUtility.HtmlDecode(email);
            this._notes = "";
            this._isFrequentlyMailed = false;
        }
    }
}
