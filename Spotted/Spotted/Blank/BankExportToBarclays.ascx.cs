﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;

namespace Spotted.Blank
{
	public partial class BankExportToBarclays : BlankUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Usr.Current == null || !Usr.Current.IsSuperAdmin)
            {
                throw new DsiUserFriendlyException("You do not have permission to view this page.");
            }

            Response.ContentType = "text/css";
			Response.Write(BankExport.BarclaysExport(GetBatchOfUncompletedBankExports()));
            Response.Flush();
            Response.End();
        }

        private BankExportSet GetBatchOfUncompletedBankExports()
        {
            Query uncompletedBankExportsQuery = new Query(new Or(new Q(BankExport.Columns.Status, BankExport.Statuses.Added),
                                                                 new Q(BankExport.Columns.Status, BankExport.Statuses.AwaitingConfirmation),
                                                                 new Q(BankExport.Columns.Status, BankExport.Statuses.Failed)));
            BankExportSet uncompletedBankExports = new BankExportSet(uncompletedBankExportsQuery);

            return uncompletedBankExports;
        }
    }
}
