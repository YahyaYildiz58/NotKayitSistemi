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
using System.Diagnostics.Eventing.Reader;
using NotKayitSistemi.NotKayitSistemiDataSetTableAdapters;


namespace NotKayitSistemi
{
    public partial class frmogretmendetay : Form
    {
        public frmogretmendetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=YAHYA\YAHYAMSSQL;Initial Catalog=NotKayitSistemi;Integrated Security=True;TrustServerCertificate=True");
        private string connectionString;

        private void frmogretmendetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'notKayitSistemiDataSet.Tbl_Ders' table. You can move, or remove it, as needed.
            this.tbl_DersTableAdapter.Fill(this.notKayitSistemiDataSet.Tbl_Ders);

        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Ders (OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1",msknumara.Text);
            komut.Parameters.AddWithValue("@p2",tboxad.Text);
            komut.Parameters.AddWithValue("@p3",tboxsoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci sisteme eklendi.");
            this.tbl_DersTableAdapter.Fill(this.notKayitSistemiDataSet.Tbl_Ders);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            msknumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            tboxad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            tboxsoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            tboxsinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            tboxsinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            tboxsinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(tboxsinav1.Text);
            s2 = Convert.ToDouble(tboxsinav2.Text);
            s3 = Convert.ToDouble(tboxsinav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblsınavortalama.Text= ortalama.ToString();

            if (ortalama >= 50)
            {
                durum ="True";
            }
            else 
            {
                durum="False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Tbl_Ders set OGRS1=@p1,OGRS2=@p2,OGRS3=@p3,ORTALAMA=@p4,DURUM=@p5 where OGRNUMARA=@p6",baglanti);
            komut.Parameters.AddWithValue("@p1", tboxsinav1.Text);
            komut.Parameters.AddWithValue("@p2", tboxsinav2.Text);
            komut.Parameters.AddWithValue("@p3", tboxsinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(lblsınavortalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", msknumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci notları güncellendi.");
            this.tbl_DersTableAdapter.Fill(this.notKayitSistemiDataSet.Tbl_Ders);
        }
    }
}
