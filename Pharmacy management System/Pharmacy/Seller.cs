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
    public partial class Seller : Form
    {
        public Seller()
        {
            InitializeComponent();
            ShowSeller();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rehman\Documents\PharmacyC0b.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowSeller()
        {
            Con.Open();
            String Query = "Select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

            private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            SNameTb.Text = "";
            SPhoneTb.Text = "";
            SAddTb.Text = "";
            SPasswordTb.Text = "";
            SGenCb.SelectedIndex = 0;
            Key = 0;

        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SPasswordTb.Text == "" || SAddTb.Text == "" || SGenCb.SelectedIndex == -1 )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SellerTbl(SName,SDOB,SPhone,SAdd,SGen,SPass) values (@SN,@SD,@SP,@SA,@SG,@SPA)", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SPasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Added");
                    Con.Close();
                    ShowSeller();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        int Key=0;
        private void SellerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SNameTb.Text = SellerDGV.SelectedRows[0].Cells[1].Value.ToString();
            SDOB.Text = SellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            SPhoneTb.Text = SellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            SAddTb.Text = SellerDGV.SelectedRows[0].Cells[4].Value.ToString();
            SGenCb.SelectedValue = SellerDGV.SelectedRows[0].Cells[5].Value.ToString();
            SPasswordTb.Text = SellerDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (SNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(SellerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Seller");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SellerTbl where SNum=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Deleted");
                    Con.Close();
                    ShowSeller();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Editbutton_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SPasswordTb.Text == "" || SAddTb.Text == "" || SGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update SellerTbl Set SName=@SN,SDOB=@SD,SPhone=@SP,SAdd=@SA,SGen=@SG,SPass=@SPA where SNum=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@SKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated");
                    Con.Close();
                    ShowSeller();
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
