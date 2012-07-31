using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;



namespace BobsCommonFileGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
	 
        private void button1_Click(object sender, EventArgs e)
        {
			ClassGenerator cg = new ClassGenerator(this.uiConnectionString.Text);
			cg.Progress += new ClassGenerator.ProgressDelegate(cg_Progress);	
			this.Cursor = Cursors.WaitCursor;
            this.richTextBox1.Text = "";
			progressBar1.Value = 0;
			progressBar1.Maximum = 100;
			progressBar1.Increment(5);
			progressBar1.Step = 1;
			progressBar1.Refresh();
			progressBar1.Maximum = cg.NumberOfTables() * 4 + 60;
			progressBar1.Increment(50);
			progressBar1.Refresh();
			
			


			progressBar1.Value = progressBar1.Maximum;
            this.richTextBox1.Text = cg.GetPartialClassesFromDatabase();


            this.Cursor = Cursors.Default;
        }

		void cg_Progress() {
			progressBar1.PerformStep();
			progressBar1.Refresh();
		}

		
    }
}
