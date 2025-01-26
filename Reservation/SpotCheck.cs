using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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



        private string _username;
        public SpotCheck(string username)
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

            _username = username;
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
            Home home = new Home(_username);
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
            cashiernamelabel.Text = _username;
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
                string query = @"
                    SELECT 
                    PaymentID,
                    Name,
                    ReservationID,
                    PaidAmount,
                    PaymentDate,
                    CustomerID,
                    Cashiername,
                    PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                    AND Cashiername = @Username
                UNION ALL
                SELECT 
                    NULL AS PaymentID,
                    'Total Cash' AS Name,
                    NULL AS ReservationID,
                    SUM(PaidAmount) AS PaidAmount,
                    NULL AS PaymentDate,
                    NULL AS CustomerID,
                    NULL AS Cashiername,
                    'Cash' AS PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                    AND Cashiername = @Username
                    AND PaymentMethod = 'Cash'
                UNION ALL
                SELECT 
                    NULL AS PaymentID,
                    'Total Visa' AS Name,
                    NULL AS ReservationID,
                    SUM(PaidAmount) AS PaidAmount,
                    NULL AS PaymentDate,
                    NULL AS CustomerID,
                    NULL AS Cashiername,
                    'Visa' AS PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                    AND Cashiername = @Username
                    AND PaymentMethod = 'Visa'
                UNION ALL
                SELECT 
                    NULL AS PaymentID,
                    'Subtotal' AS Name,
                    NULL AS ReservationID,
                    SUM(PaidAmount) AS PaidAmount,
                    NULL AS PaymentDate,
                    NULL AS CustomerID,
                    NULL AS Cashiername,
                    NULL AS PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                    AND Cashiername = @Username;
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
                        command.Parameters.AddWithValue("@Paymentdate", selectedDate);
                        command.Parameters.AddWithValue("@Username", _username);

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
            ReservationsReport reservationsReport = new ReservationsReport(_username);
            this.Hide();
            reservationsReport.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPayment addPayment = new AddPayment(_username);
            this.Hide();
            addPayment.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }



        private int currentPageIndex = 0;
        private List<DataGridViewColumn> columnsToPrint;
        private int currentPage = 0; // Track the current page number
        private int rowsPerPage; // Number of rows per page
        private int totalRows; // Total number of rows

        private Dictionary<string, string> columnHeaderMappings = new Dictionary<string, string>
{

     { "Cashiername", "القائم بالحجز" },
    
    
    { "PaymentDate", "تاريخ الدفع" },
    { "PaidAmount", "المدفوع" },
   { "ReservationID", "رقم الحجز" },
    { "Name", "الاسم" },
    
    { "PaymentID", "رقم العمليه" }
};


        private void Printbtn_Click(object sender, EventArgs e)
        {
            currentPageIndex = 0;

            // Manually define the column order you want for printing
            columnsToPrint = new List<DataGridViewColumn>
    {
        reservationsview.Columns["Cashiername"],
        
        reservationsview.Columns["PaymentDate"],
        reservationsview.Columns["PaidAmount"],
        reservationsview.Columns["ReservationID"],
        reservationsview.Columns["Name"],
        reservationsview.Columns["PaymentID"]
    };

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

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Calculate scale factor for fitting content to page width
            int totalWidth = columnsToPrint.Sum(col => col.Width);
            int printableWidth = e.MarginBounds.Width;
            float scaleFactor = (float)printableWidth / totalWidth;

            // Calculate rows per page
            rowsPerPage = (int)((e.MarginBounds.Height - e.MarginBounds.Top) / (reservationsview.RowTemplate.Height + 5)); // Adjust spacing as needed

            // Print header with title and date on each page
            string headerText = "تقرير يومي";
            string reportDateText = $"التاريخ: {dateTimePicker1.Value.Date.ToShortDateString()}";

            // Adjust the y position to decrease space above the header
            float y = e.MarginBounds.Top - 30; // Start closer to the top of the page
            float x = e.MarginBounds.Left;

            // Define font sizes
            Font headerFont = new Font(reservationsview.Font.FontFamily, 14, FontStyle.Bold);
            Font dateFont = new Font(reservationsview.Font.FontFamily, 12, FontStyle.Regular);

            // Measure the width of the header and date texts
            SizeF headerSize = e.Graphics.MeasureString(headerText, headerFont);
            SizeF dateSize = e.Graphics.MeasureString(reportDateText, dateFont);

            // Set x positions for right-aligned text
            float headerX = e.MarginBounds.Right - headerSize.Width;
            float dateX = e.MarginBounds.Right - dateSize.Width;

            // Print the header text and date
            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, new PointF(headerX, y));
            e.Graphics.DrawString(reportDateText, dateFont, Brushes.Black, new PointF(dateX, y + headerSize.Height + 5)); // Add space between header and date

            // Add less additional space between date and content
            y += (int)headerSize.Height + (int)dateSize.Height + 30; // Reduce the space as needed

            // Print column headers in the manually defined order
            x = e.MarginBounds.Left;
            foreach (var column in columnsToPrint)
            {
                int columnWidth = (int)(column.Width * scaleFactor);
                RectangleF rect = new RectangleF(x, y, columnWidth, reservationsview.RowTemplate.Height);
                string headerColumnText = columnHeaderMappings.ContainsKey(column.Name)
                    ? columnHeaderMappings[column.Name]
                    : column.HeaderText;

                e.Graphics.DrawString(headerColumnText, reservationsview.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += columnWidth;
            }

            // Move down for rows, adjust spacing as needed
            y += reservationsview.RowTemplate.Height + 5; // Move down for the next row

            // Track rows printed on current page
            int rowsPrinted = 0;

            // Print rows
            for (int i = currentPage * rowsPerPage; i < reservationsview.Rows.Count; i++)
            {
                if (reservationsview.Rows[i].IsNewRow) continue;

                x = e.MarginBounds.Left;
                foreach (var column in columnsToPrint)
                {
                    var cell = reservationsview.Rows[i].Cells[column.Name];
                    int cellWidth = (int)(column.Width * scaleFactor);
                    RectangleF rect = new RectangleF(x, y, cellWidth, reservationsview.RowTemplate.Height);
                    e.Graphics.DrawString(cell.Value?.ToString(), reservationsview.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    x += cellWidth;
                }

                y += reservationsview.RowTemplate.Height + 5; // Move down for the next row
                rowsPrinted++;

                // Check if we need to create a new page
                if (rowsPrinted >= rowsPerPage)
                {
                    currentPage++; // Increment page number
                    e.HasMorePages = true;
                    return; // Exit method to trigger the next page
                }
            }

            // If we've finished printing all rows, reset for the next print job
            e.HasMorePages = false;
            currentPage = 0; // Reset page number for the next print job
        }

        private void backkbtn_Click(object sender, EventArgs e)
        {
            Navigation navigation = new Navigation(_username);
            this.Hide();
            navigation.ShowDialog();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }
    }

}



