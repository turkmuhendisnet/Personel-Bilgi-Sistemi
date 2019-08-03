using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelBilgiSistemiCalismasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbUnvan.Items.AddRange(Enum.GetNames(typeof(PersonelUnvalari)));
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog SecilenResim = new OpenFileDialog();
            SecilenResim.Title = "Resim Seç";
            SecilenResim.Filter= " (*.jpg)|*.jpg|(*.png)|*.png";

            if (SecilenResim.ShowDialog()==DialogResult.OK)
            {
                pcbPersonelResmi.Image = Image.FromFile(SecilenResim.FileName);
                pcbPersonelResmi.Tag = Path.GetExtension(SecilenResim.FileName);
                pcbPersonelResmi.Image = new Bitmap(SecilenResim.OpenFile());
            }
        }

        PersonelSinifi personel = new PersonelSinifi();

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            PersonelSinifi personel = new PersonelSinifi();
           
            if (cmbGirlmismi==true)
            {
                personel = PersonelOlusturucu(personel);
                ListViewItem listview = lisviewDoldur(personel);
                lstPersonelListesi.Items.Add(listview);
            }
            else
                MessageBox.Show("Unvan Seçmediniz");
                       
            KontrolTemizlemeSinifi.KontrolTemizle(this.Controls);

        }

        private ListViewItem lisviewDoldur(PersonelSinifi personel)
        {
            ListViewItem listview = new ListViewItem(personel.PersonelTcKimlikNo);

            listview.SubItems.Add(personel.PersonelAdi);
            listview.SubItems.Add(personel.PersonelSoyad);
            listview.SubItems.Add(personel.PersonelDogumTarihi.ToShortDateString());
            listview.SubItems.Add(personel.PersonelTelefon);
            listview.SubItems.Add(personel.PerosnelMail);
            listview.SubItems.Add(personel.PersonelAdres);
            listview.SubItems.Add(personel.PersoneliseGirisTarihi.ToShortDateString());
            listview.SubItems.Add(personel.PersonelUnvani.ToString());

            listview.Tag = personel;
            return listview;

        }

        private PersonelSinifi PersonelOlusturucu(PersonelSinifi personel)
        {
            personel.PersonelTcKimlikNo =      txbTcKimlikNo.Text;
            personel.PersonelAdi =             txbAd.Text;
            personel.PersonelSoyad =           txbSoyad.Text;
            personel.PersonelDogumTarihi =     dtpDogumTarihi.Value;
            personel.PersonelTelefon =         txbTelefon.Text;
            personel.PerosnelMail =            txbMail.Text;
            personel.PersonelAdres =           txbAdres.Text;
            personel.PersoneliseGirisTarihi =  dtpiseGirisTarihi.Value;
            personel.PersonelUnvani =          (PersonelUnvalari)cmbUnvan.SelectedIndex;
          //  personel.PersonelUnvani =        (PersonelUnvalari)Enum.Parse(typeof(PersonelUnvalari), cmbUnvan.Text);

            if (pcbPersonelResmi.Tag != null)
            {
                personel.PersonelResmi = Guid.NewGuid() + pcbPersonelResmi.Tag.ToString();
                pcbPersonelResmi.Image.Save(Application.StartupPath + "/Images/" + personel.PersonelResmi);
            }

            return personel;
        }


        PersonelSinifi GuncellenecekPersonel;
        int indexNo;
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            GuncellenecekPersonel = PersonelOlusturucu(GuncellenecekPersonel);
            lstPersonelListesi.Items.RemoveAt(indexNo);
            lstPersonelListesi.Items.Insert(indexNo, lisviewDoldur(GuncellenecekPersonel));
            KontrolTemizlemeSinifi.KontrolTemizle(this.Controls);
        }


        private void btnSil_Click(object sender, EventArgs e)
        {
 
            lstPersonelListesi.Items.RemoveAt(lstPersonelListesi.SelectedItems[0].Index);
        }

        private void lstPersonelListesi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            indexNo = lstPersonelListesi.SelectedItems[0].Index;
            GuncellenecekPersonel = (PersonelSinifi)lstPersonelListesi.SelectedItems[0].Tag;

            txbAd.Text =           GuncellenecekPersonel.PersonelAdi;
            txbSoyad.Text =        GuncellenecekPersonel.PersonelSoyad;
            txbTcKimlikNo.Text =   GuncellenecekPersonel.PersonelTcKimlikNo;
            txbMail.Text =         GuncellenecekPersonel.PerosnelMail;
            txbTelefon.Text =      GuncellenecekPersonel.PersonelTelefon;
            txbAdres.Text =        GuncellenecekPersonel.PersonelAdres;
            dtpDogumTarihi.Text =  GuncellenecekPersonel.PersonelDogumTarihi.ToShortDateString();
            dtpiseGirisTarihi.Text=GuncellenecekPersonel.PersoneliseGirisTarihi.ToShortDateString();
            cmbUnvan.Text =        GuncellenecekPersonel.PersonelUnvani.ToString();

            if (!string.IsNullOrWhiteSpace(GuncellenecekPersonel.PersonelResmi))
            {
                pcbPersonelResmi.Image=Image.FromFile("Images/" + GuncellenecekPersonel.PersonelResmi);
                pcbPersonelResmi.Tag = Path.GetExtension(GuncellenecekPersonel.PersonelResmi);
            }

        }

        bool cmbGirlmismi = false;
        private void cmbUnvan_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbGirlmismi = true;
        }

        private void txbTcKimlikNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txbAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void txbSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void txbTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
