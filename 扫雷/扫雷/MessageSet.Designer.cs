namespace 扫雷
{
    partial class MessageSet
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtRow_Num = new System.Windows.Forms.TextBox();
            this.txtCol_Num = new System.Windows.Forms.TextBox();
            this.txtBoom_Num = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "高度（行数）：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽度（列数）：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "雷数：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(213, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtRow_Num
            // 
            this.txtRow_Num.Location = new System.Drawing.Point(107, 19);
            this.txtRow_Num.MaxLength = 3;
            this.txtRow_Num.Name = "txtRow_Num";
            this.txtRow_Num.Size = new System.Drawing.Size(100, 21);
            this.txtRow_Num.TabIndex = 5;
            this.txtRow_Num.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRow_Num_KeyPress);
            // 
            // txtCol_Num
            // 
            this.txtCol_Num.Location = new System.Drawing.Point(107, 46);
            this.txtCol_Num.MaxLength = 3;
            this.txtCol_Num.Name = "txtCol_Num";
            this.txtCol_Num.Size = new System.Drawing.Size(100, 21);
            this.txtCol_Num.TabIndex = 6;
            this.txtCol_Num.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCol_Num_KeyPress);
            // 
            // txtBoom_Num
            // 
            this.txtBoom_Num.Location = new System.Drawing.Point(107, 73);
            this.txtBoom_Num.MaxLength = 3;
            this.txtBoom_Num.Name = "txtBoom_Num";
            this.txtBoom_Num.Size = new System.Drawing.Size(100, 21);
            this.txtBoom_Num.TabIndex = 7;
            this.txtBoom_Num.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoom_Num_KeyPress);
            // 
            // MessageSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 118);
            this.Controls.Add(this.txtBoom_Num);
            this.Controls.Add(this.txtCol_Num);
            this.Controls.Add(this.txtRow_Num);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MessageSet";
            this.Text = "自定义";
            this.Load += new System.EventHandler(this.MessageSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtRow_Num;
        private System.Windows.Forms.TextBox txtCol_Num;
        private System.Windows.Forms.TextBox txtBoom_Num;
    }
}