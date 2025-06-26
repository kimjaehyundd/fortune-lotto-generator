namespace FortuneLottoGenerator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtStory;
        private System.Windows.Forms.TextBox txtInterpretation;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStoryPrompt;
        private System.Windows.Forms.Label lblInterpretationTitle;
        private System.Windows.Forms.Label lblNumbers;
        private System.Windows.Forms.Label[] numberLabels;
        private System.Windows.Forms.Panel pnlNumbers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtStory = new System.Windows.Forms.TextBox();
            this.txtInterpretation = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStoryPrompt = new System.Windows.Forms.Label();
            this.lblInterpretationTitle = new System.Windows.Forms.Label();
            this.lblNumbers = new System.Windows.Forms.Label();
            this.pnlNumbers = new System.Windows.Forms.Panel();
            this.numberLabels = new System.Windows.Forms.Label[6];
            
            this.SuspendLayout();
            
            // Initialize number labels
            for (int i = 0; i < numberLabels.Length; i++)
            {
                this.numberLabels[i] = new System.Windows.Forms.Label();
                this.numberLabels[i].Size = new System.Drawing.Size(50, 50);
                this.numberLabels[i].Location = new System.Drawing.Point(10 + (i * 60), 10);
                this.numberLabels[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                this.numberLabels[i].Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
                this.numberLabels[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.numberLabels[i].Visible = false;
                this.pnlNumbers.Controls.Add(this.numberLabels[i]);
            }
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(387, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "행운의 로또번호 생성기 - 사주팔자 해석";
            
            // lblStoryPrompt
            this.lblStoryPrompt.AutoSize = true;
            this.lblStoryPrompt.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStoryPrompt.Location = new System.Drawing.Point(12, 50);
            this.lblStoryPrompt.Name = "lblStoryPrompt";
            this.lblStoryPrompt.Size = new System.Drawing.Size(490, 17);
            this.lblStoryPrompt.TabIndex = 1;
            this.lblStoryPrompt.Text = "오늘이나 어제 있었던 일에 대해 이야기해 주세요. (예: 점심에 라면을 먹었는데...)";
            
            // txtStory
            this.txtStory.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtStory.Location = new System.Drawing.Point(15, 80);
            this.txtStory.Multiline = true;
            this.txtStory.Name = "txtStory";
            this.txtStory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStory.Size = new System.Drawing.Size(550, 120);
            this.txtStory.TabIndex = 2;
            this.txtStory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStory_KeyDown);
            
            // btnGenerate
            this.btnGenerate.BackColor = System.Drawing.Color.DarkBlue;
            this.btnGenerate.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(15, 215);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(120, 40);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "운명 해석";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            
            // btnClear
            this.btnClear.BackColor = System.Drawing.Color.Gray;
            this.btnClear.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(150, 215);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 40);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "초기화";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            
            // lblInterpretationTitle
            this.lblInterpretationTitle.AutoSize = true;
            this.lblInterpretationTitle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInterpretationTitle.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblInterpretationTitle.Location = new System.Drawing.Point(12, 275);
            this.lblInterpretationTitle.Name = "lblInterpretationTitle";
            this.lblInterpretationTitle.Size = new System.Drawing.Size(88, 17);
            this.lblInterpretationTitle.TabIndex = 5;
            this.lblInterpretationTitle.Text = "사주팔자 해석:";
            
            // txtInterpretation
            this.txtInterpretation.BackColor = System.Drawing.Color.LightYellow;
            this.txtInterpretation.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtInterpretation.Location = new System.Drawing.Point(15, 300);
            this.txtInterpretation.Multiline = true;
            this.txtInterpretation.Name = "txtInterpretation";
            this.txtInterpretation.ReadOnly = true;
            this.txtInterpretation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInterpretation.Size = new System.Drawing.Size(550, 100);
            this.txtInterpretation.TabIndex = 6;
            
            // lblNumbers
            this.lblNumbers.AutoSize = true;
            this.lblNumbers.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblNumbers.ForeColor = System.Drawing.Color.Red;
            this.lblNumbers.Location = new System.Drawing.Point(15, 420);
            this.lblNumbers.Name = "lblNumbers";
            this.lblNumbers.Size = new System.Drawing.Size(337, 19);
            this.lblNumbers.TabIndex = 7;
            this.lblNumbers.Text = "이곳에 운명의 로또번호가 나타납니다.";
            
            // pnlNumbers
            this.pnlNumbers.Location = new System.Drawing.Point(15, 450);
            this.pnlNumbers.Name = "pnlNumbers";
            this.pnlNumbers.Size = new System.Drawing.Size(400, 70);
            this.pnlNumbers.TabIndex = 8;
            
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(584, 541);
            this.Controls.Add(this.pnlNumbers);
            this.Controls.Add(this.lblNumbers);
            this.Controls.Add(this.txtInterpretation);
            this.Controls.Add(this.lblInterpretationTitle);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtStory);
            this.Controls.Add(this.lblStoryPrompt);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "행운의 로또번호 생성기";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}