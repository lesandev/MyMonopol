namespace MyMonopol
{
    partial class Monopoly
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
            this.Move_Button = new System.Windows.Forms.Button();
            this.End_Game_Button = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Move_Button
            // 
            this.Move_Button.Location = new System.Drawing.Point(318, 318);
            this.Move_Button.Name = "Move_Button";
            this.Move_Button.Size = new System.Drawing.Size(75, 23);
            this.Move_Button.TabIndex = 0;
            this.Move_Button.Text = "Move";
            this.Move_Button.UseVisualStyleBackColor = true;
            this.Move_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // End_Game_Button
            // 
            this.End_Game_Button.Location = new System.Drawing.Point(318, 376);
            this.End_Game_Button.Name = "End_Game_Button";
            this.End_Game_Button.Size = new System.Drawing.Size(75, 23);
            this.End_Game_Button.TabIndex = 1;
            this.End_Game_Button.Text = "End Game";
            this.End_Game_Button.UseVisualStyleBackColor = true;
            this.End_Game_Button.Click += new System.EventHandler(this.End_Game_Button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(304, 292);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Monopoly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 774);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.End_Game_Button);
            this.Controls.Add(this.Move_Button);
            this.Name = "Monopoly";
            this.Text = "Monopoly Board";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Move_Button;
        private System.Windows.Forms.Button End_Game_Button;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

