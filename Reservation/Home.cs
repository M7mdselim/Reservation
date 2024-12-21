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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            PopulateMenuItems();
            reservationdateandtime.Value = DateTime.Now;
            Resturantnamecombo.Enabled = false; // Disable combobox until a date is selected
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
            

           

        }

        private void addEmployee_btn_Click(object sender, EventArgs e)
        {
            

           

        }

        private void salary_btn_Click(object sender, EventArgs e)
        {
           

            

        }

        private void Customernametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Phonenumbertxt_TextChanged(object sender, EventArgs e)
        {

        }
        private void LockCustomerFields()
        {
            Customernametxt.Enabled = false;
            Phonenumbertxt.Enabled = false;
        }
        private void addnewcustomerbtn_Click(object sender, EventArgs e)
        {
            string customerName = Customernametxt.Text.Trim();
            string phoneNumber = Phonenumbertxt.Text.Trim();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please fill in both Customer Name and Phone Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Customer (Name, PhoneNumber) VALUES (@Name, @PhoneNumber)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", customerName);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LockCustomerFields();    //  Make them readonly
                        }
                        else
                        {
                            MessageBox.Show("Failed to add customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reservationnumberlabel_Click(object sender, EventArgs e)
        {

        }

        private void reservationdateandtime_ValueChanged(object sender, EventArgs e)
        {
            LoadAvailableRestaurants();
        }

        private void Resturantnamecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Resturantnamecombo.SelectedIndex != -1)
            {
                // Get the selected item
                ComboboxItem selectedItem = (ComboboxItem)Resturantnamecombo.SelectedItem;

                // Get the RestaurantID from the selected item
                int selectedRestaurantId = selectedItem.Value;

                // Print the Restaurant ID (for debugging purposes)
                Console.WriteLine("Selected RestaurantID: " + selectedRestaurantId);

                // Do whatever you need with the selectedRestaurantId (e.g., store it, make queries, etc.)
            }
        }


        private void capacitytxt_TextChanged(object sender, EventArgs e)
        {

        }



        private void LockReservationFields()
        {
            reservationdateandtime.Enabled = false;
            Resturantnamecombo.Enabled = false;
            capacitytxt.Enabled = false;
        }

        private void Reservationdatabtn_Click(object sender, EventArgs e)
        { 
            // Validate input fields
            if (string.IsNullOrWhiteSpace(capacitytxt.Text) || Resturantnamecombo.SelectedIndex == -1 || reservationdateandtime.Value == null
                || string.IsNullOrWhiteSpace(Customernametxt.Text) || string.IsNullOrWhiteSpace(Phonenumbertxt.Text))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate requested capacity
            if (!int.TryParse(capacitytxt.Text, out int requestedCapacity) || requestedCapacity <= 0)
            {
                MessageBox.Show("Capacity must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedRestaurantName = Resturantnamecombo.SelectedItem.ToString();
            string customerName = Customernametxt.Text;
            string phoneNumber = Phonenumbertxt.Text;
            DateTime selectedDate = reservationdateandtime.Value.Date;

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();

                    // Get RestaurantID
                    string getRestaurantIdQuery = "SELECT RestaurantID FROM Restaurant WHERE Name = @RestaurantName";
                    int restaurantId = 0;
                    using (SqlCommand cmd = new SqlCommand(getRestaurantIdQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@RestaurantName", selectedRestaurantName);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            restaurantId = Convert.ToInt32(result);
                        else
                        {
                            MessageBox.Show("Restaurant not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Get CustomerID
                    string getCustomerIdQuery = "SELECT CustomerID FROM Customer WHERE Name = @CustomerName AND PhoneNumber = @PhoneNumber";
                    int customerId = 0;
                    using (SqlCommand cmd = new SqlCommand(getCustomerIdQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            customerId = Convert.ToInt32(result);
                        else
                        {
                            MessageBox.Show("Customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Check remaining capacity
                    string checkCapacityQuery = @"
            SELECT RemainingCapacity 
            FROM RestaurantDailyCapacity 
            WHERE RestaurantID = @RestaurantID AND Date = @Date";
                    int remainingCapacity = 0;
                    using (SqlCommand cmd = new SqlCommand(checkCapacityQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@RestaurantID", restaurantId);
                        cmd.Parameters.AddWithValue("@Date", selectedDate);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            remainingCapacity = Convert.ToInt32(result);
                        else
                        {
                            MessageBox.Show("No capacity record found for the selected restaurant and date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (remainingCapacity < requestedCapacity)
                    {
                        MessageBox.Show($"Insufficient capacity. Only {remainingCapacity} seats are available.", "Capacity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert reservation and get the ReservationID
                    string insertReservationQuery = @"
            INSERT INTO Reservations (CustomerID, RestaurantID, ReservationDate, [NumberOfGuests])
            VALUES (@CustomerID, @RestaurantID, @Date, @Capacity);
            SELECT SCOPE_IDENTITY();";
                    int reservationId = 0;
                    using (SqlCommand cmd = new SqlCommand(insertReservationQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@RestaurantID", restaurantId);
                        cmd.Parameters.AddWithValue("@Date", selectedDate);
                        cmd.Parameters.AddWithValue("@Capacity", requestedCapacity);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            reservationId = Convert.ToInt32(result);
                            MessageBox.Show("Reservation successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Update the reservationnumberlabel
                            reservationnumberlabel.Text = $"Reservation number is = {reservationId}";
                            reservationnumberlabel.ForeColor = Color.Red;
                            reservationidtxt.Text = reservationId.ToString(); // Update TextBox


                            LockReservationFields();
                        }
                        else
                        {
                            MessageBox.Show("Failed to save the reservation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadAvailableRestaurants()
        {
            // Clear existing items
            Resturantnamecombo.Items.Clear();

            // Load data from the database
            DateTime selectedDate = reservationdateandtime.Value.Date;

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT r.RestaurantID, r.Name
                FROM Restaurant r
                INNER JOIN RestaurantDailyCapacity rd ON r.RestaurantID = rd.RestaurantID
                WHERE rd.Date = @SelectedDate AND rd.RemainingCapacity > 10";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedDate", selectedDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through all available restaurants and add to ComboBox
                            while (reader.Read())
                            {
                                ComboboxItem item = new ComboboxItem
                                {
                                    Text = reader["Name"].ToString(),  // Restaurant Name
                                    Value = Convert.ToInt32(reader["RestaurantID"])  // Restaurant ID
                                };
                                Resturantnamecombo.Items.Add(item);  // Add the item to ComboBox
                            }
                        }
                    }
                }

                // Enable the ComboBox if we have any restaurants
                Resturantnamecombo.Enabled = Resturantnamecombo.Items.Count > 0;

                if (Resturantnamecombo.Items.Count == 0)
                {
                    MessageBox.Show("No restaurants with sufficient capacity available for the selected date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int GetSelectedCustomerId()
        {
            // Logic to fetch the selected customer ID
            // This can be implemented by providing a way to select a customer in the form
            // Placeholder logic below:
            return 1; // Replace with actual customer selection logic
        }

        private void ClearReservationFields()
        {
            reservationdateandtime.Value = DateTime.Now;
            Resturantnamecombo.SelectedIndex = -1;
            capacitytxt.Clear();
        }






        private void PopulateMenuItems()
        {
            try
            {
                // Create a connection to the database
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();

                    // SQL query to get all items from the Menu table
                    string query = "SELECT ItemName FROM Menu";  // Assuming ItemName is the column containing menu items

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and read the data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Clear any existing items in the ComboBox
                            itemmenucombo.Items.Clear();

                            // Add items to ComboBox from the database
                            while (reader.Read())
                            {
                                string itemName = reader.GetString(0); // Assuming the first column is ItemName
                                itemmenucombo.Items.Add(itemName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading menu items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void itemmenucombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle selection change if necessary
            string selectedItem = itemmenucombo.SelectedItem.ToString();
            
        }

       

        
        // Define a variable to store the total price
        private decimal totalPrice = 0;

        private void additembtn_Click(object sender, EventArgs e)
        {
            // Get the selected item from ComboBox
            string selectedItem = itemmenucombo.SelectedItem.ToString();

            // Get the quantity from the TextBox
            if (int.TryParse(quantitytxt.Text, out int quantity) && quantity > 0)
            {
                try
                {
                    // Fetch the price of the selected item from the Menu table
                    decimal itemPrice = GetItemPrice(selectedItem);

                    // Calculate the total price for this item
                    decimal itemTotalPrice = itemPrice * quantity;
                    totalPrice += itemTotalPrice;

                    // Add the item to the panel
                    AddItemToPanel(selectedItem, quantity, itemPrice, itemTotalPrice);

                    // Update the total price label
                    totalPriceLabel.Text = $"Total Price: {totalPrice:C}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Method to fetch the price of the selected item from the Menu table
        private decimal GetItemPrice(string itemName)
        {
            decimal itemPrice = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();

                    string query = "SELECT ItemPrice FROM Menu WHERE ItemName = @ItemName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemName", itemName);

                        // Execute the query and get the price
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            itemPrice = Convert.ToDecimal(result);
                        }
                        else
                        {
                            throw new Exception("Item not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving item price: " + ex.Message);
            }

            return itemPrice;
        }

        // Method to add the selected item to the panel
        private Label selectedItemLabel = null;  // To store the selected row

        private void AddItemToPanel(string itemName, int quantity, decimal itemPrice, decimal itemTotalPrice)
        {
            // Constants for padding, column widths, and row height
            int padding = 5;
            int columnWidthItemName = 100;  // Width for the item name column
            int columnWidthQuantity = 70;   // Width for the quantity column
            int columnWidthPrice = 80;      // Width for the price column
            int rowHeight = 40;             // Fixed height for each row

            // Add headers if this is the first row
            if (menuitemspanel.Controls.Count == 0)
            {
                AddHeaders(columnWidthItemName, columnWidthQuantity, columnWidthPrice, rowHeight, padding);
            }

            // Calculate the Y position for the new row
            int lastRowYPosition = menuitemspanel.Controls.Count > 3
                ? menuitemspanel.Controls[menuitemspanel.Controls.Count - 3].Location.Y + rowHeight
                : rowHeight;

            // Create and configure the labels for the new row
            Label itemNameLabel = new Label
            {
                Text = itemName,
                Width = columnWidthItemName,
                Height = rowHeight,
                Location = new Point(padding, lastRowYPosition),
                Cursor = Cursors.Hand // Indicate that the label is clickable
            };
            itemNameLabel.Click += new EventHandler(RowLabel_Click);  // Add click event for row selection

            Label quantityLabel = new Label
            {
                Text = quantity.ToString(),
                Width = columnWidthQuantity,
                Height = rowHeight,
                Location = new Point(itemNameLabel.Location.X + columnWidthItemName + padding, lastRowYPosition)
            };

            Label priceLabel = new Label
            {
                Text = $"{"   "+itemTotalPrice}",
                Width = columnWidthPrice,
                Height = rowHeight,
                Location = new Point(quantityLabel.Location.X + columnWidthQuantity + padding, lastRowYPosition),
                Tag = itemTotalPrice // Store raw price for calculations
            };

            // Add the labels to the panel
            menuitemspanel.Controls.Add(itemNameLabel);
            menuitemspanel.Controls.Add(quantityLabel);
            menuitemspanel.Controls.Add(priceLabel);

            // Adjust the scrollable area of the panel
            int totalHeight = ((menuitemspanel.Controls.Count / 3) + 1) * rowHeight;
            menuitemspanel.AutoScrollMinSize = new Size(menuitemspanel.Width, totalHeight);
            menuitemspanel.AutoScroll = true;
        }

        /// <summary>
        /// Adds static headers to the panel if they are not already present.
        /// </summary>
        private void AddHeaders(int columnWidthItemName, int columnWidthQuantity, int columnWidthPrice, int rowHeight, int padding)
        {
            Label itemHeaderLabel = new Label
            {
                Text = "ITEM",
                Width = columnWidthItemName,
                Height = rowHeight,
                Location = new Point(padding, 0),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };

            Label quantityHeaderLabel = new Label
            {
                Text = "PIECE",
                Width = columnWidthQuantity,
                Height = rowHeight,
                Location = new Point(itemHeaderLabel.Location.X + columnWidthItemName + padding, 0),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };

            Label priceHeaderLabel = new Label
            {
                Text = "PRICE",
                Width = columnWidthPrice,
                Height = rowHeight,
                Location = new Point(quantityHeaderLabel.Location.X + columnWidthQuantity + padding, 0),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };

            menuitemspanel.Controls.Add(itemHeaderLabel);
            menuitemspanel.Controls.Add(quantityHeaderLabel);
            menuitemspanel.Controls.Add(priceHeaderLabel);
        }

        // Event handler when a row is clicked
        private void RowLabel_Click(object sender, EventArgs e)
        {
            // Deselect previously selected row if any
            if (selectedItemLabel != null)
            {
                selectedItemLabel.BackColor = SystemColors.Control; // Reset background color
            }

            // Set the new selected row
            selectedItemLabel = (Label)sender;
            selectedItemLabel.BackColor = Color.LightBlue; // Highlight the selected row
        }

        // Delete button click handler


        // Adjust row positions if needed after deletion
        private void AdjustRowPositions()
        {
            int rowHeight = 40; // Height of each row
            int padding = 10; // Padding between columns
            int columnWidthItemName = 100;
            int columnWidthQuantity = 70;
            int columnWidthPrice = 80;

            // Start positioning after headers
            int yPosition = rowHeight;

            for (int i = 3; i < menuitemspanel.Controls.Count; i += 3)
            {
                var itemNameLabel = (Label)menuitemspanel.Controls[i];
                itemNameLabel.Location = new Point(padding, yPosition);

                var quantityLabel = (Label)menuitemspanel.Controls[i + 1];
                quantityLabel.Location = new Point(itemNameLabel.Location.X + columnWidthItemName + padding, yPosition);

                var priceLabel = (Label)menuitemspanel.Controls[i + 2];
                priceLabel.Location = new Point(quantityLabel.Location.X + columnWidthQuantity + padding, yPosition);

                yPosition += rowHeight;
            }

            // Update the minimum scrollable area
            int totalHeight = (menuitemspanel.Controls.Count / 3) * rowHeight + rowHeight;
            menuitemspanel.AutoScrollMinSize = new Size(menuitemspanel.Width, totalHeight);
        }

        private void UpdateTotalPrice(decimal change)
        {
            totalPrice += change;
            totalPriceLabel.Text = $"Total Price: {totalPrice:C}";
        }




        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (selectedItemLabel != null)
            {
                int selectedIndex = menuitemspanel.Controls.IndexOf(selectedItemLabel);

                if (selectedIndex < 3 || selectedIndex + 2 >= menuitemspanel.Controls.Count)
                {
                    MessageBox.Show("Invalid selection. Please select a valid row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the labels for the selected row
                var itemNameLabel = selectedItemLabel;
                var quantityLabel = (Label)menuitemspanel.Controls[selectedIndex + 1];
                var priceLabel = (Label)menuitemspanel.Controls[selectedIndex + 2];

                // Retrieve the raw price from the Tag property     

                if (priceLabel.Tag is decimal itemTotalPrice)
                {
                    totalPrice -= itemTotalPrice;
                    totalPrice = Math.Max(totalPrice, 0); // Ensure it doesn't go negative
                    totalPriceLabel.Text = $"Total Price: {totalPrice:C}";
                }
                else
                {
                    MessageBox.Show("Price information is missing or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Remove labels from the panel
                menuitemspanel.Controls.Remove(itemNameLabel);
                menuitemspanel.Controls.Remove(quantityLabel);
                menuitemspanel.Controls.Remove(priceLabel);

                // Deselect the row
                selectedItemLabel = null;

                // Adjust positions of remaining rows
                AdjustRowPositions();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }





        private void printnpaybtn_Click(object sender, EventArgs e)
        {
            // Logic for printing or paying (this can be implemented later)
            MessageBox.Show("Print and Pay functionality goes here.", "Print and Pay", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void menuitemspanel_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
    



// THATS IT FOR THIS VIDEO, THANKS FOR WATCHING!
// SUBSCRIBE FOR MORE C# PROJECT TUTORIALS
// THANKS : ) 