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
using System.Web;
using System.Text;

#endregion

namespace Johnvey.GmailAgent
{
    /// <summary>
    /// Represents Gmail invitations.
    /// </summary>
    // Added by Eric Larson [larson.eric@gmail.com]; 5/2/2005
    public class GmailInvite
    {
        private int _count;

        /// <summary>
        /// Gets or sets the number of invites available.
        /// </summary>
        public int Count { get { return _count; } set { _count = value; } }

        /// <summary>
        /// Initializes the GmailInvite class
        /// </summary>
        public GmailInvite()
        {
            this._count = 0;
        }
    }
}
