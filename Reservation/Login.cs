using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();


            // Inside the Form_Load or constructor
            usernametxt.KeyDown += new KeyEventHandler(usernametxt_KeyDown);
            passwordtxt.KeyDown += new KeyEventHandler(passwordtxt_KeyDown);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_signupBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            passwordtxt.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            string username = usernametxt.Text;
            string password = passwordtxt.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();

                    // SQL query to check if username and password match
                    string query = "SELECT Role FROM Cashier WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        object result = command.ExecuteScalar(); // Get the role of the cashier if login is successful

                        if (result != null)
                        {
                            int role = Convert.ToInt32(result); // Get the role from the query result

                            // Optionally, store the role or other info in a global variable if needed
                            // Example: Set the logged-in cashier's role
                           int LoggedInCashierRole = role;

                            // Hide the login form and show the Home form
                            Home home = new Home();
                            this.Hide();
                            home.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            // If no matching user is found
                            MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while validating login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usernametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Check if the Enter key is pressed
            {
                passwordtxt.Focus(); // Move focus to the password textbox
            }
        }

        private void passwordtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Check if the Enter key is pressed
            {
                string username = usernametxt.Text;
                string password = passwordtxt.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                    {
                        connection.Open();

                        // SQL query to check if username and password match
                        string query = "SELECT Role FROM Cashier WHERE Username = @Username AND Password = @Password";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);

                            object result = command.ExecuteScalar(); // Get the role of the cashier if login is successful

                            if (result != null)
                            {
                                int role = Convert.ToInt32(result); // Get the role from the query result

                                // Optionally, store the role or other info in a global variable if needed
                                int LoggedInCashierRole = role;

                                // Hide the login form and show the Home form
                                Home home = new Home();
                                this.Hide();
                                home.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                // If no matching user is found
                                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while validating login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }



    }
    }

