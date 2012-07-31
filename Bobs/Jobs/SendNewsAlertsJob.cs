using System;
using System.Collections.Generic;
using System.Text;
using Bobs.JobProcessor;
using System.Xml;
using SpottedScript.Controls.ChatClient.Shared;

namespace Bobs.Jobs
{
	public class SendNewsAlertsJob : Job
	{
		JobDataMapItemProperty<int> ThreadK { get { return new JobDataMapItemProperty<int>("ThreadK", JobDataMap); } }
		public SendNewsAlertsJob() { }
		public SendNewsAlertsJob(Thread thread) 
		{
			ThreadK.Value = thread.K;
		}
	
		protected override void Execute()
		{
			Thread thread = new Thread(ThreadK.Value);
			if (thread.GroupK == 0)
				throw new Exception("Can't send group news emails - thread not in a group!");

			if (!thread.IsNews)
				throw new Exception("Can't send group news emails - thread is not news!");

			Query q = new Query();
			q.TableElement = new TableElement(TablesEnum.Usr);
			q.TableElement = new Join(
				q.TableElement,
				new TableElement(TablesEnum.ThreadUsr),
				QueryJoinType.Left,
				new And(new Q(Usr.Columns.K, ThreadUsr.Columns.UsrK, true), new Q(ThreadUsr.Columns.ThreadK, thread.K)));
			q.TableElement = new Join(
				q.TableElement,
				new TableElement(TablesEnum.GroupUsr),
				QueryJoinType.Inner,
				new And(new Q(Usr.Columns.K, GroupUsr.Columns.UsrK, true), new Q(GroupUsr.Columns.GroupK, thread.GroupK), new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));
			q.QueryCondition = new Q(ThreadUsr.Columns.UsrK, QueryOperator.IsNull, null);
			q.Columns = new ColumnSet(
				Usr.EmailColumns,
				Usr.LinkColumns,
				Usr.Columns.IsLoggedOn,
				Usr.Columns.DateTimeLastPageRequest);
			UsrSet us = new UsrSet(q);

			foreach (Usr u in us)
			{
				ThreadUsr tu = thread.GetThreadUsr(u);
				if (tu.IsNew)
				{
					tu.ChangeStatus(ThreadUsr.StatusEnum.NewGroupNewsAlert, DateTime.Now, false, false);
					tu.StatusChangeObjectK = thread.GroupK;
					tu.StatusChangeObjectType = Model.Entities.ObjectType.Group;
					tu.InvitingUsrK = thread.NewsUsrK;
					tu.Update();

					try
					{
						Mailer usrMail = new Mailer();
						usrMail.Subject = "New news in " + thread.Group.FriendlyName + ": \"" + thread.SubjectSnip(40) + "\"";
						usrMail.Body += "<h1>New news in " + thread.Group.FriendlyName + "</h1>";
						usrMail.Body += "<p>The subject is: \"" + thread.Subject + "\"</p>";
						usrMail.Body += "<p>To read and reply, check out the <a href=\"[LOGIN]\">topic page</a>.</p>";
						usrMail.Body += "<p>If you want to stop getting news alerts for the <i>" + thread.Group.FriendlyName + "</i> group, click the 'Exit this group' button on the <a href=\"[LOGIN(" + thread.Group.Url() + ")]\">group homepage</a>.</p>";
						usrMail.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						usrMail.RedirectUrl = thread.UrlDiscussion();
						usrMail.UsrRecipient = u;
						usrMail.Bulk = true;
						usrMail.Inbox = true;
						usrMail.Send();
					}
					catch (Exception ex) { Global.Log("e96aef2f-ac6d-4f2f-af84-a67c8e53a638", ex); }

					//try
					//{
					//    if (u.IsLoggedOn && u.DateTimeLastPageRequest > DateTime.Now.AddMinutes(-5))
					//    {
					//        XmlDocument xmlDoc = new XmlDocument();
					//        XmlNode n = xmlDoc.CreateElement("groupNewsAlert");
					//        n.AddAttribute("groupName", thread.Group.Name);
					//        n.AddAttribute("groupUrl", thread.Group.Url());
					//        if (thread.Group.HasPic)
					//            n.AddAttribute("pic", thread.Group.Pic.ToString());
					//        else
					//            n.AddAttribute("pic", "0");
					//        n.AddAttribute("subject", thread.SubjectSnip(50));
					//        n.InnerText = thread.UrlDiscussion();

					//        Chat.SendChatItem(ItemType.GroupNewsAlert, n, DateTime.Now.Ticks, u.K, Guid.NewGuid());



					//        //u.AddChatItem(ChatMessage.ItemTypes.GroupNewsAlert,n,DateTime.Now.Ticks);
					//        //Usr.AddChatItemStatic(ChatMessage.ItemTypes.GroupNewsAlert, n, DateTime.Now.Ticks, Guid.NewGuid(), u.K);
					//    }
					//}
					//catch (Exception ex) { Global.Log("39af266c-ad5e-4ddb-b32c-d1bbad0b5189", ex); }
				}
			}
		}

	
	}
}
