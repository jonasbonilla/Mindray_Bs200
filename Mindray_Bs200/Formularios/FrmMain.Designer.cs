namespace Mindray_Bs200.Formularios
{
    partial class FrmMain
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            _txtLog = new TextBox();
            tcpServer1 = new tcpServer.TcpServer(components);
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            _txtText = new TextBox();
            _label2 = new Label();
            _txtPort = new TextBox();
            _label3 = new Label();
            _btnChangePort = new Button();
            _btnSend = new Button();
            _btnClose = new Button();
            _label4 = new Label();
            _lblStatus = new Label();
            _label5 = new Label();
            _txtIdleTime = new TextBox();
            _label6 = new Label();
            _txtMaxThreads = new TextBox();
            _label7 = new Label();
            _txtAttempts = new TextBox();
            _lblConnected = new Label();
            _label9 = new Label();
            _label8 = new Label();
            _txtValidateInterval = new TextBox();
            _dgvResultado = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_dgvResultado).BeginInit();
            SuspendLayout();
            // 
            // _txtLog
            // 
            _txtLog.BackColor = Color.White;
            _txtLog.ForeColor = Color.Black;
            _txtLog.Location = new Point(261, 2);
            _txtLog.Margin = new Padding(4, 3, 4, 3);
            _txtLog.Multiline = true;
            _txtLog.Name = "_txtLog";
            _txtLog.ReadOnly = true;
            _txtLog.ScrollBars = ScrollBars.Vertical;
            _txtLog.Size = new Size(506, 197);
            _txtLog.TabIndex = 0;
            // 
            // tcpServer1
            // 
            tcpServer1.Encoding = null;
            tcpServer1.IdleTime = 50;
            tcpServer1.IsOpen = false;
            tcpServer1.MaxCallbackThreads = 100;
            tcpServer1.MaxSendAttempts = 3;
            tcpServer1.Port = -1;
            tcpServer1.VerifyConnectionInterval = 0;
            tcpServer1.OnConnect += tcpServer1_OnConnect;
            tcpServer1.OnDataAvailable += tcpServer1_OnDataAvailable;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.BS_200e;
            pictureBox1.Location = new Point(2, 2);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(252, 197);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // _txtText
            // 
            _txtText.Enabled = false;
            _txtText.Location = new Point(2, 415);
            _txtText.Margin = new Padding(4, 3, 4, 3);
            _txtText.Multiline = true;
            _txtText.Name = "_txtText";
            _txtText.ScrollBars = ScrollBars.Vertical;
            _txtText.Size = new Size(696, 32);
            _txtText.TabIndex = 1;
            // 
            // _label2
            // 
            _label2.AutoSize = true;
            _label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label2.ForeColor = Color.Black;
            _label2.Location = new Point(2, 397);
            _label2.Margin = new Padding(4, 0, 4, 0);
            _label2.Name = "_label2";
            _label2.Size = new Size(100, 13);
            _label2.TabIndex = 2;
            _label2.Text = "Texto de difusión:";
            // 
            // _txtPort
            // 
            _txtPort.Enabled = false;
            _txtPort.Location = new Point(55, 485);
            _txtPort.Margin = new Padding(4, 3, 4, 3);
            _txtPort.Name = "_txtPort";
            _txtPort.Size = new Size(42, 23);
            _txtPort.TabIndex = 3;
            _txtPort.Text = "5000";
            // 
            // _label3
            // 
            _label3.AutoSize = true;
            _label3.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label3.ForeColor = Color.Black;
            _label3.Location = new Point(2, 488);
            _label3.Margin = new Padding(4, 0, 4, 0);
            _label3.Name = "_label3";
            _label3.Size = new Size(45, 13);
            _label3.TabIndex = 4;
            _label3.Text = "Puerto:";
            // 
            // _btnChangePort
            // 
            _btnChangePort.Enabled = false;
            _btnChangePort.Location = new Point(596, 458);
            _btnChangePort.Margin = new Padding(4, 3, 4, 3);
            _btnChangePort.Name = "_btnChangePort";
            _btnChangePort.Size = new Size(88, 51);
            _btnChangePort.TabIndex = 5;
            _btnChangePort.Text = "Cambiar";
            _btnChangePort.UseVisualStyleBackColor = true;
            _btnChangePort.Click += _btnChangePort_Click;
            // 
            // _btnSend
            // 
            _btnSend.Enabled = false;
            _btnSend.Location = new Point(706, 415);
            _btnSend.Margin = new Padding(4, 3, 4, 3);
            _btnSend.Name = "_btnSend";
            _btnSend.Size = new Size(62, 32);
            _btnSend.TabIndex = 6;
            _btnSend.Text = "Enviar";
            _btnSend.UseVisualStyleBackColor = true;
            _btnSend.Click += _btnSend_Click;
            // 
            // _btnClose
            // 
            _btnClose.Enabled = false;
            _btnClose.Location = new Point(682, 458);
            _btnClose.Margin = new Padding(4, 3, 4, 3);
            _btnClose.Name = "_btnClose";
            _btnClose.Size = new Size(85, 51);
            _btnClose.TabIndex = 7;
            _btnClose.Text = "Cerrar";
            _btnClose.UseVisualStyleBackColor = true;
            _btnClose.Click += _btnClose_Click;
            // 
            // _label4
            // 
            _label4.AutoSize = true;
            _label4.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label4.ForeColor = Color.Black;
            _label4.Location = new Point(2, 458);
            _label4.Margin = new Padding(4, 0, 4, 0);
            _label4.Name = "_label4";
            _label4.Size = new Size(113, 13);
            _label4.TabIndex = 8;
            _label4.Text = "Estado servidor TCP:";
            // 
            // _lblStatus
            // 
            _lblStatus.AutoSize = true;
            _lblStatus.BackColor = Color.Coral;
            _lblStatus.ForeColor = Color.Black;
            _lblStatus.Location = new Point(134, 458);
            _lblStatus.Margin = new Padding(4, 0, 4, 0);
            _lblStatus.Name = "_lblStatus";
            _lblStatus.Size = new Size(98, 15);
            _lblStatus.TabIndex = 9;
            _lblStatus.Text = "NO CONECTADO";
            // 
            // _label5
            // 
            _label5.AutoSize = true;
            _label5.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label5.ForeColor = Color.Black;
            _label5.Location = new Point(126, 488);
            _label5.Margin = new Padding(4, 0, 4, 0);
            _label5.Name = "_label5";
            _label5.Size = new Size(67, 13);
            _label5.TabIndex = 10;
            _label5.Text = "Inactividad:";
            // 
            // _txtIdleTime
            // 
            _txtIdleTime.Enabled = false;
            _txtIdleTime.Location = new Point(202, 485);
            _txtIdleTime.Margin = new Padding(4, 3, 4, 3);
            _txtIdleTime.Name = "_txtIdleTime";
            _txtIdleTime.Size = new Size(44, 23);
            _txtIdleTime.TabIndex = 11;
            _txtIdleTime.Text = "50";
            _txtIdleTime.TextChanged += _txtIdleTime_TextChanged;
            // 
            // _label6
            // 
            _label6.AutoSize = true;
            _label6.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label6.ForeColor = Color.Black;
            _label6.Location = new Point(282, 489);
            _label6.Margin = new Padding(4, 0, 4, 0);
            _label6.Name = "_label6";
            _label6.Size = new Size(36, 13);
            _label6.TabIndex = 12;
            _label6.Text = "Hilos:";
            // 
            // _txtMaxThreads
            // 
            _txtMaxThreads.Enabled = false;
            _txtMaxThreads.Location = new Point(328, 486);
            _txtMaxThreads.Margin = new Padding(4, 3, 4, 3);
            _txtMaxThreads.Name = "_txtMaxThreads";
            _txtMaxThreads.Size = new Size(54, 23);
            _txtMaxThreads.TabIndex = 13;
            _txtMaxThreads.Text = "100";
            _txtMaxThreads.TextChanged += _txtMaxThreads_TextChanged;
            // 
            // _label7
            // 
            _label7.AutoSize = true;
            _label7.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label7.ForeColor = Color.Black;
            _label7.Location = new Point(404, 458);
            _label7.Margin = new Padding(4, 0, 4, 0);
            _label7.Name = "_label7";
            _label7.Size = new Size(101, 13);
            _label7.TabIndex = 14;
            _label7.Text = "Intentos de envío:";
            // 
            // _txtAttempts
            // 
            _txtAttempts.Enabled = false;
            _txtAttempts.Location = new Point(532, 455);
            _txtAttempts.Margin = new Padding(4, 3, 4, 3);
            _txtAttempts.Name = "_txtAttempts";
            _txtAttempts.Size = new Size(46, 23);
            _txtAttempts.TabIndex = 15;
            _txtAttempts.Text = "3";
            _txtAttempts.TextChanged += _txtAttempts_TextChanged;
            // 
            // _lblConnected
            // 
            _lblConnected.AutoSize = true;
            _lblConnected.BackColor = Color.Transparent;
            _lblConnected.ForeColor = Color.Black;
            _lblConnected.Location = new Point(365, 458);
            _lblConnected.Margin = new Padding(4, 0, 4, 0);
            _lblConnected.Name = "_lblConnected";
            _lblConnected.Size = new Size(13, 15);
            _lblConnected.TabIndex = 16;
            _lblConnected.Text = "0";
            // 
            // _label9
            // 
            _label9.AutoSize = true;
            _label9.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label9.ForeColor = Color.Black;
            _label9.Location = new Point(282, 458);
            _label9.Margin = new Padding(4, 0, 4, 0);
            _label9.Name = "_label9";
            _label9.Size = new Size(71, 13);
            _label9.TabIndex = 17;
            _label9.Text = "Conexiones:";
            // 
            // _label8
            // 
            _label8.AutoSize = true;
            _label8.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label8.ForeColor = Color.Black;
            _label8.Location = new Point(404, 488);
            _label8.Margin = new Padding(4, 0, 4, 0);
            _label8.Name = "_label8";
            _label8.Size = new Size(112, 13);
            _label8.TabIndex = 18;
            _label8.Text = "Intervalo validación:";
            // 
            // _txtValidateInterval
            // 
            _txtValidateInterval.Enabled = false;
            _txtValidateInterval.Location = new Point(532, 485);
            _txtValidateInterval.Margin = new Padding(4, 3, 4, 3);
            _txtValidateInterval.Name = "_txtValidateInterval";
            _txtValidateInterval.Size = new Size(46, 23);
            _txtValidateInterval.TabIndex = 19;
            _txtValidateInterval.Text = "100";
            _txtValidateInterval.TextChanged += _txtValidateInterval_TextChanged;
            // 
            // _dgvResultado
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            _dgvResultado.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            _dgvResultado.BackgroundColor = Color.White;
            _dgvResultado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvResultado.Location = new Point(2, 207);
            _dgvResultado.Margin = new Padding(4, 3, 4, 3);
            _dgvResultado.Name = "_dgvResultado";
            _dgvResultado.Size = new Size(765, 186);
            _dgvResultado.TabIndex = 20;
            _dgvResultado.DataBindingComplete += dataGridView1_DataBindingComplete;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(770, 515);
            Controls.Add(pictureBox1);
            Controls.Add(_dgvResultado);
            Controls.Add(_txtValidateInterval);
            Controls.Add(_label8);
            Controls.Add(_label9);
            Controls.Add(_lblConnected);
            Controls.Add(_txtAttempts);
            Controls.Add(_label7);
            Controls.Add(_txtMaxThreads);
            Controls.Add(_label6);
            Controls.Add(_txtIdleTime);
            Controls.Add(_label5);
            Controls.Add(_lblStatus);
            Controls.Add(_label4);
            Controls.Add(_btnClose);
            Controls.Add(_btnSend);
            Controls.Add(_btnChangePort);
            Controls.Add(_label3);
            Controls.Add(_txtPort);
            Controls.Add(_label2);
            Controls.Add(_txtText);
            Controls.Add(_txtLog);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BS-200/E TCP - TECNOMEDIC";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)_dgvResultado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox _txtLog;
        private tcpServer.TcpServer tcpServer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox _txtText;
        private System.Windows.Forms.Label _label2;
        private System.Windows.Forms.TextBox _txtPort;
        private System.Windows.Forms.Label _label3;
        private System.Windows.Forms.Button _btnChangePort;
        private System.Windows.Forms.Button _btnSend;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Label _label4;
        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Label _label5;
        private System.Windows.Forms.TextBox _txtIdleTime;
        private System.Windows.Forms.Label _label6;
        private System.Windows.Forms.TextBox _txtMaxThreads;
        private System.Windows.Forms.Label _label7;
        private System.Windows.Forms.TextBox _txtAttempts;
        private System.Windows.Forms.Label _lblConnected;
        private System.Windows.Forms.Label _label9;
        private System.Windows.Forms.Label _label8;
        private System.Windows.Forms.TextBox _txtValidateInterval;
        private System.Windows.Forms.DataGridView _dgvResultado;
    }
}