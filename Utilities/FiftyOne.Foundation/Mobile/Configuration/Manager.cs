﻿/* *********************************************************************
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

using System.Web.Configuration;

#endregion

namespace FiftyOne.Foundation.Mobile.Configuration
{
    internal static class Manager
    {
        #region Fields

        internal static LogSection Log;
        internal static RedirectSection Redirect;

        #endregion

        #region Constructor

        static Manager()
        {
            Log = (LogSection)Support.GetWebApplicationSection("fiftyOne/log", false);
            Redirect = (RedirectSection)Support.GetWebApplicationSection("fiftyOne/redirect", false);

            if (Redirect == null)
                Redirect = new RedirectSection();
        }

        #endregion

    }
}
