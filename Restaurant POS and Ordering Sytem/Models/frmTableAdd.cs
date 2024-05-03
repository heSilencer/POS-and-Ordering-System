using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmTableAdd : Form
    {
        public event EventHandler TableUpdated;
        private int tableId;
        public frmTableAdd()
        {
            InitializeComponent();
        }
        public void SetTableInfo(int id, string tableName)
        {
            tableId = id;
            lbltable.Text = tableName;
        }

        private void frmTableAdd_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool CheckIftableExists(string categoryName, MySqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM tbl_table WHERE tblName = @tblName";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@tblName", categoryName);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string tableName = lbltable.Text.Trim();

                    if (string.IsNullOrEmpty(tableName))
                    {
                        guna2MessageDialog1.Show("Please Input a Table name.");
                        return;
                    }
                    bool tableExists = CheckIftableExists(tableName, connection);
                    if (tableExists)
                    {
                        guna2MessageDialog1.Show("This Table already exists.");
                        return;
                    }

                    if (tableId == 0)
                    {
                        // Insert a new table
                        string insertQuery = "INSERT INTO tbl_Table (tblName) VALUES (@tblName)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@tblName", tableName);
                            insertCommand.ExecuteNonQuery();

                            // Clear the textbox after adding a new table
                            lbltable.Text = "";

                            // Close the current form
                            DialogResult result = guna2MessageDialog1.Show("Successfully added");
                            OnTableUpdated();
                        }
                    }
                    else
                    {
                        // Update an existing table
                        string updateQuery = "UPDATE tbl_Table SET tblName = @tblName WHERE tblID = @tblID";
                        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@tblID", tableId);
                            updateCommand.Parameters.AddWithValue("@tblName", tableName);
                            updateCommand.ExecuteNonQuery();

                            // Clear the textbox after updating an existing table
                            lbltable.Text = "";

                            DialogResult result = guna2MessageDialog1.Show("Successfully updated");
                            OnTableUpdated();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error: " + ex.Message);
                }
            }
        }

        protected virtual void OnTableUpdated()
        {
            TableUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void lbltable_TextChanged(object sender, EventArgs e)
        {
            if (lbltable == null || string.IsNullOrEmpty(lbltable.Text))
            {
                return;
            }

            // Capitalize the first character of the text
            lbltable.Text = char.ToUpper(lbltable.Text[0]) + lbltable.Text.Substring(1);

            // Set the caret position to the end of the text
            lbltable.SelectionStart = lbltable.Text.Length;
        }
    }
}
