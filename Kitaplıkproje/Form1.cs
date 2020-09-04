using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplıkproje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\utkua\Desktop\c#kodlar\sharp\access\Kitaplıkproje\kitaplik.mdb");

        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select*from kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                listele();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string durum;
            if (radioButton1.Checked==true)
            {
                durum = "0";
            }
            else
            {
                durum = "1";
            }
            baglanti.Open();
            OleDbCommand kaydet = new OleDbCommand("insert into kitaplar (kitapid,kitapad,yazar,tur,sayfa,durum)values(@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            kaydet.Parameters.AddWithValue("@p1",textBox1.Text);
            kaydet.Parameters.AddWithValue("@p2",textBox2.Text);
            kaydet.Parameters.AddWithValue("@p3",textBox3.Text);
            kaydet.Parameters.AddWithValue("@p4",textBox4.Text);
            kaydet.Parameters.AddWithValue("@p5",comboBox1.Text);
            kaydet.Parameters.AddWithValue("@p6",durum);
            kaydet.ExecuteNonQuery();
            baglanti.Close();

            listele();
            MessageBox.Show("Bilgiler Kaydedilmiştir");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string durum,sabitid=textBox1.Text;
            if (radioButton1.Checked == true)
            {
                durum = "0";
            }
            else
            {
                durum = "1";
            }

            baglanti.Open();
            OleDbCommand guncelle = new OleDbCommand("update kitaplar set kitapid=@p1,kitapad=@p2,yazar=@p3,tur=@p4,sayfa=@p5,durum=@p6 where kitapid=@p7 ", baglanti);
            guncelle.Parameters.AddWithValue("@p1",textBox1.Text);
            guncelle.Parameters.AddWithValue("@p2",textBox2.Text);
            guncelle.Parameters.AddWithValue("@p3",textBox3.Text);
            guncelle.Parameters.AddWithValue("@p4",textBox4.Text);
            guncelle.Parameters.AddWithValue("@p5",comboBox1.Text);
            guncelle.Parameters.AddWithValue("@p6",durum);
            guncelle.Parameters.AddWithValue("@p7",sabitid);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            listele();
            MessageBox.Show("Bilgiler Güncellenmiştir");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand sil = new OleDbCommand("delete from kitaplar where kitapid=@p1",baglanti);
            sil.Parameters.AddWithValue("@p1",textBox1.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();
            listele();
            MessageBox.Show("Başarıyla Silinmiştir");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string durum;
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            durum = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            
            if (durum == "True")
            {
                radioButton1.Checked = true;
            }
            if (durum=="False")
            {
                radioButton2.Checked=true;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand bul = new OleDbCommand("select*from kitaplar where kitapad=@p1", baglanti);
            bul.Parameters.AddWithValue("@p1", textBox5.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(bul);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand bul = new OleDbCommand("select*from kitaplar where kitapad like'%"+textBox6.Text+"%'", baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(bul);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            baglanti.Open();
            OleDbCommand bul = new OleDbCommand("select*from kitaplar where kitapad like'%" + textBox6.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(bul);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
