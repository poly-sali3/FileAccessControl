using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAccessControl
{
    public partial class RegisterForm : Form
    {
        private const string DbFileName = "FileAccessControl.db";
        public RegisterForm()
        {
            InitializeComponent();
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all required fields (Username, Password, Email).");
                return;
            }

            // Проверка сложности пароля (пример)
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();

                // Проверка, существует ли уже пользователь с таким именем или email
                if (UserExists(connection, username, email))
                {
                    MessageBox.Show("Username or Email already exists.");
                    connection.Close();
                    return;
                }

                // Хеширование пароля
                string hashedPassword = HashPassword(password);

                // SQL-запрос для вставки нового пользователя
                string insertUserQuery = "INSERT INTO User (username, password, email, first_name, last_name) VALUES (@username, @password, @email, @firstName, @lastName)";
                using (var command = new SQLiteCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Registration successful!");
                connection.Close();
                this.Close(); // Закрыть форму регистрации после успешной регистрации
            }
        }

        // Хеширование пароля (перемещено сюда)
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

        private bool UserExists(SQLiteConnection connection, string username, string email)
        {
            string checkUserQuery = "SELECT COUNT(*) FROM User WHERE username = @username OR email = @email;";
            using (var command = new SQLiteCommand(checkUserQuery, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@email", email);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }
    

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
