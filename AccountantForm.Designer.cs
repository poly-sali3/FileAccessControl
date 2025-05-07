namespace FileAccessControl
{
    partial class AccountantForm
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
            this.txtAccountantInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtAccountantInfo
            // 
            this.txtAccountantInfo.Location = new System.Drawing.Point(146, 172);
            this.txtAccountantInfo.Name = "txtAccountantInfo";
            this.txtAccountantInfo.Size = new System.Drawing.Size(188, 22);
            this.txtAccountantInfo.TabIndex = 0;
            // 
            // AccountantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtAccountantInfo);
            this.Name = "AccountantForm";
            this.Text = "AccountantForm";
            this.Load += new System.EventHandler(this.AccountantForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAccountantInfo;
    }
}