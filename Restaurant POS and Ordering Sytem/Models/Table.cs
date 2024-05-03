using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Restaurant_POS_and_Ordering_Sytem.Models
{

    public partial class Table : Form
    {
        private string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private List<string> selectedTables = new List<string>();
        public List<string> SelectedTables { get { return selectedTables; } }
        public event EventHandler<List<string>> SelectedTablesChanged;
        public string TableName { get; private set; }
   
        public Table()
        {
            InitializeComponent();
        }
        private void Table_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open(); // Open the connection

                string qry = "SELECT tblName FROM tbl_table";

                // Use the MySqlConnection to create the MySqlCommand
                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string tableName = row["tblName"].ToString();

                        // Create a button for the table
                        Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                        b.Text = tableName;
                        b.Width = 150;
                        b.Height = 50;

                        // Check if the table is already selected in tblMain and the status is not "Check Out"
                        if (IsTableSelected(tableName))
                        {
                            b.Enabled = false;
                            b.FillColor = Color.Gray; // Change color to indicate it's disabled
                        }
                        else
                        {
                            b.FillColor = Color.FromArgb(241, 85, 126);
                            b.HoverState.FillColor = Color.FromArgb(50, 56, 89);
                            b.Click += new EventHandler(CategoryButton_Click);
                        }

                        // Add the button to the form's controls collection
                        flowLayoutPanel1.Controls.Add(b);
                    }
                }
            }
        }
        private bool IsTableSelected(string tableName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string[] tableNames = tableName.Split(',').Select(t => t.Trim()).ToArray();

                foreach (string table in tableNames)
                {
                    string query = "SELECT COUNT(*) FROM tblMain WHERE TableName LIKE @tableName";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@tableName", "%" + table + "%");

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            // Table is found in tblMain, now check its status
                            string statusQuery = "SELECT Status, Table_Status FROM tblMain WHERE TableName LIKE @tableName";

                            using (MySqlCommand statusCmd = new MySqlCommand(statusQuery, connection))
                            {
                                statusCmd.Parameters.AddWithValue("@tableName", "%" + table + "%");

                                using (MySqlDataReader reader = statusCmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string status = reader.GetString("Status");
                                        string tableStatus = reader.GetString("Table_Status");

                                        if (status == "Check Out" && tableStatus == "Ready")
                                        {
                                            reader.Close(); // Close the current reader before executing another command

                                            // Check if there are any active orders associated with the table
                                            string activeOrdersQuery = "SELECT COUNT(*) FROM tblMain " +
                                                                        "INNER JOIN tblDetails ON tblMain.MainID = tblDetails.MainID " +
                                                                        "WHERE tblMain.TableName LIKE @tableName AND tblMain.Status <> 'Check Out'";

                                            using (MySqlCommand activeOrdersCmd = new MySqlCommand(activeOrdersQuery, connection))
                                            {
                                                activeOrdersCmd.Parameters.AddWithValue("@tableName", "%" + table + "%");
                                                int activeOrdersCount = Convert.ToInt32(activeOrdersCmd.ExecuteScalar());

                                                if (activeOrdersCount > 0)
                                                {
                                                    return true; // Table is in use if there are active orders
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Table is in use if status is not "Check Out" or table status is not "Ready"
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Table is not selected if none of its parts are found in tblMain
            return false;
        }




        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button selectedButton = sender as Guna.UI2.WinForms.Guna2Button;
            string tableName = selectedButton.Text;

            if (!SelectedTables.Contains(tableName))
            {
                // Add the selected table to the list of selected tables
                SelectedTables.Add(tableName);
            }
            else
            {
                // Remove the table from the selected tables list
                SelectedTables.Remove(tableName);
            }

            // Update the appearance of the button to indicate selection
            selectedButton.FillColor = SelectedTables.Contains(tableName) ? Color.Green : Color.FromArgb(241, 85, 126);

            // Raise the event to notify the frmPos form about the updated selected tables
            SelectedTablesChanged?.Invoke(this, SelectedTables);
        }

        private void ProceedButton_Click(object sender, EventArgs e)
        {

            if (SelectedTables.Count > 0)
            {
                // Close the Table form
                this.Close();

                // Pass the selected tables back to the frmPos form
                (Application.OpenForms["frmPos"] as frmPos)?.SetSelectedTables(SelectedTables);
            }
            else
            {
                MessageBox.Show("Please select at least one table to proceed.", "No Tables Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
