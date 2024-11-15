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
    public partial class VendorView : Form
    {
        /*
         * 
         * This class  handles the view operation on Vendor data.
         * 
         * 
         */ 
        SqlConnection con;

        public VendorView()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                Connect connectObj = new Connect();

                using (con = connectObj.connect())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT VID, VNAME, PHONE_NUMBER, ADDRESS, EMAIL FROM VENDOR WHERE VNAME = @vname", con))
                    {
                        cmd.Parameters.AddWithValue("@vname", VendorName.Text);
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows) // Check if any rows are returned
                            {
                                sdr.Read();
                                VendorID.Text = sdr["VID"].ToString();
                                VendorName2.Text = sdr["VNAME"].ToString();
                                VendorPhoneNo.Text = sdr["PHONE_NUMBER"].ToString();
                                VendorAddress.Text = sdr["ADDRESS"].ToString();
                                Email.Text = sdr["EMAIL"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Vendor not found!", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
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
            VendorName.Clear();
            VendorAddress.Clear();
            VendorPhoneNo.Clear();
            Email.Clear();
            VendorID.Clear();
            VendorName2.Clear();
        }
    }
}
