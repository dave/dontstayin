using System;
using System.Collections.Generic;
using System.Text;
using Bobs.JobProcessor;

namespace Bobs.Jobs
{
	//class SendThreadAlertsJob : Job
	//{
	//    JobDataMapItemProperty<int> ThreadK { get { return new JobDataMapItemProperty<int>("ThreadK", JobDataMap); } }
	//    JobDataMapItemProperty<List<int>> AlertedUsrs { get { return new JobDataMapItemProperty<List<int>>("AlertedUsers", JobDataMap); } }
	//    JobDataMapItemProperty<int> PostingUsrK { get { return new JobDataMapItemProperty<int>("PostingUsrK", JobDataMap); } }
	//    JobDataMapItemProperty<string> Subject { get { return new JobDataMapItemProperty<string>("Subject", JobDataMap); } }
	//    JobDataMapItemProperty<List<int>> InviteKs { get { return new JobDataMapItemProperty<List<int>>("InviteKs", JobDataMap); } }
	//    public SendThreadAlertsJob() { } //required by Quartz.net
	//    public SendThreadAlertsJob(int newThreadK, List<int> alertedUsrs, int postingUsrK, string subject, List<int> inviteKs)
	//        : this()
	//    {
	//        ThreadK.Value = newThreadK;
	//        AlertedUsrs.Value = alertedUsrs;
	//        PostingUsrK.Value = postingUsrK;
	//        Subject.Value = subject;
	//        InviteKs.Value = inviteKs;
	//    }
	//    #region process methods
	//    protected override void Execute()
	//    {
	//        Thread newThread = null;
	//        try
	//        {
	//            newThread = new Thread(this.ThreadK.Value);
	//        }
	//        catch (BobNotFound ex)
	//        {
	//            return;  //sometimes the user deletes the thread before this is executed
	//        }
	//        Usr postingUsr = new Usr(this.PostingUsrK.Value);
	//        string subject = this.Subject.Value;
	//        List<int> inviteKs = this.InviteKs.Value;
	//        List<int> alertedUsrs = this.AlertedUsrs.Value;

	//        DateTime dt = DateTime.Today;
	//        Log.Increment(Log.Items.DoAlertsStart, 1, dt);

	//        //first lets do all the inbox stuff, then send the emails...
	//        ThreadSendMessagesToCommentAlerts(newThread, postingUsr, subject, alertedUsrs);
	//        ThreadSendInvites(newThread, alertedUsrs, postingUsr, inviteKs);
	//        ThreadSendGroupNewsAdminsAReminderIfIsRecommendedNews(newThread);
	//        ThreadSendNewsAlerts(newThread);
	//        ThreadUpdateTotalParticipants(newThread);
	//        Log.Increment(Log.Items.DoAlertsEnd, 1, dt);

	//    }

		
		
	//    private static void ThreadUpdateTotalParticipants(Thread newThread)
	//    {
	//        #region UpdateTotalParticipants
	//        try
	//        {
	//            UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob(newThread);
	//            job.ExecuteSynchronously();
	//        }
	//        catch (Exception ex) { Bobs.Global.Log("572d2bb0-bcd8-4e88-a3d3-ab25d00db417", ex); }
	//        #endregion
	//    }
		
		
	//    #endregion
	//}
}
