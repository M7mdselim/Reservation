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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Reservation
{
    public partial class UserLog : Form
    {
        private string _username;



        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;


        public UserLog(string username)
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
            LoadUserLogs();
        }

        private void LoadUserLogs()
        {
            // SQL query to fetch logs and order by action in descending order
            string userLogQuery = @"
        SELECT 
            LogID,
            CashierName, 
            Action, 
            DateAndTime
        FROM UserLog
        ORDER BY DateAndTime DESC;
    ";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.connectionString))
            {
                try
                {
                    // Fetch user logs with descending order by DateAndTime
                    SqlDataAdapter userLogAdapter = new SqlDataAdapter(userLogQuery, conn);
                    DataTable userLogTable = new DataTable();
                    userLogAdapter.Fill(userLogTable);

                    // Bind the data to the DataGridView
                    ManageReservationGridview.DataSource = userLogTable;

                    // Set the DataGridView to stretch columns and take up available space
                    ManageReservationGridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Manually adjust the width of the columns
                    if (ManageReservationGridview.Columns.Contains("LogID"))
                    {
                        ManageReservationGridview.Columns["LogID"].Width = 50; // Smaller width for LogID
                    }

                    if (ManageReservationGridview.Columns.Contains("CashierName"))
                    {
                        ManageReservationGridview.Columns["CashierName"].Width = 80; // Slightly larger width for CashierName
                    }

                    if (ManageReservationGridview.Columns.Contains("DateAndTime"))
                    {
                        ManageReservationGridview.Columns["DateAndTime"].Width = 120; // Slightly larger width for CashierName
                    }

                    // Optionally, adjust the row height to ensure readability
                    foreach (DataGridViewRow row in ManageReservationGridview.Rows)
                    {
                        row.Height = 25; // Set row height for better readability
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading user logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }





        // This method will handle the search button click
        private void loadbtn_Click(object sender, EventArgs e)
        {



            LoadUserLogs();
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

        private void DailyReports_Load(object sender, EventArgs e)
        {
            cashiernamelabel.Text = _username;
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            MonthlyReport monthlyReport = new MonthlyReport(_username);
            this.Hide();
            monthlyReport.ShowDialog();
            this.Close();
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
                .Where(col => col.Visible && col.Name != "CustomerPhoneNumber" && col.Name != "UserID")
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
            int totalWidth = columnsToPrint.Sum(col => col.Width);
            int printableWidth = e.MarginBounds.Width;
            float scaleFactor = (float)printableWidth / totalWidth;

            rowsPerPage = (int)((e.MarginBounds.Height - e.MarginBounds.Top) / (ManageReservationGridview.RowTemplate.Height + 5));

            string headerText = "تقرير يومي";
            string reportDateText = $"التاريخ: {dateTimePicker1.Value.Date.ToShortDateString()}";

            float y = e.MarginBounds.Top - 30;
            float x = e.MarginBounds.Left;

            Font headerFont = new Font(ManageReservationGridview.Font.FontFamily, 14, FontStyle.Bold);
            Font dateFont = new Font(ManageReservationGridview.Font.FontFamily, 12, FontStyle.Regular);

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

        private void label1_Click(object sender, EventArgs e)
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
    }
}
