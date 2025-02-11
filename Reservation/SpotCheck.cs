using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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


            ConfigureNameAutoComplete();
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
           
            ConfigureNameAutoComplete();
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
                    PaymentID,
                    Name,
                    ReservationID,
                    PaidAmount,
                    PaymentDate,
                   
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
                        if (GlobalUser.Role == 1)
                        {

                            command.Parameters.AddWithValue("@Username", usertxt.Text);

                        }
                        else
                        {

                            command.Parameters.AddWithValue("@Username",_username);

                        }

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



        private void LoadAllReservations()
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
                   
                    Cashiername,
                    PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                   
                UNION ALL
                SELECT 
                    NULL AS PaymentID,
                    'Total Cash' AS Name,
                    NULL AS ReservationID,
                    SUM(PaidAmount) AS PaidAmount,
                    NULL AS PaymentDate,
                  
                    NULL AS Cashiername,
                    'Cash' AS PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                   
                    AND PaymentMethod = 'Cash'
                UNION ALL
                SELECT 
                    NULL AS PaymentID,
                    'Total Visa' AS Name,
                    NULL AS ReservationID,
                    SUM(PaidAmount) AS PaidAmount,
                    NULL AS PaymentDate,
                  
                    NULL AS Cashiername,
                    'Visa' AS PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                  
                    AND PaymentMethod = 'Visa'
                UNION ALL
                SELECT 
                    NULL AS PaymentID,
                    'Subtotal' AS Name,
                    NULL AS ReservationID,
                    SUM(PaidAmount) AS PaidAmount,
                    NULL AS PaymentDate,
                   
                    NULL AS Cashiername,
                    NULL AS PaymentMethod
                FROM 
                    vw_DailyPaymentsSummary
                WHERE 
                    CAST(PaymentDate AS DATE) = @PaymentDate 
                    
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
                       // command.Parameters.AddWithValue("@Username", usertxt.Text);

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
            if (GlobalUser.Role == 1)
            {

                usertxt.Enabled = true;
                // If usertxt has text, load specific reservations instead of all
                if (!string.IsNullOrWhiteSpace(usertxt.Text))
                {
                    LoadReservations();
                }
                else
                {
                    LoadAllReservations();
                }
            }
            else
            {
                LoadReservations();
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
            try
            {
                int paperWidth = e.MarginBounds.Width; // Thermal printer width
                int y = 5; // Start at top of page

                // Use monospaced font for better alignment
                Font titleFont = new Font("Courier New", 14, FontStyle.Bold);
                Font headerFont = new Font("Courier New", 10, FontStyle.Bold);
                Font bodyFont = new Font("Courier New", 9, FontStyle.Regular);
                Font summaryFont = new Font("Courier New", 10, FontStyle.Bold);

                // Print Store Name (Centered)
                string storeName = "Dareldyafa Sportshub"; // Customize if needed
                SizeF storeSize = e.Graphics.MeasureString(storeName, titleFont);
                e.Graphics.DrawString(storeName, titleFont, Brushes.Black, (paperWidth - storeSize.Width) / 2, y);
                y += (int)storeSize.Height + 5;

                // Print Report Title
                string headerText = "تقرير يومي";
                SizeF headerSize = e.Graphics.MeasureString(headerText, titleFont);
                e.Graphics.DrawString(headerText, titleFont, Brushes.Black, (paperWidth - headerSize.Width) / 2, y);
                y += (int)headerSize.Height + 5;

                // Print Date
                string reportDateText = $"التاريخ: {dateTimePicker1.Value:yyyy-MM-dd}";
                e.Graphics.DrawString(reportDateText, headerFont, Brushes.Black, 5, y);
                y += 20;

                // Define Column Positions (Manual Spacing)
                int[] colWidths = { 60, 100, 80, 60, 100, 70 }; // Adjust widths
                int x = 5; // Start X position

                // Print Column Headers with Arabic Labels
                string[] headers = { "القائم بالحجز", "تاريخ الدفع", "المدفوع", "رقم الحجز", "الاسم", "رقم العملية" };

                for (int i = 0; i < headers.Length; i++)
                {
                    e.Graphics.DrawString(headers[i], headerFont, Brushes.Black, x, y, new StringFormat { Alignment = StringAlignment.Center });
                    x += colWidths[i];
                }

                y += 20;
                e.Graphics.DrawString("------------------------------------------", bodyFont, Brushes.Black, 5, y);
                y += 15;

                // Print Rows
                decimal totalCash = 0;
                decimal totalVisa = 0;
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in reservationsview.Rows)
                {
                    if (row.IsNewRow) continue; // Skip empty rows

                    try
                    {
                        string cashier = row.Cells["Cashiername"]?.Value?.ToString() ?? "N/A";
                        string date = row.Cells["PaymentDate"]?.Value != null
                            ? Convert.ToDateTime(row.Cells["PaymentDate"].Value).ToString("yy-MM-dd")
                            : "N/A";
                        string amount = row.Cells["PaidAmount"]?.Value?.ToString() ?? "0";
                        string resID = row.Cells["ReservationID"]?.Value?.ToString() ?? "-";
                        string name = row.Cells["Name"]?.Value?.ToString() ?? "Unknown";
                        string payID = row.Cells["PaymentID"]?.Value?.ToString() ?? "-";
                        string paymentMethod = row.Cells["PaymentMethod"]?.Value?.ToString() ?? "N/A";

                        // Parse and accumulate totals
                        if (decimal.TryParse(amount, out decimal parsedAmount))
                        {
                            totalAmount += parsedAmount;
                            if (paymentMethod.Equals("Cash")) totalCash += parsedAmount;
                            else if (paymentMethod.Equals("Visa")) totalVisa += parsedAmount;
                        }

                        // Reset X Position
                        x = 5;

                        // Print values with column alignment
                        e.Graphics.DrawString(cashier, bodyFont, Brushes.Black, x, y, new StringFormat { Alignment = StringAlignment.Center });
                        x += colWidths[0];

                        e.Graphics.DrawString(date, bodyFont, Brushes.Black, x, y, new StringFormat { Alignment = StringAlignment.Center });
                        x += colWidths[1];

                        e.Graphics.DrawString(amount, bodyFont, Brushes.Black, x, y, new StringFormat { Alignment = StringAlignment.Far });
                        x += colWidths[2];

                        e.Graphics.DrawString(resID, bodyFont, Brushes.Black, x, y, new StringFormat { Alignment = StringAlignment.Center });
                        x += colWidths[3];

                        e.Graphics.DrawString(name, bodyFont, Brushes.Black, x, y, new StringFormat { Alignment = StringAlignment.Near });
                        x += colWidths[4];

                        e.Graphics.DrawString(payID, bodyFont, Brushes.Black, x, y, new StringFormat { Alignment = StringAlignment.Far });

                        y += 20;
                    }
                    catch (Exception rowEx)
                    {
                        e.Graphics.DrawString($"خطأ في قراءة الصف: {row.Index}", bodyFont, Brushes.Red, 5, y);
                        y += 20;
                        Console.WriteLine($"Row Error: {rowEx.Message}"); // Debugging
                    }
                }

                // Print Summary Rows
                y += 10;
                e.Graphics.DrawString("------------------------------------------", bodyFont, Brushes.Black, 5, y);
                y += 15;

                e.Graphics.DrawString($"إجمالي النقدي: {totalCash:0.00} ج", summaryFont, Brushes.Black, 5, y);
                y += 20;
                e.Graphics.DrawString($"إجمالي الفيزا: {totalVisa:0.00} ج", summaryFont, Brushes.Black, 5, y);
                y += 20;
                e.Graphics.DrawString($"المبلغ الإجمالي: {totalAmount:0.00} ج", summaryFont, Brushes.Black, 5, y);
                y += 20;

                e.Graphics.DrawString("------------------------------------------", bodyFont, Brushes.Black, 5, y);
                y += 15;

                // Print Footer
                string footerText = "شكراً لاستخدامكم نظامنا!";
                SizeF footerSize = e.Graphics.MeasureString(footerText, titleFont);
                e.Graphics.DrawString(footerText, titleFont, Brushes.Black, (paperWidth - footerSize.Width) / 2, y);
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString($"⚠ خطأ أثناء الطباعة: {ex.Message}", new Font("Arial", 10, FontStyle.Bold), Brushes.Red, 5, 10);
                Console.WriteLine($"Print Error: {ex.Message}");
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home(_username);
            this.Hide();
            home .ShowDialog();
            this.Close();
        }




        private void ConfigureNameAutoComplete()
        {
            AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();
            string query = "SELECT FullName FROM Cashier";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                // Add both normalized and reverse-normalized versions to the collection
                                autoCompleteCollection.Add(NormalizeArabicText(name));
                                autoCompleteCollection.Add(ReverseNormalizeArabicText(name));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }

            usertxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            usertxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            usertxt.AutoCompleteCustomSource = autoCompleteCollection;

          
        }


        private async Task SearchUserByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {

                return;
            }

            // Normalize the input name
            string normalizedInput = NormalizeArabicText(name);
            Debug.WriteLine($"Normalized Input: {normalizedInput}");

            string query = @"
            SELECT CashierID, FullName
            FROM Cashier 
            WHERE Name LIKE '%' + @Name + '%' OR dbo.NormalizeArabicText(Name) LIKE '%' + dbo.NormalizeArabicText(@Name) + '%'
";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Pass the normalized input to the query
                    command.Parameters.AddWithValue("@Name", normalizedInput);

                    Debug.WriteLine($"Query: {query}");
                    Debug.WriteLine($"Parameter @Name: {normalizedInput}");

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string id = reader["CashierID"]?.ToString() ?? "N/A";
                               
                                string names = reader["FullName"]?.ToString() ?? "N/A";


                                if (InvokeRequired)
                                {
                                    Invoke(new Action(() =>
                                    {

                                       
                                        usertxt.Text = names;

                                    }));
                                }
                                else
                                {

                                   
                                    usertxt.Text = names;

                                }

                              
                            }

                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("A SQL error occurred: " + ex.Message);
                        Debug.WriteLine($"SQL Exception: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                        Debug.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }
        }



        private string NormalizeArabicText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return text
                .Replace('أ', 'ا')  // Normalize 'أ' to 'ا'
                .Replace('إ', 'ا')  // Normalize 'إ' to 'ا'
                .Replace('آ', 'ا')  // Normalize 'آ' to 'ا'


                .Replace('ة', 'ه'); // Normalize 'ة' to 'ه'
        }

        private string ReverseNormalizeArabicText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return text
                .Replace('ا', 'أ')  // Reverse normalize 'ا' to 'أ'
                .Replace('ا', 'إ')  // Reverse normalize 'ا' to 'إ'
                .Replace('ا', 'آ')  // Reverse normalize 'ا' to 'آ'

                .Replace('ه', 'ة');  // Reverse normalize 'ه' to 'ة'

        }

        private async void usertxt_TextChanged(object sender, EventArgs e)
        {
            if (usertxt.AutoCompleteMode == AutoCompleteMode.None)
            {
              await  SearchUserByNameAsync(usertxt.Text); 
            }
        }
    }

}



