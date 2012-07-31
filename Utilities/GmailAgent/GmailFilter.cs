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

#region Using directives

using System;
using System.Text;

#endregion

namespace Johnvey.GmailAgent
{
    /// <summary>
    /// Represents a Gmail filter.
    /// </summary>
    // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
    public class GmailFilter
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
            id = 0,

            /// <summary>
            /// Name
            /// </summary>
            Name = 1,

            /// <summary>
            /// Sub-array
            /// </summary>
            SubArray = 2,

            /// <summary>
            /// Star it
            /// </summary>
            StarIt = 3,

            /// <summary>
            /// Label to apply
            /// </summary>
            LabelToApply = 4,

            /// <summary>
            /// Move to trash
            /// </summary>
            MoveToTrash = 5,

            /// <summary>
            /// Move to trash (again)
            /// </summary>
            MoveToTrash2 = 6,

            /// <summary>
            /// Forward to
            /// </summary>
            ForwardTo = 7,

            /// <summary>
            /// Always seems to be false
            /// </summary>
            PerpetualFalse = 8
        }

        /// <summary>
        /// Defintes the positions of the data in the DataPack sub-array.
        /// </summary>
        // Added by Eric Larson [larson.eric@gmail.com]; 5/10/2005
        public enum SubIndeces
        {
            /// <summary>
            /// Sender's Address
            /// </summary>
            From = 0,

            /// <summary>
            /// Recipient's Address
            /// </summary>
            To = 1,

            /// <summary>
            /// Subject
            /// </summary>
            Subject = 2,

            /// <summary>
            /// Has these words
            /// </summary>
            HasWords = 3,

            /// <summary>
            /// Doesn't have these words
            /// </summary>
            DoesntHave = 4,

            /// <summary>
            /// Has attachment
            /// </summary>
            HasAttachment = 5,

            /// <summary>
            /// Skip Inbox
            /// </summary>
            SkipInbox = 6,

            /// <summary>
            /// Star it
            /// </summary>
            StarIt = 7,

            /// <summary>
            /// Apply label
            /// </summary>
            ApplayLabel = 8,

            /// <summary>
            /// Label to apply
            /// </summary>
            LabelToApply = 9,

            /// <summary>
            /// Forward message
            /// </summary>
            Forward = 10,

            /// <summary>
            /// Forward to
            /// </summary>
            ForwardTo = 11,

            /// <summary>
            /// Move to trash
            /// </summary>
            MoveToTrash = 12
        }
        #endregion

        private Int64 _id;
        private string _name;
        private string _from;
        private string _to;
        private string _subject;
        private string _hasWords;
        private string _doesntHave;
        private bool _hasAttachment;
        private bool _skipInbox;
        private bool _starIt;
        private bool _applyLabel;
        private string _labelToApply;
        private bool _forward;
        private string _forwardTo;
        private bool _moveToTrash;

        /// <summary>
        /// Gets or sets the contact id, also the order the contact was added.
        /// </summary>
        public Int64 id { get { return _id; } set { _id = value; } }

        /// <summary>
        /// Gets or sets the contact email address.
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }

        /// <summary>
        /// Gets or sets the contact display name.
        /// </summary>
        public string From { get { return _from; } set { _from = value; } }

        /// <summary>
        /// Gets or sets the contact email address in HTML unescaped format (I've never noticed a difference with email).
		/// </summary>
        public string To { get { return _to; } set { _to = value; } }

        /// <summary>
		/// Gets or sets the contact note.
		/// </summary>
        public string Subject { get { return _subject; } set { _subject = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public string HasWords { get { return _hasWords; } set { _hasWords = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public string DoesntHave { get { return _doesntHave; } set { _doesntHave = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public bool HasAttachment { get { return _hasAttachment; } set { _hasAttachment = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public bool SkipInbox { get { return _skipInbox; } set { _skipInbox = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public bool StarIt { get { return _starIt; } set { _starIt = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public bool ApplyLabel { get { return _applyLabel; } set { _applyLabel = value; } }

        /// <summary>
        /// Gets or sets the contact note.
        /// </summary>
        public string LabelToApply { get { return _labelToApply; } set { _labelToApply = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public bool Forward { get { return _forward; } set { _forward = value; } }

        /// <summary>
        /// Gets or sets the contact note.
        /// </summary>
        public string ForwardTo { get { return _forwardTo; } set { _forwardTo = value; } }

        /// <summary>
        /// Gets or sets the contact frequently mailed flag.
        /// </summary>
        public bool MoveToTrash { get { return _moveToTrash; } set { _moveToTrash = value; } }

        /// <summary>
		/// Initializes a new GmailFilter class.
		/// </summary>
		public GmailFilter()
		{
            this._id = -1;
            this._name = string.Empty;
            this._from = string.Empty;
            this._to = string.Empty;
            this._subject = string.Empty;
            this._hasWords = string.Empty;
            this._doesntHave = string.Empty;
            this._hasAttachment = false;
            this._skipInbox = false;
            this._starIt = false;
            this._applyLabel = false;
            this._labelToApply = string.Empty;
            this._forward = false;
            this._forwardTo = string.Empty;
            this._moveToTrash = false;
        }
    }
}
