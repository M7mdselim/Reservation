using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation
{
    public partial class SpotCheck : Form
    {
        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;
        public SpotCheck()
        {
            InitializeComponent();




            _initialFormWidth = this.Width;
            _initialFormHeight = this.Height;
            // Store initial size and location of all controls
            _controlsInfo = new ControlInfo[this.Controls.Count];
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control c = this.Controls[i];
                _controlsInfo[i] = new ControlInfo(c.Left, c.Top, c.Width, c.Height, c.Font.Size);
            }

            // Set event handler for form resize
            this.Resize += Home_Resize;



        }


        private bool _isResizing = false;

        private void Home_Resize(object sender, EventArgs e)
        {
            if (_isResizing) return;

            _isResizing = true;
            try
            {
                float widthRatio = this.Width / _initialFormWidth;
                float heightRatio = this.Height / _initialFormHeight;
                ResizeControls(this.Controls, widthRatio, heightRatio);
            }
            finally
            {
                _isResizing = false;
            }
        }


        private void ResizeControls(Control.ControlCollection controls, float widthRatio, float heightRatio)
        {
            foreach (Control control in controls)
            {

                if (control is Panel panel)
                {
                    // Recursively adjust panel's child controls
                    ResizeControls(panel.Controls, widthRatio, heightRatio);
                }
                else
                {
                    // Adjust control dimensions
                    var controlInfo = Array.Find(_controlsInfo, c => c.Left == control.Left && c.Top == control.Top);
                    if (controlInfo != null)
                    {
                        control.Left = (int)(controlInfo.Left * widthRatio);
                        control.Top = (int)(controlInfo.Top * heightRatio);
                        control.Width = (int)(controlInfo.Width * widthRatio);
                        control.Height = (int)(controlInfo.Height * heightRatio);
                        control.Font = new Font(control.Font.FontFamily, controlInfo.FontSize * Math.Min(widthRatio, heightRatio));
                    }
                }
            }
        }


        private class ControlInfo
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public float FontSize { get; set; }

            public ControlInfo(int left, int top, int width, int height, float fontSize)
            {
                Left = left;
                Top = top;
                Width = width;
                Height = height;
                FontSize = fontSize;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?"
                , "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {

            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.ShowDialog();
            this.Close();




        }
































        // Define a variable to store the total price





        // Method to fetch the price of the selected item from the Menu table



        // Event handler when a row is clicked






        private void menuitemspanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }






































        private void paidamount_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }




        private void minusbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // Minimize the form to the taskbar
        }










        private void Fetchdatabtn_Click(object sender, EventArgs e)
        {

        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Database connection string (update with your actual connection details)


                // Get the selected date from the DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value.Date;

                // SQL query to fetch data filtered by ReservationDate
                string query = "SELECT * FROM Vw_DailyPaymentsSummary WHERE CAST(Paymentdate AS DATE) = @Paymentdate";

                // Create a connection to the database
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameter for the selected date
                        command.Parameters.AddWithValue("@Paymentdate", selectedDate);

                        // Create a data adapter
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Create a DataTable to load the results
                            DataTable dataTable = new DataTable();

                            // Fill the DataTable with the query results
                            adapter.Fill(dataTable);

                            // Set the DataSource of the DataGridView
                            reservationsview.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReservationsReport reservationsReport = new ReservationsReport();
            this.Hide();
            reservationsReport.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPayment addPayment = new AddPayment();
            this.Hide();
            addPayment.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }

}



