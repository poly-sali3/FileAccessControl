namespace FileAccessControl
{
    partial class TesterForm
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
            this.txtTesterInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTesterInfo
            // 
            this.txtTesterInfo.Location = new System.Drawing.Point(102, 132);
            this.txtTesterInfo.Name = "txtTesterInfo";
            this.txtTesterInfo.Size = new System.Drawing.Size(241, 22);
            this.txtTesterInfo.TabIndex = 0;
            // 
            // TesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTesterInfo);
            this.Name = "TesterForm";
            this.Text = "TesterForm";
            this.Load += new System.EventHandler(this.TesterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTesterInfo;
    }
}