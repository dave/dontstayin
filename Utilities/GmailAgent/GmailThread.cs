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


namespace Johnvey.GmailAgent
{
	/// <summary>
	/// Represents a Gmail conversation thread summary.
	/// </summary>
	public class GmailThread
	{
        #region Enumerations
        /// <summary>
        /// Defintes the positions of the data in the DataPack array.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
        public enum Indeces
        {
            /// <summary>
            /// Index
            /// </summary>
            ID = 0,

            /// <summary>
            /// Is message read?
            /// </summary>
            IsRead = 1,

            /// <summary>
            /// Is message starred?
            /// </summary>
            IsStarred = 2,

            /// <summary>
            /// HTML formatted date.
            /// </summary>
            DateHtml = 3,

            /// <summary>
            /// HTML formatted authors list.
            /// </summary>
            AuthorsHtml = 4,

            /// <summary>
            /// Flags
            /// </summary>
            Flags = 5,

            /// <summary>
            /// HTML formatted subject.
            /// </summary>
            SubjectHtml = 6,

            /// <summary>
            /// HTML formatted snippet.
            /// </summary>
            SnippetHtml = 7,

            /// <summary>
            /// Categories/Labels the message belongs to.
            /// </summary>
            Categories = 8,

            /// <summary>
            /// HTML formatted attachment list.
            /// </summary>
            AttachHtml = 9,

            /// <summary>
            /// Matching message ID
            /// </summary>
            MatchingMessageID = 10
        }
        #endregion

        private string _threadID;
		private bool _isRead;
		private bool _isStarred;
		private string _dateHtml;
		private string _authorsHtml;
		private string _flags;
		private string _subjectHtml;
		private string _snippetHtml;
		private ArrayList _categories;
		private string _attachHtml;
		private string _matchingMessageID;
		private bool _hasExtraSnippet;

		/// <summary>
		/// Gets or sets the thread identifier.
		/// </summary>
		public string ThreadID			{ get { return _threadID; } set { _threadID = value; } }

		/// <summary>
		/// Gets or sets the 'thread is read' flag.
		/// </summary>
		public bool IsRead				{ get { return _isRead; } set { _isRead = value; } }
		/// <summary>
		/// Gets or sets the 'thread is starred' flag.
		/// </summary>
		public bool IsStarred			{ get { return _isStarred; } set { _isStarred = value; } }

		/// <summary>
		/// Gets or sets the HTML-formatted thread date.
		/// </summary>
		public string DateHtml			{ get { return _dateHtml; } set { _dateHtml = value; } }

		/// <summary>
		/// Gets or sets the HTML-formatted thread author(s) text.
		/// </summary>
		public string AuthorsHtml		{ get { return _authorsHtml; } set { _authorsHtml = value; } }

		/// <summary>
		/// Gets or sets the flags string (unknown).
		/// </summary>
		public string Flags				{ get { return _flags; } set { _flags = value; } }

		/// <summary>
		/// Gets or sets the thread subject.
		/// </summary>
		public string SubjectHtml		{ get { return _subjectHtml; } set { _subjectHtml = value; } }

		/// <summary>
		/// Gets or sets the thread snippet.
		/// </summary>
		public string SnippetHtml		{ get { return _snippetHtml; } set { _snippetHtml = value; } }

		/// <summary>
		/// Gets or sets a list of zero or more categories in which the thread is classified.
		/// </summary>
		public ArrayList Categories		{ get { return _categories; } set { _categories = value; } }

		/// <summary>
		/// Gets or sets the HTML-formatted 'thread has attachment' text (contains an IMG tag).
		/// </summary>
		public string AttachHtml		{ get { return _attachHtml; } set { _attachHtml = value; } }

		/// <summary>
		/// Gets or sets the thread-message link identifier.
		/// </summary>
		public string MatchingMessageID { get { return _matchingMessageID; } set { _matchingMessageID = value; } }

		/// <summary>
		/// DEPRECATED: Gets or sets the flag indicating whether or not the thread has an extra snippet.
		/// </summary>
		public bool HasExtraSnippet		{ get { return _hasExtraSnippet; } set { _hasExtraSnippet = value; } }

		/// <summary>
		/// Initializes a new instance of the GmailThread class.
		/// </summary>
		public GmailThread()
		{
		}
	}
}
