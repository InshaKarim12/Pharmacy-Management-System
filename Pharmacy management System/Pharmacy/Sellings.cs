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
    public partial class Sellings : Form
    {
        public Sellings()
        {
            InitializeComponent();
            ShowMed();
            ShowBill();
            SnameLbl.Text = Logins.User;
            GetCustomer();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rehman\Documents\PharmacyC0b.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustNum from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(Rdr);
            CustIdCb.ValueMember = "CustNum";
            CustIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetCustName()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl where CustNum=' " + CustIdCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }
        private void UpdateQty()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(MedQtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedQty=@MQ where MedNum=@MKey", Con);

                cmd.Parameters.AddWithValue("@MQ", NewQty);

                cmd.Parameters.AddWithValue("@MKey", Key);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Medicine Updated");
                Con.Close();
                ShowMed();
                // Reset();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
    private void InsertBill()
    {
            if (CustNameTb.Text == "")
            {

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(SName,CustNum,CustName,BDate,BAmount) values (@UN,@CN,@CNa,@BD,@BA)", Con);
                    cmd.Parameters.AddWithValue("@UN", SnameLbl.Text);
                    cmd.Parameters.AddWithValue("@CN", CustIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CNa", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@BA", GrdTotal);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved");
                    Con.Close();
                    ShowBill();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        
    }
        private void ShowBill()
        {
            Con.Open();
            String Query = "Select * from BillTbl where Sname = '"+SnameLbl.Text+"'" ;
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionDGV.DataSource = ds.Tables[0];

            Con.Close();

        }



        private void ShowMed()
    {
        Con.Open();
        String Query = "Select * from MedicineTbl";
        SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
        SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
        var ds = new DataSet();
        sda.Fill(ds);
        MedicineDGV.DataSource = ds.Tables[0];

        Con.Close();

    }
    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void label6_Click(object sender, EventArgs e)
    {

    }
    int n = 0, GrdTotal = 0;
    private void Savebutton_Click(object sender, EventArgs e)
    {
        if (MedQtyTb.Text == "" || Convert.ToInt32(MedQtyTb.Text) > Stock)
        {
            MessageBox.Show("Enter correct Quantity");
        }
        else
        {
            int total = Convert.ToInt32(MedQtyTb.Text) * Convert.ToInt32(MedPriceTb.Text);
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(BillDGV);
            newRow.Cells[0].Value = n + 1;
            newRow.Cells[1].Value = MedNameTb.Text;
            newRow.Cells[2].Value = MedQtyTb.Text;
            newRow.Cells[3].Value = MedPriceTb.Text;
            newRow.Cells[4].Value = total;
            BillDGV.Rows.Add(newRow);
            GrdTotal = GrdTotal + total;
            TotalLbl.Text = "Rs" + GrdTotal;
            n++;
            UpdateQty();



        }

    }


        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }
    int Key = 0, Stock;

        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void PrinttButton_Click(object sender, EventArgs e)
        {
            InsertBill();
        }

        private void MedicinesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            MedNameTb.Text = MedicineDGV.SelectedRows[0].Cells[1].Value.ToString();
            //MedTypeCb.Text = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();
            Stock = Convert.ToInt32(MedicineDGV.SelectedRows[0].Cells[3].Value.ToString());
            MedPriceTb.Text = MedicineDGV.SelectedRows[0].Cells[4].Value.ToString();
            //MedManCb.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
            //MedManTb.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MedicineDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
