namespace MFW.WF
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gpCallList = new System.Windows.Forms.GroupBox();
            this.gvCalls = new System.Windows.Forms.DataGridView();
            this.gpDevices = new System.Windows.Forms.GroupBox();
            this.cbxVideoInput = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAudioOutput = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxAudioInput = new System.Windows.Forms.ComboBox();
            this.lblAudioInput = new System.Windows.Forms.Label();
            this.txtCallee = new System.Windows.Forms.TextBox();
            this.btnAudioCall = new System.Windows.Forms.Button();
            this.btnVideoCall = new System.Windows.Forms.Button();
            this.btm7 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn9 = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.CallName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CallState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAnswer = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnHold = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnResume = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnHangup = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CallHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpCallList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCalls)).BeginInit();
            this.gpDevices.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpCallList
            // 
            this.gpCallList.Controls.Add(this.gvCalls);
            this.gpCallList.Location = new System.Drawing.Point(12, 12);
            this.gpCallList.Name = "gpCallList";
            this.gpCallList.Size = new System.Drawing.Size(523, 355);
            this.gpCallList.TabIndex = 0;
            this.gpCallList.TabStop = false;
            this.gpCallList.Text = "呼叫列表";
            // 
            // gvCalls
            // 
            this.gvCalls.AllowUserToAddRows = false;
            this.gvCalls.AllowUserToDeleteRows = false;
            this.gvCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvCalls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CallName,
            this.CallState,
            this.btnAnswer,
            this.btnHold,
            this.btnResume,
            this.btnHangup,
            this.CallHandle});
            this.gvCalls.Location = new System.Drawing.Point(6, 18);
            this.gvCalls.Name = "gvCalls";
            this.gvCalls.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCalls.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCalls.RowTemplate.Height = 23;
            this.gvCalls.Size = new System.Drawing.Size(511, 331);
            this.gvCalls.TabIndex = 0;
            this.gvCalls.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCalls_CellClick);
            this.gvCalls.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvCalls_CellFormatting);
            this.gvCalls.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gvCalls_CellPainting);
            // 
            // gpDevices
            // 
            this.gpDevices.Controls.Add(this.cbxVideoInput);
            this.gpDevices.Controls.Add(this.label2);
            this.gpDevices.Controls.Add(this.cbxAudioOutput);
            this.gpDevices.Controls.Add(this.label1);
            this.gpDevices.Controls.Add(this.cbxAudioInput);
            this.gpDevices.Controls.Add(this.lblAudioInput);
            this.gpDevices.Location = new System.Drawing.Point(553, 12);
            this.gpDevices.Name = "gpDevices";
            this.gpDevices.Size = new System.Drawing.Size(219, 96);
            this.gpDevices.TabIndex = 1;
            this.gpDevices.TabStop = false;
            this.gpDevices.Text = "设备";
            // 
            // cbxVideoInput
            // 
            this.cbxVideoInput.DisplayMember = "DeviceName";
            this.cbxVideoInput.FormattingEnabled = true;
            this.cbxVideoInput.Location = new System.Drawing.Point(65, 70);
            this.cbxVideoInput.Name = "cbxVideoInput";
            this.cbxVideoInput.Size = new System.Drawing.Size(148, 20);
            this.cbxVideoInput.TabIndex = 1;
            this.cbxVideoInput.ValueMember = "DeviceHandle";
            this.cbxVideoInput.SelectedIndexChanged += new System.EventHandler(this.cbxVideoInput_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "视频输入:";
            // 
            // cbxAudioOutput
            // 
            this.cbxAudioOutput.DisplayMember = "DeviceName";
            this.cbxAudioOutput.FormattingEnabled = true;
            this.cbxAudioOutput.Location = new System.Drawing.Point(65, 44);
            this.cbxAudioOutput.Name = "cbxAudioOutput";
            this.cbxAudioOutput.Size = new System.Drawing.Size(148, 20);
            this.cbxAudioOutput.TabIndex = 1;
            this.cbxAudioOutput.ValueMember = "DeviceHandle";
            this.cbxAudioOutput.SelectedIndexChanged += new System.EventHandler(this.cbxAudioOutput_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "音频输出:";
            // 
            // cbxAudioInput
            // 
            this.cbxAudioInput.DisplayMember = "DeviceName";
            this.cbxAudioInput.FormattingEnabled = true;
            this.cbxAudioInput.Location = new System.Drawing.Point(65, 18);
            this.cbxAudioInput.Name = "cbxAudioInput";
            this.cbxAudioInput.Size = new System.Drawing.Size(148, 20);
            this.cbxAudioInput.TabIndex = 1;
            this.cbxAudioInput.ValueMember = "DeviceHandle";
            this.cbxAudioInput.SelectedIndexChanged += new System.EventHandler(this.cbxAudioInput_SelectedIndexChanged);
            // 
            // lblAudioInput
            // 
            this.lblAudioInput.AutoSize = true;
            this.lblAudioInput.Location = new System.Drawing.Point(7, 21);
            this.lblAudioInput.Name = "lblAudioInput";
            this.lblAudioInput.Size = new System.Drawing.Size(59, 12);
            this.lblAudioInput.TabIndex = 0;
            this.lblAudioInput.Text = "音频输入:";
            // 
            // txtCallee
            // 
            this.txtCallee.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCallee.Location = new System.Drawing.Point(24, 20);
            this.txtCallee.Name = "txtCallee";
            this.txtCallee.Size = new System.Drawing.Size(178, 21);
            this.txtCallee.TabIndex = 3;
            this.txtCallee.Text = "2164334";
            // 
            // btnAudioCall
            // 
            this.btnAudioCall.Location = new System.Drawing.Point(24, 224);
            this.btnAudioCall.Name = "btnAudioCall";
            this.btnAudioCall.Size = new System.Drawing.Size(75, 23);
            this.btnAudioCall.TabIndex = 4;
            this.btnAudioCall.Text = "音频报号";
            this.btnAudioCall.UseVisualStyleBackColor = true;
            this.btnAudioCall.Click += new System.EventHandler(this.btnAudioCall_Click);
            // 
            // btnVideoCall
            // 
            this.btnVideoCall.Location = new System.Drawing.Point(127, 224);
            this.btnVideoCall.Name = "btnVideoCall";
            this.btnVideoCall.Size = new System.Drawing.Size(75, 23);
            this.btnVideoCall.TabIndex = 5;
            this.btnVideoCall.Text = "视频拨号";
            this.btnVideoCall.UseVisualStyleBackColor = true;
            this.btnVideoCall.Click += new System.EventHandler(this.btnVideoCall_Click);
            // 
            // btm7
            // 
            this.btm7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btm7.Location = new System.Drawing.Point(51, 47);
            this.btm7.Name = "btm7";
            this.btm7.Size = new System.Drawing.Size(40, 40);
            this.btm7.TabIndex = 6;
            this.btm7.Text = "7";
            this.btm7.UseVisualStyleBackColor = true;
            this.btm7.Click += new System.EventHandler(this.btm7_Click);
            // 
            // btn4
            // 
            this.btn4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn4.Location = new System.Drawing.Point(51, 86);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(40, 40);
            this.btn4.TabIndex = 6;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn1.Location = new System.Drawing.Point(51, 125);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(40, 40);
            this.btn1.TabIndex = 6;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCallee);
            this.groupBox1.Controls.Add(this.btnAudioCall);
            this.groupBox1.Controls.Add(this.btnVideoCall);
            this.groupBox1.Controls.Add(this.btn9);
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.btn3);
            this.groupBox1.Controls.Add(this.btn0);
            this.groupBox1.Controls.Add(this.btn8);
            this.groupBox1.Controls.Add(this.btn2);
            this.groupBox1.Controls.Add(this.btn6);
            this.groupBox1.Controls.Add(this.btm7);
            this.groupBox1.Controls.Add(this.btnC);
            this.groupBox1.Controls.Add(this.btn5);
            this.groupBox1.Controls.Add(this.btn1);
            this.groupBox1.Controls.Add(this.btn4);
            this.groupBox1.Location = new System.Drawing.Point(547, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 253);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "排号";
            // 
            // btn9
            // 
            this.btn9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn9.Location = new System.Drawing.Point(130, 47);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(40, 40);
            this.btn9.TabIndex = 6;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btn9_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.Location = new System.Drawing.Point(130, 164);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(40, 40);
            this.btnDel.TabIndex = 6;
            this.btnDel.Text = "<-";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btn3
            // 
            this.btn3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn3.Location = new System.Drawing.Point(130, 125);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(40, 40);
            this.btn3.TabIndex = 6;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn0
            // 
            this.btn0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn0.Location = new System.Drawing.Point(90, 164);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(40, 40);
            this.btn0.TabIndex = 6;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // btn8
            // 
            this.btn8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn8.Location = new System.Drawing.Point(90, 47);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(40, 40);
            this.btn8.TabIndex = 6;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btn8_Click);
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn2.Location = new System.Drawing.Point(90, 125);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(40, 40);
            this.btn2.TabIndex = 6;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn6
            // 
            this.btn6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn6.Location = new System.Drawing.Point(130, 86);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(40, 40);
            this.btn6.TabIndex = 6;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // btnC
            // 
            this.btnC.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnC.Location = new System.Drawing.Point(51, 164);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(40, 40);
            this.btnC.TabIndex = 6;
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // btn5
            // 
            this.btn5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn5.Location = new System.Drawing.Point(90, 86);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(40, 40);
            this.btn5.TabIndex = 6;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // CallName
            // 
            this.CallName.DataPropertyName = "DisplayCallName";
            this.CallName.HeaderText = "名称";
            this.CallName.Name = "CallName";
            this.CallName.ReadOnly = true;
            this.CallName.Width = 120;
            // 
            // CallState
            // 
            this.CallState.DataPropertyName = "CallState";
            this.CallState.HeaderText = "状态";
            this.CallState.Name = "CallState";
            this.CallState.ReadOnly = true;
            this.CallState.Width = 80;
            // 
            // btnAnswer
            // 
            this.btnAnswer.DataPropertyName = "接听";
            this.btnAnswer.HeaderText = "接听";
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.ReadOnly = true;
            this.btnAnswer.Text = "接听";
            this.btnAnswer.UseColumnTextForButtonValue = true;
            this.btnAnswer.Width = 60;
            // 
            // btnHold
            // 
            this.btnHold.DataPropertyName = "保持";
            this.btnHold.HeaderText = "保持";
            this.btnHold.Name = "btnHold";
            this.btnHold.ReadOnly = true;
            this.btnHold.Text = "保持";
            this.btnHold.UseColumnTextForButtonValue = true;
            this.btnHold.Width = 60;
            // 
            // btnResume
            // 
            this.btnResume.DataPropertyName = "恢复";
            this.btnResume.HeaderText = "恢复";
            this.btnResume.Name = "btnResume";
            this.btnResume.ReadOnly = true;
            this.btnResume.Text = "恢复";
            this.btnResume.UseColumnTextForButtonValue = true;
            this.btnResume.Width = 60;
            // 
            // btnHangup
            // 
            this.btnHangup.DataPropertyName = "挂断";
            this.btnHangup.HeaderText = "挂断";
            this.btnHangup.Name = "btnHangup";
            this.btnHangup.ReadOnly = true;
            this.btnHangup.Text = "挂断";
            this.btnHangup.UseColumnTextForButtonValue = true;
            this.btnHangup.Width = 60;
            // 
            // CallHandle
            // 
            this.CallHandle.DataPropertyName = "CallHandle";
            this.CallHandle.HeaderText = "CallHandle";
            this.CallHandle.Name = "CallHandle";
            this.CallHandle.ReadOnly = true;
            this.CallHandle.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 379);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpDevices);
            this.Controls.Add(this.gpCallList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MFW";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.gpCallList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvCalls)).EndInit();
            this.gpDevices.ResumeLayout(false);
            this.gpDevices.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpCallList;
        private System.Windows.Forms.GroupBox gpDevices;
        private System.Windows.Forms.ComboBox cbxAudioInput;
        private System.Windows.Forms.Label lblAudioInput;
        private System.Windows.Forms.ComboBox cbxVideoInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAudioOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCallee;
        private System.Windows.Forms.Button btnAudioCall;
        private System.Windows.Forms.Button btnVideoCall;
        private System.Windows.Forms.Button btm7;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.DataGridView gvCalls;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallState;
        private System.Windows.Forms.DataGridViewButtonColumn btnAnswer;
        private System.Windows.Forms.DataGridViewButtonColumn btnHold;
        private System.Windows.Forms.DataGridViewButtonColumn btnResume;
        private System.Windows.Forms.DataGridViewButtonColumn btnHangup;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallHandle;
    }
}