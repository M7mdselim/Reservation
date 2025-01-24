﻿using System;
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
    public partial class MonthlyReport : Form
    {

        private string _username;


        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;


        public MonthlyReport(string username)
        {
            InitializeComponent();


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

        private void Editorder_Load(object sender, EventArgs e)
        {
            // Load reservation data when the form loads
            LoadReservations();
        }

        private void LoadReservations()
        {
            // Get the selected month and year from the DateTimePicker
            int selectedYear = dateTimePicker1.Value.Year;
            int selectedMonth = dateTimePicker1.Value.Month;

            // SQL query to fetch reservations for the selected month and year, and add a totals row
            string reservationsQuery = @"
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
    WHERE YEAR(DateSubmitted) = @Year AND MONTH(DateSubmitted) = @Month

    UNION ALL

    SELECT 
        'TOTAL' AS ReservationID, 
        NULL AS CustomerName, 
        NULL AS CustomerPhoneNumber, 
        NULL AS RestaurantName, 
        NULL AS NumberOfGuests, 
        NULL AS ReservationDate, 
        NULL AS DateSubmitted, 
        SUM(TotalAmount) AS TotalAmount, 
        SUM(PaidAmount) AS PaidAmount, 
        SUM(RemainingAmount) AS RemainingAmount, 
        NULL AS CashierName
       
    FROM View_ReservationsDetails
    WHERE YEAR(DateSubmitted) = @Year AND MONTH(DateSubmitted) = @Month;
    ";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.connectionString))
            {
                try
                {
                    // Fetch reservations data with filtering by year and month, and totals
                    SqlDataAdapter reservationsAdapter = new SqlDataAdapter(reservationsQuery, conn);
                    reservationsAdapter.SelectCommand.Parameters.AddWithValue("@Year", selectedYear);
                    reservationsAdapter.SelectCommand.Parameters.AddWithValue("@Month", selectedMonth);

                    DataTable reservationsTable = new DataTable();
                    reservationsAdapter.Fill(reservationsTable);

                    // Bind the data to the DataGridView
                    ManageReservationGridview.DataSource = reservationsTable;

                   
                    // Hide specific columns from the view
                    string[] columnsToHide = new string[]
                    {
                "CustomerPhoneNumber", // Hide sensitive data
               // Hide the helper column used for identifying totals
                    };

                    foreach (var column in columnsToHide)
                    {
                        if (ManageReservationGridview.Columns.Contains(column))
                        {
                            ManageReservationGridview.Columns[column].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading monthly reservations: {ex.Message}");
                }
            }
        }




        // This method will handle the search button click
        private void loadbtn_Click(object sender, EventArgs e)
        {



            LoadReservations();

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DailyReports dailyReports = new DailyReports(_username);
            this.Hide();
            dailyReports.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Home home = new Home(_username);
            this.Hide();
            home.ShowDialog();
            this.Close();
        }

        private void MonthlyReport_Load(object sender, EventArgs e)
        {
            cashiernamelabel.Text = _username;
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
            currentPageIndex = 0;

            // Explicitly exclude "CustomerPhoneNumber" from the columns to print
            columnsToPrint = ManageReservationGridview.Columns.Cast<DataGridViewColumn>()
                .Where(col => col.Visible && col.Name != "CustomerPhoneNumber" )
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

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Calculate scale factor for fitting content to page width
            int totalWidth = columnsToPrint.Sum(col => col.Width);
            int printableWidth = e.MarginBounds.Width;
            float scaleFactor = (float)printableWidth / totalWidth;

            // Calculate rows per page
            rowsPerPage = (int)((e.MarginBounds.Height - e.MarginBounds.Top) / (ManageReservationGridview.RowTemplate.Height + 5)); // Adjust spacing as needed

            // Print header with title and date on each page
            string headerTitle = "تقرير شهري";
            string headerDate = dateTimePicker1.Value.ToString("MMMM yyyy");  // Format the date as needed
            e.Graphics.DrawString(headerTitle, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top);
            e.Graphics.DrawString(headerDate, new Font("Arial", 12), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top + 30);  // Add some space below the title

            // Move y position below the header
            float y = e.MarginBounds.Top + 60; // Adjust as needed
            float x = e.MarginBounds.Left;

            // Print column headers
            foreach (var column in columnsToPrint)
            {
                int columnWidth = (int)(column.Width * scaleFactor);
                RectangleF rect = new RectangleF(x, y, columnWidth, ManageReservationGridview.RowTemplate.Height);
                string headerColumnText = columnHeaderMappings.ContainsKey(column.Name) ? columnHeaderMappings[column.Name] : column.HeaderText;
                e.Graphics.DrawString(headerColumnText, ManageReservationGridview.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += columnWidth;
            }

            y += 25 + 5; // Move down for rows
            x = e.MarginBounds.Left;

            if (totalRows == 0)
            {
                totalRows = ManageReservationGridview.Rows.Count;
            }

            int rowsPrinted = 0;

            for (int i = currentPage * rowsPerPage; i < totalRows; i++)
            {
                if (ManageReservationGridview.Rows[i].IsNewRow) continue;

                x = e.MarginBounds.Left;
                foreach (var cell in ManageReservationGridview.Rows[i].Cells.Cast<DataGridViewCell>().Where(c => c.OwningColumn.Name != "CustomerPhoneNumber"))
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

                    RectangleF rect = new RectangleF(x, y, cellWidth, ManageReservationGridview.RowTemplate.Height);
                    e.Graphics.DrawString(cellValue, ManageReservationGridview.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    x += cellWidth;
                }

                y += ManageReservationGridview.RowTemplate.Height + 5;
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


        private void ApplyFilter()
        {
            if (ManageReservationGridview.DataSource is DataTable dataTable)
            {
                string filter = "";

                if (filterselectioncombo.SelectedIndex == 0)  // "اسم المبلغ" selected
                {
                    if (!string.IsNullOrWhiteSpace(filteringTxtBox.Text))
                    {
                        filter = $"RestaurantName LIKE '%{filteringTxtBox.Text}%'";
                    }
                }
                else if (filterselectioncombo.SelectedIndex == 1)  // "المكان" selected
                {
                    if (!string.IsNullOrWhiteSpace(filteringTxtBox.Text))
                    {
                        filter = $"CashierName LIKE '%{filteringTxtBox.Text}%'";
                    }
                }



                // Apply the filter to the DataTable
                dataTable.DefaultView.RowFilter = filter;
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

        private void button2_Click(object sender, EventArgs e)
        {
            UserLog userLog = new UserLog(_username);
            this.Hide();
            userLog.ShowDialog();
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backkbtn_Click(object sender, EventArgs e)
        {
            Navigation navigation = new Navigation(_username);
            this.Hide();
            navigation.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeletedReservations deletedReservations = new DeletedReservations(_username);
            this.Hide();
            deletedReservations.ShowDialog();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void MonthlyReport_Load_1(object sender, EventArgs e)
        {

        }
    }
}
