using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pharmacy
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rehman\Documents\PharmacyC0b.mdf;Integrated Security=True;Connect Timeout=30");
        public static string User;
        private void Loginbutton_Click(object sender, EventArgs e)
        {
            if(UNameTb.Text==""||PasswordTb.Text=="")
            {
                MessageBox.Show("Ener both UserName and Password");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda=new SqlDataAdapter("Select count(*) from SellerTbl where SName='"+UNameTb.Text+"' and SPass='"+PasswordTb.Text+"'",Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    User = UNameTb.Text;
                    Sellings obj = new Sellings();
                    obj.Show();
                    this.Hide();
                      Con.Close();
                }
                else
                {
                    MessageBox.Show("Wrong Username Or Password");
                }
                Con.Close();

            }
                  
        }
    }
}
