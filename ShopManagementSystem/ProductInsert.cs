﻿using System;
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
    public partial class ProductInsert : Form
    {
        /*
         * 
         * This class handles the insertion of product details.
         * 
         * 
         */ 
        SqlConnection con;

        public ProductInsert()
        {
            InitializeComponent();
            AutoIdGeneration();
        }

        private void AutoIdGeneration()
        {
            int Num = 1; // Default to 1 if no records exist
            Connect connectObj = new Connect();
            con = connectObj.connect();

            try
            {
                string useDbSql = "SELECT ISNULL(MAX(PID), 0) + 1 FROM PRODUCT;";
                SqlCommand cmd = new SqlCommand(useDbSql, con);

                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out Num))
                {
                    ProductID.Text = Num.ToString(); ; // Set the new customer ID in the form
                }
                else
                {
                    MessageBox.Show("Failed to generate a new Product ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        private void Submit_Click(object sender, EventArgs e)
        {
            if (ProductName.Text == "" || VendorID.Text == "" || Amount.Text == "" || ProductID.Text == "")
            {
                MessageBox.Show("Please provide all the details", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                Connect connectObj = new Connect();
                con = connectObj.connect();

                SqlCommand cmd = new SqlCommand("Insert into PRODUCT (PID,PNAME,AMOUNT,VID) values(@pid,@pname,@amount,@vid);", con);
                
                cmd.Parameters.AddWithValue("@pid", ProductID.Text);
                cmd.Parameters.AddWithValue("@pname", ProductName.Text);

                cmd.Parameters.AddWithValue("@vid", VendorID.Text);
                cmd.Parameters.AddWithValue("@amount", Amount.Text);

                int i = cmd.ExecuteNonQuery();
                //If count is equal to 1, than show frmMain form
                if (i != 0)
                {
                    MessageBox.Show("Product Insertion Successful!", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Product Insertion Failed", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                con.Close();

                ProductName.Clear();
                VendorID.Clear();
                Amount.Clear();
                ProductID.Clear();
                AutoIdGeneration();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(con != null)
                {
                    con.Close();
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ProductName.Clear();
            VendorID.Clear();
            Amount.Clear();
            ProductID.Clear();
            AutoIdGeneration();
        }

        private void ProductInsert_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
