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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string tableName = lbltable.Text;

                    if (string.IsNullOrEmpty(tableName))
                    {
                        MessageBox.Show("Please Input a Table name.");
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
                            DialogResult result = MessageBox.Show("Successfully added");
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

                            DialogResult result = MessageBox.Show("Successfully updated");
                            OnTableUpdated();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        protected virtual void OnTableUpdated()
        {
            TableUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
