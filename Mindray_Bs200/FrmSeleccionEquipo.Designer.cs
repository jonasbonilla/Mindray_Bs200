namespace Mindray_Bs200
{
    partial class FrmSeleccionEquipo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            _label2 = new Label();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            _btnClose = new Button();
            _btnChangePort = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // _label2
            // 
            _label2.AutoSize = true;
            _label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _label2.ForeColor = Color.Black;
            _label2.Location = new Point(6, 6);
            _label2.Margin = new Padding(4, 0, 4, 0);
            _label2.Name = "_label2";
            _label2.Size = new Size(200, 13);
            _label2.TabIndex = 3;
            _label2.Text = "Seleccione el equipo para conectarse:";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(2, 25);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(408, 167);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "ID";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 50;
            // 
            // Column2
            // 
            Column2.HeaderText = "EQUIPO";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 220;
            // 
            // Column3
            // 
            Column3.HeaderText = "PUERTO";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 60;
            // 
            // _btnClose
            // 
            _btnClose.Location = new Point(2, 200);
            _btnClose.Margin = new Padding(4, 3, 4, 3);
            _btnClose.Name = "_btnClose";
            _btnClose.Size = new Size(202, 39);
            _btnClose.TabIndex = 9;
            _btnClose.Text = "Cancelar (Salir)";
            _btnClose.UseVisualStyleBackColor = true;
            _btnClose.Click += _btnClose_Click;
            // 
            // _btnChangePort
            // 
            _btnChangePort.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _btnChangePort.Location = new Point(214, 200);
            _btnChangePort.Margin = new Padding(4, 3, 4, 3);
            _btnChangePort.Name = "_btnChangePort";
            _btnChangePort.Size = new Size(197, 39);
            _btnChangePort.TabIndex = 8;
            _btnChangePort.Text = "Aceptar (Continuar)";
            _btnChangePort.UseVisualStyleBackColor = true;
            _btnChangePort.Click += _btnChangePort_Click;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.Refresh_icon;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(386, 0);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(24, 24);
            button1.TabIndex = 10;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmSeleccionEquipo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(413, 245);
            Controls.Add(button1);
            Controls.Add(_btnClose);
            Controls.Add(_btnChangePort);
            Controls.Add(dataGridView1);
            Controls.Add(_label2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmSeleccionEquipo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EQUIPO ANALIZADOR - TECNOMEDIC";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label _label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Button _btnChangePort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}