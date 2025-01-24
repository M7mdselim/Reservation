﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation
{
    public partial class ReservationsReport : Form
    {


        private string _username;
        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;





        public ReservationsReport(string username)
        {
            InitializeComponent();

            reservationsview.CellDoubleClick += reservationsview_CellDoubleClick;



          

            this.reservationsview.CellFormatting += new DataGridViewCellFormattingEventHandler(this.reservationsview_CellFormatting);


            _username = username;


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



        private void Home_Resize(object sender, EventArgs e)
        {
            float widthRatio = this.Width / _initialFormWidth;
            float heightRatio = this.Height / _initialFormHeight;
            ResizeControls(this.Controls, widthRatio, heightRatio);
        }

        private void ResizeControls(Control.ControlCollection controls, float widthRatio, float heightRatio)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                Control control = controls[i];
                ControlInfo controlInfo = _controlsInfo[i];

                control.Left = (int)(controlInfo.Left * widthRatio);
                control.Top = (int)(controlInfo.Top * heightRatio);
                control.Width = (int)(controlInfo.Width * widthRatio);
                control.Height = (int)(controlInfo.Height * heightRatio);

                // Adjust font size
                control.Font = new Font(control.Font.FontFamily, controlInfo.FontSize * Math.Min(widthRatio, heightRatio));
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

            NavigateToForm(4, new Home(_username));



        }

      













        













      


        // Define a variable to store the total price





        // Method to fetch the price of the selected item from the Menu table
       


        // Event handler when a row is clicked




   

        private void menuitemspanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            cashiernamelabel.Text = _username;
        }



        private void reservationsview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ensure the click is on a valid row and not on headers
                if (e.RowIndex >= 0)
                {
                    // Get the ReservationID from the clicked row
                    int reservationId = Convert.ToInt32(reservationsview.Rows[e.RowIndex].Cells["ReservationID"].Value);

                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                    {
                        connection.Open();

                        // Query to fetch order details with ItemName and ItemPrice for the selected ReservationID
                        string query = @"SELECT od.ReservationID, m.ItemName, od.Quantity, m.ItemPrice
                                 FROM OrderDetails od
                                 INNER JOIN Menu m ON od.MenuItemID = m.MenuItemID
                                 WHERE od.ReservationID = @ReservationID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ReservationID", reservationId);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    string details = "Order Details:\n";
                                    decimal totalAmount = 0; // Variable to store total amount of the order

                                    // Read the order details
                                    while (reader.Read())
                                    {
                                        decimal itemAmount = Convert.ToDecimal(reader["ItemPrice"]) * Convert.ToInt32(reader["Quantity"]);
                                        totalAmount += itemAmount; // Add the item amount to the total

                                        details +=
                                                   $"Item Name: {reader["ItemName"]}, " +
                                                   $"Quantity: {reader["Quantity"]}, " +
                                                   $"Item Price: {reader["ItemPrice"]}, " +
                                                   $"Price: {itemAmount:C}\n";
                                    }

                                    reader.Close();  // Close the reader before executing the next query

                                    // Now fetch the total paid for the reservation from the Payments table
                                    string totalPaidQuery = "SELECT SUM(PaidAmount) FROM Payments WHERE ReservationID = @ReservationID";
                                    using (SqlCommand paidCommand = new SqlCommand(totalPaidQuery, connection))
                                    {
                                        paidCommand.Parameters.AddWithValue("@ReservationID", reservationId);
                                        var totalPaid = paidCommand.ExecuteScalar();
                                        decimal amountPaid = totalPaid != DBNull.Value ? Convert.ToDecimal(totalPaid) : 0;

                                        details += $"\nTotal Amount for this order: {totalAmount:C}\n";
                                        details += $"PaidAmount: {amountPaid:C}";

                                        MessageBox.Show(details, "Order Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No order details found for this reservation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
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

        private void reservationsview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // Ensure formatting is applied to rows only (skip header rows and new rows)
                if (e.RowIndex >= 0 && !reservationsview.Rows[e.RowIndex].IsNewRow)
                {
                    // Check if the CustomerName column is "Total" and skip the formatting for this row
                    var row = reservationsview.Rows[e.RowIndex];
                    string customerName = row.Cells["CustomerName"].Value != DBNull.Value ? row.Cells["CustomerName"].Value.ToString() : string.Empty;

                    // If CustomerName is "Total", skip formatting
                    if (customerName.Equals("Total", StringComparison.OrdinalIgnoreCase))
                    {
                        return; // Skip further processing for this row
                    }

                    // Check if the RemainingAmount and PaidAmount columns exist in the grid
                    if (reservationsview.Columns.Contains("RemainingAmount") && reservationsview.Columns.Contains("PaidAmount"))
                    {
                        // Get the value of RemainingAmount and PaidAmount for the current row
                        var remainingAmountValue = row.Cells["RemainingAmount"].Value;
                        var paidAmountValue = row.Cells["PaidAmount"].Value;

                        // Initialize parsed values
                        decimal remainingAmount = 0;
                        decimal paidAmount = 0;

                        bool isRemainingAmountParsed = remainingAmountValue != null && decimal.TryParse(remainingAmountValue.ToString(), out remainingAmount);
                        bool isPaidAmountParsed = paidAmountValue != null && decimal.TryParse(paidAmountValue.ToString(), out paidAmount);

                        // Determine the background color to apply
                        Color newColor = reservationsview.DefaultCellStyle.BackColor;

                        // Highlight yellow if RemainingAmount > 0
                        if (isRemainingAmountParsed && remainingAmount > 0 || !isPaidAmountParsed)
                        {
                            newColor = Color.Yellow;
                            if (!isPaidAmountParsed || paidAmount <= 0)
                            {
                                newColor = Color.Red;
                            }
                        }
                        // Highlight red if PaidAmount is null, not a valid decimal, or <= 0
                        else
                        {
                            // If no other condition is met, reset to default color
                            newColor = reservationsview.DefaultCellStyle.BackColor;
                        }

                        // Apply the color only if it has changed
                        if (!row.Tag?.Equals(newColor) ?? true)
                        {
                            row.DefaultCellStyle.BackColor = newColor;
                            row.Tag = newColor; // Store the applied color in the row's Tag property
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during cell formatting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadReservations()
        {
            try
            {
                // Database connection string (update with your actual connection details)


                // Get the selected date from the DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value.Date;

                // SQL query to fetch data filtered by ReservationDate
                string query = @"
       SELECT 
    CAST(ReservationID AS NVARCHAR) AS ReservationID, 
    CustomerName, 
    CustomerPhoneNumber, 
    RestaurantName, 
    NumberOfGuests,
    Important AS اهميه,
    ReservationDate, 
    DateSubmitted, 
    TotalAmount, 
    PaidAmount, 
    RemainingAmount, 
    CashierName
   
FROM View_ReservationsDetails
WHERE ReservationDate = @ReservationDate

UNION ALL

SELECT 
    NULL AS ReservationID, 
   'TOTAL' AS CustomerName, 
    NULL AS CustomerPhoneNumber, 
    NULL AS RestaurantName, 
    SUM(NumberofGuests) AS NumberOfGuests,
    NULL AS Important,
    NULL AS ReservationDate, 
    NULL AS DateSubmitted, 
   NULL AS TotalAmount, 
    NULL AS PaidAmount, 
   NULL AS RemainingAmount, 
    NULL AS CashierName
  
FROM View_ReservationsDetails
WHERE ReservationDate = @ReservationDate;
;
";
                // Create a connection to the database
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameter for the selected date
                        command.Parameters.AddWithValue("@ReservationDate", selectedDate);

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



            private void searchbtn_Click(object sender, EventArgs e)
        {

            LoadReservations();
           
        }

       

       

        private void button2_Click(object sender, EventArgs e)
        {
            NavigateToForm(4, new AddPayment(_username));
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            NavigateToForm(4, new SpotCheck(_username));

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        private int currentPageIndex = 0;
        private int currentPage = 0; // Track the current page number
        private int rowsPerPage; // Number of rows per page
        private int totalRows; // Total number of rows
        private List<DataGridViewColumn> columnsToPrint;

        private Dictionary<string, string> columnHeaderMappings = new Dictionary<string, string>
{
   { "ReservationID", "ID" },
    { "CustomerName", "الاسم" },
    { "RestaurantName", "مطعم" },
    { "NumberOfGuests", "عدد " },
    { "ReservationDate", "معاد الحجز" },
    { "DateSubmitted", "تاريخ العمليه" },
    { "TotalAmount", "الاجمالي" },
    { "PaidAmount", "مدفوع" },
    { "RemainingAmount", "متبقي" },
    { "CashierName", "كاشير" }

};

        private Dictionary<string, object> printDataContainer = new Dictionary<string, object>();


        public class PrintHeaderInfo
        {
            public string HeaderText { get; set; }
            public string ReportDateText { get; set; }
            public string MonthText { get; set; }
        }


        private void PrintButton_Click(object sender, EventArgs e)
        {
            decimal grandTotalAmount = 0; // Variable to store total amount of all orders
            List<OrderSummary> orderSummaryList = new List<OrderSummary>(); // List to store order summary

            // Loop through each row in the reservations view
            foreach (DataGridViewRow row in reservationsview.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Check if the CustomerName is "Total" and skip the row if so
                    string customerName = row.Cells["CustomerName"].Value != DBNull.Value ? row.Cells["CustomerName"].Value.ToString() : string.Empty;
                    if (customerName.Equals("Total", StringComparison.OrdinalIgnoreCase))
                    {
                        continue; // Skip the row if CustomerName is "Total"
                    }

                    int reservationId = row.Cells["ReservationId"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["ReservationId"].Value) : 0;
                    decimal totalAmount = row.Cells["TotalAmount"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["TotalAmount"].Value) : 0m;
                    decimal paidAmount = row.Cells["PaidAmount"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["PaidAmount"].Value) : 0m;
                    string numberOfGuests = row.Cells["NumberOfGuests"].Value != DBNull.Value ? row.Cells["NumberOfGuests"].Value.ToString() : string.Empty;
                    string name = row.Cells["CustomerName"].Value.ToString();

                    // Call PrintReceipt for each row with the appropriate data
                    PrintReceipt(reservationId, totalAmount, paidAmount, numberOfGuests, name);

                    // Add the total amount of each reservation to the grand total
                    grandTotalAmount += totalAmount;

                    // Get order details for summary
                    var reservationSummary = GetOrderSummaryByReservation(reservationId);
                    orderSummaryList.AddRange(reservationSummary);
                }
            }

            // After all receipts, print the summary of total orders
            PrintGrandSummary();
        }





        private decimal GetTotalamount(int reservationId)
        {
            decimal currentTotalAmount = 0;

            string retrieveAmountsQuery = "SELECT TotalAmount FROM Payments WHERE ReservationID = @ReservationID";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                using (SqlCommand retrieveCommand = new SqlCommand(retrieveAmountsQuery, connection))
                {

                    retrieveCommand.Parameters.AddWithValue("@ReservationID", reservationId);

                    using (SqlDataReader reader = retrieveCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentTotalAmount = Convert.ToDecimal(reader["TotalAmount"]);

                        }
                    }
                }
            }



            return currentTotalAmount;
        }


        private string GetCapacityFromDatabase(int reservationId)
        {
            string capacity = string.Empty;
            string connectionString = DatabaseConfig.connectionString; // Replace with your actual connection string

            // Query to fetch capacity from reservations table
            string query = "SELECT NumberOfGuests FROM reservations WHERE reservationid = @reservationId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@reservationId", reservationId);
                        var result = cmd.ExecuteScalar();
                        capacity = result?.ToString(); // If result is null, capacity remains empty
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching capacity: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return capacity;
        }




        private string GetRestaurantNameByReservationId(int reservationId)
        {
            string restaurantName = string.Empty;
            // Replace with actual database logic to fetch the restaurant name
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                string query = "SELECT Name FROM Restaurant WHERE RestaurantID = (SELECT RestaurantId FROM Reservations WHERE reservationid = @reservationId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reservationId", reservationId);
                    restaurantName = command.ExecuteScalar()?.ToString() ?? "Restaurant Name Not Found";
                }
            }
            return restaurantName;
        }


        public DateTime GetReservationDateById(int reservationId)
        {
            // Define your connection string (update with your actual connection string)
            string connectionString = DatabaseConfig.connectionString;

            // SQL query to fetch the ReservationDate based on reservationId
            string query = "SELECT ReservationDate FROM reservations WHERE ReservationID = @ReservationID";

            // Declare the reservationDate variable to hold the retrieved value
            DateTime reservationDate = DateTime.MinValue;

            try
            {
                // Create and open a new SQL connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create the SQL command to execute
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);

                        // Execute the command and retrieve the reservation date
                        object result = cmd.ExecuteScalar();

                        // If a result is found, assign it to the reservationDate
                        if (result != DBNull.Value)
                        {
                            reservationDate = Convert.ToDateTime(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any potential errors (e.g., connection issues)
                Console.WriteLine($"Error retrieving reservation date: {ex.Message}");
            }

            // Return the retrieved reservation date or DateTime.MinValue if not found
            return reservationDate;
        }





        private void PrintReceipt(int reservationId, decimal totalAmount, decimal paidAmount, string numberOfGuests, string name)
        {
            DateTime reservationDate = GetReservationDateById(reservationId);
            string formattedReservationDateAndTime = reservationDate.ToString("dd/MM/yyyy");

            // Retrieve the restaurant name from the database based on reservationId
            string restaurantName = GetRestaurantNameByReservationId(reservationId);


            string notes = string.Empty;

            // Fetch notes from the Reservations table
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                string notesQuery = "SELECT Notes FROM Reservations WHERE ReservationID = @ReservationID";
                using (SqlCommand notesCommand = new SqlCommand(notesQuery, connection))
                {
                    notesCommand.Parameters.AddWithValue("@ReservationID", reservationId);
                    var result = notesCommand.ExecuteScalar();
                    if (result != null)
                    {
                        notes = result.ToString();
                    }
                }
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                float yPosition = 10; // Starting Y position
                float rightMargin = e.PageBounds.Width - 10; // Right margin
                float leftMargin = 10; // Left margin
                float header = 50;
                float lineHeight = 25; // Line height for spacing
                Font font = new Font("Arial", 10);
                Font boldFont = new Font("Arial", 10, FontStyle.Bold);
                Font titleFont = new Font("Arial", 12, FontStyle.Bold);
                StringFormat rtlFormat = new StringFormat { Alignment = StringAlignment.Far }; // Right-to-left alignment
                StringFormat leftAlignFormat = new StringFormat { Alignment = StringAlignment.Near }; // Left alignment
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center }; // Center alignment

                // Draw the company logo
                string logoPath = Path.Combine(Application.StartupPath, "logo.jpg");
                // Replace with the actual path to the logo
                if (System.IO.File.Exists(logoPath))
                {
                    Image logo = Image.FromFile(logoPath);
                    e.Graphics.DrawString(" COPY ", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, e.PageBounds.Width / 2, 5, centerFormat);
                    yPosition += 40; // Adjust for logo height
                    e.Graphics.DrawImage(logo, (e.PageBounds.Width - 100) / 2, yPosition, 100, 100); // Center the logo
                    yPosition += 110; // Adjust for logo height
                }

                // Draw the company name or title
                string companyName = "Royal Resort";
                e.Graphics.DrawString(companyName, titleFont, Brushes.Black, e.PageBounds.Width / 2, yPosition, centerFormat);
                yPosition += header;

                // Add the date and time
                e.Graphics.DrawString($"تاريخ: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", font, Brushes.Black, rightMargin, yPosition, rtlFormat);
                yPosition += lineHeight;

                // Add reservation ID on the right and reservation date on the left (same line)
                e.Graphics.DrawString($"رقم الحجز: {reservationId}", boldFont, Brushes.Black, rightMargin, yPosition, rtlFormat); // Right aligned
                e.Graphics.DrawString($"تاريخ الحجز: {formattedReservationDateAndTime}", boldFont, Brushes.Black, leftMargin, yPosition, leftAlignFormat); // Left aligned
                yPosition += lineHeight;

                // Add customer name on the right and cashier name on the left (same line)
                e.Graphics.DrawString($"حجز باسم: {name}", boldFont, Brushes.Black, rightMargin, yPosition, rtlFormat); // Right aligned
                e.Graphics.DrawString($"القائم بالحجز: {_username}", boldFont, Brushes.Black, leftMargin, yPosition, leftAlignFormat); // Left aligned
                yPosition += lineHeight;

                string resturantname = $" مطعم:  {restaurantName}";
                // Add the restaurant name under the customer name and number of guests
                e.Graphics.DrawString(resturantname, boldFont, Brushes.Black, leftMargin, yPosition, leftAlignFormat);
                string numberOfGuestsText = $"عدد اشخاص: {numberOfGuests}";
                e.Graphics.DrawString(numberOfGuestsText, boldFont, Brushes.Black, rightMargin, yPosition, rtlFormat);
                yPosition += lineHeight;

                // Draw a separator
                e.Graphics.DrawLine(Pens.Black, leftMargin, yPosition, e.PageBounds.Width - leftMargin, yPosition);
                yPosition += 10;

                // Add the order details (old items)
                e.Graphics.DrawString(":تفاصيل الاوردر", titleFont, Brushes.Black, rightMargin, yPosition, rtlFormat);
                yPosition += lineHeight;

                // Retrieve old items and print them
                string oldItemsQuery = "SELECT MenuItemName, Quantity, ItemPrice FROM View_ManageReservationsDetails WHERE ReservationID = @ReservationID";
                decimal oldTotalAmount = 0;

                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(oldItemsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ReservationID", reservationId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string menuItemName = reader["MenuItemName"] != DBNull.Value ? reader["MenuItemName"].ToString() : string.Empty;
                                int quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0;
                                decimal itemPrice = reader["ItemPrice"] != DBNull.Value ? Convert.ToDecimal(reader["ItemPrice"]) : 0m;

                                string itemDetails = $"{menuItemName} - {quantity} x {itemPrice:0.##}";
                                e.Graphics.DrawString(itemDetails, font, Brushes.Black, rightMargin, yPosition, rtlFormat);
                                oldTotalAmount += itemPrice * quantity;
                                yPosition += lineHeight;
                            }
                        }
                    }
                }


                // Add notes (ملحوظات) if present
                if (!string.IsNullOrEmpty(notes))
                {
                    // Draw another separator
                    e.Graphics.DrawLine(Pens.Black, leftMargin, yPosition, e.PageBounds.Width - leftMargin, yPosition);
                    yPosition += 10;

                    e.Graphics.DrawString(":ملحوظات", titleFont, Brushes.Black, rightMargin, yPosition, rtlFormat);
                    yPosition += lineHeight;

                    // Calculate the size of the notes string
                    float availableWidth = e.PageBounds.Width - leftMargin - rightMargin;
                    SizeF notesSize = e.Graphics.MeasureString(notes, font, (int)availableWidth);

                    // Define a rectangle for multi-line text rendering
                    RectangleF notesRect = new RectangleF(rightMargin, yPosition, availableWidth, notesSize.Height);

                    // Render the notes text inside the rectangle
                    e.Graphics.DrawString(notes, font, Brushes.Black, notesRect, rtlFormat);

                    // Update yPosition to account for the height of the rendered text
                    yPosition += (int)notesSize.Height;
                }

                // Add a separator line between old and new orders
                e.Graphics.DrawLine(Pens.Black, leftMargin, yPosition, e.PageBounds.Width - leftMargin, yPosition);
                yPosition += 10;

                e.Graphics.DrawString($"اجمالي المبلغ: {totalAmount:0.##} ج.م", boldFont, Brushes.Black, leftMargin, yPosition);
                yPosition += lineHeight;

                e.Graphics.DrawString($"المبلغ المدفوع: {paidAmount:0.##} ج.م", boldFont, Brushes.Black, leftMargin, yPosition);
                yPosition += lineHeight;

                // Draw another separator line before the footer
                e.Graphics.DrawLine(Pens.Black, leftMargin, yPosition, e.PageBounds.Width - leftMargin, yPosition);
                yPosition += 10;

                // Add the footer message
                string footerMessage = "شكرا على اختيارك دار الضيافه";
                e.Graphics.DrawString(footerMessage, boldFont, Brushes.Black, e.PageBounds.Width / 2, yPosition, centerFormat);
                yPosition += lineHeight;
            };

            printDocument.Print();
        }

        public class OrderSummary
        {
            public string MenuItemName { get; set; }
            public int TotalQuantity { get; set; }
            public decimal TotalPrice { get; set; }
        }

        private Dictionary<string, (int TotalQuantity, decimal TotalPrice)> grandTotalSummary = new Dictionary<string, (int, decimal)>();


        private List<OrderSummary> GetOrderSummaryByReservation(int reservationId)
        {
            List<OrderSummary> orderSummaryList = new List<OrderSummary>();

            string query = "SELECT MenuItemName, SUM(Quantity) AS TotalQuantity, SUM(Quantity * ItemPrice) AS TotalPrice " +
                           "FROM View_ManageReservationsDetails " +
                           "WHERE ReservationID = @ReservationID " +
                           "GROUP BY MenuItemName";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string menuItemName = reader["MenuItemName"]?.ToString() ?? "Unknown";
                            int itemQuantity = reader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(reader["TotalQuantity"]) : 0;
                            decimal itemPrice = reader["TotalPrice"] != DBNull.Value ? Convert.ToDecimal(reader["TotalPrice"]) : 0m;

                            // Add to order summary
                            orderSummaryList.Add(new OrderSummary
                            {
                                MenuItemName = menuItemName,
                                TotalQuantity = itemQuantity,
                                TotalPrice = itemPrice
                            });

                            // Update grand totals
                            if (grandTotalSummary.ContainsKey(menuItemName))
                            {
                                var existing = grandTotalSummary[menuItemName];
                                grandTotalSummary[menuItemName] = (existing.TotalQuantity + itemQuantity, existing.TotalPrice + itemPrice);
                            }
                            else
                            {
                                grandTotalSummary[menuItemName] = (itemQuantity, itemPrice);
                            }
                        }
                    }
                }
            }

            return orderSummaryList;
        }


        private void PrintGrandSummary()
        {
            // Exclude the 'total' row before printing
            var filteredSummary = grandTotalSummary.Where(item => item.Key != "TOTAL").ToList();  // Filter out "TOTAL" row

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                float yPosition = 10; // Starting Y position
                float lineHeight = 30; // Line height for spacing
                Font headerFont = new Font("Arial", 14, FontStyle.Bold);
                Font columnFont = new Font("Arial", 12, FontStyle.Bold);
                Font contentFont = new Font("Arial", 12);
                StringFormat centerAlignFormat = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat leftAlignFormat = new StringFormat { Alignment = StringAlignment.Near };

                // Page width and column positions
                float pageWidth = e.PageBounds.Width;
                float leftMargin = 50;
                float columnWidth = (pageWidth - 2 * leftMargin) / 2; // Divide space equally

                // Draw the header
                string headerText = "ملخص جميع الطلبات\nSummary of All Orders";
                if (!string.IsNullOrEmpty(currentFilterText)) // Add restaurant name if filtered
                {
                    headerText += $"\nمطعم : {currentFilterText}";
                }
                e.Graphics.DrawString(headerText, headerFont, Brushes.Black, pageWidth / 2, yPosition, centerAlignFormat);
                yPosition += lineHeight * 3;

                // Draw column headers
                e.Graphics.DrawLine(Pens.Black, leftMargin, yPosition - 5, pageWidth - leftMargin, yPosition - 5);
                e.Graphics.DrawString("Item", columnFont, Brushes.Black, leftMargin, yPosition, leftAlignFormat); // Left column
                e.Graphics.DrawString("Quantity", columnFont, Brushes.Black, leftMargin + columnWidth, yPosition, leftAlignFormat); // Right column
                yPosition += lineHeight;

                // Draw a line below column headers
                e.Graphics.DrawLine(Pens.Black, leftMargin, yPosition - 5, pageWidth - leftMargin, yPosition - 5);

                // Draw the item rows (after filtering out the "TOTAL" row)
                foreach (var item in filteredSummary)
                {
                    e.Graphics.DrawString(item.Key, contentFont, Brushes.Black, leftMargin, yPosition, leftAlignFormat); // Menu item
                    e.Graphics.DrawString(item.Value.TotalQuantity.ToString(), contentFont, Brushes.Black, leftMargin + columnWidth, yPosition, leftAlignFormat); // Total quantity
                    yPosition += lineHeight;
                }

                // Final separator line
                e.Graphics.DrawLine(Pens.Black, leftMargin, yPosition, pageWidth - leftMargin, yPosition);
            };

            printDocument.Print();
            grandTotalSummary.Clear();
        }






        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int totalWidth = columnsToPrint.Sum(col => col.Width);
            int printableWidth = e.MarginBounds.Width;
            float scaleFactor = (float)printableWidth / totalWidth;

            rowsPerPage = (int)((e.MarginBounds.Height - e.MarginBounds.Top) / (reservationsview.RowTemplate.Height + 5));

            string headerText = "تقرير يومي";
            string reportDateText = $"التاريخ: {dateTimePicker1.Value.Date.ToShortDateString()}";

            float y = e.MarginBounds.Top - 30;
            float x = e.MarginBounds.Left;

            Font headerFont = new Font(reservationsview.Font.FontFamily, 14, FontStyle.Bold);
            Font dateFont = new Font(reservationsview.Font.FontFamily, 12, FontStyle.Regular);

            SizeF headerSize = e.Graphics.MeasureString(headerText, headerFont);
            SizeF dateSize = e.Graphics.MeasureString(reportDateText, dateFont);

            float headerX = e.MarginBounds.Right - headerSize.Width;
            float dateX = e.MarginBounds.Right - dateSize.Width;

            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, new PointF(headerX, y));
            e.Graphics.DrawString(reportDateText, dateFont, Brushes.Black, new PointF(dateX, y + headerSize.Height + 5));

            y += (int)headerSize.Height + (int)dateSize.Height + 30;

            if (totalWidth > printableWidth)
            {
                scaleFactor = (float)printableWidth / totalWidth;
            }

            int remainingWidth = printableWidth;

            // Print column headers
            foreach (var column in columnsToPrint)
            {
                int columnWidth = (int)(column.Width * scaleFactor);
                RectangleF rect = new RectangleF(x, y, columnWidth, reservationsview.RowTemplate.Height);
                string headerColumnText = columnHeaderMappings.ContainsKey(column.Name) ? columnHeaderMappings[column.Name] : column.HeaderText;
                e.Graphics.DrawString(headerColumnText, reservationsview.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += columnWidth;
            }

            y += 25 + 5; // Move down for rows
            x = e.MarginBounds.Left;

            if (totalRows == 0)
            {
                totalRows = reservationsview.Rows.Count;
            }
           
            int rowsPrinted = 0;

            for (int i = currentPage * rowsPerPage; i < totalRows; i++)
            {
                if (reservationsview.Rows[i].IsNewRow) continue;

                x = e.MarginBounds.Left;
                foreach (var cell in reservationsview.Rows[i].Cells.Cast<DataGridViewCell>().Where(c => c.OwningColumn.Name != "CustomerPhoneNumber" && c.OwningColumn.Name != "RemainingAmount"
                && c.OwningColumn.Name != "PaidAmount" && c.OwningColumn.Name != "TotalAmount" && c.OwningColumn.Name != "CashierName" && c.OwningColumn.Name != "DateSubmitted" ))
                {
                    int cellWidth = (int)(cell.OwningColumn.Width * scaleFactor);
                    string cellValue = cell.Value?.ToString();

                    // Replace '0' and '1' values in specific columns with an empty string
                    if (cellValue == "0" || cellValue == "1")
                    {
                        if (i == totalRows - 1) // Only apply for the total row
                        {
                            cellValue = ""; // Replace with an empty string or another placeholder
                        }
                    }

                    RectangleF rect = new RectangleF(x, y, cellWidth, reservationsview.RowTemplate.Height);
                    e.Graphics.DrawString(cellValue, reservationsview.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    x += cellWidth;
                }

                y += reservationsview.RowTemplate.Height + 5;
                rowsPrinted++;

                if (rowsPrinted >= rowsPerPage)
                {
                    currentPage++;
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
            currentPage = 0;
            totalRows = 0;
        }
       

        private string currentFilterText = ""; // To store the current filter text

        private void ApplyFilter()
        {
            string restaurantFilter = "";

            // Ensure the filtering logic is correct based on the selected index
            if (filterselectioncombo.SelectedIndex == 0) // "اسم المبلغ" selected
            {
                restaurantFilter = "عزايم"; // If needed, ensure this value is valid in your context
                currentFilterText = restaurantFilter;
            }
            else if (filterselectioncombo.SelectedIndex == 1) // "المكان" selected
            {
                restaurantFilter = "انترناشونال";
                currentFilterText = restaurantFilter;
            }
            else if (filterselectioncombo.SelectedIndex == 2) // "المكان" selected
            {
                restaurantFilter = "ايطالي";
                currentFilterText = restaurantFilter;
            }
            else if (filterselectioncombo.SelectedIndex == 3) // "المكان" selected
            {
                restaurantFilter = "بونكا";
                currentFilterText = restaurantFilter;
            }

            // Ensure that the filter is applied correctly
            if (!string.IsNullOrEmpty(currentFilterText))
            {
                FilterReservation(restaurantFilter); // Call the method to apply the filter
            }
            else
            {
                // Handle the case where no filter is applied
                MessageBox.Show("Please select a filter before applying.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void filterselectioncombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void filteringTxtBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }



        private void FilterReservation(string restaurantFilter)
        {
            try
            {
                DateTime selectedDate = dateTimePicker1.Value.Date;

                string query = @"
           SELECT 
    CAST(ReservationID AS NVARCHAR) AS ReservationID, 
    CustomerName, 
    CustomerPhoneNumber, 
    RestaurantName, 
    NumberOfGuests, 
    ReservationDate, 
    DateSubmitted, 
    TotalAmount, 
    PaidAmount, 
    RemainingAmount, 
    CashierName
FROM View_ReservationsDetails
WHERE ReservationDate = @ReservationDate
AND RestaurantName = @RestaurantName

UNION ALL

SELECT 
    NULL AS ReservationID, 
    'TOTAL' AS CustomerName, 
    NULL AS CustomerPhoneNumber, 
    NULL AS RestaurantName, 
   NULL AS NumberOfGuests, 
    NULL AS ReservationDate, 
    NULL AS DateSubmitted, 
    NULL AS TotalAmount, 
    NULL AS PaidAmount, 
    NULL AS RemainingAmount, 
    NULL AS CashierName
FROM View_ReservationsDetails
WHERE ReservationDate = @ReservationDate
AND RestaurantName = @RestaurantName
GROUP BY RestaurantName;

        ";

                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservationDate", selectedDate);
                        command.Parameters.AddWithValue("@RestaurantName", restaurantFilter);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                           

                            reservationsview.DataSource = dataTable;

                         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void pdfbutton_Click(object sender, EventArgs e)
        {
            currentPageIndex = 0;

            // Explicitly exclude "CustomerPhoneNumber" from the columns to print
            columnsToPrint = reservationsview.Columns.Cast<DataGridViewColumn>()
       .Where(col => col.Visible &&
                     col.Name != "CustomerPhoneNumber" &&
                     col.Name != "RemainingAmount" &&
                     col.Name != "PaidAmount" &&
                     col.Name != "TotalAmount" &&
                     col.Name != "DateSubmitted" &&
                     col.Name != "CashierName")
       .ToList();


            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            // Set landscape mode
            printDocument.DefaultPageSettings.Landscape = true;

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void backkbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(4, new Navigation(_username));
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }



        private void NavigateToForm(int requiredRole, Form targetForm, string unauthorizedMessage = "غير مسموح بالضغط على هذا الزرار")
        {
            if (GlobalUser.Role != requiredRole)
            {
                this.Hide();
                targetForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show(unauthorizedMessage, "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            NavigateToForm(4, new Addorders(_username));


        }
    }

}



