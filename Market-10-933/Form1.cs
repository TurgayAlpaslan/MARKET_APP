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
    public partial class Form1 : Form
    {
        //Veritabanı bağlantılarını tanımlama kodu
        public static Boolean bln = false;
        OleDbConnection cnt = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" + Application.StartupPath + "/Market.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Form3e yöneltme ve form 3 boyutu ayarlama
            Form3 frm3 = new Form3();
            frm3.Width = 681;
            frm3.Height = 500;
            frm3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Kontrol komut satırı için tanım ve bağlantı açılımı
            bln = false;
            Boolean kont = false;
            if (cnt.State == ConnectionState.Closed)
                cnt.Open();
            komut = new OleDbCommand("SELECT user_name,user_pass FROM USERS", cnt);
            dr = komut.ExecuteReader();
            //Kullanıcı adı ve şifreyi tablo ile kıyaslama ve admin girişi yapıldı ise admin girişine özel bağlantı gösterimi ve form sayfasına geçiş
            while (dr.Read())
            {
                if ((txtuser_ad.Text == dr["user_name"].ToString()) && (txtuser_pass.Text == dr["user_pass"].ToString()))
                {

                    if (txtuser_ad.Text == "admin") bln = true;
                    kont = true;
                    Form2 frm2 = new Form2();
                    this.Hide();
                    frm2.Show();
                }
            }
            //Kontrol yapımında hatalı giriş olduysa bunu bildirme
            if (kont != true)
            {
                MessageBox.Show("HATALI GİRİŞ YAPTINIZ LÜTFEN BİLGİLERİNİZİ KONTROL EDİNİZ");
                txtuser_ad.Clear();
                txtuser_pass.Clear();
                txtuser_ad.Focus();
            }
            cnt.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Çıkış butonu seçenekleri
            DialogResult cevap;
            cevap = MessageBox.Show("ÇIKIŞ YAPMAK İSTİYORMUSUNUZ", "ÇIKIŞ PENCERESİ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
                Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
