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
using System.Data.Common;

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-BJO2DGU\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLKISILER",baglanti);            
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }
        
        void temizle()
        {
            txAd.Text = "";
            txsoyad.Text = "";
            msktel.Text = "";
            txmail.Text = "";
            txAd.Focus();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKISILER(AD,SOYAD,TELEFON,MAIL) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.Add("@p1", SqlDbType.VarChar).Value = txAd.Text;
            komut.Parameters.Add("@p2", SqlDbType.VarChar).Value = txsoyad.Text;
            komut.Parameters.Add("@p3", SqlDbType.VarChar).Value = msktel.Text;
            komut.Parameters.Add("@p4", SqlDbType.VarChar).Value = txmail.Text;
            komut.ExecuteNonQuery();
            baglanti.Close();            
            MessageBox.Show("Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txmail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            DialogResult cevap = MessageBox.Show("Bu Kaydı Silmek İstiyor musun", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                SqlCommand komutsil = new SqlCommand("delete from TBLKISILER where ID=" + txid.Text, baglanti);
                komutsil.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı Silindi");
                listele();
                temizle();
            }
            if(cevap == DialogResult.No)
            {
                MessageBox.Show("İşlem İptal Edildi");
                listele();
                temizle();
                
            }
            baglanti.Close();
            listele();
            temizle();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("update TBLKISILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,MAIL=@P4 where ID=@p5", baglanti);
            komut2.Parameters.AddWithValue("@p1", txAd.Text);
            komut2.Parameters.AddWithValue("@p2", txsoyad.Text);
            komut2.Parameters.AddWithValue("@p3", msktel.Text);
            komut2.Parameters.AddWithValue("@p4", txmail.Text);
            komut2.Parameters.AddWithValue("@p5", txid.Text);
            komut2.ExecuteNonQuery();
            MessageBox.Show("Kullanıcı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
