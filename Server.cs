// Server code, program 2
// Ryan Rozelle - CS553 Fall 2012

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Services;


namespace Server
{
	public class frmMain : System.Windows.Forms.Form
	{
		// namespace Remote is located in Remoting.DLL
		private Remote.cTransfer            mi_Transfer   = null;
		private ObjRef                      mi_Service    = null;
		private TcpChannel                  mi_Channel    = null;
		private bool                        mb_WaitButton = false;
        private TextBox data1;
        private TextBox data2;
        private TextBox data4;
        private TextBox data3;
        private TextBox data6;
        private TextBox data5;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;

		public frmMain()
		{
			InitializeComponent();
			checkBoxListen.Checked = true; // calls StartListen()
		}

		private void textBoxPort_TextChanged(object sender, EventArgs e)
		{
			checkBoxListen.Checked = false; // calls StopListen()
		}

		private void checkBoxListen_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBoxListen.Checked) StartListen();
			else                        StopListen();
		}

		public void StopListen()
		{
			if (mi_Service != null)
				RemotingServices.Unmarshal (mi_Service);

			if (mi_Transfer != null)
				RemotingServices.Disconnect(mi_Transfer);

			if (mi_Channel != null)
				ChannelServices.UnregisterChannel(mi_Channel);

			mi_Service  = null;
			mi_Transfer = null;
			mi_Channel  = null;
		}

		public void StartListen()
		{
			StopListen(); // if there is any channel still open --> close it

			try
			{
				int s32_Port = int.Parse(textBoxPort.Text);

				mi_Channel   = new TcpChannel(s32_Port);
				ChannelServices.RegisterChannel(mi_Channel);

				mi_Transfer  = new Remote.cTransfer();
				mi_Service = RemotingServices.Marshal(mi_Transfer, "TestService");

				// define the event which is triggered when the Client calls the CallServer() function
				mi_Transfer.ev_ServerCall += new Remote.cTransfer.del_ServerCall(OnClientEvent);
			}
			catch (Exception Ex)
			{
				MessageBox.Show(this, "Error starting listening:\n" + Ex.Message, "Server");
				checkBoxListen.Checked = false; // calls StopListen()
			}			
		}

		Remote.kResponse OnClientEvent(Remote.kAction k_Action)
		{
			Remote.kResponse k_Response = new Remote.kResponse();

			// If multiple Clients try to connect at once
			if (mb_WaitButton)
			{
				k_Response.s_Result = "Sorry! Server is currently busy.\r\nTry again later";
				return k_Response;
			}

            data1.Text = string.Format("{0}", k_Action.frictionL);
            data2.Text = string.Format("{0}", k_Action.frictionR);
            data3.Text = string.Format("{0}", k_Action.tempL);
            data4.Text = string.Format("{0}", k_Action.tempR);
            data5.Text = string.Format("{0}", k_Action.interiorTemp);
            data6.Text = string.Format("{0}", k_Action.brake);

            while (int.Parse(data6.Text) > 0)
            {
                data1.Text = string.Format("{0}", (int.Parse(data1.Text) + int.Parse(data6.Text)));
                data2.Text = string.Format("{0}", (int.Parse(data2.Text) + int.Parse(data6.Text)));
                if (int.Parse(data1.Text) > 100) data1.Text = "100";
                if (int.Parse(data2.Text) > 100) data2.Text = "100";
                System.Threading.Thread.Sleep(500);
                if (int.Parse(data1.Text) == 100 && int.Parse(data2.Text) == 100) data6.Text = "0";
            }

			return k_Response;
		}

		#region Windows Form Designer generated code

        private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxListen;
		private System.Windows.Forms.TextBox textBoxPort;



		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxListen = new System.Windows.Forms.CheckBox();
            this.data1 = new System.Windows.Forms.TextBox();
            this.data2 = new System.Windows.Forms.TextBox();
            this.data4 = new System.Windows.Forms.TextBox();
            this.data3 = new System.Windows.Forms.TextBox();
            this.data6 = new System.Windows.Forms.TextBox();
            this.data5 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(159, 29);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(48, 20);
            this.textBoxPort.TabIndex = 14;
            this.textBoxPort.Text = "1500";
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxPort_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(159, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "TCP Port";
            // 
            // checkBoxListen
            // 
            this.checkBoxListen.Location = new System.Drawing.Point(95, 29);
            this.checkBoxListen.Name = "checkBoxListen";
            this.checkBoxListen.Size = new System.Drawing.Size(56, 24);
            this.checkBoxListen.TabIndex = 16;
            this.checkBoxListen.Text = "Listen";
            this.checkBoxListen.CheckedChanged += new System.EventHandler(this.checkBoxListen_CheckedChanged);
            // 
            // data1
            // 
            this.data1.Location = new System.Drawing.Point(95, 86);
            this.data1.Multiline = true;
            this.data1.Name = "data1";
            this.data1.ReadOnly = true;
            this.data1.Size = new System.Drawing.Size(39, 21);
            this.data1.TabIndex = 23;
            this.data1.Text = "0";
            // 
            // data2
            // 
            this.data2.Location = new System.Drawing.Point(95, 113);
            this.data2.Multiline = true;
            this.data2.Name = "data2";
            this.data2.ReadOnly = true;
            this.data2.Size = new System.Drawing.Size(39, 21);
            this.data2.TabIndex = 24;
            this.data2.Text = "0";
            // 
            // data4
            // 
            this.data4.Location = new System.Drawing.Point(95, 167);
            this.data4.Multiline = true;
            this.data4.Name = "data4";
            this.data4.ReadOnly = true;
            this.data4.Size = new System.Drawing.Size(39, 21);
            this.data4.TabIndex = 26;
            this.data4.Text = "0";
            // 
            // data3
            // 
            this.data3.Location = new System.Drawing.Point(95, 140);
            this.data3.Multiline = true;
            this.data3.Name = "data3";
            this.data3.ReadOnly = true;
            this.data3.Size = new System.Drawing.Size(39, 21);
            this.data3.TabIndex = 25;
            this.data3.Text = "0";
            // 
            // data6
            // 
            this.data6.Location = new System.Drawing.Point(95, 221);
            this.data6.Multiline = true;
            this.data6.Name = "data6";
            this.data6.ReadOnly = true;
            this.data6.Size = new System.Drawing.Size(39, 21);
            this.data6.TabIndex = 28;
            this.data6.Text = "0";
            // 
            // data5
            // 
            this.data5.Location = new System.Drawing.Point(95, 194);
            this.data5.Multiline = true;
            this.data5.Name = "data5";
            this.data5.ReadOnly = true;
            this.data5.Size = new System.Drawing.Size(39, 21);
            this.data5.TabIndex = 27;
            this.data5.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 18);
            this.label4.TabIndex = 29;
            this.label4.Text = "Friction, left:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 30;
            this.label5.Text = "Friction, right:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 32;
            this.label6.Text = "Temp, right:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 31;
            this.label7.Text = "Temp, left:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 34;
            this.label8.Text = "Brake:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 33;
            this.label9.Text = "Temp, interior:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(243, 260);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.data6);
            this.Controls.Add(this.data5);
            this.Controls.Add(this.data4);
            this.Controls.Add(this.data3);
            this.Controls.Add(this.data2);
            this.Controls.Add(this.data1);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.checkBoxListen);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(400, 250);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Server (dashboard)";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		#endregion
	}
}
