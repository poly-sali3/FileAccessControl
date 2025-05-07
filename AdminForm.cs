using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAccessControl
{
    public partial class AdminForm : Form
    {
        private const string DbFileName = "FileAccessControl.db";
        public AdminForm()
        {
            InitializeComponent();
            LoadUsers();
            LoadRoles();
        }
        private void LoadUsers()
        {
            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();
                string selectUsersQuery = "SELECT user_id, username FROM User;";
                using (var command = new SQLiteCommand(selectUsersQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        lstUsers.Items.Clear();
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            lstUsers.Items.Add(new ListViewItem(new string[] { userId.ToString(), username }));
                        }
                    }
                }
                connection.Close();
            }
        }

        private void LoadRoles()
        {
            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();
                string selectRolesQuery = "SELECT role_id, role_name FROM Role;";
                using (var command = new SQLiteCommand(selectRolesQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        lstRoles.Items.Clear();
                        while (reader.Read())
                        {
                            int roleId = reader.GetInt32(0);
                            string roleName = reader.GetString(1);
                            lstRoles.Items.Add(new ListViewItem(new string[] { roleId.ToString(), roleName }));
                        }
                    }
                }
                connection.Close();
            }
        }

        private void BtnAssignRole_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count == 0 || lstRoles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user and a role.");
                return;
            }

            int userId = int.Parse(lstUsers.SelectedItems[0].SubItems[0].Text);
            int roleId = int.Parse(lstRoles.SelectedItems[0].SubItems[0].Text);

            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();
                string assignRoleQuery = "INSERT INTO User_Role (user_id, role_id) VALUES (@userId, @roleId);";
                using (var command = new SQLiteCommand(assignRoleQuery, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@roleId", roleId);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Role assigned successfully.");
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show($"Error assigning role: {ex.Message}");
                    }
                }
                connection.Close();
            }
        }

        private void BtnCreateRole_Click(object sender, EventArgs e)
        {
            string roleName = txtNewRoleName.Text;
            string roleDescription = txtNewRoleDescription.Text;

            if (string.IsNullOrEmpty(roleName))
            {
                MessageBox.Show("Please enter a role name.");
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();
                string insertRoleQuery = "INSERT INTO Role (role_name, description) VALUES (@roleName, @description);";
                using (var command = new SQLiteCommand(insertRoleQuery, connection))
                {
                    command.Parameters.AddWithValue("@roleName", roleName);
                    command.Parameters.AddWithValue("@description", roleDescription);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Role created successfully.");
                        LoadRoles(); // Refresh the role list
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show($"Error creating role: {ex.Message}");
                    }
                }
                connection.Close();
            }
        }
    
        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
    }
}
