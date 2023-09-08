namespace Cluster_UI_Stresser
{
    partial class ClusterForm
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
            label1 = new Label();
            txtApi = new TextBox();
            ChkApi = new Button();
            lblStats = new Label();
            label2 = new Label();
            LblNodes = new Label();
            BtnStart = new Button();
            label3 = new Label();
            TxtMaxNodes = new NumericUpDown();
            label5 = new Label();
            TxtIncrement = new NumericUpDown();
            ChkNodes = new Button();
            Report = new Button();
            label4 = new Label();
            TxtSetupNodes = new NumericUpDown();
            BtnSetupNodes = new Button();
            ((System.ComponentModel.ISupportInitialize)TxtMaxNodes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TxtIncrement).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TxtSetupNodes).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 31);
            label1.Name = "label1";
            label1.Size = new Size(31, 20);
            label1.TabIndex = 0;
            label1.Text = "API";
            // 
            // txtApi
            // 
            txtApi.Location = new Point(123, 31);
            txtApi.Margin = new Padding(3, 4, 3, 4);
            txtApi.Name = "txtApi";
            txtApi.Size = new Size(212, 27);
            txtApi.TabIndex = 1;
            txtApi.Text = "http://127.0.0.1:5000/api/";
            // 
            // ChkApi
            // 
            ChkApi.BackColor = SystemColors.ButtonFace;
            ChkApi.ForeColor = SystemColors.ActiveCaptionText;
            ChkApi.Location = new Point(361, 29);
            ChkApi.Margin = new Padding(3, 4, 3, 4);
            ChkApi.Name = "ChkApi";
            ChkApi.Size = new Size(109, 30);
            ChkApi.TabIndex = 13;
            ChkApi.Text = "Start";
            ChkApi.UseVisualStyleBackColor = false;
            ChkApi.Click += ChkApi_Click;
            // 
            // lblStats
            // 
            lblStats.AutoSize = true;
            lblStats.Location = new Point(31, 243);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(49, 20);
            lblStats.TabIndex = 9;
            lblStats.Text = "Status";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(215, 76);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 15;
            label2.Text = "Nodes";
            // 
            // LblNodes
            // 
            LblNodes.AutoSize = true;
            LblNodes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LblNodes.Location = new Point(290, 72);
            LblNodes.Name = "LblNodes";
            LblNodes.Size = new Size(24, 28);
            LblNodes.TabIndex = 16;
            LblNodes.Text = "0";
            // 
            // BtnStart
            // 
            BtnStart.BackColor = SystemColors.ButtonFace;
            BtnStart.FlatStyle = FlatStyle.Flat;
            BtnStart.ForeColor = SystemColors.ActiveCaptionText;
            BtnStart.Location = new Point(125, 238);
            BtnStart.Margin = new Padding(3, 4, 3, 4);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(214, 30);
            BtnStart.TabIndex = 17;
            BtnStart.Text = "Start Stress";
            BtnStart.UseVisualStyleBackColor = false;
            BtnStart.Click += BtnStart_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 187);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 18;
            label3.Text = "Max Nodes";
            // 
            // TxtMaxNodes
            // 
            TxtMaxNodes.Location = new Point(125, 184);
            TxtMaxNodes.Margin = new Padding(3, 4, 3, 4);
            TxtMaxNodes.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            TxtMaxNodes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            TxtMaxNodes.Name = "TxtMaxNodes";
            TxtMaxNodes.Size = new Size(69, 27);
            TxtMaxNodes.TabIndex = 19;
            TxtMaxNodes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(263, 187);
            label5.Name = "label5";
            label5.Size = new Size(75, 20);
            label5.TabIndex = 20;
            label5.Text = "Increment";
            // 
            // TxtIncrement
            // 
            TxtIncrement.Location = new Point(361, 184);
            TxtIncrement.Margin = new Padding(3, 4, 3, 4);
            TxtIncrement.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            TxtIncrement.Name = "TxtIncrement";
            TxtIncrement.Size = new Size(109, 27);
            TxtIncrement.TabIndex = 21;
            TxtIncrement.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ChkNodes
            // 
            ChkNodes.BackColor = SystemColors.ButtonFace;
            ChkNodes.Location = new Point(361, 71);
            ChkNodes.Margin = new Padding(3, 4, 3, 4);
            ChkNodes.Name = "ChkNodes";
            ChkNodes.Size = new Size(109, 30);
            ChkNodes.TabIndex = 22;
            ChkNodes.Text = "Check";
            ChkNodes.UseVisualStyleBackColor = false;
            ChkNodes.Click += ChkNodes_Click;
            // 
            // Report
            // 
            Report.Location = new Point(361, 238);
            Report.Margin = new Padding(3, 4, 3, 4);
            Report.Name = "Report";
            Report.Size = new Size(109, 30);
            Report.TabIndex = 23;
            Report.Text = "Report";
            Report.UseVisualStyleBackColor = true;
            Report.Click += Report_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 76);
            label4.Name = "label4";
            label4.Size = new Size(47, 20);
            label4.TabIndex = 24;
            label4.Text = "Setup";
            // 
            // TxtSetupNodes
            // 
            TxtSetupNodes.Location = new Point(125, 74);
            TxtSetupNodes.Margin = new Padding(3, 4, 3, 4);
            TxtSetupNodes.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            TxtSetupNodes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            TxtSetupNodes.Name = "TxtSetupNodes";
            TxtSetupNodes.Size = new Size(69, 27);
            TxtSetupNodes.TabIndex = 25;
            TxtSetupNodes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // BtnSetupNodes
            // 
            BtnSetupNodes.BackColor = SystemColors.ButtonFace;
            BtnSetupNodes.Location = new Point(125, 109);
            BtnSetupNodes.Margin = new Padding(3, 4, 3, 4);
            BtnSetupNodes.Name = "BtnSetupNodes";
            BtnSetupNodes.Size = new Size(109, 30);
            BtnSetupNodes.TabIndex = 26;
            BtnSetupNodes.Text = "Generate";
            BtnSetupNodes.UseVisualStyleBackColor = false;
            BtnSetupNodes.Click += BtnSetupNodes_Click;
            // 
            // ClusterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 302);
            Controls.Add(BtnSetupNodes);
            Controls.Add(TxtSetupNodes);
            Controls.Add(label4);
            Controls.Add(Report);
            Controls.Add(ChkNodes);
            Controls.Add(TxtIncrement);
            Controls.Add(label5);
            Controls.Add(TxtMaxNodes);
            Controls.Add(label3);
            Controls.Add(BtnStart);
            Controls.Add(LblNodes);
            Controls.Add(label2);
            Controls.Add(ChkApi);
            Controls.Add(lblStats);
            Controls.Add(txtApi);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ClusterForm";
            Text = "Cluster UI Stress Tool";
            ((System.ComponentModel.ISupportInitialize)TxtMaxNodes).EndInit();
            ((System.ComponentModel.ISupportInitialize)TxtIncrement).EndInit();
            ((System.ComponentModel.ISupportInitialize)TxtSetupNodes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtApi;
        private Button ChkApi;
        private Label lblStats;
        private Label label2;
        private Label LblNodes;
        private Button BtnStart;
        private Label label3;
        private NumericUpDown TxtMaxNodes;
        private Label label5;
        private NumericUpDown TxtIncrement;
        private Button ChkNodes;
        private Button Report;
        private Label label4;
        private NumericUpDown TxtSetupNodes;
        private Button BtnSetupNodes;
    }
}