using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Market_10_933
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //Marka tanımlama
        public static string[] marka = { "PINAR", "PİLİÇ", "NAMET", "FİLİZ", "İÇİM", "COCO-COLA", "FANTA", "SPRİTE", "ERİKLİ", "SUPERFRESH", "KNOR", "TORKU", "ETİ", "ELİDOR", "HACI ŞAKİR", "JACK DANİELS", "BANVİT", "KENT", "BALPARMAK", "AVON", "ŞAHİN", "DANONE", "SÜTAŞ", "YUDUM", "ORKİDE", "AYÇİCEK", "LE-COLA", "PEPSİ", "KENT" };
       //Tür tanımlama
        public static string[] tür = { "ET URUNLERI", "MEYVE-SEBZE", "SUT URUNLERI", "İCİCEKLER", "KONSERVELER", "BAKLİYAT", "BAKIM URUNLERI", "KURUYEMİS" };
        //Veritabanı bağlantılarını tanımlama kodu
        OleDbDataAdapter Adaptor = new OleDbDataAdapter();
        DataSet DS = new DataSet();
        OleDbDataReader DR;
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" + Application.StartupPath + "/Market.accdb");
        OleDbCommand komut = new OleDbCommand();
        public int kn;
        //Listeleme olusturma ve ürünleri gösterme metodu olusturma
        public void listele()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            Adaptor.SelectCommand = new OleDbCommand("SELECT * FROM URUNLER", conn);
            Adaptor.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView1.Update();
            Adaptor.Dispose();
            conn.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Textbox kullanım kapatma
            txt_id.Enabled = false;
            txt_topfyt.Enabled = false;
            //Veritabanı bağlantı açılımı
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            //Ürünler tablosunu textboxlara okuyup yazdırma
            komut = new OleDbCommand("SELECT * FROM URUNLER", conn);
            DR = komut.ExecuteReader();
            DR.Read();
            if (DR.HasRows)
            {
                txt_id.Text = DR["urun_id"].ToString();
                txt_ad.Text = DR["urun_adi"].ToString();
                txt_mrk.Text = DR["urun_marka"].ToString();
                txt_tür.Text = DR["urun_turu"].ToString();
                txt_fyt.Text = DR["urun_fiyat"].ToString();
                txt_adet.Text = DR["urun_adet"].ToString();
                txt_topfyt.Text = DR["urun_topfiyat"].ToString();
            }

            conn.Close();
            listele();
        }

        private void btn_yeniurun_Click(object sender, EventArgs e)
        {
            //Yeni giriş için textboxları sıfırlama
            txt_fyt.Clear();
            txt_adet.Clear();
            txt_id.Clear();
            txt_ad.Clear();
            txt_topfyt.Clear();
            //Gereken buton ve textboxların görünürlüğünü kapatma
            btn_yeniurun.Enabled = false;
            txt_mrk.Visible = false;
            txt_tür.Visible = false;

            cmb_tür.Items.Clear();
            cmb_mrk.Items.Clear();
            for (int i = 0; i <= marka.Length - 1; i++)
                cmb_tür.Items.Add(marka[i]);
            for (int i = 0; i <= tür.Length - 1; i++)
                cmb_mrk.Items.Add(tür[i]);
        }

        private void btn_kydt_Click(object sender, EventArgs e)
        {
            //Kayıt butonu için bağlantı olusumu
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            //Ürünler tablosuna kayıt ekleme
            komut = new OleDbCommand("INSERT INTO URUNLER(urun_adi,urun_fiyat,urun_marka,urun_turu,urun_adet,urun_topfiyat) VALUES('"
                + txt_ad.Text + "',"
                + Convert.ToInt32(txt_fyt.Text) + ",'"
                + cmb_tür.Items[cmb_tür.SelectedIndex].ToString() + "','"
                + cmb_mrk.Items[cmb_mrk.SelectedIndex].ToString() + "',"
                + Convert.ToInt32(txt_adet.Text) + ","
                + Convert.ToInt32(txt_topfyt.Text) + ")", conn);

            komut.ExecuteNonQuery();
            conn.Close();
            dataGridView1.Columns.Clear();
            DS.Tables.Clear();
            listele();
        }

        private void btn_gnc_Click(object sender, EventArgs e)
        {
            //Güncelleme buton olusumu
            if (btn_gnc.Text == "")
            {
                btn_gnc.Text = "Kaydet";
                txt_tür.Visible = false;
                txt_mrk.Visible = false;
               
                for (int i = 0; i <= marka.Length - 1; i++)
                    cmb_mrk.Items.Add(marka[i]);
                for (int i = 0; i <= tür.Length - 1; i++)
                    cmb_tür.Items.Add(tür[i]);

                cmb_mrk.SelectedText = dataGridView1.Rows[Convert.ToInt32(dataGridView1.CurrentRow.Index)].Cells[2].Value.ToString();
                cmb_tür.SelectedText = dataGridView1.Rows[Convert.ToInt32(dataGridView1.CurrentRow.Index)].Cells[3].Value.ToString();
            }
                //Güncellemeyi tabloda üzerine kaydetme
            else
            {
                btn_gnc.Text = "";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                komut = new OleDbCommand("UPDATE URUNLER SET urun_ad='"
                    + txt_ad.Text +
                    ",urun_fyt=" + Convert.ToInt32(txt_fyt) +
                    "',urun_tür='" + cmb_mrk.Text +
                    "',urun_mrk='" + cmb_tür.Text +
                    ",urun_adet=" + Convert.ToInt32(txt_adet.Text) +
                    ",urun_topfiyat" + Convert.ToInt32(txt_topfyt.Text) + 
                    "' WHERE urun_id=" + Convert.ToInt32(txt_id.Text)
                    , conn);
                komut.ExecuteNonQuery();
                txt_tür.Visible = true;
                txt_mrk.Visible = true;
                txt_tür.Text = cmb_tür.Text;
                txt_mrk.Text = cmb_mrk.Text;
                conn.Close();
                dataGridView1.Columns.Clear();
                DS.Tables.Clear();
                listele();

            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            //Silme butonu için bağlantı olusumu
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            //Tablodan idye göre silme için kullanılan kod satırı ve diğer verileri temizleme
            komut = new OleDbCommand("DELETE FROM URUNLER WHERE urun_id=" + Convert.ToInt32(txt_id.Text), conn);
            komut.ExecuteNonQuery();
            dataGridView1.Columns.Clear();
            DS.Tables.Clear();
            listele();
            txt_id.Clear();
            txt_ad.Clear();
            txt_mrk.Clear();
            txt_tür.Clear();
            txt_fyt.Clear();
            txt_adet.Clear();
            txt_topfyt.Clear();
            cmb_mrk.Items.Clear();
            cmb_tür.Items.Clear();
        }

        private void btn_cks_Click(object sender, EventArgs e)
        {
            //Çıkış butonu
            DialogResult cevap;
            cevap = MessageBox.Show("UYGULAMADAN ÇIKMAK İSTİYORMUSUNUZ ?", "ÇIKIŞ PENCERESİ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
                Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Textboxlara gridviewden veri doldurma
            kn = e.RowIndex;
            txt_id.Text = dataGridView1.Rows[kn].Cells[0].Value.ToString();
            txt_ad.Text = dataGridView1.Rows[kn].Cells[1].Value.ToString();
            txt_mrk.Text = dataGridView1.Rows[kn].Cells[2].Value.ToString();
            txt_tür.Text = dataGridView1.Rows[kn].Cells[3].Value.ToString();
            txt_fyt.Text = dataGridView1.Rows[kn].Cells[4].Value.ToString();
            txt_adet.Text = dataGridView1.Rows[kn].Cells[5].Value.ToString();
            txt_topfyt.Text = dataGridView1.Rows[kn].Cells[6].Value.ToString();
        }

        private void txt_fyt_TextChanged(object sender, EventArgs e)
        {
            //fiyat değişikliğine göre toplam fiyat hesaplama
            if ((txt_fyt.Text != "") && (txt_adet.Text != ""))

                txt_topfyt.Text = ((Convert.ToInt32(txt_fyt.Text) * Convert.ToInt32(txt_adet.Text))).ToString();
        }

        private void txt_adet_TextChanged(object sender, EventArgs e)
        {
            //adet değişikliğine göre toplam fiyat hesaplama
            if ((txt_fyt.Text != "") && (txt_adet.Text != ""))

                txt_topfyt.Text = ((Convert.ToInt32(txt_fyt.Text) * Convert.ToInt32(txt_adet.Text))).ToString();
        }

        private void txt_fyt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Toplam fiyata sayısal değer girilmediğine dair uyarı
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("LÜTFEN SAYISAL DEĞER GİRİNİZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                e.Handled = false;
            }
        }

        private void txt_adet_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Adet textboxuna sayısal değer girilmediğine dair uyarı
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("LÜTFEN SAYISAL DEĞER GİRİNİZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
