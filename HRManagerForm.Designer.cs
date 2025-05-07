namespace FileAccessControl
{
    partial class HRManagerForm
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
            this.txtHRManagerInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtHRManagerInfo
            // 
            this.txtHRManagerInfo.Location = new System.Drawing.Point(89, 134);
            this.txtHRManagerInfo.Name = "txtHRManagerInfo";
            this.txtHRManagerInfo.Size = new System.Drawing.Size(214, 22);
            this.txtHRManagerInfo.TabIndex = 0;
            // 
            // HRManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtHRManagerInfo);
            this.Name = "HRManagerForm";
            this.Text = "HRManagerForm";
            this.Load += new System.EventHandler(this.HRManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHRManagerInfo;
    }
}