namespace FileAccessControl
{
    partial class DeveloperForm
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
            this.txtDeveloperInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDeveloperInfo
            // 
            this.txtDeveloperInfo.Location = new System.Drawing.Point(76, 117);
            this.txtDeveloperInfo.Name = "txtDeveloperInfo";
            this.txtDeveloperInfo.Size = new System.Drawing.Size(171, 22);
            this.txtDeveloperInfo.TabIndex = 0;
            // 
            // DeveloperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtDeveloperInfo);
            this.Name = "DeveloperForm";
            this.Text = "DeveloperForm";
            this.Load += new System.EventHandler(this.DeveloperForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDeveloperInfo;
    }
}