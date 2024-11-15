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

namespace ShopManagementSystem
{
    public partial class StockView : Form
    {
        /*
         * 
         * This class handles view operation on Stock data.
         * 
         * 
         */ 
        SqlConnection con;

        public StockView()
        {
            InitializeComponent();
        }

        private void search_Click(object sender, EventArgs e)
        {
            try
            {
                Connect connectObj = new Connect();

                using (con = connectObj.connect())
                {
                    // Search for the product by its ID
                    using (SqlCommand cmd = new SqlCommand("SELECT PNAME FROM PRODUCT WHERE PID = @pid", con))
                    {
                        cmd.Parameters.AddWithValue("@pid", ProductID.Text);
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows) // Check if any rows were returned
                            {
                                sdr.Read();
                                Productname.Text = sdr["PNAME"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Product not found!", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if no product is found
                            }
                        }
                    }

                    // Search for the stock quantity by product ID
                    using (SqlCommand cmd = new SqlCommand("SELECT QUANTITY FROM STOCK WHERE PID = @pid", con))
                    {
                        cmd.Parameters.AddWithValue("@pid", ProductID.Text);
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows) // Check if any rows were returned
                            {
                                sdr.Read();
                                Quantity.Text = sdr["QUANTITY"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Stock information not found for the product.", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if no stock information is found
                            }
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            ProductID.Clear();
            Productname.Clear();
            Quantity.Clear();
        }

        private void StockView_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
