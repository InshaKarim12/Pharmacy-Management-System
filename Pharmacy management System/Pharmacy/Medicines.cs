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
    public partial class Medicines : Form
    {
        public Medicines()
        {
            InitializeComponent();
            ShowMed();
            GetManufacturer();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rehman\Documents\PharmacyC0b.mdf;Integrated Security=True;Connect Timeout=30");
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

        private void Medicines_Load(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            MedManTb.Text = "";
            MedManTb.Text = "";
            MedPriceTb.Text = "";
            MedQtyTb.Text = "";
            MedTypeCb.SelectedIndex = 0;
            Key = 0;


        }
        private void GetManufacturer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select ManId from ManufacturerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ManId", typeof(int));
            dt.Load(Rdr);
            MedManCb.ValueMember = "ManId";
            MedManCb.DataSource = dt;
            Con.Close();
        }
        private void GetManName()
        {
            Con.Open();
            string Query = "Select * from ManufacturerTbl where ManId=' " + MedManCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                MedManTb.Text = dr["ManName"].ToString();
            }
            Con.Close();
        }
        private void Savebutton_Click(object sender, EventArgs e)
        {
            if (MedNameTb.Text == "" || MedPriceTb.Text == "" || MedQtyTb.Text == "" || MedPriceTb.Text == "" || MedTypeCb.SelectedIndex == -1 || MedManTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MedicineTbl(MedName,MedType,MedQty,MedPrice,MedManId,MedManfact) values (@MN,@MT,@MQ,@MP,@MMI,@MM)", Con);
                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MM", MedManTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Added");
                    Con.Close();
                    ShowMed();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void MedManCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManName();
        }
        int Key = 0;

        private void MedicineDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedNameTb.Text = MedicineDGV.SelectedRows[0].Cells[1].Value.ToString();
            MedTypeCb.Text = MedicineDGV.SelectedRows[0].Cells[2].Value.ToString();
            MedQtyTb.Text = MedicineDGV.SelectedRows[0].Cells[3].Value.ToString();
            MedPriceTb.Text = MedicineDGV.SelectedRows[0].Cells[4].Value.ToString();
            MedManCb.SelectedValue = MedicineDGV.SelectedRows[0].Cells[5].Value.ToString();
            MedManTb.Text = MedicineDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else { 
                Key = Convert.ToInt32(MedicineDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Medicine");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from MedicineTbl where MedNum=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Deleted");
                    Con.Close();
                    ShowMed();
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
            if (MedNameTb.Text == "" || MedPriceTb.Text == "" || MedQtyTb.Text == "" || MedPriceTb.Text == "" || MedTypeCb.SelectedIndex == -1 || MedManTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedName=@MN,MedType=@MT,MedQty=@MQ,MedPrice=@MP,MedManId=@MMI,MedManfact=@MM where MedNum=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Updated");
                    Con.Close();
                    ShowMed();
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
