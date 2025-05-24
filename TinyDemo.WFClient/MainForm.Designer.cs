namespace TinyDemo.WFClient
{
    partial class MainForm
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
            numberControl1 = new TinyDemo.WFClient.Controls.NumberControl();
            numberControl2 = new TinyDemo.WFClient.Controls.NumberControl();
            numberControl3 = new TinyDemo.WFClient.Controls.NumberControl();
            numberControl4 = new TinyDemo.WFClient.Controls.NumberControl();
            numberControl5 = new TinyDemo.WFClient.Controls.NumberControl();
            numberControl6 = new TinyDemo.WFClient.Controls.NumberControl();
            numberControl7 = new TinyDemo.WFClient.Controls.NumberControl();
            grpLottoNumbers = new GroupBox();
            lblPlus = new Label();
            btnGenerate = new Button();
            statusStrip1 = new StatusStrip();
            tsStatus = new ToolStripStatusLabel();
            grpLottoNumbers.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // numberControl1
            // 
            numberControl1.Location = new Point(47, 57);
            numberControl1.Name = "numberControl1";
            numberControl1.Number = 1;
            numberControl1.Size = new Size(78, 78);
            numberControl1.TabIndex = 0;
            // 
            // numberControl2
            // 
            numberControl2.Location = new Point(131, 57);
            numberControl2.Name = "numberControl2";
            numberControl2.Number = 1;
            numberControl2.Size = new Size(78, 78);
            numberControl2.TabIndex = 1;
            // 
            // numberControl3
            // 
            numberControl3.Location = new Point(215, 57);
            numberControl3.Name = "numberControl3";
            numberControl3.Number = 1;
            numberControl3.Size = new Size(78, 78);
            numberControl3.TabIndex = 2;
            // 
            // numberControl4
            // 
            numberControl4.Location = new Point(299, 57);
            numberControl4.Name = "numberControl4";
            numberControl4.Number = 1;
            numberControl4.Size = new Size(78, 78);
            numberControl4.TabIndex = 3;
            // 
            // numberControl5
            // 
            numberControl5.Location = new Point(383, 57);
            numberControl5.Name = "numberControl5";
            numberControl5.Number = 1;
            numberControl5.Size = new Size(78, 78);
            numberControl5.TabIndex = 4;
            // 
            // numberControl6
            // 
            numberControl6.Location = new Point(467, 57);
            numberControl6.Name = "numberControl6";
            numberControl6.Number = 1;
            numberControl6.Size = new Size(78, 78);
            numberControl6.TabIndex = 5;
            // 
            // numberControl7
            // 
            numberControl7.Location = new Point(637, 57);
            numberControl7.Name = "numberControl7";
            numberControl7.Number = 1;
            numberControl7.Size = new Size(78, 78);
            numberControl7.TabIndex = 6;
            // 
            // grpLottoNumbers
            // 
            grpLottoNumbers.Controls.Add(lblPlus);
            grpLottoNumbers.Controls.Add(numberControl1);
            grpLottoNumbers.Controls.Add(numberControl7);
            grpLottoNumbers.Controls.Add(numberControl2);
            grpLottoNumbers.Controls.Add(numberControl6);
            grpLottoNumbers.Controls.Add(numberControl3);
            grpLottoNumbers.Controls.Add(numberControl5);
            grpLottoNumbers.Controls.Add(numberControl4);
            grpLottoNumbers.Location = new Point(12, 12);
            grpLottoNumbers.Name = "grpLottoNumbers";
            grpLottoNumbers.Size = new Size(776, 165);
            grpLottoNumbers.TabIndex = 7;
            grpLottoNumbers.TabStop = false;
            grpLottoNumbers.Text = "Picked lotto numbers";
            // 
            // lblPlus
            // 
            lblPlus.AutoSize = true;
            lblPlus.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblPlus.Location = new Point(563, 57);
            lblPlus.Name = "lblPlus";
            lblPlus.Size = new Size(68, 72);
            lblPlus.TabIndex = 7;
            lblPlus.Text = "+";
            lblPlus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(12, 183);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(776, 121);
            btnGenerate.TabIndex = 8;
            btnGenerate.Text = "Generate new lotto numbers";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsStatus });
            statusStrip1.Location = new Point(0, 342);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsStatus
            // 
            tsStatus.Name = "tsStatus";
            tsStatus.Size = new Size(0, 16);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 364);
            Controls.Add(statusStrip1);
            Controls.Add(btnGenerate);
            Controls.Add(grpLottoNumbers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lotto picker";
            Load += MainForm_Load;
            grpLottoNumbers.ResumeLayout(false);
            grpLottoNumbers.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.NumberControl numberControl1;
        private Controls.NumberControl numberControl2;
        private Controls.NumberControl numberControl3;
        private Controls.NumberControl numberControl4;
        private Controls.NumberControl numberControl5;
        private Controls.NumberControl numberControl6;
        private Controls.NumberControl numberControl7;
        private GroupBox grpLottoNumbers;
        private Button btnGenerate;
        private Label lblPlus;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsStatus;
    }
}
