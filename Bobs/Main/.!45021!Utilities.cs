using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

using Common;
using Bobs.JobProcessor;
using NUnit.Framework;

namespace Bobs
{

	public class Utilities
	{
		// Do not allow users to instantiate a Utilities object
		private Utilities()
		{ }

		public static string TruncateIp(string ip)
		{
			if (ip.Length > 15)
				return ip.Substring(0, 15);
			else
				return ip;
		}

		public class UpdateChildUrlFragmentsJob : Job
		{
			JobDataMapItemProperty<Model.Entities.ObjectType> ObjectType { get { return new JobDataMapItemProperty<Model.Entities.ObjectType>("ObjectType", JobDataMap); } }
			JobDataMapItemProperty<int> ObjectK { get { return new JobDataMapItemProperty<int>("ObjectK", JobDataMap); } }
			JobDataMapItemProperty<bool> Cascade { get { return new JobDataMapItemProperty<bool>("Cascade", JobDataMap); } }

			public UpdateChildUrlFragmentsJob()
			{ }
			public UpdateChildUrlFragmentsJob(Model.Entities.ObjectType objectType, int objectK, bool cascade)
			{
				ObjectType.Value = objectType;
				ObjectK.Value = objectK;
				Cascade.Value = cascade;
			}

			protected override void Execute()
			{
				var b = Bob.Get(ObjectType, ObjectK);
				if (b is IObjectPage)
					((IObjectPage)b).UpdateChildUrlFragments(Cascade);
			}
		}

		public static void DeletePic(IPic Pic)
		{
			try
			{
				if (!Pic.Equals(Guid.Empty))
				{
					Storage.RemoveFromStore(Storage.Stores.Pix, Pic.Pic, "jpg");
				}
			}
			catch { }

			try
			{
				if (Pic.PicMiscK > 0 && Pic.PicMisc != null)
					Pic.PicMisc.DeleteAll(null);
			}
			catch { }

			Pic.Pic = Guid.Empty;
			Pic.PicMiscK = 0;
			Pic.PicPhotoK = 0;
			Pic.PicState = "";
			((IBob)Pic).Update();
		}
		public static void CopyPic(IPic From, IPic To)
		{
			if (To.HasPic)
				Utilities.DeletePic(To);

			if (From.HasPic)
			{
				To.Pic = Guid.NewGuid();
				Storage.AddToStore(
					Storage.GetFromStore(Storage.Stores.Pix, From.Pic, "jpg"),
					Storage.Stores.Pix,
					To.Pic,
					"jpg",
					(IBob)To,
					"Pic");
				To.PicState = From.PicState;
				To.PicPhotoK = From.PicPhotoK;
				if (From.PicMiscK > 0)
				{
					Misc m = From.PicMisc.Duplicate();
					To.PicMiscK = m.K;
				}
				((IBob)To).Update();
			}
		}

		public delegate void Action();

		public static System.Threading.Thread GetSafeThread(Action action)
		{
			System.Threading.Thread thread = new System.Threading.Thread
			(
				new ThreadStart
					(
						() =>
						{
							try
							{
								action();
							}
							catch (Exception ex)
							{
								SpottedException.TryToSaveExceptionAndChildExceptions(ex, null, null, null, "", "SafeThreadException", "", 0, null);
							}
						}
				)
			);
			return thread;
		}


			
		
		public static void EnableDisableControls(WebControl control, bool enable)
		{
			if (control is TextBox)
			{
				((TextBox)control).ReadOnly = !enable;
				if (!enable)
					control.CssClass = "disabledTextBox";
				else
					control.CssClass = "";
			}
			else
				control.Enabled = enable;

			// Never disable Panels, Labels, ValidationSummaries
			if (control is Panel || control is Label || control is ValidationSummary)
				control.Enabled = true;

			EnableDisableChildControls(control, enable);
		}

		public static void EnableDisableChildControls(Control control, bool enable)
		{
			foreach (Control ctl in control.Controls)
			{
				if (ctl.GetType().Equals(typeof(Cambro.Web.DbCombo.DbCombo)))
					((Cambro.Web.DbCombo.DbCombo)ctl).Enabled = enable;
				else if (ctl is WebControl)
					EnableDisableControls((WebControl)ctl, enable);
				else if(!(ctl is System.Web.UI.UserControl))
					EnableDisableChildControls(ctl, enable);
			}
		}

		#region Enums
		public enum DateRange
		{
			Current = 1,
			Old = 2,
			All = 3
		}
		#endregion

		#region String Tools
		#region Convert Percentage string to double
		public static double ConvertPercentageStringToDouble(string percentage)
		{
			double value;
			double.TryParse(percentage.Replace("%", "").Trim(), out value);
			return value / 100d;
		}
		#endregion
		#region Convert Money string to double
		public static decimal ConvertMoneyStringToDecimal(string money)
		{
