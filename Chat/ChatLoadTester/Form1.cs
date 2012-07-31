using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ChatLibrary;
using System.Net;
using System.Threading;

namespace ChatLoadTester
{

	#region enum

	public enum ServerPickerType
	{
		Local,
		Random,
		Specific,
		Cycle
	}

	#endregion
    

	public partial class Form1 : Form
	{



		public Form1()
		{
			InitializeComponent();
			ChatServerFactory.SpecificAddress = txtServerAddress.Text;
		}

		#region Fields
		int _postFrequency = 10;
        int _requestFrequency = 10;
		static bool _testInProgress = false;
		List<TestUser> _posters = new List<TestUser>();
		List<TestUser> _requestors = new List<TestUser>();
		List<TestUser> _users = new List<TestUser>();
		int _numberOfPosters = 100;
		int _numberOfRequestors = 200;
		int _nextUser = 4;
		static ServerPickerType _serverPickerType = ServerPickerType.Specific;
		string _localAddress = "localhost:9000";
        Thread postThread;
        Thread requestThread;
		#endregion



		#region Properties

		public int NextUserK
		{
			get { return _nextUser++;}
		}

		public string LocalAddress
		{
			get{
				if( _localAddress==null)
				{
					_localAddress = ChatServerFactory.GetMyIP() + ":9000";
				}
				return _localAddress;
			}
		
		}

        public static ServerPickerType ServerPicker
        {
            get { return _serverPickerType; }
        }
		#endregion

		

		#region Event Handlers

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			txtServerAddress.Enabled = (radioButton3.Checked);
			ChatServerFactory.SpecificAddress = txtServerAddress.Text;
		}
		
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			numPostFrequency.Enabled = !chkNoPosts.Checked;
			_postFrequency = (chkNoPosts.Checked) ? int.MaxValue : (int)numPostFrequency.Value;
		}
		/// <summary>
		/// no of rooms
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			
		}
		/// <summary>
		/// no of users
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
            SuspendThreads();
 
            _numberOfPosters = (int)numUsers.Value;
			UpdateUserList();
            
            ResumeThreads();
		}

		private void numPostFrequency_ValueChanged(object sender, EventArgs e)
		{
			_postFrequency = (int)numPostFrequency.Value;
			foreach (TestUser user in _posters)
			{
				user.PostFrequency = _postFrequency;
			}
		}
        
        private void numRequestFrequency_ValueChanged(object sender, EventArgs e)
        {
            _requestFrequency = (int)numPostFrequency.Value;
            foreach (TestUser user in _requestors)
            {
                user.RequestFrequency = _requestFrequency;
            }
            foreach (TestUser user in _posters )
            {
                user.RequestFrequency = _requestFrequency;
            }
        }

		private void numRequestorUsers_ValueChanged(object sender, EventArgs e)
		{
            SuspendThreads();
            _numberOfRequestors = (int)numRequestorUsers.Value;
			UpdateUserList();
            ResumeThreads();
		}


		private void button1_Click(object sender, EventArgs e)
		{
			//GO!
			if (btnGo.Text == "GO")
			{
				StartLoadTest();
				
			}
			else
			{
				EndLoadTest();
				
			}
			
		}

        

        private void btnShowUserDetails_Click(object sender, EventArgs e)
        {
            //int i = _users.Count - 1;
            //int selected = int.Parse(comboBox1.SelectedText) ;
            //while (_users[i].UsrK != selected) i--;

            //this.richTextBox1.Text = _users[i].ToString();

            this.richTextBox1.Text = ((TestUser)comboBox1.SelectedItem).ToString();
        }

		#endregion

		#region Private Methods

        private void SuspendThreads()
        {
            _testInProgress = false;
            if (postThread != null)
                postThread.Join();

            if (requestThread != null)
                requestThread.Join();

        }

        private void ResumeThreads()
        {
            if (postThread != null)
                StartLoadTest();

        }
		private void StartLoadTest()
		{
			_testInProgress = true;

            this.comboBox1.Enabled = false;

			this.btnGo.BackColor = Color.Red;
			this.btnGo.Text = "Stop";

			try
			{
				UpdateUserList();

				ThreadStart runTest = new ThreadStart(RunTest);
				postThread = new Thread(runTest);
                postThread.Start();

                ThreadStart runRequests = new ThreadStart(RunRequests);
                requestThread = new Thread(runRequests);
                requestThread.Start();

                //while (myThread.IsAlive)
                //{
                //}
                //EndLoadTest();
				//Thread.Sleep(1000);
			}
			catch (Exception ex)
			{
				EndLoadTest();
				MessageBox.Show(ex.ToString());
			}
			
		}

		private void EndLoadTest()
		{
			_testInProgress = false;
			this.btnGo.BackColor = Color.Green;

			this.btnGo.Text = "GO";

            this.comboBox1.Enabled = true;
            this.comboBox1.DataSource = _users;
            this.comboBox1.DisplayMember = "UsrK";
            
            this.comboBox1.Refresh();

            postThread = null;
            requestThread = null;
		}

		private void RunTest()
		{
			//in new thread, create chat item and post to public chatroom, or one of other specified number of chatrooms

			//<chatItem dateTime="632435000974370000" guid="cd8d41c3-9cbc-4583-8a2b-0f628aa8811c" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078857"> neither do I chromofan </chatMessage></chatItem><chatItem dateTime="632435000900630000" guid="71c7e203-4b68-44d8-822a-978bb22bf433" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078856"> it'd tip me off my rocker </chatMessage></chatItem><chatItem dateTime="632435000866270000" guid="6c6b293a-4b4f-4bab-b770-a0482941b932" type="3"><commentAlert nickName="viv" stmu="0,0,0,0,1,0,0,0,0,0" usrK="11417" pic="0" k="309471" thread="0">/uk/bournemouth/club-destiny/2005/mar/04/event-6163/chat/k-62455/M-309471#CommentK-309471</commentAlert></chatItem><chatItem dateTime="632435000833600000" guid="30af1979-389d-425c-9622-d16495447e87" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078854"> i dont think i could do drugs man </chatMessage></chatItem><chatItem dateTime="632435000814070000" guid="2ef5c5aa-da7c-4724-943a-f93ef5a5f4c1" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078852"> Or Beechams powders </chatMessage></chatItem><chatItem dateTime="632435000717330000" guid="3d29770d-af6a-4f29-b134-3b3ad7ce83bd" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078847"> I was being extremely sarcastic </chatMessage></chatItem><chatItem dateTime="632435000675770000" guid="26ea9797-57e2-4f04-9465-f26c5e8afafe" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078843"> wheres the ajax when ya need it!! </chatMessage></chatItem><chatItem dateTime="632435000607670000" guid="4469e92c-d3f3-42cc-8035-f083e3929441" type="1"><chatMessage nickName="Funky-Matt" stmu="0,1,0,0,1,0,0,0,0,0" usrK="12149" pic="6df71c48-42fd-4c4b-bb08-dd630df36638" k="1078841"> pure class! always done in a moment of madness when you empty the dodgy bag!!! </chatMessage></chatItem><chatItem dateTime="632435000597330000" guid="98950d53-3364-4f19-a21b-b09e82e65eec" type="1"><chatMessage nickName="Tommi-White" stmu="0,0,0,0,1,0,0,1,0,0" usrK="24677" pic="0" k="1078840"> www.vinylradiofm.com </chatMessage></chatItem><chatItem dateTime="632435000544070000" guid="482fec2c-93dc-48de-89ce-1a793bc4a06f" type="1"><chatMessage nickName="TINA" stmu="0,1,1,1,1,1,0,0,0,0" usrK="5138" pic="cd0b00e4-5de1-47a0-8b3d-6579cfbeccdb" k="1078838"> its not big or clever </chatMessage></chatItem><chatItem dateTime="632435000538430000" guid="22ec888d-4b94-4c82-915b-27411e452784" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078837"> your pro plus is snorting your flatmates, get it right </chatMessage></chatItem><chatItem dateTime="632435000530770000" guid="6ecb5dc9-2ef8-404e-8be3-aeffbdc291b7" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078836"> Did you tell them it was c*k*? ;-) </chatMessage></chatItem><chatItem dateTime="632435000456730000" guid="3690b7df-09e6-434f-9d91-aa9146e537fa" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078834"> lol </chatMessage></chatItem><chatItem dateTime="632435000448270000" guid="64eafa41-3787-4c67-b9b5-1049d8eb5e85" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078833"> snortin,oooooh now there's somethin i aint tried </chatMessage></chatItem><chatItem dateTime="632435000442670000" guid="304b2b74-f9eb-426e-bdde-7a504d2c5178" type="1"><chatMessage nickName="TINA" stmu="0,1,1,1,1,1,0,0,0,0" usrK="5138" pic="cd0b00e4-5de1-47a0-8b3d-6579cfbeccdb" k="1078832"> nothing harcore about that </chatMessage></chatItem><chatItem dateTime="632435000428900000" guid="2db0ad56-eea0-49db-bb87-9a7b19f370ff" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078831"> proper boshers!!! lol!! </chatMessage></chatItem><chatItem dateTime="632435000379700000" guid="9f03c055-2982-4219-b334-3812c5a363b3" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078829"> women </chatMessage></chatItem><chatItem dateTime="632435000281270000" guid="49395212-3ee0-46ac-a3bc-2df0ee5dafbd" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078828"> oh dear my hardcore flatmates are snorting pro plus </chatMessage></chatItem><chatItem dateTime="632435000155770000" guid="503a75d5-d49a-4eb2-a7cb-25c7815dfeeb" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078824"> MSN is tosswank.....im annoyed with it. </chatMessage></chatItem><chatItem dateTime="632435000132500000" guid="b11a72e1-84fd-4a6e-af9c-b793463b9482" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078823"> wooohoo </chatMessage></chatItem><chatItem dateTime="632435000106270000" guid="76798c86-3789-43e7-a548-f88ce1dfbe7a" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078822"> wicked </chatMessage></chatItem><chatItem dateTime="632434999972330000" guid="1d71e9d5-5eab-470c-ba64-c39701a231e3" type="1"><chatMessage nickName="Funky-Matt" stmu="0,1,0,0,1,0,0,0,0,0" usrK="12149" pic="6df71c48-42fd-4c4b-bb08-dd630df36638" k="1078818"> msn back on! </chatMessage></chatItem><chatItem dateTime="632434999960000000" guid="1ea3bb83-c9e6-4daf-9a5d-61e498ec93e7" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078817"> Es machts nicht sheebs </chatMessage></chatItem><chatItem dateTime="632434999953270000" guid="4eaa6225-dfc6-453a-b966-80c1139cb46a" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078816"> and i hope she didnt just read that </chatMessage></chatItem><chatItem dateTime="632434999950770000" guid="0b9a99f6-70f0-41b1-8d15-114af6de1cb0" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078815"> me neither mate, i got c 2 loo!! </chatMessage></chatItem><chatItem dateTime="632434999902800000" guid="5304355c-d5db-4876-9e2a-3fc4b235b2bc" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078813"> she's tasty </chatMessage></chatItem><chatItem dateTime="632434999876270000" guid="63243423-26ac-44ad-9e5a-7bce91cec145" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078812"> that girl Ema_B </chatMessage></chatItem><chatItem dateTime="632434999817670000" guid="63f5b4be-287b-4eee-8271-0e57a1c82dc4" type="3"><commentAlert nickName="Sister-Midnight" stmu="0,1,1,0,1,1,0,0,0,2" usrK="10497" pic="48c85258-9dfe-46da-bd4a-945110dac1d4" k="309469" thread="0">/uk/london/heaven/2005/feb/05/photo-184165/home/M-309469#CommentK-309469</commentAlert></chatItem><chatItem dateTime="632434999750170000" guid="d8cdd394-f036-4e25-870e-671377a19a1e" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078807"> i got a C in german,kinda forgot it all lol </chatMessage></chatItem><chatItem dateTime="632434999743900000" guid="89908587-6fb7-4565-9536-b79b3a448765" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078806"> lol </chatMessage></chatItem><chatItem dateTime="632434999709070000" guid="6e7cb818-8e5e-49ac-910b-bd0031296266" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078804"> i apologise 4 my oafishness!! </chatMessage></chatItem><chatItem dateTime="632434999693430000" guid="c8bff011-932a-497f-8dc9-dd9d68c59d9f" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078803"> So what's hat in German then? </chatMessage></chatItem><chatItem dateTime="632434999642970000" guid="5b3a4583-9ae6-4afd-9027-dd108ebda536" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078802"> i cant remember any though </chatMessage></chatItem><chatItem dateTime="632434999611730000" guid="1eb0854f-9199-4aa1-847e-9a7096095db6" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078799"> german's alright </chatMessage></chatItem><chatItem dateTime="632434999570470000" guid="5b214741-0238-4574-833d-593862f30a9a" type="3"><commentAlert nickName="DoubleDutch" stmu="0,1,1,1,1,1,1,0,0,2" usrK="319" pic="ee3618e0-65b9-4815-989b-1b033e1d2e99" k="309467" thread="0">/uk/london/heaven/2005/feb/05/photo-184049/home/M-309467#CommentK-309467</commentAlert></chatItem><chatItem dateTime="632434999504700000" guid="ce914ca2-d61a-4720-a9dc-c49d2e3ccade" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078794"> ah, did german ya see!! </chatMessage></chatItem><chatItem dateTime="632434999282670000" guid="2a8619de-0920-4c88-bdfe-3005e137e80c" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078791"> hahaha @ oaf </chatMessage></chatItem><chatItem dateTime="632434999253770000" guid="8e8541e0-1f2e-4bdd-9a23-ffd51f7a263b" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078788"> aaah shame.... </chatMessage></chatItem><chatItem dateTime="632434999210470000" guid="32b07f36-3b85-41bf-aac2-1716150dd0bc" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078787"> Chapeau. It's French, you uneducated oaf ;-) </chatMessage></chatItem><chatItem dateTime="632434999149700000" guid="ef958ff1-5307-44a1-99b3-a580d714bb1f" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078785"> chapeau....French for hat innit </chatMessage></chatItem><chatItem dateTime="632434999035170000" guid="36eb47b1-4e01-40d2-8c0e-7b802470c3e0" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078783"> yo sheebs </chatMessage></chatItem><chatItem dateTime="632434999022500000" guid="e2062553-e8d6-4e8d-b258-64009e99614a" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078781"> If only it was mine - my mate turned up at Sundazed with it a while ago and decided it looked better on me. But I wasn't allowed to keep :-( </chatMessage></chatItem><chatItem dateTime="632434998949370000" guid="44f5493f-04a9-4fd1-80f2-67a6bfc94b3f" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078779"> vererable what!!! </chatMessage></chatItem><chatItem dateTime="632434998855300000" guid="c4f3132a-6e8f-468f-8bea-0c464e89b6e9" type="3"><commentAlert nickName="Smiler-xx" stmu="0,1,1,0,1,1,1,0,0,1" usrK="3232" pic="f2c14fae-f09c-44fe-9555-a19f72c362b0" k="309466" thread="0">/uk/london/heaven/2005/feb/05/photo-184165/home/M-309466#CommentK-309466</commentAlert></chatItem><chatItem dateTime="632434998774530000" guid="fdcda39a-99c1-45b1-9c85-9cccb63ec830" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078777"> yo chromofoam, how ya diddlin?? </chatMessage></chatItem><chatItem dateTime="632434998643130000" guid="d6924f0f-3ba1-4406-9e03-7a36443ff2f3" type="3"><commentAlert nickName="nimo" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26334" pic="44cacf85-724d-4960-a9ec-eaf38a3a5dc1" k="309465" thread="0">/uk/maidstone/the-loft-nightclub/2005/feb/05/photo-181453/home/M-309465#CommentK-309465</commentAlert></chatItem><chatItem dateTime="632434998580170000" guid="08d9944a-065d-49d1-883c-2a4d4d7ddc49" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078773"> i must say i am envious of your venerable chapeau </chatMessage></chatItem><chatItem dateTime="632434998514370000" guid="cbc548cf-15bc-49eb-9125-1c6c394013e1" type="3"><commentAlert nickName="DoubleDutch" stmu="0,1,1,1,1,1,1,0,0,2" usrK="319" pic="ee3618e0-65b9-4815-989b-1b033e1d2e99" k="309464" thread="0">/uk/london/heaven/2005/feb/05/photo-184047/home/M-309464#CommentK-309464</commentAlert></chatItem><chatItem dateTime="632434998449700000" guid="76f991f7-6e1e-4e27-b6a3-9d4e61d3f5e6" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078770"> lol binbag </chatMessage></chatItem><chatItem dateTime="632434998347800000" guid="026a8a8e-9dea-4fac-b4d9-0374de465323" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078767"> My hat has pretty much the same effect ;-) </chatMessage></chatItem><chatItem dateTime="632434998269700000" guid="e088d93f-9aec-487f-b81e-fef168e7ac3d" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078765"> im also lying. </chatMessage></chatItem><chatItem dateTime="632434998197030000" guid="1ad82e73-029f-45a6-b795-367dec31a17a" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078762"> lol </chatMessage></chatItem><chatItem dateTime="632434998116400000" guid="26072daa-1916-49dc-9662-2abd7a7e6b6c" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078758"> yeh, i knock the ladies out ;-) </chatMessage></chatItem></doc>

			try
			{
				//Step 1 : Create Users
				while (_testInProgress)
				{

					foreach (TestUser user in _users)
					{
						if (user.MustPost)
						{
							//Step 2 : Get the ChatServer
							ChatServerInterface cs = ChatServerFactory.GetChatServer(_serverPickerType);

							//Step 3 : Make a random message
							string message = string.Format("<chatItem dateTime=\"{0}\" guid=\"{1}\" type=\"1\"><chatMessage nickName=\"test\" stmu=\"0,0,0,0,0,0,0,1,0,0\" usrK=\"26579\" pic=\"6af53b8e-e2cc-43f6-8beb-f948d96abee4\" k=\"{2}\"> Hello, I'm User {2} </chatMessage></chatItem>", DateTime.Now.Ticks, Guid.NewGuid().ToString(), user.UsrK);

							//Step 4 : Send 
							user.Post( cs);

                            //Step 5 : Wait a short while
                            //int waitPeriod = _postFrequency * 10 / _numberOfPosters;
                            //Thread.Sleep(waitPeriod);
						}
					}
                    
					//Thread.Sleep(1000);
				}
			}
			catch (Exception ex)
			{
                throw ex;
			}

			Thread.CurrentThread.Abort();

		}

        private void RunRequests()
        {
            //in new thread, create chat item and post to public chatroom, or one of other specified number of chatrooms

            //<chatItem dateTime="632435000974370000" guid="cd8d41c3-9cbc-4583-8a2b-0f628aa8811c" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078857"> neither do I chromofan </chatMessage></chatItem><chatItem dateTime="632435000900630000" guid="71c7e203-4b68-44d8-822a-978bb22bf433" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078856"> it'd tip me off my rocker </chatMessage></chatItem><chatItem dateTime="632435000866270000" guid="6c6b293a-4b4f-4bab-b770-a0482941b932" type="3"><commentAlert nickName="viv" stmu="0,0,0,0,1,0,0,0,0,0" usrK="11417" pic="0" k="309471" thread="0">/uk/bournemouth/club-destiny/2005/mar/04/event-6163/chat/k-62455/M-309471#CommentK-309471</commentAlert></chatItem><chatItem dateTime="632435000833600000" guid="30af1979-389d-425c-9622-d16495447e87" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078854"> i dont think i could do drugs man </chatMessage></chatItem><chatItem dateTime="632435000814070000" guid="2ef5c5aa-da7c-4724-943a-f93ef5a5f4c1" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078852"> Or Beechams powders </chatMessage></chatItem><chatItem dateTime="632435000717330000" guid="3d29770d-af6a-4f29-b134-3b3ad7ce83bd" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078847"> I was being extremely sarcastic </chatMessage></chatItem><chatItem dateTime="632435000675770000" guid="26ea9797-57e2-4f04-9465-f26c5e8afafe" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078843"> wheres the ajax when ya need it!! </chatMessage></chatItem><chatItem dateTime="632435000607670000" guid="4469e92c-d3f3-42cc-8035-f083e3929441" type="1"><chatMessage nickName="Funky-Matt" stmu="0,1,0,0,1,0,0,0,0,0" usrK="12149" pic="6df71c48-42fd-4c4b-bb08-dd630df36638" k="1078841"> pure class! always done in a moment of madness when you empty the dodgy bag!!! </chatMessage></chatItem><chatItem dateTime="632435000597330000" guid="98950d53-3364-4f19-a21b-b09e82e65eec" type="1"><chatMessage nickName="Tommi-White" stmu="0,0,0,0,1,0,0,1,0,0" usrK="24677" pic="0" k="1078840"> www.vinylradiofm.com </chatMessage></chatItem><chatItem dateTime="632435000544070000" guid="482fec2c-93dc-48de-89ce-1a793bc4a06f" type="1"><chatMessage nickName="TINA" stmu="0,1,1,1,1,1,0,0,0,0" usrK="5138" pic="cd0b00e4-5de1-47a0-8b3d-6579cfbeccdb" k="1078838"> its not big or clever </chatMessage></chatItem><chatItem dateTime="632435000538430000" guid="22ec888d-4b94-4c82-915b-27411e452784" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078837"> your pro plus is snorting your flatmates, get it right </chatMessage></chatItem><chatItem dateTime="632435000530770000" guid="6ecb5dc9-2ef8-404e-8be3-aeffbdc291b7" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078836"> Did you tell them it was c*k*? ;-) </chatMessage></chatItem><chatItem dateTime="632435000456730000" guid="3690b7df-09e6-434f-9d91-aa9146e537fa" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078834"> lol </chatMessage></chatItem><chatItem dateTime="632435000448270000" guid="64eafa41-3787-4c67-b9b5-1049d8eb5e85" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078833"> snortin,oooooh now there's somethin i aint tried </chatMessage></chatItem><chatItem dateTime="632435000442670000" guid="304b2b74-f9eb-426e-bdde-7a504d2c5178" type="1"><chatMessage nickName="TINA" stmu="0,1,1,1,1,1,0,0,0,0" usrK="5138" pic="cd0b00e4-5de1-47a0-8b3d-6579cfbeccdb" k="1078832"> nothing harcore about that </chatMessage></chatItem><chatItem dateTime="632435000428900000" guid="2db0ad56-eea0-49db-bb87-9a7b19f370ff" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078831"> proper boshers!!! lol!! </chatMessage></chatItem><chatItem dateTime="632435000379700000" guid="9f03c055-2982-4219-b334-3812c5a363b3" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078829"> women </chatMessage></chatItem><chatItem dateTime="632435000281270000" guid="49395212-3ee0-46ac-a3bc-2df0ee5dafbd" type="1"><chatMessage nickName="izak" stmu="0,1,1,1,1,1,0,0,0,2" usrK="14376" pic="5bd7af96-e9e2-4c9b-95e5-e8506431814e" k="1078828"> oh dear my hardcore flatmates are snorting pro plus </chatMessage></chatItem><chatItem dateTime="632435000155770000" guid="503a75d5-d49a-4eb2-a7cb-25c7815dfeeb" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078824"> MSN is tosswank.....im annoyed with it. </chatMessage></chatItem><chatItem dateTime="632435000132500000" guid="b11a72e1-84fd-4a6e-af9c-b793463b9482" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078823"> wooohoo </chatMessage></chatItem><chatItem dateTime="632435000106270000" guid="76798c86-3789-43e7-a548-f88ce1dfbe7a" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078822"> wicked </chatMessage></chatItem><chatItem dateTime="632434999972330000" guid="1d71e9d5-5eab-470c-ba64-c39701a231e3" type="1"><chatMessage nickName="Funky-Matt" stmu="0,1,0,0,1,0,0,0,0,0" usrK="12149" pic="6df71c48-42fd-4c4b-bb08-dd630df36638" k="1078818"> msn back on! </chatMessage></chatItem><chatItem dateTime="632434999960000000" guid="1ea3bb83-c9e6-4daf-9a5d-61e498ec93e7" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078817"> Es machts nicht sheebs </chatMessage></chatItem><chatItem dateTime="632434999953270000" guid="4eaa6225-dfc6-453a-b966-80c1139cb46a" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078816"> and i hope she didnt just read that </chatMessage></chatItem><chatItem dateTime="632434999950770000" guid="0b9a99f6-70f0-41b1-8d15-114af6de1cb0" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078815"> me neither mate, i got c 2 loo!! </chatMessage></chatItem><chatItem dateTime="632434999902800000" guid="5304355c-d5db-4876-9e2a-3fc4b235b2bc" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078813"> she's tasty </chatMessage></chatItem><chatItem dateTime="632434999876270000" guid="63243423-26ac-44ad-9e5a-7bce91cec145" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078812"> that girl Ema_B </chatMessage></chatItem><chatItem dateTime="632434999817670000" guid="63f5b4be-287b-4eee-8271-0e57a1c82dc4" type="3"><commentAlert nickName="Sister-Midnight" stmu="0,1,1,0,1,1,0,0,0,2" usrK="10497" pic="48c85258-9dfe-46da-bd4a-945110dac1d4" k="309469" thread="0">/uk/london/heaven/2005/feb/05/photo-184165/home/M-309469#CommentK-309469</commentAlert></chatItem><chatItem dateTime="632434999750170000" guid="d8cdd394-f036-4e25-870e-671377a19a1e" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078807"> i got a C in german,kinda forgot it all lol </chatMessage></chatItem><chatItem dateTime="632434999743900000" guid="89908587-6fb7-4565-9536-b79b3a448765" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078806"> lol </chatMessage></chatItem><chatItem dateTime="632434999709070000" guid="6e7cb818-8e5e-49ac-910b-bd0031296266" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078804"> i apologise 4 my oafishness!! </chatMessage></chatItem><chatItem dateTime="632434999693430000" guid="c8bff011-932a-497f-8dc9-dd9d68c59d9f" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078803"> So what's hat in German then? </chatMessage></chatItem><chatItem dateTime="632434999642970000" guid="5b3a4583-9ae6-4afd-9027-dd108ebda536" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078802"> i cant remember any though </chatMessage></chatItem><chatItem dateTime="632434999611730000" guid="1eb0854f-9199-4aa1-847e-9a7096095db6" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078799"> german's alright </chatMessage></chatItem><chatItem dateTime="632434999570470000" guid="5b214741-0238-4574-833d-593862f30a9a" type="3"><commentAlert nickName="DoubleDutch" stmu="0,1,1,1,1,1,1,0,0,2" usrK="319" pic="ee3618e0-65b9-4815-989b-1b033e1d2e99" k="309467" thread="0">/uk/london/heaven/2005/feb/05/photo-184049/home/M-309467#CommentK-309467</commentAlert></chatItem><chatItem dateTime="632434999504700000" guid="ce914ca2-d61a-4720-a9dc-c49d2e3ccade" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078794"> ah, did german ya see!! </chatMessage></chatItem><chatItem dateTime="632434999282670000" guid="2a8619de-0920-4c88-bdfe-3005e137e80c" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078791"> hahaha @ oaf </chatMessage></chatItem><chatItem dateTime="632434999253770000" guid="8e8541e0-1f2e-4bdd-9a23-ffd51f7a263b" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078788"> aaah shame.... </chatMessage></chatItem><chatItem dateTime="632434999210470000" guid="32b07f36-3b85-41bf-aac2-1716150dd0bc" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078787"> Chapeau. It's French, you uneducated oaf ;-) </chatMessage></chatItem><chatItem dateTime="632434999149700000" guid="ef958ff1-5307-44a1-99b3-a580d714bb1f" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078785"> chapeau....French for hat innit </chatMessage></chatItem><chatItem dateTime="632434999035170000" guid="36eb47b1-4e01-40d2-8c0e-7b802470c3e0" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078783"> yo sheebs </chatMessage></chatItem><chatItem dateTime="632434999022500000" guid="e2062553-e8d6-4e8d-b258-64009e99614a" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078781"> If only it was mine - my mate turned up at Sundazed with it a while ago and decided it looked better on me. But I wasn't allowed to keep :-( </chatMessage></chatItem><chatItem dateTime="632434998949370000" guid="44f5493f-04a9-4fd1-80f2-67a6bfc94b3f" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078779"> vererable what!!! </chatMessage></chatItem><chatItem dateTime="632434998855300000" guid="c4f3132a-6e8f-468f-8bea-0c464e89b6e9" type="3"><commentAlert nickName="Smiler-xx" stmu="0,1,1,0,1,1,1,0,0,1" usrK="3232" pic="f2c14fae-f09c-44fe-9555-a19f72c362b0" k="309466" thread="0">/uk/london/heaven/2005/feb/05/photo-184165/home/M-309466#CommentK-309466</commentAlert></chatItem><chatItem dateTime="632434998774530000" guid="fdcda39a-99c1-45b1-9c85-9cccb63ec830" type="1"><chatMessage nickName="sheebs" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26579" pic="6af53b8e-e2cc-43f6-8beb-f948d96abee4" k="1078777"> yo chromofoam, how ya diddlin?? </chatMessage></chatItem><chatItem dateTime="632434998643130000" guid="d6924f0f-3ba1-4406-9e03-7a36443ff2f3" type="3"><commentAlert nickName="nimo" stmu="0,0,0,0,0,0,0,1,0,0" usrK="26334" pic="44cacf85-724d-4960-a9ec-eaf38a3a5dc1" k="309465" thread="0">/uk/maidstone/the-loft-nightclub/2005/feb/05/photo-181453/home/M-309465#CommentK-309465</commentAlert></chatItem><chatItem dateTime="632434998580170000" guid="08d9944a-065d-49d1-883c-2a4d4d7ddc49" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078773"> i must say i am envious of your venerable chapeau </chatMessage></chatItem><chatItem dateTime="632434998514370000" guid="cbc548cf-15bc-49eb-9125-1c6c394013e1" type="3"><commentAlert nickName="DoubleDutch" stmu="0,1,1,1,1,1,1,0,0,2" usrK="319" pic="ee3618e0-65b9-4815-989b-1b033e1d2e99" k="309464" thread="0">/uk/london/heaven/2005/feb/05/photo-184047/home/M-309464#CommentK-309464</commentAlert></chatItem><chatItem dateTime="632434998449700000" guid="76f991f7-6e1e-4e27-b6a3-9d4e61d3f5e6" type="1"><chatMessage nickName="loopyloo" stmu="0,0,1,0,1,0,0,0,0,0" usrK="6171" pic="df797fec-57bf-4045-a835-1b3e93b15a1b" k="1078770"> lol binbag </chatMessage></chatItem><chatItem dateTime="632434998347800000" guid="026a8a8e-9dea-4fac-b4d9-0374de465323" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078767"> My hat has pretty much the same effect ;-) </chatMessage></chatItem><chatItem dateTime="632434998269700000" guid="e088d93f-9aec-487f-b81e-fef168e7ac3d" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078765"> im also lying. </chatMessage></chatItem><chatItem dateTime="632434998197030000" guid="1ad82e73-029f-45a6-b795-367dec31a17a" type="1"><chatMessage nickName="binbag" stmu="0,0,0,0,1,1,0,0,0,0" usrK="16750" pic="3b783772-d7aa-4b17-90af-00f8ac264095" k="1078762"> lol </chatMessage></chatItem><chatItem dateTime="632434998116400000" guid="26072daa-1916-49dc-9662-2abd7a7e6b6c" type="1"><chatMessage nickName="chromofoam" stmu="0,0,0,0,1,0,0,1,0,0" usrK="26591" pic="0" k="1078758"> yeh, i knock the ladies out ;-) </chatMessage></chatItem></doc>

            try
            {
                while (_testInProgress)
                {

                    foreach (TestUser user in _users)
                    {
                        if (user.MustRequest)
                        {
                            //Step 2 : Get the ChatServer
                            ChatServerInterface cs = ChatServerFactory.GetChatServer(_serverPickerType);

                            //Step 3 : Request 
                            user.Request(cs);

                            //Step 4 : Wait a short while
                            //int waitPeriod = _requestFrequency * 10 / _numberOfRequestors;
                            //Thread.Sleep(waitPeriod);
                        }
                    }
                    //Thread.Sleep(1000);
                }
            }
            catch //(Exception ex)
            {
               // throw ex;
            }

            Thread.CurrentThread.Abort();

        }


		private void UpdateUserList()
		{
            UpdatePosters();
            UpdateRequestors();


			//_users = _requestors.InsertRange(0,_posters);

			_users.Clear();
			_users.InsertRange(0, _requestors);
			_users.InsertRange(0, _posters );
		}

        private void UpdatePosters()
        {
            if (_numberOfPosters == _posters.Count) return;

            if (_numberOfPosters > _posters.Count)
            {
                while (_numberOfPosters > _posters.Count)
                    _posters.Add(new TestUser(NextUserK, this._postFrequency));
            }
            else
            {
                while (_posters.Count > _numberOfPosters)
                    _posters.RemoveAt(_posters.Count - 1);
            }
        }
        private void UpdateRequestors()
        {
            if (_numberOfRequestors == _requestors.Count) return;

            if (_numberOfRequestors > _requestors.Count)
            {
                while (_numberOfRequestors > _requestors.Count)
                    _requestors.Add(new TestUser(NextUserK));
            }
            else
            {
                while (_requestors.Count > _numberOfRequestors)
                    _requestors.RemoveAt(_requestors.Count - 1);
            }
        }
		#endregion



      






	}

	public class ChatServerFactory
	{
		//never instantiated
		private ChatServerFactory()
		{ }

		static string _localAddress = GetMyIP() + ":9000";
		static string _specificAddress = GetMyIP() + ":9000";


        public static ChatServerInterface GetChatServer(ServerPickerType serverPickerType)
		{
			string address = _localAddress;


			switch (serverPickerType)
			{
				case ServerPickerType.Local:
					address = _localAddress;
					break;
				case ServerPickerType.Cycle:
					address = GetNextAddress();
					break;
				case ServerPickerType.Random:
					address = GetRandomAddress();
					break;
				case ServerPickerType.Specific:
					address = _specificAddress ;
					break;
			}
            return (ChatServerInterface)Activator.GetObject(typeof(ChatServerInterface), "tcp://" + address + "/Reg");

		}

		public static string GetMyIP()
		{
			string hostName = Dns.GetHostName();
			foreach (IPAddress ipAddress in Dns.GetHostAddresses(hostName))
			{
				if (ipAddress != System.Net.IPAddress.Loopback)
					return ipAddress.ToString();
			}
			return null;
		}

		private static string GetNextAddress()
		{
			return _localAddress;
		}

		private static string GetRandomAddress()
		{
			return _localAddress;
		}

		public static string SpecificAddress
		{
			get { return _specificAddress; }
			set { _specificAddress = value; }
		}

	}

	#region Test User

	public class TestUser
	{
		public TestUser(int usrK, int frequencyOfPost)
		{
			_usrK = usrK;
			_frequencyOfPost = frequencyOfPost;
			_chatRoomIds.Add(new Guid("77777777-7777-7777-7777-777777777777"));
		}
		public TestUser(int usrK)
		{
			_usrK = usrK;
			_chatRoomIds.Add(new Guid("77777777-7777-7777-7777-777777777777"));
			//this is required to get them registered on the chatserver
			this.Post("Signing on!",ChatServerFactory.GetChatServer(Form1.ServerPicker));
		}
		
		int _usrK;
		int _frequencyOfPost;
		long _lastPost;
        long _lastRequest;
		List<Guid> _chatRoomIds = new List<Guid>();
		int _frequencyOfRequest = 10;
		int sessionId;
		bool firstRequest = true;
		int _noOfPosts = 0;
		int _noOfRequests = 0;
        StringBuilder _received = new StringBuilder();

		public bool MustPost
		{
			get { return (_frequencyOfPost!=0 && (DateTime.Now.Ticks - _lastPost) > TimeSpan.FromSeconds(_frequencyOfPost).Ticks); }
		}

		public bool MustRequest
		{
			get { return ((DateTime.Now.Ticks - _lastRequest) > TimeSpan.FromSeconds(_frequencyOfRequest).Ticks); }
		}
		

		public int UsrK
		{
			get { return _usrK; }
		}

        public int PostFrequency
        {
            set { _frequencyOfPost = value; }
        }

        public int RequestFrequency
        {
            set { _frequencyOfRequest = value; }
        }

        public void Post(ChatServerInterface cs)
        {
            try
            {
                _lastPost = DateTime.Now.Ticks;
                string testMessage = string.Format("<chatItem dateTime=\"{0}\" guid=\"{1}\" type=\"1\"><chatMessage nickName=\"test\" stmu=\"0,0,0,0,0,0,0,1,0,0\" usrK=\"26579\" pic=\"6af53b8e-e2cc-43f6-8beb-f948d96abee4\" k=\"{2}\"> Hello, I'm User {2}. This is message {3} </chatMessage></chatItem>", DateTime.Now.Ticks, Guid.NewGuid().ToString(), _usrK, _noOfPosts);
                cs.SendTo(_chatRoomIds[0], testMessage, _usrK);
                _noOfPosts++;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Post(string message, ChatServerInterface cs)
        {
            try
            {
                _lastPost = DateTime.Now.Ticks;
                string testMessage = string.Format("<chatItem dateTime=\"{0}\" guid=\"{1}\" type=\"1\"><chatMessage nickName=\"test_{2}\" stmu=\"0,0,0,0,0,0,0,1,0,0\" usrK=\"26579\" pic=\"6af53b8e-e2cc-43f6-8beb-f948d96abee4\" k=\"{2}\"> {4}. This is message {3} </chatMessage></chatItem>", DateTime.Now.Ticks, Guid.NewGuid().ToString(), _usrK, _noOfPosts,message );
                cs.SendTo(_chatRoomIds[0], testMessage, _usrK);
                _noOfPosts++;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Request(ChatServerInterface cs)
		{
            _lastRequest = DateTime.Now.Ticks;
			_received.Append( cs.GetLatest(_usrK, sessionId, firstRequest) );
            if (_received.Length > 100000)
                _received = new StringBuilder();
			_noOfRequests++;
			firstRequest = false;
				
		}

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append("User     : ");
            output.Append(UsrK);
            output.Append(System.Environment.NewLine);
            output.Append("Posts    : ");
            output.Append(_noOfPosts );
            output.Append(System.Environment.NewLine);
            output.Append("Requests : ");
            output.Append(_noOfRequests);
            output.Append(System.Environment.NewLine);
            output.Append(_received.ToString());

            return output.ToString();

        }
	}

	#endregion
}
