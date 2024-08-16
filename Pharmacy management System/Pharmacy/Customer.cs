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
using System.Diagnostics;

namespace Pharmacy
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            ShowCust();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rehman\Documents\PharmacyC0b.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCust()
        {
            Con.Open();
            String Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];

            Con.Close();

        }
        private void Reset()
        {
            CusNameTb.Text = "";
            CusPhoneTb.Text = "";
            CusGenCb.SelectedIndex = 0;
            CusAddTb.Text = "";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CusNameTb.Text == "" || CusPhoneTb.Text == "" || CusAddTb.Text == "" || CusGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CustName,CustPhone,CustAdd,CustDOB,CustGen) values (@CN,@CP,@CA,@CD,@CG)", Con);
                    cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CusPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CusAddTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CusGenCb.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                    Con.Close();
                    ShowCust();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        int Key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CusNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CusPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CusAddTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CusDOB.Text = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            CusGenCb.SelectedItem = CustomerDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (CusNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CusNameTb.Text == "" || CusPhoneTb.Text == "" || CusAddTb.Text == "" || CusGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl setCustName=@CN,CustPhone=@CP,CustAdd=@CA,CustDOB=@CD,CustGen=@CG where CustNum=@CKey ", Con);
                    cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CusPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CusAddTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CusGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                    Con.Close();
                    ShowCust();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CustomerTbl where CustNum=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    Con.Close();
                    ShowCust();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}





