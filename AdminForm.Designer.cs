namespace FileAccessControl
{
    partial class AdminForm
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
            this.lstUsers = new System.Windows.Forms.ListView();
            this.lstRoles = new System.Windows.Forms.ListView();
            this.btnAssignRole = new System.Windows.Forms.Button();
            this.btnCreateRole = new System.Windows.Forms.Button();
            this.txtNewRoleName = new System.Windows.Forms.TextBox();
            this.txtNewRoleDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstUsers
            // 
            this.lstUsers.HideSelection = false;
            this.lstUsers.Location = new System.Drawing.Point(63, 28);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(196, 173);
            this.lstUsers.TabIndex = 0;
            this.lstUsers.UseCompatibleStateImageBehavior = false;
            // 
            // lstRoles
            // 
            this.lstRoles.HideSelection = false;
            this.lstRoles.Location = new System.Drawing.Point(491, 28);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(196, 173);
            this.lstRoles.TabIndex = 1;
            this.lstRoles.UseCompatibleStateImageBehavior = false;
            // 
            // btnAssignRole
            // 
            this.btnAssignRole.Location = new System.Drawing.Point(77, 335);
            this.btnAssignRole.Name = "btnAssignRole";
            this.btnAssignRole.Size = new System.Drawing.Size(154, 63);
            this.btnAssignRole.TabIndex = 2;
            this.btnAssignRole.Text = "Назначить роль";
            this.btnAssignRole.UseVisualStyleBackColor = true;
            // 
            // btnCreateRole
            // 
            this.btnCreateRole.Location = new System.Drawing.Point(513, 335);
            this.btnCreateRole.Name = "btnCreateRole";
            this.btnCreateRole.Size = new System.Drawing.Size(154, 63);
            this.btnCreateRole.TabIndex = 3;
            this.btnCreateRole.Text = "Создать  роль";
            this.btnCreateRole.UseVisualStyleBackColor = true;
            // 
            // txtNewRoleName
            // 
            this.txtNewRoleName.Location = new System.Drawing.Point(63, 231);
            this.txtNewRoleName.Name = "txtNewRoleName";
            this.txtNewRoleName.Size = new System.Drawing.Size(269, 22);
            this.txtNewRoleName.TabIndex = 4;
            // 
            // txtNewRoleDescription
            // 
            this.txtNewRoleDescription.Location = new System.Drawing.Point(63, 287);
            this.txtNewRoleDescription.Name = "txtNewRoleDescription";
            this.txtNewRoleDescription.Size = new System.Drawing.Size(269, 22);
            this.txtNewRoleDescription.TabIndex = 5;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtNewRoleDescription);
            this.Controls.Add(this.txtNewRoleName);
            this.Controls.Add(this.btnCreateRole);
            this.Controls.Add(this.btnAssignRole);
            this.Controls.Add(this.lstRoles);
            this.Controls.Add(this.lstUsers);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstUsers;
        private System.Windows.Forms.ListView lstRoles;
        private System.Windows.Forms.Button btnAssignRole;
        private System.Windows.Forms.Button btnCreateRole;
        private System.Windows.Forms.TextBox txtNewRoleName;
        private System.Windows.Forms.TextBox txtNewRoleDescription;
    }
}