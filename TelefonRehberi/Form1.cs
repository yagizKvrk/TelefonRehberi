using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelefonRehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(TelefonTipi)));

        }
        BindingList<Kisi> kisiListesi = new BindingList<Kisi>();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtAd.Text.Trim()) || string.IsNullOrEmpty(mtxtTelefon.Text.Trim()) || comboBox1.SelectedIndex == -1 || (rdbBayi.Checked == false && rdbCalisan.Checked == false && rdbMusteri.Checked == false))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
            else
            {
                if(duzenlenen == null)
                {
                    Kisi k = new Kisi();
                    k.Adi = txtAd.Text;
                    k.Rol = rdbBayi.Checked ? "Bayii" : rdbCalisan.Checked == true ? "Çalışan" : "Müşteri";
                    k.Telefon = mtxtTelefon.Text;
                    k.TelefonTipi = (TelefonTipi)Enum.Parse(typeof(TelefonTipi), comboBox1.SelectedItem.ToString());

                    try
                    {
                        kisiListesi.Add(k);
                    
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Hata Oluştu");
                    }
                }
                else
                {
                    duzenlenen.Adi = txtAd.Text;
                    duzenlenen.Telefon = mtxtTelefon.Text;
                    duzenlenen.Rol = rdbBayi.Checked ? "Bayii" : rdbCalisan.Checked == true ? "Çalışan" : "Müşteri";
                    duzenlenen.TelefonTipi = (TelefonTipi)Enum.Parse(typeof(TelefonTipi), comboBox1.SelectedItem.ToString());

                    kisiListesi.ResetBindings();

                    duzenlenen = null;
                }

                dataGridView1.DataSource = kisiListesi;

                txtAd.Text = mtxtTelefon.Text = "";
                comboBox1.SelectedIndex = -1;
                rdbBayi.Checked = rdbCalisan.Checked = rdbMusteri.Checked = false;


            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            if(txtAra.Text == "")
            {
                dataGridView1.DataSource = kisiListesi;
            }
            else
            {
                BindingList<Kisi> aramaSonucu = new BindingList<Kisi>();
                foreach (var item in kisiListesi)
                {
                    if (item.Adi.ToLower().Contains(txtAra.Text.Trim().ToLower()) || item.Rol.ToLower().Contains(txtAra.Text.Trim().ToLower()) || item.Telefon.ToLower().Contains(txtAra.Text.Trim().ToLower()))
                    {
                        aramaSonucu.Add(item);
                    }
                }
                dataGridView1.DataSource = aramaSonucu;
            }           
        }
        Kisi duzenlenen;
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            duzenlenen = (Kisi)dataGridView1.SelectedRows[0].DataBoundItem;
            txtAd.Text = duzenlenen.Adi;
            mtxtTelefon.Text = duzenlenen.Telefon;
            rdbBayi.Checked = duzenlenen.Rol == "Bayi" ? true : false;
            rdbCalisan.Checked = duzenlenen.Rol == "Çalışan" ? true : false;
            rdbMusteri.Checked = duzenlenen.Rol == "Müşteri" ? true : false;

            comboBox1.SelectedIndex = (int)duzenlenen.TelefonTipi;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            Kisi silinen = (Kisi)dataGridView1.SelectedRows[0].DataBoundItem;

            kisiListesi.Remove(silinen);

        }
    }
}
