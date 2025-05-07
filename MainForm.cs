using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAccessControl
{
    public partial class MainForm : Form
    {
        private const string DbFileName = "FileAccessControl.db"; // Имя файла базы данных
        private string _currentUserRole;
        private int _currentUserId;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            if (!File.Exists(DbFileName))
            {
                CreateDatabase();
                CreateDefaultAdminUser(); // Создание админа при первом запуске
            }
        }

        private void CreateDatabase()
        {
            SQLiteConnection.CreateFile(DbFileName);

            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();

                string createUserTableQuery = @"
                    CREATE TABLE IF NOT EXISTS User (
                        user_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        username TEXT NOT NULL UNIQUE,
                        password TEXT NOT NULL,
                        email TEXT UNIQUE,
                        first_name TEXT,
                        last_name TEXT,
                        creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
                        last_login DATETIME,
                        is_active BOOLEAN DEFAULT 1
                    );";

                string createRoleTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Role (
                        role_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        role_name TEXT NOT NULL UNIQUE,
                        description TEXT
                    );";

                string createFileTableQuery = @"
                    CREATE TABLE IF NOT EXISTS File (
                        file_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        file_path TEXT NOT NULL UNIQUE,
                        file_name TEXT,
                        file_size INTEGER,
                        upload_date DATETIME DEFAULT CURRENT_TIMESTAMP,
                        uploader_id INTEGER,
                        FOREIGN KEY (uploader_id) REFERENCES User(user_id)
                    );";

                string createPermissionTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Permission (
                        permission_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        permission_name TEXT NOT NULL UNIQUE,
                        description TEXT
                    );";

                string createUserRoleTableQuery = @"
                    CREATE TABLE IF NOT EXISTS User_Role (
                        user_id INTEGER,
                        role_id INTEGER,
                        FOREIGN KEY (user_id) REFERENCES User(user_id),
                        FOREIGN KEY (role_id) REFERENCES Role(role_id),
                        PRIMARY KEY (user_id, role_id)
                    );";

                string createRolePermissionTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Role_Permission (
                        role_id INTEGER,
                        permission_id INTEGER,
                        FOREIGN KEY (role_id) REFERENCES Role(role_id),
                        FOREIGN KEY (permission_id) REFERENCES Permission(permission_id),
                        PRIMARY KEY (role_id, permission_id)
                    );";

                string createFilePermissionTableQuery = @"
                    CREATE TABLE IF NOT EXISTS File_Permission (
                        file_id INTEGER,
                        permission_id INTEGER,
                        user_id INTEGER,
                        FOREIGN KEY (file_id) REFERENCES File(file_id),
                        FOREIGN KEY (permission_id) REFERENCES Permission(permission_id),
                        FOREIGN KEY (user_id) REFERENCES User(user_id),
                        PRIMARY KEY (file_id, permission_id, user_id)
                    );";

                using (var command = new SQLiteCommand(createUserTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createRoleTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createFileTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createPermissionTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createUserRoleTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createRolePermissionTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createFilePermissionTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert default roles
                InsertDefaultRoles(connection);
                InsertDefaultPermissions(connection);

                connection.Close();
            }
        }

        private void InsertDefaultPermissions(SQLiteConnection connection)
        {
            // Example permissions: read, write, delete
            InsertPermission(connection, "Read", "Allows reading the file.");
            InsertPermission(connection, "Write", "Allows writing to the file.");
            InsertPermission(connection, "Delete", "Allows deleting the file.");
        }

        private void InsertPermission(SQLiteConnection connection, string name, string description)
        {
            string insertQuery = "INSERT INTO Permission (permission_name, description) VALUES (@name, @description);";
            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                command.ExecuteNonQuery();
            }
        }

        private void InsertDefaultRoles(SQLiteConnection connection)
        {
            InsertRole(connection, "Admin", "Full access to the system.");
            InsertRole(connection, "Accountant", "Access to financial data.");
            InsertRole(connection, "HR Manager", "Access to employee data.");
            InsertRole(connection, "Developer", "Access to code repositories.");
            InsertRole(connection, "Tester", "Access to test environments and results.");
        }

        private void InsertRole(SQLiteConnection connection, string roleName, string description)
        {
            string insertQuery = "INSERT INTO Role (role_name, description) VALUES (@roleName, @description);";
            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@roleName", roleName);
                command.Parameters.AddWithValue("@description", description);
                command.ExecuteNonQuery();
            }
        }

        private void CreateDefaultAdminUser()
        {
            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();
                string adminUsername = "admin";
                if (!UserExists(connection, adminUsername))
                {
                    string adminPassword = HashPassword("admin123");
                    string insertAdminQuery = "INSERT INTO User (username, password, email, first_name, last_name) VALUES (@username, @password, @email, @firstName, @lastName); SELECT last_insert_rowid();";

                    using (var command = new SQLiteCommand(insertAdminQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", adminUsername);
                        command.Parameters.AddWithValue("@password", adminPassword);
                        command.Parameters.AddWithValue("@email", "admin@example.com");
                        command.Parameters.AddWithValue("@firstName", "Admin");
                        command.Parameters.AddWithValue("@lastName", "User");

                        long adminUserId = (long)command.ExecuteScalar();

                        // Assign the Admin role to the created user
                        AssignRoleToUser(connection, (int)adminUserId, "Admin");
                    }
                }
                connection.Close();
            }
        }

        private bool UserExists(SQLiteConnection connection, string username)
        {
            string checkUserQuery = "SELECT COUNT(*) FROM User WHERE username = @username;";
            using (var command = new SQLiteCommand(checkUserQuery, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private void AssignRoleToUser(SQLiteConnection connection, int userId, string roleName)
        {
            string getRoleIdQuery = "SELECT role_id FROM Role WHERE role_name = @roleName;";
            using (var command = new SQLiteCommand(getRoleIdQuery, connection))
            {
                command.Parameters.AddWithValue("@roleName", roleName);
                object roleIdResult = command.ExecuteScalar();

                if (roleIdResult != null && roleIdResult != DBNull.Value)
                {
                    int roleId = Convert.ToInt32(roleIdResult);
                    string assignRoleQuery = "INSERT INTO User_Role (user_id, role_id) VALUES (@userId, @roleId);";
                    using (var assignCommand = new SQLiteCommand(assignRoleQuery, connection))
                    {
                        assignCommand.Parameters.AddWithValue("@userId", userId);
                        assignCommand.Parameters.AddWithValue("@roleId", roleId);
                        assignCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show($"Role '{roleName}' not found.");
                }
            }
        }


        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();
                string hashedPassword = HashPassword(password);
                string selectUserQuery = "SELECT user_id, password FROM User WHERE username = @username;";

                using (var command = new SQLiteCommand(selectUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string storedHashedPassword = reader.GetString(1);

                            if (VerifyPassword(password, storedHashedPassword))
                            {
                                _currentUserId = userId;
                                _currentUserRole = GetUserRole(userId);
                                MessageBox.Show($"Login successful! Welcome, {username}. Your role is {_currentUserRole}");
                                ShowRoleSpecificInformation();
                                OpenRoleSpecificForm(_currentUserRole);
                            }
                            else
                            {
                                MessageBox.Show("Invalid password.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid username.");
                        }
                    }
                }
                connection.Close();
            }
        }

        private string GetUserRole(int userId)
        {
            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();
                string getRoleQuery = @"
                    SELECT R.role_name 
                    FROM User_Role UR
                    JOIN Role R ON UR.role_id = R.role_id
                    WHERE UR.user_id = @userId;";

                using (var command = new SQLiteCommand(getRoleQuery, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    object result = command.ExecuteScalar();
                    connection.Close();

                    return result != null ? result.ToString() : "Unknown";
                }
            }
        }

        private void ShowRoleSpecificInformation()
        {
            switch (_currentUserRole)
            {
                case "Admin":
                    txtRoleInfo.Text = "Admin: Full access to all files and system settings.";
                    break;
                case "Accountant":
                    txtRoleInfo.Text = "Accountant: Access to financial reports and accounting documents.";
                    break;
                case "HR Manager":
                    txtRoleInfo.Text = "HR Manager: Access to employee records and HR policies.";
                    break;
                case "Developer":
                    txtRoleInfo.Text = "Developer: Access to source code repositories and development tools.";
                    break;
                case "Tester":
                    txtRoleInfo.Text = "Tester: Access to test plans, test cases, and bug reports.";
                    break;
                default:
                    txtRoleInfo.Text = "Role information not available.";
                    break;
            }
        }

        private void OpenRoleSpecificForm(string role)
        {
            switch (role)
            {
                case "Admin":
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                    break;
                case "Accountant":
                    AccountantForm accountantForm = new AccountantForm();
                    accountantForm.Show();
                    break;
                case "HR Manager":
                    HRManagerForm hrManagerForm = new HRManagerForm();
                    hrManagerForm.Show();
                    break;
                case "Developer":
                    DeveloperForm developerForm = new DeveloperForm();
                    developerForm.Show();
                    break;
                case "Tester":
                    TesterForm testerForm = new TesterForm();
                    testerForm.Show();
                    break;
                default:
                    MessageBox.Show("No specific form for this role.");
                    break;
            }
        }

        // Хеширование пароля
        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        // Верификация пароля
        private bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
