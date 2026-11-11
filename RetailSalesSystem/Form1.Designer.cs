namespace RetailSalesSystem
{
    partial class Form1
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
            btnKetNoi = new Button();
            SuspendLayout();
            // 
            // btnKetNoi
            // 
            btnKetNoi.Location = new Point(271, 184);
            btnKetNoi.Name = "btnKetNoi";
            btnKetNoi.Size = new Size(208, 55);
            btnKetNoi.TabIndex = 0;
            btnKetNoi.Text = "Kiểm tra kết nối";
            btnKetNoi.UseVisualStyleBackColor = true;
            btnKetNoi.Click += btnKetNoi_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnKetNoi);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnKetNoi;
    }
}
