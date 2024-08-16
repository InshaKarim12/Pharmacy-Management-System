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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountMed();
            CountSellers();
            CountCust();
            SumAmt();
            GetSeller();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rehman\Documents\PharmacyC0b.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountMed()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from  MedicineTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MedNum.Text=dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountSellers()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from  SellerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellerLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCust()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from  CustomerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmt()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(BAmount) from  BillTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
           SellAmtLbl.Text = "Rs"+dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmtBySeller()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(BAmount) from  BillTbl where SName='"+SellerCb.SelectedValue.ToString()+"'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellsBySellerLbl.Text = "Rs" + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void GetSeller()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select SName from SellerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName", typeof(string));
            dt.Load(Rdr);
            SellerCb.ValueMember = "SName";
            SellerCb.DataSource = dt;
            Con.Close();

        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void MedTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SellerCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SumAmtBySeller();
        }

        private void BestSellelLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
