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
    public partial class Form3 : Form
    {
        //Veritabanı bağlantılarını tanımlama kodu
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=" + Application.StartupPath + "/Market.accdb");
        OleDbCommand komut = new OleDbCommand();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Textboxlar boş işe uyarı kodu
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                label5.Text = "Giriş Bilgilerinizde Eksiklik Var";
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus();
            }
                //Şifre girişi için textbox 2-3 aynı değilse aynı olmasını bildiren kod
            else if (textBox2.Text != textBox3.Text)
            {
                label5.Text = "Girdiğiniz Şifreler Aynı Olmalı. Lütfen Uyumlu Hale Getirin";
                textBox2.Clear();
                textBox3.Clear();
                textBox2.Focus();
            }
                //Sorun olmadıgında girilen kullanıcı adı ve şifreyi users tablosuna ekleme bildirme ve girişe yöneltme kodu
            else
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                komut = new OleDbCommand("INSERT INTO USERS(user_name,user_pass) VALUES('" + textBox1.Text + "','" + textBox2.Text + "')", conn);
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("YENİ PERSONEL KAYDI GERÇEKLEŞTİ. PERSONEL GİRİŞ FORMUNA DÖNEREK OTURUM AÇABİLRSİNİZ..");

                this.Close();
                Form1 frm1 = new Form1();
                frm1.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Giriş ekranına dönüş kodu
            DialogResult cevap;
            cevap = MessageBox.Show("GİRİŞ EKRANINA GERİ DÖNÜLECEKTİR... EMİNMİSİNİZ ?", "PENCEREYİ KAPAT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                Form1 frm1 = new Form1();
                this.Close();
                frm1.Show();
            }
        }
    }
}
