namespace FingerRing
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cboShockPort = new System.Windows.Forms.ComboBox();
            this.btnPortControl = new System.Windows.Forms.Button();
            this.btnRefreshPort = new System.Windows.Forms.Button();
            this.serialPortShock = new System.IO.Ports.SerialPort(this.components);
            this.cboProxPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialPortProx = new System.IO.Ports.SerialPort(this.components);
            this.txtboxReceive_Shock = new System.Windows.Forms.TextBox();
            this.lblSampleRate_Shock = new System.Windows.Forms.Label();
            this.lblSampleReceived_Shock = new System.Windows.Forms.Label();
            this.btnScopeControl = new System.Windows.Forms.Button();
            this.txtboxReceive_Prox = new System.Windows.Forms.TextBox();
            this.lblSampleRate_Prox = new System.Windows.Forms.Label();
            this.lblSampleReceived_Prox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboShockPort
            // 
            this.cboShockPort.FormattingEnabled = true;
            this.cboShockPort.Location = new System.Drawing.Point(39, 88);
            this.cboShockPort.Name = "cboShockPort";
            this.cboShockPort.Size = new System.Drawing.Size(114, 21);
            this.cboShockPort.TabIndex = 0;
            // 
            // btnPortControl
            // 
            this.btnPortControl.Location = new System.Drawing.Point(126, 126);
            this.btnPortControl.Name = "btnPortControl";
            this.btnPortControl.Size = new System.Drawing.Size(83, 35);
            this.btnPortControl.TabIndex = 1;
            this.btnPortControl.Text = "Open\r\nPorts";
            this.btnPortControl.UseVisualStyleBackColor = true;
            this.btnPortControl.Click += new System.EventHandler(this.btnPortControl_Click);
            // 
            // btnRefreshPort
            // 
            this.btnRefreshPort.Location = new System.Drawing.Point(39, 126);
            this.btnRefreshPort.Name = "btnRefreshPort";
            this.btnRefreshPort.Size = new System.Drawing.Size(81, 35);
            this.btnRefreshPort.TabIndex = 2;
            this.btnRefreshPort.Text = "Refresh\r\nPorts";
            this.btnRefreshPort.UseVisualStyleBackColor = true;
            this.btnRefreshPort.Click += new System.EventHandler(this.btnRefreshPort_Click);
            // 
            // cboProxPort
            // 
            this.cboProxPort.FormattingEnabled = true;
            this.cboProxPort.Location = new System.Drawing.Point(191, 88);
            this.cboProxPort.Name = "cboProxPort";
            this.cboProxPort.Size = new System.Drawing.Size(100, 21);
            this.cboProxPort.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 58);
            this.label1.TabIndex = 5;
            this.label1.Text = "Shock\r\nSensor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(186, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 58);
            this.label2.TabIndex = 6;
            this.label2.Text = "Proximity\r\nSensor";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtboxReceive_Shock
            // 
            this.txtboxReceive_Shock.Location = new System.Drawing.Point(39, 179);
            this.txtboxReceive_Shock.Multiline = true;
            this.txtboxReceive_Shock.Name = "txtboxReceive_Shock";
            this.txtboxReceive_Shock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtboxReceive_Shock.Size = new System.Drawing.Size(114, 188);
            this.txtboxReceive_Shock.TabIndex = 7;
            // 
            // lblSampleRate_Shock
            // 
            this.lblSampleRate_Shock.AutoSize = true;
            this.lblSampleRate_Shock.Location = new System.Drawing.Point(36, 385);
            this.lblSampleRate_Shock.Name = "lblSampleRate_Shock";
            this.lblSampleRate_Shock.Size = new System.Drawing.Size(75, 13);
            this.lblSampleRate_Shock.TabIndex = 9;
            this.lblSampleRate_Shock.Text = "lblSampleRate";
            // 
            // lblSampleReceived_Shock
            // 
            this.lblSampleReceived_Shock.AutoSize = true;
            this.lblSampleReceived_Shock.Location = new System.Drawing.Point(36, 407);
            this.lblSampleReceived_Shock.Name = "lblSampleReceived_Shock";
            this.lblSampleReceived_Shock.Size = new System.Drawing.Size(98, 13);
            this.lblSampleReceived_Shock.TabIndex = 11;
            this.lblSampleReceived_Shock.Text = "lblSampleReceived";
            // 
            // btnScopeControl
            // 
            this.btnScopeControl.Location = new System.Drawing.Point(215, 126);
            this.btnScopeControl.Name = "btnScopeControl";
            this.btnScopeControl.Size = new System.Drawing.Size(76, 35);
            this.btnScopeControl.TabIndex = 12;
            this.btnScopeControl.Text = "Open Oscilloscope";
            this.btnScopeControl.UseVisualStyleBackColor = true;
            this.btnScopeControl.Click += new System.EventHandler(this.btnScopeControl_Click);
            // 
            // txtboxReceive_Prox
            // 
            this.txtboxReceive_Prox.Location = new System.Drawing.Point(171, 179);
            this.txtboxReceive_Prox.Multiline = true;
            this.txtboxReceive_Prox.Name = "txtboxReceive_Prox";
            this.txtboxReceive_Prox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtboxReceive_Prox.Size = new System.Drawing.Size(120, 188);
            this.txtboxReceive_Prox.TabIndex = 13;
            // 
            // lblSampleRate_Prox
            // 
            this.lblSampleRate_Prox.AutoSize = true;
            this.lblSampleRate_Prox.Location = new System.Drawing.Point(168, 385);
            this.lblSampleRate_Prox.Name = "lblSampleRate_Prox";
            this.lblSampleRate_Prox.Size = new System.Drawing.Size(75, 13);
            this.lblSampleRate_Prox.TabIndex = 14;
            this.lblSampleRate_Prox.Text = "lblSampleRate";
            // 
            // lblSampleReceived_Prox
            // 
            this.lblSampleReceived_Prox.AutoSize = true;
            this.lblSampleReceived_Prox.Location = new System.Drawing.Point(168, 407);
            this.lblSampleReceived_Prox.Name = "lblSampleReceived_Prox";
            this.lblSampleReceived_Prox.Size = new System.Drawing.Size(98, 13);
            this.lblSampleReceived_Prox.TabIndex = 15;
            this.lblSampleReceived_Prox.Text = "lblSampleReceived";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 438);
            this.Controls.Add(this.lblSampleReceived_Prox);
            this.Controls.Add(this.lblSampleRate_Prox);
            this.Controls.Add(this.txtboxReceive_Prox);
            this.Controls.Add(this.btnScopeControl);
            this.Controls.Add(this.lblSampleReceived_Shock);
            this.Controls.Add(this.lblSampleRate_Shock);
            this.Controls.Add(this.txtboxReceive_Shock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProxPort);
            this.Controls.Add(this.btnRefreshPort);
            this.Controls.Add(this.btnPortControl);
            this.Controls.Add(this.cboShockPort);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboShockPort;
        private System.Windows.Forms.Button btnPortControl;
        private System.Windows.Forms.Button btnRefreshPort;
        private System.IO.Ports.SerialPort serialPortShock;
        private System.Windows.Forms.ComboBox cboProxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort serialPortProx;
        private System.Windows.Forms.TextBox txtboxReceive_Shock;
        private System.Windows.Forms.Label lblSampleRate_Shock;
        private System.Windows.Forms.Label lblSampleReceived_Shock;
        private System.Windows.Forms.Button btnScopeControl;
        private System.Windows.Forms.TextBox txtboxReceive_Prox;
        private System.Windows.Forms.Label lblSampleRate_Prox;
        private System.Windows.Forms.Label lblSampleReceived_Prox;
    }
}

