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
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowMan();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rehman\Documents\PharmacyC0b.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowMan()
        {
            Con.Open();
            String Query = "Select * from ManufacturerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ManufacturerDGC.DataSource = ds.Tables[0];

            Con.Close();

        }
        private void panel1_Paint(object sender, EventArgs e) { }
        private void SaveButton_Click(object sender, EventArgs e)

        {
            if (ManAddTb.Text == "" || ManNameTb.Text == "" || ManPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ManufacturerTbl(ManName,ManAdd,ManPhone,ManJDate) values (@MN,@MA,@MP,@MJD)", Con);
                    cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@MJD", ManJDate.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Added");
                    Con.Close();
                    ShowMan();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key = 0;

        private void ManufacturerDGC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ManNameTb.Text = ManufacturerDGC.SelectedRows[0].Cells[1].Value.ToString();
            ManAddTb.Text = ManufacturerDGC.SelectedRows[0].Cells[2].Value.ToString();
            ManPhoneTb.Text = ManufacturerDGC.SelectedRows[0].Cells[3].Value.ToString();
            ManJDate.Text = ManufacturerDGC.SelectedRows[0].Cells[4].Value.ToString();
            if (ManNameTb.Text == "")
            {
                Key = 0;
            }
            else { 
                Key = Convert.ToInt32(ManufacturerDGC.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the manufacturer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Manufacturer where ManId=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Deleted");
                    Con.Close();
                    ShowMan();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Reset()
        {
            ManNameTb.Text = "";
            ManAddTb.Text="";
            ManPhoneTb.Text = "";
            Key = 0;

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ManAddTb.Text == "" || ManNameTb.Text == "" || ManPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ManufacturerTbl set ManName=@MN,ManAdd=@MA,ManPhone=@MP,ManJDate=@MJD where ManId=@Key", Con);
                    cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@MJD", ManJDate.Text);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Updated");
                    Con.Close();
                    ShowMan();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}


