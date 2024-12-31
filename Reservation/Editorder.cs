using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Reservation
{
    public partial class Editorder : Form
    {
        public Editorder()
        {
            InitializeComponent();
        }

        private void Editorder_Load(object sender, EventArgs e)
        {
            // Load reservation data when the form loads
            LoadReservations();
        }

        private void LoadReservations()
        {
            string query = "SELECT * FROM View_ManageReservationsDetails"; // SQL query for the view

            // Create a SqlDataAdapter to fetch data from the view
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();

                // Fill the DataTable with data from the view
                dataAdapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView
                ManageReservationGridview.DataSource = dataTable;
            }
        }

        // This method will handle the search button click
        private void loadbtn_Click(object sender, EventArgs e)
        {



            LoadReservations();
        }


        private void ManageReservationGridview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the column is either CustomerName or CustomerPhoneNumber
            if (e.RowIndex >= 0 && (e.ColumnIndex == ManageReservationGridview.Columns["CustomerName"].Index ||
                e.ColumnIndex == ManageReservationGridview.Columns["CustomerPhoneNumber"].Index))
            {
                string customerName = ManageReservationGridview.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();
                string customerPhoneNumber = ManageReservationGridview.Rows[e.RowIndex].Cells["CustomerPhoneNumber"].Value.ToString();

                // Update CustomerID based on CustomerName and CustomerPhoneNumber
                UpdateCustomerId(customerName, customerPhoneNumber);
            }
        }



        private void UpdateCustomerId(string customerName, string customerPhoneNumber)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.connectionString))
            {
                conn.Open();

                // Ensure we're only updating Name and PhoneNumber, not CustomerID
                string query = @"
            UPDATE Customer
            SET Name = @CustomerName, PhoneNumber = @PhoneNumber
            WHERE CustomerID = (
                SELECT TOP 1 CustomerID
                FROM Customer
                WHERE Name = @CustomerName AND PhoneNumber = @PhoneNumber
            )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customerPhoneNumber);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No matching record found to update.");
                    }
                }
            }
        }


        private void updatebtn_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (ManageReservationGridview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }

            // Retrieve the selected row
            var selectedRow = ManageReservationGridview.SelectedRows[0];

            // Accessing the values from the correct columns
            string customerName = selectedRow.Cells["CustomerName"].Value.ToString(); // CustomerName column
            string customerPhoneNumber = selectedRow.Cells["CustomerPhoneNumber"].Value.ToString(); // CustomerPhoneNumber column

            // Check if CustomerName or CustomerPhoneNumber has changed
            if (!string.IsNullOrEmpty(customerName) && !string.IsNullOrEmpty(customerPhoneNumber))
            {
                try
                {
                    // Update CustomerID based on CustomerName and CustomerPhoneNumber
                    UpdateCustomerId(customerName, customerPhoneNumber);

                    // Inform the user that the update was successful
                    MessageBox.Show("Customer information updated successfully.");

                    // Optionally, refresh the DataGridView to reflect the changes
                    LoadReservations();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating the customer information: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Customer Name and Phone Number cannot be empty.");
            }
        }
    }
}
