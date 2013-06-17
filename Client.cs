// Client code, program 2
// Ryan Rozelle - CS553 Fall 2012

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Services;

namespace Client
{
	public class frmMain : System.Windows.Forms.Form
	{
		// namespace Remote is located in Remoting.DLL
		private Remote.cTransfer            mi_Transfer = null;
        private Label label8;
        private Label label9;
        private Label label4;
        private Label label7;
        private Label label5;
        private Label label10;
        private TextBox data6;
        private TextBox data5;
        private TextBox data4;
        private TextBox data3;
        private TextBox data2;
        private TextBox data1;

		public frmMain()
		{
			InitializeComponent();
		}

		private void OnBtnSendClick(object sender, System.EventArgs e)
		{
			Remote.kAction k_Action = new Remote.kAction();
            
            k_Action.frictionL = int.Parse(data1.Text);
            k_Action.frictionR = int.Parse(data2.Text);
            k_Action.tempL = int.Parse(data3.Text);
            k_Action.tempR = int.Parse(data4.Text);
            k_Action.interiorTemp = int.Parse(data5.Text);
            k_Action.brake = int.Parse(data6.Text);

            if (k_Action.frictionL > 100) k_Action.frictionL = 100;
            else if (k_Action.frictionL < 0) k_Action.frictionL = 0;
            if (k_Action.frictionR > 100) k_Action.frictionR = 100;
            else if (k_Action.frictionR < 0) k_Action.frictionR = 0;
            if (k_Action.tempL > 100) k_Action.tempL = 100;
            else if (k_Action.tempL < 0) k_Action.tempL = 0;
            if (k_Action.tempR > 100) k_Action.tempR = 100;
            else if (k_Action.tempR < 0) k_Action.tempR = 0;
            if (k_Action.interiorTemp > 100) k_Action.interiorTemp = 100;
            else if (k_Action.interiorTemp < 0) k_Action.interiorTemp = 0;
            if (k_Action.brake > 10) k_Action.brake = 10;
            else if (k_Action.brake < 0) k_Action.brake = 0;

			this.Cursor = Cursors.WaitCursor;

			string s_URL = string.Format("tcp://{0}:{1}/TestService", textBoxComputer.Text, textBoxPort.Text);

			try
			{
				mi_Transfer = (Remote.cTransfer) Activator.GetObject(typeof(Remote.cTransfer), s_URL);

				// triggers the event mi_Transfer.ev_ServerCall in the Server
				Remote.kResponse k_Response = mi_Transfer.CallServer(k_Action);
			}
			catch (Exception Ex)
			{
				MessageBox.Show(this, "Error sending message to Server:\n" + Ex.Message, "Client Error");
			}

			this.Cursor = Cursors.Arrow;
		}


		#region Windows Form Designer generated code

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxComputer;
		private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button btnSend;
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxComputer = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.data6 = new System.Windows.Forms.TextBox();
            this.data5 = new System.Windows.Forms.TextBox();
            this.data4 = new System.Windows.Forms.TextBox();
            this.data3 = new System.Windows.Forms.TextBox();
            this.data2 = new System.Windows.Forms.TextBox();
            this.data1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(192, 142);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(44, 24);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.OnBtnSendClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Computer name or IP";
            // 
            // textBoxComputer
            // 
            this.textBoxComputer.Location = new System.Drawing.Point(16, 24);
            this.textBoxComputer.Name = "textBoxComputer";
            this.textBoxComputer.Size = new System.Drawing.Size(136, 20);
            this.textBoxComputer.TabIndex = 4;
            this.textBoxComputer.Text = "localhost";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(180, 24);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(56, 20);
            this.textBoxPort.TabIndex = 6;
            this.textBoxPort.Text = "1500";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(180, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "TCP Port";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 223);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 46;
            this.label8.Text = "Brake:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 45;
            this.label9.Text = "Temp, interior:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 44;
            this.label4.Text = "Temp, right:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 43;
            this.label7.Text = "Temp, left:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 42;
            this.label5.Text = "Friction, right:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(13, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 18);
            this.label10.TabIndex = 41;
            this.label10.Text = "Friction, left:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // data6
            // 
            this.data6.Location = new System.Drawing.Point(102, 220);
            this.data6.Multiline = true;
            this.data6.Name = "data6";
            this.data6.Size = new System.Drawing.Size(39, 21);
            this.data6.TabIndex = 40;
            this.data6.Text = "0";
            // 
            // data5
            // 
            this.data5.Location = new System.Drawing.Point(102, 193);
            this.data5.Multiline = true;
            this.data5.Name = "data5";
            this.data5.Size = new System.Drawing.Size(39, 21);
            this.data5.TabIndex = 39;
            this.data5.Text = "0";
            // 
            // data4
            // 
            this.data4.Location = new System.Drawing.Point(102, 166);
            this.data4.Multiline = true;
            this.data4.Name = "data4";
            this.data4.Size = new System.Drawing.Size(39, 21);
            this.data4.TabIndex = 38;
            this.data4.Text = "0";
            // 
            // data3
            // 
            this.data3.Location = new System.Drawing.Point(102, 139);
            this.data3.Multiline = true;
            this.data3.Name = "data3";
            this.data3.Size = new System.Drawing.Size(39, 21);
            this.data3.TabIndex = 37;
            this.data3.Text = "0";
            // 
            // data2
            // 
            this.data2.Location = new System.Drawing.Point(102, 112);
            this.data2.Multiline = true;
            this.data2.Name = "data2";
            this.data2.Size = new System.Drawing.Size(39, 21);
            this.data2.TabIndex = 36;
            this.data2.Text = "0";
            // 
            // data1
            // 
            this.data1.Location = new System.Drawing.Point(102, 85);
            this.data1.Multiline = true;
            this.data1.Name = "data1";
            this.data1.Size = new System.Drawing.Size(39, 21);
            this.data1.TabIndex = 35;
            this.data1.Text = "0";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(272, 266);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.data6);
            this.Controls.Add(this.data5);
            this.Controls.Add(this.data4);
            this.Controls.Add(this.data3);
            this.Controls.Add(this.data2);
            this.Controls.Add(this.data1);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxComputer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(50, 250);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Client (sensors)";
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
